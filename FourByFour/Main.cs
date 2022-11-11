using M;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourByFour
{
    public partial class Main : Form
    {
        MidiStream _play;
        public Main()
        {
            InitializeComponent();
            var devs = MidiDevice.Outputs;

            OutputComboBox.ComboBox.DisplayMember = "Name";
            foreach (var dev in devs)
                OutputComboBox.Items.Add(dev);
            OutputComboBox.SelectedIndex = 0;
            PatternComboBox.SelectedIndex = 0;

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //var beats = new BeatControl((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value);
            //BeatsPanel.Controls.Add(beats);
            //beats.Delete += Beats_Delete;

            BeatsPanel.AddTrack((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value, 36, 9);
        }

        //private void Beats_Delete(object sender, EventArgs e)
        //{
        //	BeatsPanel.Controls.Remove(sender as Control);
        //}

        private void PlayButton_Click(object sender, EventArgs e)
        {
            // we use 100 events, which should be safe and allow
            // for some measure of SYSEX messages in the stream
            // without bypassing the 64kb limit
            const int MAX_EVENT_COUNT = 100;
            const int RATE_TICKS = 500;
            if ("Stop" == PlayButton.Text)
            {
                if (null != _play) // sanity check
                    _play.Close();
                PlayButton.Text = "Play";
                PatternComboBox.Enabled = true;
                BarsUpDown.Enabled = true;
                OutputComboBox.Enabled = true;
                return;
            }
            PatternComboBox.Enabled = false;
            BarsUpDown.Enabled = false;
            OutputComboBox.Enabled = false;
            PlayButton.Text = "Stop";

            _play = (OutputComboBox.SelectedItem as MidiOutputDevice).Stream;
            var mf = _CreateMidiFile();
            var stm = _play;

            // our current cursor pos
            int pos = 0;
            // for tracking deltas
            var ofs = 0;
            // merge our file for playback
            var seq = MidiSequence.Merge(mf.Tracks);
            // the number of events in the seq
            int len = seq.Events.Count;
            // stores the next set of events
            var eventList = new List<MidiEvent>(MAX_EVENT_COUNT);

            // open the stream
            stm.Open();
            // start it
            stm.Start();

            // first set the timebase
            stm.TimeBase = mf.TimeBase;

            // set up our send complete handler
            stm.SendComplete += delegate (object s, EventArgs ea)
            {
                try
                {
                    BeginInvoke(new Action(delegate ()
                    {
                        // clear the list	
                        eventList.Clear();
                        mf = _CreateMidiFile();
                        seq = MidiSequence.Merge(mf.Tracks);
                        ofs = 0;
                        len = seq.Events.Count;
                        // iterate through the next events
                        var next = pos + MAX_EVENT_COUNT;
                        for (; pos < next && ofs <= RATE_TICKS; ++pos)

                        {
                            // if it's past the end, loop it
                            if (len <= pos)
                            {
                                pos = 0;
                                break;
                            }
                            var ev = seq.Events[pos];
                            ofs += ev.Position;
                            if (ev.Position < RATE_TICKS && RATE_TICKS < ofs)
                                break;
                            // otherwise add the next event
                            eventList.Add(ev);
                        }
                        // send the list of events
                        if (MidiStreamState.Closed != stm.State)
                            stm.SendDirect(eventList);
                    }));
                }
                catch
                {

                }

            };
            // add the first events
            for (pos = 0; pos < MAX_EVENT_COUNT && ofs <= RATE_TICKS; ++pos)
            {
                // if it's past the end, loop it
                if (len <= pos)
                {
                    pos = 0;
                    break;
                }
                var ev = seq.Events[pos];
                ofs += ev.Position;
                if (ev.Position < RATE_TICKS && RATE_TICKS < ofs)
                    break;
                // otherwise add the next event
                eventList.Add(ev);
            }
            // send the list of events
            stm.SendDirect(eventList);

        }

        MidiFile _CreateMidiFile()
        {
            var file = new MidiFile();
            // we'll need a track 0 for our tempo map
            var track0 = new MidiSequence();
            // set the tempo at the first position
            track0.Events.Add(new MidiEvent(0, new MidiMessageMetaTempo((double)TempoUpDown.Value)));
            // compute the length of our loop
            var len = ((int)BarsUpDown.Value) * ((int)StepsUpDown.Value) * file.TimeBase / 4; //steps are in 16ths
                                                                                              // add an end of track marker just so all
                                                                                              // of our tracks will be the loop length
            track0.Events.Add(new MidiEvent(len, new MidiMessageMetaEndOfTrack()));

            // here we need a track end with an 
            // absolute position for the MIDI end
            // of track meta message. We'll use this
            // later to set the length of the track
            var trackEnd = new MidiSequence();
            trackEnd.Events.Add(new MidiEvent(len, new MidiMessageMetaEndOfTrack()));

            // add track 0 (our tempo map)
            file.Tracks.Add(track0);

            // create track 1 (our drum track)
            var track1 = new MidiSequence();

            // we're going to create a new sequence for
            // each one of the drum sequencer tracks in
            // the UI
            var trks = new List<MidiSequence>(BeatsPanel.Controls.Count);
            foreach (var beat in BeatsPanel.Beats)
            {
                // get the note for the drum
                var note = beat.NoteId;
                // it's easier to use a note map
                // to build the drum sequence
                var noteMap = new List<MidiNote>();
                for (int ic = beat.Steps.Count, i = 0; i < ic; ++i)
                {
                    // if the step is pressed create 
                    // a note for it
                    if (beat.Steps[i])
                        noteMap.Add(new MidiNote(i * (file.TimeBase / 4), beat.Channel, note, 127, file.TimeBase / 4 - 1)); ;
                }
                // convert the note map to a sequence
                // and add it to our working tracks
                trks.Add(MidiSequence.FromNoteMap(noteMap));
            }
            // now we merge the sequences into one
            var t = MidiSequence.Merge(trks);
            // we merge everything down to track 1
            track1 = MidiSequence.Merge(track1, t, trackEnd);
            // .. and add it to the file
            file.Tracks.Add(track1);
            return file;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (null != _play && MidiStreamState.Closed != _play.State)
            {
                _play.Close();
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == SaveMidiFile.ShowDialog())
            {
                var file = _CreateMidiFile();
                file.WriteTo(SaveMidiFile.FileName);
            }
        }

        private void StepsUpDown_ValueChanged(object sender, EventArgs e)
        {
            foreach (var beat in BeatsPanel.Beats)
            {
                var steps = new List<bool>(beat.Steps);
                beat.StepCount = (int)StepsUpDown.Value;
                for (int ic = steps.Count, i = 0; i < ic; ++i)
                {
                    if (i >= beat.Steps.Count)
                        break;
                    beat.Steps[i] = steps[i];
                }
            }
        }

        private void BarsUpDown_ValueChanged(object sender, EventArgs e)
        {
            foreach (var beat in BeatsPanel.Beats)
            {
                var steps = new List<bool>(beat.Steps);
                beat.Bars = (int)BarsUpDown.Value;
                for (int ic = steps.Count, i = 0; i < ic; ++i)
                {
                    if (i >= beat.Steps.Count)
                        break;
                    beat.Steps[i] = steps[i];
                }
            }
        }

        //TODO - Elektron Octatrack basic mode??

        private void PatternComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BeatsPanel.Controls.Clear();
            switch (PatternComboBox.SelectedIndex)
            {
                case 0: // none
                    break;
                case 1: // Basic Empty
                    MakeBasicKit(new byte[] { 36, 40, 42, 46 }, new byte[] { 9 }, null);
                    break;

                case 2: //Volca Sample1. TODO - maybe diff notes just for visuals?
                        //for (byte i=0; i < 10; i++)
                        //               {
                        //	var beat = new BeatControl((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value) { Channel = i};
                        //	beat.NoteId = 36; // volca - we don't care
                        //	//beat.Bars = (int)BarsUpDown.Value;
                        //	beat.Delete += Beats_Delete;
                        //	BeatsPanel.Controls.Add(beat);
                        //}
                    MakeBasicKit(new byte[] { 36 }, new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, _volcaSampleCCs);
                    break;
                case 3: //Roland TR-06
                    MakeBasicKit(new byte[] { 35, 38, 45, 48, 49, 46, 42 }, new byte[] { 9 }, _rd06CCs);
                    break;
            }
        }

        void MakeBasicKit(byte[] notes, byte[] channels, List<KeyValuePair<string, byte>> ccs)
        {
            BeatsPanel.Setup((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value, notes, channels, ccs);
        }

        static readonly List<KeyValuePair<string, byte>> _volcaSampleCCs = new List<KeyValuePair<string, byte>>()
        {
            new KeyValuePair<string, byte>("LEVEL"                , 7 ),
            new KeyValuePair<string, byte>("PAN"                  , 10),
            new KeyValuePair<string, byte>("SAMPLE START POINT"   , 40),
            new KeyValuePair<string, byte>("SAMPLE LENGTH"        , 41),
            new KeyValuePair<string, byte>("HI CUT"               , 42),
            new KeyValuePair<string, byte>("SPEED"                , 43),
            new KeyValuePair<string, byte>("PITCH EG INT"         , 44),
            new KeyValuePair<string, byte>("PITCH EG ATTACK"      , 45),
            new KeyValuePair<string, byte>("PITCH EG DECAY"       , 46),
            new KeyValuePair<string, byte>("AMP EG ATTACK"        , 47),
            new KeyValuePair<string, byte>("AMP EG DECAY"         , 48)
        };
        
        static readonly List<KeyValuePair<string, byte>> _rd06CCs = new List<KeyValuePair<string, byte>>()
        {
            new KeyValuePair<string, byte>("Velocity" ,0   ), //pseudo-cc
            new KeyValuePair<string, byte>("SHUFFLE" ,9   ),
            new KeyValuePair<string, byte>("MIX IN LEVEL"       ,12  ),
            new KeyValuePair<string, byte>("OD DRIVE"           ,17  ),
            new KeyValuePair<string, byte>("DELAY TIME"         ,18  ),
            new KeyValuePair<string, byte>("DELAY DEPTH"        ,19  ),
            new KeyValuePair<string, byte>("BD TUNE"            ,20  ),
            new KeyValuePair<string, byte>("BD ATTACK"          ,21  ),
            new KeyValuePair<string, byte>("BD COMP"            ,22  ),
            new KeyValuePair<string, byte>("BD DECAY"           ,23  ),
            new KeyValuePair<string, byte>("BD LEVEL"           ,24  ),
            new KeyValuePair<string, byte>("SD TUNE"            ,25  ),
            new KeyValuePair<string, byte>("SD SNAPPY"          ,26  ),
            new KeyValuePair<string, byte>("SD COMP"            ,27  ),
            new KeyValuePair<string, byte>("SD DECAY"           ,28  ),
            new KeyValuePair<string, byte>("SD LEVEL"           ,29  ),
            new KeyValuePair<string, byte>("LT TUNE"            ,49  ),
            new KeyValuePair<string, byte>("LT DECAY"           ,50  ),
            new KeyValuePair<string, byte>("LT LEVEL"           ,51  ),
            new KeyValuePair<string, byte>("HT TUNE"            ,52  ),
            new KeyValuePair<string, byte>("HT DECAY"           ,53  ),
            new KeyValuePair<string, byte>("HT LEVEL"           ,54  ),
            new KeyValuePair<string, byte>("CH TUNE"            ,61  ),
            new KeyValuePair<string, byte>("CH DECAY"           ,62  ),
            new KeyValuePair<string, byte>("CH LEVEL"           ,63  ),
            new KeyValuePair<string, byte>("ACC LEVEL"          ,71  ),
            new KeyValuePair<string, byte>("OH TUNE"            ,80  ),
            new KeyValuePair<string, byte>("OH DECAY"           ,81  ),
            new KeyValuePair<string, byte>("OH LEVEL"           ,82  ),
            new KeyValuePair<string, byte>("CY TUNE"            ,83  ),
            new KeyValuePair<string, byte>("CY DECAY"           ,84  ),
            new KeyValuePair<string, byte>("CY LEVEL"           ,85  ),
            new KeyValuePair<string, byte>("LT COLOR"           ,92  ),
            new KeyValuePair<string, byte>("HT COLOR"           ,94  ),
            new KeyValuePair<string, byte>("BD DELAY"           ,96  ),
            new KeyValuePair<string, byte>("SD DELAY"           ,97  ),
            new KeyValuePair<string, byte>("LT DELAY"           ,103 ),
            new KeyValuePair<string, byte>("HT DELAY"           ,104 ),
            new KeyValuePair<string, byte>("CH DELAY"           ,107 ),
            new KeyValuePair<string, byte>("OH DELAY"           ,108 ),
            new KeyValuePair<string, byte>("CY DELAY"           ,109 ),
            new KeyValuePair<string, byte>("MIX IN DELAY"       ,111 ),
            new KeyValuePair<string, byte>("MASTER PROB."       ,113 )
        };
    }
}

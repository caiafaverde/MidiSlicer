﻿using M;
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
			var devs= MidiDevice.Outputs;
			
			OutputComboBox.ComboBox.DisplayMember = "Name";
			foreach (var dev in devs)
				OutputComboBox.Items.Add(dev);
			OutputComboBox.SelectedIndex = 0;
			PatternComboBox.SelectedIndex = 0;
			
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var beats = new BeatControl((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value);
			BeatsPanel.Controls.Add(beats);
			beats.Delete += Beats_Delete;
		}

		private void Beats_Delete(object sender, EventArgs e)
		{
			BeatsPanel.Controls.Remove(sender as Control);
		}

		private void PlayButton_Click(object sender, EventArgs e)
		{
			// we use 100 events, which should be safe and allow
			// for some measure of SYSEX messages in the stream
			// without bypassing the 64kb limit
			const int MAX_EVENT_COUNT = 100;
			const int RATE_TICKS = 500;
			if ("Stop"==PlayButton.Text)
			{
				if(null!=_play) // sanity check
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
						if(MidiStreamState.Closed!=stm.State)
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
			foreach (var ctl in BeatsPanel.Controls)
			{
				var beat = ctl as BeatControl;
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
			if(null!=_play && MidiStreamState.Closed!=_play.State)
			{
				_play.Close();
			}
		}

		private void SaveAsButton_Click(object sender, EventArgs e)
		{
			if(DialogResult.OK==SaveMidiFile.ShowDialog())
			{
				var file = _CreateMidiFile();
				file.WriteTo(SaveMidiFile.FileName);
			}
		}

		private void StepsUpDown_ValueChanged(object sender, EventArgs e)
		{
			foreach (var ctl in BeatsPanel.Controls)
			{
				var beat = ctl as BeatControl;
				if (null != beat) // sanity
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
		}

		private void BarsUpDown_ValueChanged(object sender, EventArgs e)
		{
			foreach(var ctl in BeatsPanel.Controls)
			{
				var beat = ctl as BeatControl;
				if (null != beat) // sanity
				{
					var steps = new List<bool>(beat.Steps);
					beat.Bars = (int)BarsUpDown.Value;
					for(int ic=steps.Count,i=0;i<ic;++i)
					{
						if (i >= beat.Steps.Count)
							break;
						beat.Steps[i] = steps[i];
					}
				}
			}
		}

		private void PatternComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			BeatsPanel.Controls.Clear();
			switch(PatternComboBox.SelectedIndex)
			{
				case 0: // none
					break;
				case 1: // Basic Empty
					_MakeBasicKit(new byte[] { 36,40,42,46});
					break;
				
				case 2:
					for (byte i=0; i < 10; i++)
                    {
						var beat = new BeatControl((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value) { Channel = i};
						beat.NoteId = 36; // volca - we don't care
						//beat.Bars = (int)BarsUpDown.Value;
						beat.Delete += Beats_Delete;
						BeatsPanel.Controls.Add(beat);
					}
					break;
				case 3:
					_MakeBasicKit(new byte[] { 35, 38, 45, 48, 49, 46, 42});
					break;
			}
		}

		//TODO - make note assignments for other devices as well
		//EG Roland TR-06
		//Inst.Tx Note Number Rx Note Number
		//BD     36             35, 36
		//SD     38             38, 40
		//LT     47             45, 47
		//HT     50             48, 50
		//CY     49             49
		//OH     46             46
		//CH     42             42, 44
		// Note that TR-06 is velocity-dependent (Volca Sample sends CC for every shannel to set velocity)

		void _MakeBasicKit(byte[] notes)
		{
			foreach (var noteId in notes)
			{
				var beat = new BeatControl((int)this.BarsUpDown.Value, (int)this.StepsUpDown.Value);
				beat.NoteId = noteId;
				beat.Delete += Beats_Delete;
				BeatsPanel.Controls.Add(beat);
			}
		}
    }
}

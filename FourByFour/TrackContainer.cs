using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourByFour
{
    public class TrackContainer : FlowLayoutPanel
    {
        class Endless<T> : IEnumerable<T>
        {
            readonly IEnumerable<T> _src;
            public Endless(IEnumerable<T> src)
            {
                _src = src;
            }

            public IEnumerator<T> GetEnumerator()
            {
                T last = default(T);
                foreach (var t in _src)
                {
                    last = t;
                    yield return last;
                }
                yield return last;
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator();
            }
        }
        
        public void Setup(int bars, int steps, byte[] notes, byte[] channels, List<KeyValuePair<string, byte>> ccs)
        {
            this.Controls.Clear();
            
            var cnt = Math.Max(notes.Length, channels.Length);
            using (var noteEnum = new Endless<byte>(notes).GetEnumerator())
            using (var channelsEnum = new Endless<byte>(channels).GetEnumerator())
                for (var i = 0; i < cnt; i++)
                {
                    noteEnum.MoveNext();
                    channelsEnum.MoveNext();

                    AddTrack(bars, steps, note:noteEnum.Current, channel:channelsEnum.Current);
                }
            if (ccs != null)
                this.Parameters.SetupCc(ccs);
        }

        public void AddTrack(int bars, int steps, byte note, byte channel)
        {
            if (Controls.Count == 0)
                this.Controls.Add(new ParameterControl(bars, steps));
            var beat = new BeatControl(bars, steps) { Channel = channel, NoteId = note };
            beat.Delete += Beats_Delete;
            this.Controls.Add(beat);
            this.Controls.SetChildIndex(beat, Controls.Count - 2);
            this.Parameters.AddChannel(channel);
        }

        private void Beats_Delete(object sender, EventArgs e)
        {
            var beat = sender as BeatControl;
            var channel = beat.Channel;
            this.Controls.Remove(sender as Control);
            if (!this.Beats.Any(b=>b.Channel == channel))
            {

            }
        }

        //Can contain multiple items of BeatControl
        //and a single instance of ParameterControl

        public ParameterControl Parameters => this.Controls[Controls.Count - 1] as ParameterControl;
        //{
        //    get
        //    {
        //        foreach (var c in this.Controls)
        //        {
        //            if (c is ParameterControl pc)
        //                return pc;
        //        }
        //        return null;
        //    }
        //}

        public IEnumerable<BeatControl> Beats
        {
            get
            {
                foreach (var ctl in this.Controls)
                {
                    if (ctl is BeatControl control)
                        yield return control;
                }
            }
        }
    }
}

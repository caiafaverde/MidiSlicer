using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourByFour
{
    public partial class ParameterControl : UserControl
    {
        int _bars;
        int _stepCount;
        public ParameterControl()
        {
            _bars = 1;
            InitializeComponent();
        }

        public ParameterControl(int bars, int stepCount)
        {
            InitializeComponent();
            _bars = bars;
            _stepCount = stepCount;
            StepControl.BuildSteps(bars, stepCount);
        }

        public void SetupCc(List<KeyValuePair<string, byte>> ccs)
        {
            foreach (var cc in ccs)
                this.Parameters.Items.Add(cc);
            this.Parameters.SelectedIndex = 0;
        }

        public void AddChannel(byte channel)
        {
            foreach (var ch in this.Tracks.Items)
            {
                if (ch is KeyValuePair<string, byte> kvp)
                {
                    if (kvp.Value == channel)
                        return;
                }
            }
            this.Tracks.Items.Add(new KeyValuePair<string, byte>($"Channel {channel + 1}", channel));
            this.Tracks.SelectedIndex = 0;
        }

        public void RemoveChannel(byte channel)
        {
            //KeyValuePair<string, byte> kvp;
            //foreach (var ch in this.Tracks.Items)
            //{
            //    kvp = (KeyValuePair<string, byte>)ch;
            //    if (channel == kvp.Value)
            //        break;
            //    kvp = null;
            //}
        }
    }
}

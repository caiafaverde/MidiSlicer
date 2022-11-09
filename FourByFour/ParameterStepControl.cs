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
    public partial class ParameterStepControl : UserControl
    {
        public ParameterStepControl()
        {
            InitializeComponent();
        }
		int _stepCount;
		int _bars;
		public void BuildSteps(int bars, int stepCount)
		{
			_stepCount = stepCount;
			_bars = bars;
			Controls.Clear();
			StepSlider ch;
			var left = 0;
			for (var k = 0; k < bars; ++k)
			{
				for (var i = 0; i < stepCount; ++i)
				{
					//for (var j = 0; j < 4; ++j)
					{
						ch = new StepSlider();
						Controls.Add(ch);
						//ch.Appearance = Appearance.Button;
						//ch.Size = new Size(16, Height);
						ch.Orientation = Orientation.Vertical;
						ch.SliderStyle = SliderStyle.Value;
						ch.Minimum = 0;
						ch.Maximum = 127;
						ch.LabelsCount = 0;
						ch.ActiveLedColor = Color.Yellow;
						ch.InactiveLedColor = Color.Gray;
						ch.Bevel.InnerBorder = true;
						ch.Bevel.OutterBorder = true;
						ch.Bevel.Style = BevelStyle.FrameRaised;
						
						ch.TicksPosition = SliderElementsPosition.None;
						ch.ActiveBarColor = Color.Yellow;

						ch.Location = new Point(left, 0);
						ch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
						ch.Width = 32;
						left += ch.Size.Width;
					}
					if ((i + k + 1) % 4 == 0)
						left += 4;
				}
			}
			//_steps = new _StepList(Controls);
			Size = new Size(left, 100);
			MinimumSize = new Size(left, 100);
		}
	}
}

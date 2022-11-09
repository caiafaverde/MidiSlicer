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
            //Channel = 9;
            StepControl.BuildSteps(bars, stepCount);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace FourByFour
{
    public class NumericUpDownToolStripItem : ToolStripControlHost
    {
        public NumericUpDown NumericUpDown => (NumericUpDown)base.Control;
        public NumericUpDownToolStripItem() :base(new NumericUpDown())
        {
            
        }

        public decimal Value { get => NumericUpDown.Value; set { NumericUpDown.Value = value; } }

        public event EventHandler ValueChanged
        {
            add
            {
                this.NumericUpDown.ValueChanged += value;
            }
            remove
            {
                this.NumericUpDown.ValueChanged -= value;
            }
        }
    }
}

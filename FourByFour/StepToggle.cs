using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FourByFour
{
    public class StepToggle : ControlWithBevel
    {
        private Color _activeLedColor = Color.LightGreen;
        private Color _inactiveLedColor = Color.Green;

        private float _prob = 0f;
        public float Probability 
        {
            get => _prob; 
            set 
            {
                _prob = value;
                if (_prob < 0f)
                    _prob = 0f;
                if (_prob > 1f)
                    _prob = 1f;
                this.Checked = _prob != 0f;
            } 
        }

        public StepToggle()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.StandardClick, true);
        }

        protected override void Draw(Graphics g, Rectangle r)
        {
            base.Draw(g, r);
            DrawToggle(g);
        }

        int Clip(int a, int b)
        {
            if (a + b < 0)
                return 0;
            else if (a + b > 256) return 255;
            else return a + b;
        }

        Color Add(Color thisColor, int value)
        {
            return Color.FromArgb(thisColor.A, Clip(thisColor.R, value), Clip(thisColor.G, value), Clip(thisColor.B, value));
        }

        private void DrawToggle(Graphics g)
        {
            Brush brush;
            Rectangle workRectangle = this.WorkRectangle;

            if (this.Pressed)
            {
                if (this.Probability != 1f)
                    brush = new SolidBrush(Add(this._activeLedColor, -20));
                else
                    brush = new SolidBrush(this._activeLedColor);
            }
            else
            {
                brush = new SolidBrush(this._inactiveLedColor);
            }

            g.FillRectangle(brush, workRectangle);

            if (this.Probability != 1f && this.Probability != 0f)
            {
                using (Brush brush3 = new SolidBrush(Color.Black))
                {
                    var font = new Font(SystemFonts.SmallCaptionFont, FontStyle.Regular);
                    g.DrawString($"{this.Probability:0.0}", this.Font, brush3, workRectangle.Top + 2, workRectangle.Left+ 2);
                }
            }
        }

        public Color ActiveLedColor
        {
            get
            {
                return this._activeLedColor;
            }
            set
            {
                if (this._activeLedColor != value)
                {
                    this._activeLedColor = value;
                    base.Invalidate();
                }
            }
        }

        public Color InactiveLedColor
        {
            get
            {
                return this._inactiveLedColor;
            }
            set
            {
                if (this._inactiveLedColor != value)
                {
                    this._inactiveLedColor = value;
                    base.Invalidate();
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(0x20, 0x30);
            }
        }

        private bool _checked;
        private bool _pressed;
        private bool _highlighted;

        public event EventHandler CheckedChanged;

        protected override bool IsInputKey(Keys keyData)
        {
            return (base.Enabled && (keyData == Keys.Space));
        }

        protected virtual void OnCheckedChanged(EventArgs e)
        {
            if (this.CheckedChanged != null)
            {
                this.CheckedChanged(this, e);
            }
        }

        //protected override void OnClick(EventArgs e)
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (base.Enabled)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.Checked = !this.Checked;
                    this._prob = (this.Checked) ? 1f : 0f;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    this.Checked = true;
                    if (this._prob == 1f)
                        this._prob = 0f;
                    this._prob += 0.1f;
                    this.Invalidate();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.Checked = false;
                    this._prob = 0f;
                }

                try
                {
                    base.OnMouseClick(e);
                }
                catch (Exception exception)
                {
                    this.Pressed = false;
                    throw exception;
                }
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (base.Enabled && (e.KeyData == Keys.Space))
            {
                this.Pressed = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (base.Enabled && (e.KeyData == Keys.Space))
            {
                this.Pressed = false;
                this.OnClick(new EventArgs());
            }
            base.OnKeyDown(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (base.TabStop && base.Enabled)
            {
                base.Focus();
            }
            if (mevent.Button == MouseButtons.Left)
            {
                this.Pressed = true;
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.Highlighted = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Highlighted = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                this.Pressed = false;
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.Invalidate();
            base.OnTextChanged(e);
        }

        public void PerformClick()
        {
            this.OnClick(new EventArgs());
        }

        public bool Checked
        {
            get
            {
                return this._checked;
            }
            set
            {
                if (this._checked != value)
                {
                    this._checked = value;
                    base.Invalidate();
                    this.OnCheckedChanged(new EventArgs());
                }
            }
        }


        protected bool Pressed
        {
            get
            {
                if (!this._pressed)
                {
                    return this._checked;
                }
                return true;
            }
            set
            {
                if (this._pressed != value)
                {
                    this._pressed = value;
                    base.Invalidate();
                }
            }
        }

        protected bool Highlighted
        {
            get
            {
                return (base.Enabled && this._highlighted);
            }
            set
            {
                if (this._highlighted != value)
                {
                    this._highlighted = value;
                    base.Invalidate();
                }
            }
        }

    }

}

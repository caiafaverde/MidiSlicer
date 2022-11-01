using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FourByFour
{
    public class StepToggle : Control
    {
        private Color _activeLedColor = Color.LightGreen;
        private Color _inactiveLedColor = Color.Green;

        public StepToggle()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected void Draw(Graphics g, Rectangle r)
        {
            DrawBevel(g, r);
            DrawToggle(g);
        }

        private void DrawToggle(Graphics g)
        {
            Brush brush;
            Rectangle workRectangle = this.WorkRectangle;

            if (this.Pressed)
            {
                brush = new SolidBrush(this._activeLedColor);
            }
            else
            {
                brush = new SolidBrush(this._inactiveLedColor);
            }

            g.FillRectangle(brush, workRectangle);
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

        protected override void OnClick(EventArgs e)
        {
            if (base.Enabled)
            {
                this.Checked = !this.Checked;
                
                try
                {
                    base.OnClick(e);
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
    
        private Bevel _bevel = new Bevel(BevelStyle.None, false, false);

        protected virtual void DrawBevel(Graphics g, Rectangle r)
        {
            Rectangle displayRectangle = this.DisplayRectangle;
            displayRectangle.Size -= new Size(1, 1);
            this._bevel.Draw(g, displayRectangle);
        }

        protected virtual void DrawFocus(Graphics g, Rectangle r)
        {
            int margin = this._bevel.GetMargin();
            Rectangle workRectangle = this.WorkRectangle;
            if (margin == 0)
            {
                workRectangle.Width--;
                workRectangle.Height--;
                Pen pen = new Pen(Color.Black)
                {
                    DashStyle = DashStyle.Dot
                };
                g.DrawRectangle(pen, workRectangle);
            }
            else
            {
                workRectangle.X--;
                workRectangle.Y--;
                workRectangle.Width++;
                workRectangle.Height++;
                Pen pen = new Pen(Color.LightGreen);
                g.DrawRectangle(pen, workRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            this.Draw(pe.Graphics, pe.ClipRectangle);
            if (this.Focused)
            {
                this.DrawFocus(pe.Graphics, pe.ClipRectangle);
            }
        }

        protected virtual Rectangle WorkRectangle
        {
            get
            {
                Rectangle displayRectangle = this.DisplayRectangle;
                int margin = this._bevel.GetMargin();
                displayRectangle.Inflate(-margin, -margin);
                return displayRectangle;
            }
        }

        public Bevel Bevel
        {
            get
            {
                return this._bevel;
            }
            set
            {
                this._bevel = value;
                base.Invalidate();
            }
        }
    }

    public class Bevel
    {
        private BevelStyle style;
        private bool outterBorder;
        private bool innerBorder;

        public Bevel(BevelStyle style, bool outterBorder, bool innerBorder)
        {
            this.style = style;
            this.innerBorder = innerBorder;
            this.outterBorder = outterBorder;
        }

        public void Draw(Graphics g, Rectangle r)
        {
            if (this.style != BevelStyle.None)
            {
                Point location = r.Location;
                Point point2 = r.Location + r.Size;
                if (this.outterBorder)
                {
                    g.DrawRectangle(Pens.Black, r);
                    location.X++;
                    location.Y++;
                    point2.X--;
                    point2.Y--;
                }
                Pen controlLightLight = SystemPens.ControlLightLight;
                Pen controlDark = SystemPens.ControlDark;
                Pen black = Pens.Black;
                Pen pen = Pens.Black;
                switch (this.style)
                {
                    case BevelStyle.Flat:
                        black = controlDark;
                        pen = controlDark;
                        break;

                    case BevelStyle.Single:
                        black = Pens.Black;
                        pen = Pens.Black;
                        break;

                    case BevelStyle.Double:
                        black = Pens.Black;
                        pen = Pens.Black;
                        break;

                    case BevelStyle.Raised:
                        black = controlLightLight;
                        pen = controlDark;
                        break;

                    case BevelStyle.Lowered:
                        black = controlDark;
                        pen = controlLightLight;
                        break;

                    case BevelStyle.DoubleRaised:
                        black = controlLightLight;
                        pen = controlDark;
                        break;

                    case BevelStyle.DoubleLowered:
                        black = controlDark;
                        pen = controlLightLight;
                        break;

                    case BevelStyle.FrameRaised:
                        black = controlLightLight;
                        pen = controlDark;
                        break;

                    case BevelStyle.FrameLowered:
                        black = controlDark;
                        pen = controlLightLight;
                        break;
                }
                g.DrawLine(black, location.X, location.Y, location.X, point2.Y);
                g.DrawLine(black, location.X, location.Y, point2.X, location.Y);
                g.DrawLine(pen, location.X, point2.Y, point2.X, point2.Y);
                g.DrawLine(pen, point2.X, location.Y, point2.X, point2.Y);
                if (((this.style == BevelStyle.Double) || (this.style == BevelStyle.DoubleRaised)) || (((this.style == BevelStyle.DoubleLowered) || (this.style == BevelStyle.FrameLowered)) || (this.style == BevelStyle.FrameRaised)))
                {
                    if ((this.style == BevelStyle.FrameLowered) || (this.style == BevelStyle.FrameRaised))
                    {
                        Pen pen5 = black;
                        black = pen;
                        pen = pen5;
                    }
                    location.X++;
                    location.Y++;
                    point2.X--;
                    point2.Y--;
                    g.DrawLine(black, location.X, location.Y, location.X, point2.Y);
                    g.DrawLine(black, location.X, location.Y, point2.X, location.Y);
                    g.DrawLine(pen, location.X, point2.Y, point2.X, point2.Y);
                    g.DrawLine(pen, point2.X, location.Y, point2.X, point2.Y);
                }
                if (this.innerBorder)
                {
                    Rectangle rect = new Rectangle(location.X + 1, location.Y + 1, (point2.X - location.X) - 2, (point2.Y - location.Y) - 2);
                    g.DrawRectangle(Pens.Black, rect);
                }
            }
        }

        public int GetMargin()
        {
            int num = 0;
            if (this.style == BevelStyle.None)
            {
                num = 0;
            }
            else if (((this.style == BevelStyle.Flat) || (this.style == BevelStyle.Single)) || ((this.style == BevelStyle.Raised) || (this.style == BevelStyle.Lowered)))
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
            if (this.outterBorder)
            {
                num++;
            }
            if (this.innerBorder)
            {
                num++;
            }
            return num;
        }

        public BevelStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        public bool OutterBorder
        {
            get
            {
                return this.outterBorder;
            }
            set
            {
                this.outterBorder = value;
            }
        }

        public bool InnerBorder
        {
            get
            {
                return this.innerBorder;
            }
            set
            {
                this.innerBorder = value;
            }
        }
    }

    public enum BevelStyle
    {
        None,
        Flat,
        Single,
        Double,
        Raised,
        Lowered,
        DoubleRaised,
        DoubleLowered,
        FrameRaised,
        FrameLowered
    }

    public enum ButtonBehaviour
    {
        Normal,
        CheckBox,
        RadioButton
    }

    public enum FaceStyle
    {
        Smooth,
        RaisedNotch,
        LoweredNotch
    }
}

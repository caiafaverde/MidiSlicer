using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourByFour
{
    public class ControlWithBevel : Control
    {
        private Bevel _bevel = new Bevel(BevelStyle.None, false, false);

        public ControlWithBevel()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.Selectable, false);
        }

        protected virtual void Draw(Graphics g, Rectangle r)
        {
            this.DrawBevel(g, r);
        }

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
                GraphicsUtils.DrawBevel(g, r, style);
                //Pen controlLightLight = SystemPens.ControlLightLight;
                //Pen controlDark = SystemPens.ControlDark;
                //Pen black = Pens.Black;
                //Pen pen = Pens.Black;
                //switch (this.style)
                //{
                //    case BevelStyle.Flat:
                //        black = controlDark;
                //        pen = controlDark;
                //        break;

                //    case BevelStyle.Single:
                //        black = Pens.Black;
                //        pen = Pens.Black;
                //        break;

                //    case BevelStyle.Double:
                //        black = Pens.Black;
                //        pen = Pens.Black;
                //        break;

                //    case BevelStyle.Raised:
                //        black = controlLightLight;
                //        pen = controlDark;
                //        break;

                //    case BevelStyle.Lowered:
                //        black = controlDark;
                //        pen = controlLightLight;
                //        break;

                //    case BevelStyle.DoubleRaised:
                //        black = controlLightLight;
                //        pen = controlDark;
                //        break;

                //    case BevelStyle.DoubleLowered:
                //        black = controlDark;
                //        pen = controlLightLight;
                //        break;

                //    case BevelStyle.FrameRaised:
                //        black = controlLightLight;
                //        pen = controlDark;
                //        break;

                //    case BevelStyle.FrameLowered:
                //        black = controlDark;
                //        pen = controlLightLight;
                //        break;
                //}
                //g.DrawLine(black, location.X, location.Y, location.X, point2.Y);
                //g.DrawLine(black, location.X, location.Y, point2.X, location.Y);
                //g.DrawLine(pen, location.X, point2.Y, point2.X, point2.Y);
                //g.DrawLine(pen, point2.X, location.Y, point2.X, point2.Y);
                //if (((this.style == BevelStyle.Double) || (this.style == BevelStyle.DoubleRaised)) || (((this.style == BevelStyle.DoubleLowered) || (this.style == BevelStyle.FrameLowered)) || (this.style == BevelStyle.FrameRaised)))
                //{
                //    if ((this.style == BevelStyle.FrameLowered) || (this.style == BevelStyle.FrameRaised))
                //    {
                //        Pen pen5 = black;
                //        black = pen;
                //        pen = pen5;
                //    }
                //    location.X++;
                //    location.Y++;
                //    point2.X--;
                //    point2.Y--;
                //    g.DrawLine(black, location.X, location.Y, location.X, point2.Y);
                //    g.DrawLine(black, location.X, location.Y, point2.X, location.Y);
                //    g.DrawLine(pen, location.X, point2.Y, point2.X, point2.Y);
                //    g.DrawLine(pen, point2.X, location.Y, point2.X, point2.Y);
                //}
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
    public enum SliderStyle
    {
        Led,
        Value,
    //    Image
    }

    public enum Orientation
    {
        Vertical,
        Horizontal
    }

    public enum SliderElementsPosition
    {
        None,
        TopLeft,
        BottomRight,
        Both,
        Internal
    }

    public enum ScaleColorMode
    {
        SingleColor,
        Sections
    }

    internal class GraphicsUtils
    {
        public static void DrawBevel(Graphics g, Rectangle rect, BevelStyle style)
        {
            if (style != BevelStyle.None)
            {
                Point location = rect.Location;
                Point point2 = rect.Location + rect.Size;
                Pen controlLightLight = SystemPens.ControlLightLight;
                Pen controlDark = SystemPens.ControlDark;
                Pen black = Pens.Black;
                Pen pen = Pens.Black;
                switch (style)
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
                if (((style == BevelStyle.Double) || (style == BevelStyle.DoubleRaised)) || (((style == BevelStyle.DoubleLowered) || (style == BevelStyle.FrameLowered)) || (style == BevelStyle.FrameRaised)))
                {
                    if ((style == BevelStyle.FrameLowered) || (style == BevelStyle.FrameRaised))
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
            }
        }

        public static Color ScaleColor(Color sourceColor, float scale)
        {
            int red = (int)(sourceColor.R * scale);
            int green = (int)(sourceColor.G * scale);
            int blue = (int)(sourceColor.B * scale);
            if (red > 0xff)
            {
                red = 0xff;
            }
            if (green > 0xff)
            {
                green = 0xff;
            }
            if (blue > 0xff)
            {
                blue = 0xff;
            }
            return Color.FromArgb(red, green, blue);
        }
    }
}

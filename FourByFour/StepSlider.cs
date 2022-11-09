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
    public class StepSlider : ControlWithBevel
    {
        private Color activeBarColor = Color.Red;
        private Color inactiveBarColor = Color.Gainsboro;
        private BevelStyle barBevel;
        private SliderStyle sliderStyle;
        private Color activeLedColor = Color.LightGreen;
        private Color inactiveLedColor = Color.Green;
        private Size sliderSize = new Size(25, 20);

        public StepSlider()
        {
            base.SetStyle(ControlStyles.Selectable, true);
        }

        protected LinearGradientBrush CreateColorsGradient(Rectangle gradRect, float k)
        {
            Color color = GraphicsUtils.ScaleColor(Color.Lime, k);
            Color color2 = GraphicsUtils.ScaleColor(Color.Yellow, k);
            Color color3 = GraphicsUtils.ScaleColor(Color.Red, k);
            int num = (this.Orientation == Orientation.Horizontal) ? 180 : 90;
            LinearGradientBrush brush = new LinearGradientBrush(gradRect, color3, color, (float)num);
            ColorBlend blend = new ColorBlend(6);
            blend.Positions[0] = 0f;
            blend.Positions[1] = 0.1f;
            blend.Positions[2] = 0.3f;
            blend.Positions[3] = 0.45f;
            blend.Positions[4] = 0.8f;
            blend.Positions[5] = 1f;
            blend.Colors[0] = color3;
            blend.Colors[1] = color3;
            blend.Colors[2] = color2;
            blend.Colors[3] = color2;
            blend.Colors[4] = color;
            blend.Colors[5] = color;
            brush.InterpolationColors = blend;
            return brush;
        }

        protected GraphicsPath CreatePath(Rectangle ir)
        {
            GraphicsPath path = new GraphicsPath();
            if (this.Orientation == Orientation.Horizontal)
            {
                path.StartFigure();
                path.AddLine(ir.X, ir.Y, ir.X + ir.Width, ir.Y);
                path.AddLine(ir.X + ir.Width, ir.Y + ir.Height, ir.X, ir.Y + ir.Height);
                path.CloseFigure();
                return path;
            }
            path.StartFigure();
            path.AddLine(ir.X, ir.Y, ir.X, ir.Y + ir.Height);
            path.AddLine(ir.X + ir.Width, ir.Y + ir.Height, ir.X + ir.Width, ir.Y);
            path.CloseFigure();
            return path;
        }

        protected LinearGradientBrush CreateTransparencyGradient(Rectangle gradRect)
        {
            Color color = Color.FromArgb(0x80, 0, 0, 0);
            Color.FromArgb(0x80, 0xff, 0xff, 0xff);
            Color.FromArgb(0, 0, 0, 0);
            Color color2 = Color.FromArgb(100, 0, 0, 0);
            int num = (this.Orientation == Orientation.Horizontal) ? 90 : 0;
            LinearGradientBrush brush = new LinearGradientBrush(gradRect, color2, color, (float)num);
            ColorBlend blend = new ColorBlend(4);
            blend.Positions[0] = 0f;
            blend.Positions[1] = 0.25f;
            blend.Positions[2] = 0.5f;
            blend.Positions[3] = 1f;
            blend.Colors[0] = Color.FromArgb(0x80, 0, 0, 0);
            blend.Colors[1] = Color.FromArgb(0x80, 0xff, 0xff, 0xff);
            blend.Colors[2] = Color.FromArgb(0, 0, 0, 0);
            blend.Colors[3] = Color.FromArgb(100, 0, 0, 0);
            brush.InterpolationColors = blend;
            return brush;
        }

        protected void DrawBar(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle barRectangle = this.BarRectangle;
            using (GraphicsPath path = this.CreatePath(barRectangle))
            {
                Rectangle gradRect = barRectangle;
                if (this.Orientation == Orientation.Horizontal)
                {
                    gradRect.Inflate(barRectangle.Height / 2, 0);
                }
                else
                {
                    gradRect.Inflate(0, barRectangle.Width / 2);
                }
                using (Brush brush = new SolidBrush(this.inactiveBarColor))
                {
                    g.FillPath(brush, path);
                }
                if (this.Value > this.Minimum)
                {
                    Rectangle ir = barRectangle;
                    if (this.Orientation == Orientation.Horizontal)
                    {
                        ir.Width = this.ValueToPoint(this.Value).X - ir.X;
                    }
                    else
                    {
                        ir.Y = this.ValueToPoint(this.Value).Y;
                        ir.Height = (barRectangle.Y + barRectangle.Height) - ir.Y;
                    }
                    using (GraphicsPath path2 = this.CreatePath(ir))
                    {
                        using (Brush brush2 = new SolidBrush(this.activeBarColor))
                        {
                            g.FillPath(brush2, path2);
                        }
                    }
                }
                using (Brush brush3 = this.CreateTransparencyGradient(gradRect))
                {
                    g.FillPath(brush3, path);
                }
                g.SmoothingMode = SmoothingMode.Default;
                GraphicsUtils.DrawBevel(g, this.BarRectangle, this.barBevel);
            }
        }

        protected void DrawSlider(Graphics g)
        {
            Point point = this.ValueToPoint(this.Value);
            Rectangle rect = new Rectangle(new Point(point.X - (this.sliderSize.Width / 2), point.Y - (this.sliderSize.Height / 2)), this.sliderSize);
            
            
                using (Brush brush = new LinearGradientBrush(rect, Color.White, Color.Gray, (float)(0x2d + ((base.Capture && (this.sliderStyle == SliderStyle.Value)) ? 180 : 0))))
                {
                    g.FillRectangle(brush, rect);
                }
                GraphicsUtils.DrawBevel(g, rect, BevelStyle.Raised);
                if (this.sliderStyle == SliderStyle.Led)
                {
                    Brush brush2;
                    if (base.Capture)
                    {
                        brush2 = new SolidBrush(this.activeLedColor);
                    }
                    else
                    {
                        brush2 = new SolidBrush(this.inactiveLedColor);
                    }
                    Rectangle rectangle2 = rect;
                    rectangle2.Inflate(-3, -3);
                    g.FillRectangle(brush2, rectangle2);
                    brush2.Dispose();
                }
                if (this.sliderStyle == SliderStyle.Value)
                {
                    StringFormat format = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Center
                    };
                    using (Brush brush3 = new SolidBrush(this.ForeColor))
                    {
                        g.DrawString(this.FormatValue(this.Value), this.Font, brush3, rect, format);
                    }
                }
            
        }

        public Color ActiveBarColor
        {
            get
            {
                return this.activeBarColor;
            }
            set
            {
                if (value != this.activeBarColor)
                {
                    this.activeBarColor = value;
                    base.Invalidate();
                }
            }
        }

        public Color InactiveBarColor
        {
            get
            {
                return this.inactiveBarColor;
            }
            set
            {
                if (value != this.inactiveBarColor)
                {
                    this.inactiveBarColor = value;
                    base.Invalidate();
                }
            }
        }

        public BevelStyle BarBevel
        {
            get
            {
                return this.barBevel;
            }
            set
            {
                if (value != this.barBevel)
                {
                    this.barBevel = value;
                    base.Invalidate();
                }
            }
        }

        public SliderStyle SliderStyle
        {
            get
            {
                return this.sliderStyle;
            }
            set
            {
                if (value != this.sliderStyle)
                {
                    this.sliderStyle = value;
                    base.Invalidate();
                }
            }
        }

        public Color ActiveLedColor
        {
            get
            {
                return this.activeLedColor;
            }
            set
            {
                if (value != this.activeLedColor)
                {
                    this.activeLedColor = value;
                    base.Invalidate();
                }
            }
        }

        public Color InactiveLedColor
        {
            get
            {
                return this.inactiveLedColor;
            }
            set
            {
                if (value != this.inactiveLedColor)
                {
                    this.inactiveLedColor = value;
                    base.Invalidate();
                }
            }
        }

        public Size SliderSize
        {
            get
            {
                return this.sliderSize;
            }
            set
            {
                if (value != this.sliderSize)
                {
                    this.sliderSize = value;
                    base.Invalidate();
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(0xc0, 100);
            }
        }

        //////
        private int barIndent = 10;
        private int barWidth = 10;
        private int indicatorWidth = 20;

        protected int CenterLine
        {
            get
            {
                int width;
                int x;
                Rectangle workRectangle = this.WorkRectangle;
                if (this.Orientation == Orientation.Vertical)
                {
                    x = workRectangle.X;
                    width = workRectangle.Width;
                }
                else
                {
                    x = workRectangle.Y;
                    width = workRectangle.Height;
                }
                int num3 = x + (width / 2);
                switch (this.LabelsPosition)
                {
                    case SliderElementsPosition.TopLeft:
                        return (((x + width) - (this.indicatorWidth / 2)) - (((this.TicksPosition == SliderElementsPosition.BottomRight) || (this.TicksPosition == SliderElementsPosition.Both)) ? (this.TicksLenght + 2) : 0));

                    case SliderElementsPosition.BottomRight:
                        return ((x + (this.indicatorWidth / 2)) + (((this.TicksPosition == SliderElementsPosition.TopLeft) || (this.TicksPosition == SliderElementsPosition.Both)) ? (this.TicksLenght + 2) : 0));
                }
                return num3;
            }
        }

        protected Rectangle IndicatorRectangle
        {
            get
            {
                Rectangle workRectangle = this.WorkRectangle;
                if (this.Orientation == Orientation.Horizontal)
                {
                    return new Rectangle(workRectangle.X + this.BarIndent, this.CenterLine - (this.indicatorWidth / 2), workRectangle.Width - (this.BarIndent * 2), this.indicatorWidth);
                }
                return new Rectangle(this.CenterLine - (this.indicatorWidth / 2), workRectangle.Y + this.BarIndent, this.indicatorWidth, workRectangle.Height - (this.BarIndent * 2));
            }
        }

        protected Rectangle BarRectangle
        {
            get
            {
                Rectangle workRectangle = this.WorkRectangle;
                if (this.Orientation == Orientation.Horizontal)
                {
                    return new Rectangle(workRectangle.X + this.BarIndent, this.CenterLine - (this.barWidth / 2), workRectangle.Width - (this.BarIndent * 2), this.barWidth);
                }
                return new Rectangle(this.CenterLine - (this.barWidth / 2), workRectangle.Y + this.BarIndent, this.barWidth, workRectangle.Height - (this.BarIndent * 2));
            }
        }

        public int BarIndent
        {
            get
            {
                return this.barIndent;
            }
            set
            {
                if (this.barIndent != value)
                {
                    this.barIndent = value;
                    base.Invalidate();
                }
            }
        }

        public int BarWidth
        {
            get
            {
                return this.barWidth;
            }
            set
            {
                if (this.barWidth != value)
                {
                    this.barWidth = value;
                    base.Invalidate();
                }
            }
        }

        public int IndicatorWidth
        {
            get
            {
                return this.indicatorWidth;
            }
            set
            {
                if (this.indicatorWidth != value)
                {
                    this.indicatorWidth = value;
                    base.Invalidate();
                }
            }
        }

        //////
        private bool indicatorOnly;
        private Orientation orientation = Orientation.Horizontal;
        private SliderElementsPosition ticksPosition = SliderElementsPosition.Both;
        private int ticksCount = 10;
        private int ticksSubDivisionsCount = 5;
        private int ticksLenght = 8;
        private string[] labels = new string[0];
        private SliderElementsPosition labelsPosition = SliderElementsPosition.Both;
        private int labelsCount = 5;
        private Color scaleColor = Color.Black;
        //private Color warningColor = Color.SaddleBrown;
        //private Color dangerColor = Color.Red;
        //private int warningValue = 50;
        //private int dangerValue = 80;
        //private ScaleColorMode scaleColorMode = FourByFour.ScaleColorMode.Sections;
        
        protected override void Draw(Graphics g, Rectangle r)
        {
            base.Draw(g, r);
            this.DrawBar(g);
            this.DrawScale(g);
            this.DrawSlider(g);
        }

        protected virtual void DrawScale(Graphics g)
        {
            Rectangle indicatorRectangle = this.IndicatorRectangle;
            if (this.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i <= this.ticksCount; i++)
                {
                    double num2 = this.Minimum + ((this.Maximum - this.Minimum) * (((double)i) / ((double)this.ticksCount)));
                    int num3 = indicatorRectangle.X + ((int)((((double)i) / ((double)this.ticksCount)) * indicatorRectangle.Width));
                    Color valueColor = this.GetValueColor((int)num2);
                    if ((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both))
                    {
                        g.DrawLine(new Pen(valueColor), num3, indicatorRectangle.Y, num3, indicatorRectangle.Y - this.ticksLenght);
                    }
                    if ((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both))
                    {
                        g.DrawLine(new Pen(valueColor), num3, indicatorRectangle.Y + indicatorRectangle.Height, num3, (indicatorRectangle.Y + indicatorRectangle.Height) + this.ticksLenght);
                    }
                    if (this.ticksPosition == SliderElementsPosition.Internal)
                    {
                        g.DrawLine(new Pen(valueColor), num3, indicatorRectangle.Y + indicatorRectangle.Height, num3, indicatorRectangle.Y);
                    }
                    if (i != this.ticksCount)
                    {
                        for (int j = 1; j < this.ticksSubDivisionsCount; j++)
                        {
                            double num5 = num2 + ((this.Maximum - this.Minimum) * ((((double)j) / ((double)this.ticksCount)) / ((double)this.ticksSubDivisionsCount)));
                            int num6 = num3 + ((int)(((j * indicatorRectangle.Width) / ((double)this.ticksCount)) / ((double)this.ticksSubDivisionsCount)));
                            valueColor = this.GetValueColor((int)num5);
                            if ((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both))
                            {
                                g.DrawLine(new Pen(valueColor), num6, indicatorRectangle.Y, num6, indicatorRectangle.Y - (this.ticksLenght / 2));
                            }
                            if ((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both))
                            {
                                g.DrawLine(new Pen(valueColor), num6, indicatorRectangle.Y + indicatorRectangle.Height, num6, (indicatorRectangle.Y + indicatorRectangle.Height) + (this.ticksLenght / 2));
                            }
                            if (this.ticksPosition == SliderElementsPosition.Internal)
                            {
                                g.DrawLine(new Pen(valueColor), num6, indicatorRectangle.Y + (indicatorRectangle.Height / 4), num6, (indicatorRectangle.Y + indicatorRectangle.Height) - (indicatorRectangle.Height / 4));
                            }
                        }
                    }
                }
                if (this.labelsPosition != SliderElementsPosition.None)
                {
                    bool flag = (this.labels != null) && (this.labels.Length != 0);
                    int num7 = flag ? (this.labels.Length - 1) : this.labelsCount;
                    int num8 = indicatorRectangle.Y - (((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both)) ? this.ticksLenght : -2);
                    int num9 = (indicatorRectangle.Y + indicatorRectangle.Height) + (((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both)) ? this.ticksLenght : 2);
                    for (int j = 0; j <= num7; j++)
                    {
                        int v = this.Minimum + ((int)((this.Maximum - this.Minimum) * (((double)j) / ((double)num7))));
                        int num12 = indicatorRectangle.X + ((int)((((double)j) / ((double)num7)) * indicatorRectangle.Width));
                        Color valueColor = this.GetValueColor(v);
                        string text = this.FormatValue(v);
                        if (flag)
                        {
                            text = this.labels[j];
                        }
                        Size size = g.MeasureString(text, this.Font).ToSize();
                        if ((this.labelsPosition == SliderElementsPosition.TopLeft) || (this.labelsPosition == SliderElementsPosition.Both))
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)(num12 - (size.Width / 2)), (float)(num8 - size.Height));
                        }
                        if ((this.labelsPosition == SliderElementsPosition.BottomRight) || (this.labelsPosition == SliderElementsPosition.Both))
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)(num12 - (size.Width / 2)), (float)num9);
                        }
                        if (this.labelsPosition == SliderElementsPosition.Internal)
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)(num12 - (size.Width / 2)), (float)(this.CenterLine - (size.Height / 2)));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i <= this.ticksCount; i++)
                {
                    double num14 = this.Minimum + ((this.Maximum - this.Minimum) * (((double)i) / ((double)this.ticksCount)));
                    int num15 = (indicatorRectangle.Height + indicatorRectangle.Y) - ((int)((((double)i) / ((double)this.ticksCount)) * indicatorRectangle.Height));
                    Color valueColor = this.GetValueColor((int)num14);
                    if ((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both))
                    {
                        g.DrawLine(new Pen(valueColor), indicatorRectangle.X, num15, indicatorRectangle.X - this.ticksLenght, num15);
                    }
                    if ((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both))
                    {
                        g.DrawLine(new Pen(valueColor), indicatorRectangle.X + indicatorRectangle.Width, num15, (indicatorRectangle.X + indicatorRectangle.Width) + this.ticksLenght, num15);
                    }
                    if (this.ticksPosition == SliderElementsPosition.Internal)
                    {
                        g.DrawLine(new Pen(valueColor), indicatorRectangle.X + indicatorRectangle.Width, num15, indicatorRectangle.X, num15);
                    }
                    if (i != this.ticksCount)
                    {
                        for (int j = 1; j < this.ticksSubDivisionsCount; j++)
                        {
                            double num17 = num14 + ((this.Maximum - this.Minimum) * ((((double)j) / ((double)this.ticksCount)) / ((double)this.ticksSubDivisionsCount)));
                            int num18 = num15 - ((int)(((j * indicatorRectangle.Height) / ((double)this.ticksCount)) / ((double)this.ticksSubDivisionsCount)));
                            valueColor = this.GetValueColor((int)num17);
                            if ((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both))
                            {
                                g.DrawLine(new Pen(valueColor), indicatorRectangle.X, num18, indicatorRectangle.X - (this.ticksLenght / 2), num18);
                            }
                            if ((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both))
                            {
                                g.DrawLine(new Pen(valueColor), indicatorRectangle.X + indicatorRectangle.Width, num18, (indicatorRectangle.X + indicatorRectangle.Width) + (this.ticksLenght / 2), num18);
                            }
                            if (this.ticksPosition == SliderElementsPosition.Internal)
                            {
                                g.DrawLine(new Pen(valueColor), indicatorRectangle.X + (indicatorRectangle.Width / 4), num18, (indicatorRectangle.X + indicatorRectangle.Width) - (indicatorRectangle.Width / 4), num18);
                            }
                        }
                    }
                }
                if (this.labelsPosition != SliderElementsPosition.None)
                {
                    bool flag2 = (this.labels != null) && (this.labels.Length != 0);
                    int num19 = flag2 ? (this.labels.Length - 1) : this.labelsCount;
                    int num20 = indicatorRectangle.X - (((this.ticksPosition == SliderElementsPosition.TopLeft) || (this.ticksPosition == SliderElementsPosition.Both)) ? this.ticksLenght : -2);
                    int num21 = (indicatorRectangle.X + indicatorRectangle.Width) + (((this.ticksPosition == SliderElementsPosition.BottomRight) || (this.ticksPosition == SliderElementsPosition.Both)) ? this.ticksLenght : 2);
                    for (int j = 0; j <= num19; j++)
                    {
                        int v = this.Minimum + ((int)((this.Maximum - this.Minimum) * (((double)j) / ((double)num19))));
                        int num24 = (indicatorRectangle.Y + indicatorRectangle.Height) - ((int)((((double)j) / ((double)num19)) * indicatorRectangle.Height));
                        Color valueColor = this.GetValueColor(v);
                        string text = this.FormatValue(v);
                        if (flag2)
                        {
                            text = this.labels[j];
                        }
                        Size size2 = g.MeasureString(text, this.Font).ToSize();
                        if ((this.labelsPosition == SliderElementsPosition.TopLeft) || (this.labelsPosition == SliderElementsPosition.Both))
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)(num20 - size2.Width), (float)(num24 - (size2.Height / 2)));
                        }
                        if ((this.labelsPosition == SliderElementsPosition.BottomRight) || (this.labelsPosition == SliderElementsPosition.Both))
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)num21, (float)(num24 - (size2.Height / 2)));
                        }
                        if (this.labelsPosition == SliderElementsPosition.Internal)
                        {
                            g.DrawString(text, this.Font, new SolidBrush(valueColor), (float)((indicatorRectangle.X + (indicatorRectangle.Width / 2)) - (size2.Width / 2)), (float)(num24 - (size2.Height / 2)));
                        }
                    }
                }
            }
        }


        protected double GetPercent(int value)
        {
            if (this.Maximum == this.Minimum)
            {
                return 0.0;
            }
            return ((value - this.Minimum) / ((double)(this.Maximum - this.Minimum)));
        }

        protected Color GetValueColor(int v)
        {
            //switch (this.scaleColorMode)
            //{
            //    case ScaleColorMode.SingleColor:
            //        return this.scaleColor;

            //    case ScaleColorMode.Sections:
            //        if (v < this.dangerValue)
            //        {
            //            if (v >= this.warningValue)
            //            {
            //                return this.warningColor;
            //            }
            //            return this.scaleColor;
            //        }
            //        return this.dangerColor;
            //}
            return this.scaleColor;
        }

        protected int PointToValue(Point p)
        {
            double num;
            Rectangle indicatorRectangle = this.IndicatorRectangle;
            if (this.Orientation == Orientation.Vertical)
            {
                if (p.Y >= indicatorRectangle.Bottom)
                {
                    return this.Minimum;
                }
                if (p.Y <= indicatorRectangle.Top)
                {
                    return this.Maximum;
                }
                num = ((double)((indicatorRectangle.Y + indicatorRectangle.Height) - p.Y)) / ((double)indicatorRectangle.Height);
            }
            else
            {
                if (p.X <= indicatorRectangle.Left)
                {
                    return this.Minimum;
                }
                if (p.X >= indicatorRectangle.Right)
                {
                    return this.Maximum;
                }
                num = ((float)(p.X - indicatorRectangle.X)) / ((float)indicatorRectangle.Width);
            }
            return (((int)(num * (this.Maximum - this.Minimum))) + this.Minimum);
        }

        protected virtual Point ValueToPoint(int value)
        {
            Rectangle indicatorRectangle = this.IndicatorRectangle;
            if (this.Orientation == Orientation.Vertical)
            {
                int num = (int)(this.GetPercent(this.Value) * indicatorRectangle.Height);
                return new Point(indicatorRectangle.X + (indicatorRectangle.Width / 2), (indicatorRectangle.Y + indicatorRectangle.Height) - num);
            }
            int num2 = (int)(this.GetPercent(this.Value) * indicatorRectangle.Width);
            return new Point(indicatorRectangle.X + num2, indicatorRectangle.Y + (indicatorRectangle.Height / 2));
        }

        protected bool Editable
        {
            get
            {
                return !this.indicatorOnly;
            }
        }
                
        public bool IndicatorOnly
        {
            get
            {
                return this.indicatorOnly;
            }
            set
            {
                if (this.indicatorOnly != value)
                {
                    this.indicatorOnly = value;
                    if (value)
                    {
                        base.SetStyle(ControlStyles.Selectable, true);
                    }
                    else
                    {
                        base.SetStyle(ControlStyles.Selectable, false);
                    }
                }
            }
        }

        public virtual Orientation Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                if (this.orientation != value)
                {
                    this.orientation = value;
                    base.Invalidate();
                }
            }
        }

        public SliderElementsPosition TicksPosition
        {
            get
            {
                return this.ticksPosition;
            }
            set
            {
                if (this.ticksPosition != value)
                {
                    this.ticksPosition = value;
                    base.Invalidate();
                }
            }
        }

        public int TicksCount
        {
            get
            {
                return this.ticksCount;
            }
            set
            {
                if (value != this.ticksCount)
                {
                    this.ticksCount = value;
                    base.Invalidate();
                }
            }
        }

        public int TicksSubDivisionsCount
        {
            get
            {
                return this.ticksSubDivisionsCount;
            }
            set
            {
                if (value != this.ticksSubDivisionsCount)
                {
                    this.ticksSubDivisionsCount = value;
                    base.Invalidate();
                }
            }
        }

        public int TicksLenght
        {
            get
            {
                return this.ticksLenght;
            }
            set
            {
                if (value != this.ticksLenght)
                {
                    this.ticksLenght = value;
                    base.Invalidate();
                }
            }
        }

        public string[] Labels
        {
            get
            {
                return this.labels;
            }
            set
            {
                this.labels = value;
                base.Invalidate();
            }
        }

        public SliderElementsPosition LabelsPosition
        {
            get
            {
                return this.labelsPosition;
            }
            set
            {
                if (this.labelsPosition != value)
                {
                    this.labelsPosition = value;
                    base.Invalidate();
                }
            }
        }

        public int LabelsCount
        {
            get
            {
                return this.labelsCount;
            }
            set
            {
                if (value != this.labelsCount)
                {
                    this.labelsCount = value;
                    base.Invalidate();
                }
            }
        }

        public Color ScaleColor
        {
            get
            {
                return this.scaleColor;
            }
            set
            {
                if (value != this.scaleColor)
                {
                    this.scaleColor = value;
                    base.Invalidate();
                }
            }
        }

        //public Color WarningColor
        //{
        //    get
        //    {
        //        return this.warningColor;
        //    }
        //    set
        //    {
        //        if (value != this.warningColor)
        //        {
        //            this.warningColor = value;
        //            base.Invalidate();
        //        }
        //    }
        //}

        //public Color DangerColor
        //{
        //    get
        //    {
        //        return this.dangerColor;
        //    }
        //    set
        //    {
        //        if (value != this.dangerColor)
        //        {
        //            this.dangerColor = value;
        //            base.Invalidate();
        //        }
        //    }
        //}

        //public int WarningValue
        //{
        //    get
        //    {
        //        return this.warningValue;
        //    }
        //    set
        //    {
        //        if (value != this.warningValue)
        //        {
        //            this.warningValue = value;
        //            base.Invalidate();
        //        }
        //    }
        //}

        //public int DangerValue
        //{
        //    get
        //    {
        //        return this.dangerValue;
        //    }
        //    set
        //    {
        //        if (value != this.dangerValue)
        //        {
        //            this.dangerValue = value;
        //            base.Invalidate();
        //        }
        //    }
        //}

        //public ScaleColorMode ScaleColorMode
        //{
        //    get
        //    {
        //        return this.scaleColorMode;
        //    }
        //    set
        //    {
        //        if (value != this.scaleColorMode)
        //        {
        //            this.scaleColorMode = value;
        //            base.Invalidate();
        //        }
        //    }
        //}

        ///////
        private bool dragging;
        private int maximum = 100;
        private int minimum;
        private int increment = 10;
        private int numberScale = 1;
        private int value;
        //private EventHandler ScaledValueChanged;
        //private EventHandler ValueChanged;

        public event EventHandler ScaledValueChanged;

        public event EventHandler ValueChanged;

        private void DecrementValue()
        {
            this.Value -= this.increment;
        }

        protected string FormatValue(int value)
        {
            if (this.numberScale == 1)
            {
                return value.ToString();
            }
            double num = ((double)value) / ((double)this.numberScale);
            return num.ToString();
        }

        private void IncrementValue()
        {
            this.Value += this.increment;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (!this.Editable || !base.Enabled)
            {
                return false;
            }
            if (((keyData != Keys.Up) && (keyData != Keys.Down)) && (keyData != Keys.Left))
            {
                return (keyData == Keys.Right);
            }
            return true;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (this.Editable && base.Enabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        this.DecrementValue();
                        e.Handled = true;
                        break;

                    case Keys.Up:
                        this.IncrementValue();
                        e.Handled = true;
                        return;

                    case Keys.Right:
                        this.IncrementValue();
                        e.Handled = true;
                        return;

                    case Keys.Down:
                        this.DecrementValue();
                        e.Handled = true;
                        return;

                    default:
                        return;
                }
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if ((this.Editable && base.Enabled) && (e.Button == MouseButtons.Left))
            {
                base.Capture = true;
                this.dragging = true;
                this.Value = this.PointToValue(new Point(e.X, e.Y));
                if (base.TabStop)
                {
                    base.Focus();
                }
                base.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.Editable && this.dragging)
            {
                this.Value = this.PointToValue(new Point(e.X, e.Y));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if ((this.Editable && this.dragging) && (e.Button == MouseButtons.Left))
            {
                base.Capture = false;
                this.dragging = false;
                base.Invalidate();
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
            if (this.ScaledValueChanged != null)
            {
                this.ScaledValueChanged(this, e);
            }
        }

        protected virtual void OptimizedInvalidate(int oldValue, int NewValue)
        {
            base.Invalidate();
        }

        public virtual int Maximum
        {
            get
            {
                return this.maximum;
            }
            set
            {
                if (this.maximum != value)
                {
                    this.maximum = value;
                    base.Invalidate();
                }
            }
        }

        public virtual int Minimum
        {
            get
            {
                return this.minimum;
            }
            set
            {
                if (this.minimum != value)
                {
                    this.minimum = value;
                    base.Invalidate();
                }
            }
        }

        public virtual int Increment
        {
            get
            {
                return this.increment;
            }
            set
            {
                if (this.increment != value)
                {
                    this.increment = value;
                    base.Invalidate();
                }
            }
        }

        public int NumberScale
        {
            get
            {
                return this.numberScale;
            }
            set
            {
                if (this.numberScale != value)
                {
                    this.numberScale = value;
                    base.Invalidate();
                }
            }
        }

        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value > this.maximum)
                {
                    value = this.maximum;
                }
                if (value < this.minimum)
                {
                    value = this.minimum;
                }
                if (value != this.value)
                {
                    int oldValue = this.value;
                    this.value = value;
                    this.OnValueChanged(EventArgs.Empty);
                    this.OptimizedInvalidate(oldValue, this.value);
                }
            }
        }

        public double ScaledValue
        {
            get
            {
                return (((double)this.Value) / ((double)this.numberScale));
            }
            set
            {
                this.Value = (int)(value * this.numberScale);
            }
        }
    }
}

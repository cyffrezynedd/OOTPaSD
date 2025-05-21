namespace Editor
{
    public class Ellipse : SimplePrimitive
    {
        public Ellipse(PrimitiveStyle selectedStyle, Point mousePos)
            : base(selectedStyle, mousePos)
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            using (var brush = new SolidBrush(style.FillColor))
            {
                int w = Convert.ToInt32(WidthHeight.Item1);
                int h = Convert.ToInt32(WidthHeight.Item2);

                var r = new System.Drawing.Rectangle(CurrentPos.X, CurrentPos.Y, w, h);
                graphics.FillEllipse(brush, r);
                graphics.DrawEllipse(pen, r);
            }
        }
    }
}

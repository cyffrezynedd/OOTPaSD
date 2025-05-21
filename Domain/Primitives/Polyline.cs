namespace Editor
{
    public class Polyline : CompositePrimitive
    {
        public Polyline(PrimitiveStyle selectedStyle, Point mousePos) 
            : base(selectedStyle, mousePos)
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            {
                graphics.DrawLines(pen, FigurePoints.ToArray());
            }
        }

        public override void Update(Point currentMousePos)
        {
            FigurePoints[FigurePoints.Count - 1] = currentMousePos;
        }
    }
}

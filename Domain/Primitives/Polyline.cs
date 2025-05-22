namespace Editor
{
    public class Polyline : CompositePrimitive
    {
        public Point? PreviewPoint { get; private set; }

        public Polyline(PrimitiveStyle selectedStyle, Point mousePos) 
            : base(selectedStyle, mousePos)
        {
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            {
                graphics.DrawLines(pen, FigurePoints.ToArray());
                if (PreviewPoint.HasValue && FigurePoints.Count > 0)
                {
                    graphics.DrawLine(pen, FigurePoints.Last(), PreviewPoint.Value);
                    PreviewPoint = null;
                }
            }
        }

        public override void Update(Point currentMousePos)
        {
            FigurePoints[FigurePoints.Count - 1] = currentMousePos;
        }

        public void UpdatePreview(Point currentMousePos)
        {
            PreviewPoint = currentMousePos;
        }
    }
}

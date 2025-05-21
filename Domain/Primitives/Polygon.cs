namespace Editor
{
    public class Polygon : CompositePrimitive
    {
        public readonly int angleCount;
        public (double, double) WidthHeight { get; private set; }
        public Polygon(PrimitiveStyle selectedStyle, Point mousePos, int aCount) 
            : base(selectedStyle, mousePos)
        {
            angleCount = aCount;
            WidthHeight = (0, 0);
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            using (var brush = new SolidBrush(style.FillColor))
            {
                graphics.FillPolygon(brush, FigurePoints.ToArray());
                graphics.DrawPolygon(pen, FigurePoints.ToArray());
            }
        }

        public override void Update(Point currentMousePos)
        {
            WidthHeight = (Math.Abs(currentMousePos.X - startPos.X) * 2,
                Math.Abs(currentMousePos.Y - startPos.Y) * 2);

            FigurePoints = this.PointsRelocate(currentMousePos);
        }
    }

    public static class PolygonMethods
    {
        readonly static double initialAngle = -Math.PI / 2;
        public static List<Point> PointsRelocate(this Polygon p, Point currentMousePos)
        {
            var result = new List<Point>(p.angleCount);
            double angleStep = 2 * Math.PI / p.angleCount;

            for (int i = 0; i < p.angleCount; i++)
            {
                double angle = initialAngle + i * angleStep;
                int x = p.startPos.X + (int)(Math.Cos(angle) * p.WidthHeight.Item1 / 2);
                int y = p.startPos.Y + (int)(Math.Sin(angle) * p.WidthHeight.Item2 / 2);
                result.Add(new Point(x, y));
            }

            return result;
        }
    }
}

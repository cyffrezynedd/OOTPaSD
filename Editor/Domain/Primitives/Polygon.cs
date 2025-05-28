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
            WidthHeight = (Math.Abs(currentMousePos.X - startPos.X),
                           Math.Abs(currentMousePos.Y - startPos.Y));

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

            int centerX = (int)(Math.Min(p.startPos.X, currentMousePos.X) + p.WidthHeight.Item1 / 2);
            int centerY = (int)(Math.Min(p.startPos.Y, currentMousePos.Y) + p.WidthHeight.Item2 / 2);
            double angle;
            int x, y;
            for (int i = 0; i < p.angleCount; i++)
            {
                angle = initialAngle + i * angleStep;
                x = centerX + (int)(Math.Cos(angle) * p.WidthHeight.Item1 / 2);
                y = centerY + (int)(Math.Sin(angle) * p.WidthHeight.Item2 / 2);
                result.Add(new Point(x, y));
            }

            return result;
        }
    }
}

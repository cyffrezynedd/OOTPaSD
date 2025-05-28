using Editor;

namespace TrapezoidPlugin
{
    public class Trapezoid : CompositePrimitive
    {
        public (double, double) WidthHeight { get; private set; }
        public Point CurrentMousePos { get; private set; }
        public Trapezoid(PrimitiveStyle selectedStyle, Point mousePos)
            : base(selectedStyle, mousePos)
        {
            WidthHeight = (0, 0);
            CurrentMousePos = mousePos;
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            using (var brush = new SolidBrush(style.FillColor))
            {
                List<Point> points = this.GetTrapezoidPoints();
                graphics.FillPolygon(brush, points.ToArray());
                graphics.DrawPolygon(pen, points.ToArray());
            }
        }

        public override void Update(Point currentMousePos)
        {
            CurrentMousePos = currentMousePos;
            WidthHeight = (Math.Abs(currentMousePos.X - startPos.X),
                           Math.Abs(currentMousePos.Y - startPos.Y));
            FigurePoints = this.GetTrapezoidPoints();
        }
    }

    public static class TrapezoidMethods
    {
        public static List<Point> GetTrapezoidPoints(this Trapezoid trapezoid)
        {
            int width = (int)trapezoid.WidthHeight.Item1;
            int height = (int)trapezoid.WidthHeight.Item2;

            int left = Math.Min(trapezoid.startPos.X, trapezoid.CurrentMousePos.X);
            int top = Math.Min(trapezoid.startPos.Y, trapezoid.CurrentMousePos.Y);
            int right = left + width;
            int bottom = top + height;

            int topBaseWidth = (int)(width * 0.6);
            int topBaseLeft = left + (width - topBaseWidth) / 2;

            var points = new List<Point>
            {
                new Point(left, bottom),                  
                new Point(right, bottom),                 
                new Point(topBaseLeft + topBaseWidth, top), 
                new Point(topBaseLeft, top)               
            };

            return points;
        }
    }
}

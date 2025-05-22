using System.Drawing.Drawing2D;

namespace Editor
{
    public abstract class ToolTemplate : PrimitiveTemplate
    {
        public readonly int Radius;
        public List<Point> ToolPoints { get; private set; }
        public Point CurrentPos { get; private set; }

        public ToolTemplate(PrimitiveStyle style, Point startPos)
            : base(style, startPos)
        {
            CurrentPos = startPos;
            Radius = Convert.ToInt32(style.StrokeWidth);
            ToolPoints = new List<Point> { startPos };
        }
        private double Distance(Point a, Point b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            return (double)Math.Sqrt(dx * dx + dy * dy);
        }

        public override void Update(Point currentMousePos)
        {
            if (ToolPoints.Count == 0)
            {
                ToolPoints.Add(currentMousePos);
                CurrentPos = currentMousePos;
                return;
            }

            Point lastPoint = ToolPoints[^1];
            double distance = Distance(lastPoint, currentMousePos);

            if (distance > 2)
            {
                int steps = Math.Max(1, (int)(distance / 2));
                List<Point> newPoints = new List<Point>(steps);
                double coeff;
                int x, y;

                for (int s = 1; s <= steps; s++)
                {
                    coeff = (double)s / steps;
                    x = lastPoint.X + (int)((currentMousePos.X - lastPoint.X) * coeff);
                    y = lastPoint.Y + (int)((currentMousePos.Y - lastPoint.Y) * coeff);
                    newPoints.Add(new Point(x, y));
                }

                ToolPoints.AddRange(newPoints);
            }
            else
            {
                ToolPoints.Add(currentMousePos);
            }

            CurrentPos = currentMousePos;
        }


        public override void Draw(Graphics graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException(nameof(graphics));

            if (ToolPoints.Count < 2)
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int diameter = Math.Max(1, style.StrokeWidth);

            using (GraphicsPath path = new GraphicsPath())
            using (SolidBrush brush = new SolidBrush(style.StrokeColor))
            {
                path.AddLines(ToolPoints.ToArray());

                graphics.DrawPath(new Pen(brush, diameter), path);
            }
        }

        public override PrimitiveTemplate? Clone()
        {
            ToolTemplate? clone = MemberwiseClone() as ToolTemplate;
            if (clone != null)
            {
                clone.ToolPoints = new List<Point>(ToolPoints);
            }
            return clone;
        }
    }
}

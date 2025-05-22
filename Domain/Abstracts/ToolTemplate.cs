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
            if (ToolPoints.Count > 0)
            {
                Point lastPoint = ToolPoints[ToolPoints.Count - 1];
                double distance = Distance(lastPoint, currentMousePos);

                if (distance > 2)
                {
                    double coeff;
                    int x, y;
                    int steps = (int)(distance / 2);
                    for (int s = 1; s <= steps; s++)
                    {
                        coeff = (double)s / steps;
                        x = lastPoint.X + (int)((currentMousePos.X - lastPoint.X) * coeff);
                        y = lastPoint.Y + (int)((currentMousePos.Y - lastPoint.Y) * coeff);
                        ToolPoints.Add(new Point(x, y));
                    }
                }
                else
                {
                    ToolPoints.Add(currentMousePos);
                }
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

            if (ToolPoints.Count == 0)
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int diameter = style.StrokeWidth;

            if (diameter < 1)
            {
                diameter = 1;
            }

            using (var pen = new Pen(style.StrokeColor, diameter))
            using (var brush = new SolidBrush(style.StrokeColor))
            {
                foreach (var point in ToolPoints)
                {
                    var r = new System.Drawing.Rectangle(point.X - diameter / 2, point.Y - diameter / 2, diameter, diameter);
                    graphics.FillEllipse(brush, r);
                }

                Point prevPoint;
                Point currentPoint;
                double distance, coeff;
                int numSteps, x, y;

                for (int i = 1; i < ToolPoints.Count; i++)
                {
                    prevPoint = ToolPoints[i - 1];
                    currentPoint = ToolPoints[i];
                    distance = Distance(prevPoint, currentPoint);

                    if (distance > diameter * 0.5f)
                    {
                        numSteps = (int)(distance / (diameter * 0.5f));
                        for (int s = 1; s < numSteps; s++)
                        {
                            coeff = (double)s / numSteps;
                            x = prevPoint.X + (int)((currentPoint.X - prevPoint.X) * coeff);
                            y = prevPoint.Y + (int)((currentPoint.Y - prevPoint.Y) * coeff);
                            var r = new System.Drawing.Rectangle(x - diameter / 2, y - diameter / 2, diameter, diameter);
                            graphics.FillEllipse(brush, r);
                        }
                    }
                }
            }
        }
    }
}

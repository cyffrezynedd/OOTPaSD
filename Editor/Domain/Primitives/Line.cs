namespace Editor
{
    public class Line : PrimitiveTemplate
    {
        public Point EndPos { get; private set; }

        public Line(PrimitiveStyle selectedStyle, Point mousePos)
            : base(selectedStyle, mousePos)
        {
            EndPos = startPos;
        }

        public override void Draw(Graphics graphics)
        {
            using (var pen = new Pen(style.StrokeColor, style.StrokeWidth))
            {
                graphics.DrawLine(pen, startPos, EndPos);
            }
        }

        public override void Update(Point currentMousePos)
        {
            EndPos = currentMousePos;
        }

        public override PrimitiveTemplate? Clone() => MemberwiseClone() as Line;
    }
}

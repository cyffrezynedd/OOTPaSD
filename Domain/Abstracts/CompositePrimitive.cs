namespace Editor
{
    public abstract class CompositePrimitive : PrimitiveTemplate
    {
        public List<Point> FigurePoints { get; set; }

        public CompositePrimitive(PrimitiveStyle selectedStyle, Point mousePos)
            : base(selectedStyle, mousePos)
        {
            FigurePoints = new List<Point>(2) { mousePos, mousePos };
        }

        public void Add(Point point)
        {
            FigurePoints.Add(point);
        }

        public abstract override void Draw(Graphics graphics);

        public abstract override void Update(Point currentMousePos);

        public override PrimitiveTemplate? Clone()
        {
            CompositePrimitive? clone = MemberwiseClone() as CompositePrimitive;
            if (clone != null)
            {
                clone.FigurePoints = new List<Point>(FigurePoints);
            }
            return clone;
        }
    }
}

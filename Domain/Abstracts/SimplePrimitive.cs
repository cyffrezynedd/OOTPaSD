namespace Editor
{
    public abstract class SimplePrimitive : PrimitiveTemplate
    {
        public (double, double) WidthHeight { get; private set; }

        public Point CurrentPos { get; private set; }
        public SimplePrimitive(PrimitiveStyle selectedStyle, Point mousePos) 
            : base(selectedStyle, mousePos)
        {
            WidthHeight = (0, 0);
            CurrentPos = mousePos;
        }

        public override void Update(Point currentMousePos)
        {
            CurrentPos = new Point(Math.Min(startPos.X, currentMousePos.X), Math.Min(startPos.Y, currentMousePos.Y));
            WidthHeight = (Math.Abs(currentMousePos.X - startPos.X), Math.Abs(currentMousePos.Y - startPos.Y)); 
        }
    }
}

namespace Editor
{
    public abstract class PrimitiveTemplate
    {
        public readonly PrimitiveStyle style;

        public readonly Point startPos;

        public abstract void Draw(Graphics graphics);

        public abstract void Update(Point currentMousePos);

        public PrimitiveTemplate(PrimitiveStyle selectedStyle, Point mousePos)
        {
            style = selectedStyle;
            startPos = mousePos;
        }
    }
}

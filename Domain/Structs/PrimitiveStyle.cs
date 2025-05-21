namespace Editor
{
    public readonly struct PrimitiveStyle
    {
        public Color FillColor { get; }
        public Color StrokeColor { get; }
        public int StrokeWidth { get; }

        public PrimitiveStyle(int sWidth, Color sColor)
        {
            FillColor = sColor;
            StrokeColor = sColor;
            StrokeWidth = sWidth;
        }

        public PrimitiveStyle(int sWidth, Color sColor, Color fColor)
        {
            FillColor = fColor;
            StrokeColor = sColor;
            StrokeWidth = sWidth;
        }
    }
}

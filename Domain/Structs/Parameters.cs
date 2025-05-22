namespace Editor
{
    public readonly struct Parameters
    {
        public PrimitiveStyle Style { get; }
        public Point StartPos { get; }
        public int AngleCount { get; }

        public Parameters(PrimitiveStyle style, Point startPos, int angleCount)
        {
            Style = style;
            StartPos = startPos;
            AngleCount = angleCount;
        }
    }
}

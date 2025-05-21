namespace Editor
{
    public class Pensil : ToolTemplate
    {
        public Pensil(PrimitiveStyle style, Point startPos) 
            : base(new PrimitiveStyle(style.StrokeWidth, style.StrokeColor), startPos)
        {
        }
    }
}

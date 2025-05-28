namespace Editor
{
    public class Pencil : ToolTemplate
    {
        public Pencil(PrimitiveStyle style, Point startPos) 
            : base(new PrimitiveStyle(style.StrokeWidth, style.StrokeColor), startPos)
        {
        }
    }
}

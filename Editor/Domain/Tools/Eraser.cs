﻿namespace Editor
{
    public class Eraser : ToolTemplate
    {
        public Eraser(PrimitiveStyle style, Point startPos)
            : base(new PrimitiveStyle(style.StrokeWidth, SystemColors.Control), startPos)
        {
        }
    }
}

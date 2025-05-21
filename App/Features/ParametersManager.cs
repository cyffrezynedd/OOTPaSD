namespace Editor
{
    public static class ParametersManager
    {
        public static (Color, Color) CurrentColor { get; private set; }
        public static int CurrentWidth { get; private set; }
        public static int CurrentAngleCount { get; private set; }
        public static ToolStripMenuItem SelectedItem { get; private set; } = default!;

        public static void SetCurrentParameters((Color, Color) colors, int width, ToolStripMenuItem? item)
        {
            CurrentColor = colors;
            CurrentWidth = width;
            SelectedItem = item ?? throw new ArgumentNullException(nameof(item));
            CurrentAngleCount = -1;
        }

        public static void SetColor((Color, Color) colors) => CurrentColor = colors;
        public static void SetWidth(int width) => CurrentWidth = width;
        public static void SetAngle(int angle) => CurrentAngleCount = angle;
        public static void SetItem(ToolStripMenuItem? item) =>
            SelectedItem = item ?? throw new ArgumentNullException(nameof(item));

        public static (string PrimitiveName, PrimitiveStyle Style) GetInformation()
        {
            string? typeName = SelectedItem.Tag as string;
            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException("No tag for this item");
            }

            PrimitiveStyle style = new PrimitiveStyle(CurrentWidth, CurrentColor.Item1, CurrentColor.Item2);
            return (typeName, style);
        }

        public static int GetAngleCount() => CurrentAngleCount;
    }
}

namespace Editor
{
    public class StrokeManager
    {
        private readonly StrokeWidthsGenerator generator;

        public int CurrentWidth { get; private set; }
        public ToolStripDropDownButton? WidthButton { get; private set; }

        public StrokeManager(StrokeWidthsGenerator strokeGenerator)
        {
            generator = strokeGenerator ?? throw new ArgumentNullException(nameof(strokeGenerator));
        }

        public void Initialize(ToolStripDropDownButton strokeToolButton, int initialWidth = 1)
        {
            if (strokeToolButton == null)
                throw new ArgumentNullException(nameof(strokeToolButton));

            CurrentWidth = initialWidth;
            WidthButton = strokeToolButton;
            generator.CreateStrokeWidthMenu(strokeToolButton, initialWidth, WidthClick);
        }

        private void WidthClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is int width)
            {
                CurrentWidth = width;
                ParametersManager.SetWidth(width);
                if (WidthButton != null)
                {
                    WidthButton.Text = width.ToString();
                }
            }
        }
    }
}

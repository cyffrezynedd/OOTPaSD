namespace Editor
{
    public class StrokeWidthsGenerator
    {
        public void CreateStrokeWidthMenu(ToolStripDropDownButton strokeToolButton, int initialWidth, EventHandler widthClickEvent)
        {
            if (strokeToolButton == null)
                throw new ArgumentNullException(nameof(strokeToolButton));

            strokeToolButton.Text = initialWidth.ToString();

            for (int i = 1; i <= 20; i++)
            {
                var item = new ToolStripMenuItem
                {
                    Text = i.ToString(),
                    Tag = i
                };

                item.Click += widthClickEvent;
                strokeToolButton.DropDownItems.Add(item);
            }
        }
    }
}

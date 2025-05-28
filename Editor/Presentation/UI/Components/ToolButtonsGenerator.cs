namespace Editor
{
    public class ToolButtonsGenerator
    {
        private readonly int reservedPlace = 1;

        public ToolStripDropDownButton? CreateFigureMenu
            (ToolStrip ts,
            Dictionary<string, ToolStripMenuItem>? menuItems,
            EventHandler menuItemClickEvent)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));
            if (menuItems == null || menuItems.Count == 0)
                return null;

            var firstFigure = menuItems.Values.FirstOrDefault();
            if (firstFigure == null)
                throw new InvalidOperationException("Can not create menu without elements");

            var menu = new ToolStripDropDownButton
            {
                Text = firstFigure.Text,
                TextImageRelation = TextImageRelation.TextBeforeImage,
                Image = firstFigure.Image
            };

            foreach (var item in menuItems.Values)
            {
                item.Click += menuItemClickEvent;
                menu.DropDownItems.Add(item);
            }

            if (reservedPlace >= 0 && reservedPlace <= ts.Items.Count)
            {
                ts.Items.Insert(reservedPlace, menu);
            }
            else
            {
                ts.Items.Add(menu);
            }

            return menu;
        }

        public void RemoveFigureMenu(ToolStrip ts, ToolStripMenuItem currentItem, ToolStripDropDownButton? button)
        {
            if (ts == null || currentItem.Tag == null)
                throw new ArgumentNullException("Item does not exist");
            if (button == null)
                return;

            if (!currentItem.Tag.Equals("Figures") && !currentItem.Tag.Equals("Lines"))
            {
                if (ts.Items.Count > reservedPlace)
                {
                    ts.Items.Remove(button);
                }
            }
        }
    }
}

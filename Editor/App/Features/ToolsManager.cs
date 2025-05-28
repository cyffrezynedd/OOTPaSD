namespace Editor
{
    public class ToolsManager
    {
        public ToolStripDropDownButton? FigureMenu { get; private set; }
        public ToolStripMenuItem? CurrentItem { get; private set; }
        public Dictionary<string, ToolStripMenuItem>? CurrentMenu { get; private set; }

        public Dictionary<string, ToolStripMenuItem> FigureMenuItems { get; }
        public Dictionary<string, ToolStripMenuItem> LineMenuItems { get; }

        private readonly ToolButtonsGenerator generator;
        private readonly PolygonAnglesGenerator angleGenerator;
        private readonly AngleManager angleManager;

        private ToolStrip toolStrip = default!;

        public ToolsManager(ToolButtonsGenerator toolButtonsGenerator)
        {
            generator = toolButtonsGenerator ?? throw new ArgumentNullException(nameof(toolButtonsGenerator));
            angleGenerator = new PolygonAnglesGenerator();
            angleManager = new AngleManager(angleGenerator);

            FigureMenuItems = new Dictionary<string, ToolStripMenuItem>
            {
                { "Rectangle", new ToolStripMenuItem { Tag = "Rectangle", Text = "Прямоугольник", Image = Properties.Resources.rectangle } },
                { "Ellipse", new ToolStripMenuItem { Tag = "Ellipse", Text = "Эллипс", Image = Properties.Resources.ellipse } },
                { "Polygon", new ToolStripMenuItem { Tag = "Polygon", Text = "Многоугольник", Image = Properties.Resources.polygon } }
            };

            LineMenuItems = new Dictionary<string, ToolStripMenuItem>
            {
                { "Line", new ToolStripMenuItem { Tag = "Line", Text = "Прямая", Image = Properties.Resources.direct } },
                { "Polyline", new ToolStripMenuItem { Tag = "Polyline", Text = "Ломаная", Image = Properties.Resources.polyline } }
            };
        }

        public void Initialize(ToolStrip ts, ToolStripMenuItem selectedTool)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));
            if (selectedTool == null)
                throw new ArgumentNullException(nameof(selectedTool));

            if (ts.Items.Count > 0)
            {
                ts.Items[0].Image = selectedTool.Image;
            }
            else
            {
                throw new InvalidOperationException("ToolStrip do not have items");
            }

            if (ts.Items[0] is ToolStripSplitButton splitButton)
            {
                foreach (ToolStripItem item in splitButton.DropDownItems)
                {
                    item.Click -= ToolSelect;
                    item.Click += ToolSelect;
                }
            }

            CurrentItem = selectedTool;
            toolStrip = ts;
            ToolChanged(ts, selectedTool);
        }

        private void ToolSelect(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem selectedTool)
            {
                ToolChanged(toolStrip, selectedTool);
                toolStrip.Items[0].Image = selectedTool.Image;
                ParametersManager.SetItem(CurrentItem);
            }
        }

        public void ToolChanged(ToolStrip ts, ToolStripMenuItem selectedTool)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));
            if (selectedTool == null)
                throw new ArgumentNullException(nameof(selectedTool));

            if (selectedTool.Tag is string tag)
            {
                if (tag.Equals("Figures"))
                {
                    CurrentMenu = FigureMenuItems;
                    CurrentItem = FigureMenuItems["Rectangle"];
                }
                else if (tag.Equals("Lines"))
                {
                    CurrentMenu = LineMenuItems;
                    CurrentItem = LineMenuItems["Line"];
                }
                else
                {
                    CurrentMenu = null;
                    CurrentItem = selectedTool;
                }

                generator.RemoveFigureMenu(ts, CurrentItem, FigureMenu);
                FigureMenu = generator.CreateFigureMenu(ts, CurrentMenu, FigureMenuItemClick);

                if (CurrentItem.Tag is string t && t.Equals("Polygon"))
                {
                    angleGenerator.RemovePolygonAnglesMenu(toolStrip);
                    angleManager.Initialize(toolStrip, 5);
                    ParametersManager.SetAngle(5);
                }
                else
                {
                    angleGenerator.RemovePolygonAnglesMenu(toolStrip);
                    ParametersManager.SetAngle(-1);
                }
            }
        }

        private void UpdateMenu(ToolStripMenuItem value)
        {
            generator.TryAddNewMenuItem(FigureMenu, value, FigureMenuItemClick);
        }

        public void TryAddToFigureMenu(ToolStripMenuItem value)
        {
            if (CurrentMenu is not null && CurrentMenu.ContainsKey("Rectangle"))
            {
                UpdateMenu(value);
            }
        }

        private void FigureMenuItemClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                FigureMenuChange(item);
                CurrentItem = item;
                ParametersManager.SetItem(CurrentItem);
            }
        }

        private void FigureMenuChange(ToolStripMenuItem item)
        {
            if (FigureMenu == null)
                return;

            FigureMenu.Text = item.Text;
            FigureMenu.Image = item.Image;

            if (item.Tag is string t && t.Equals("Polygon"))
            {
                angleGenerator.RemovePolygonAnglesMenu(toolStrip);
                angleManager.Initialize(toolStrip, 5);
                ParametersManager.SetAngle(5);
            }
            else
            {
                angleGenerator.RemovePolygonAnglesMenu(toolStrip);
                ParametersManager.SetAngle(-1);
            }
        }
    }
}

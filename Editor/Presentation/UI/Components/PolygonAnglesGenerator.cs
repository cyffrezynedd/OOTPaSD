using System;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    public class PolygonAnglesGenerator
    {
        public ToolStripDropDownButton CreatePolygonAnglesMenu(ToolStrip ts, int initialAngle, EventHandler angleClickEvent)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));

            var label = new ToolStripLabel("Углы: ");
            ts.Items.Add(label);

            var menu = new ToolStripDropDownButton
            {
                AutoSize = false,
                Size = new Size(47, 22),
                Text = initialAngle.ToString(),
                BackColor = SystemColors.GradientActiveCaption,
            };

            for (int i = initialAngle; i <= 20; i++)
            {
                var item = new ToolStripMenuItem
                {
                    Text = i.ToString(),
                    Tag = i
                };
                item.Click += angleClickEvent;
                menu.DropDownItems.Add(item);
            }

            ts.Items.Add(menu);

            return menu;
        }

        public void RemovePolygonAnglesMenu(ToolStrip ts)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));

            if (ts.Items.Count >= 2)
            {
                var lastItem = ts.Items[ts.Items.Count - 1];
                var secondLastItem = ts.Items[ts.Items.Count - 2];

                if (lastItem is ToolStripDropDownButton && secondLastItem is ToolStripLabel label)
                {
                    string labelText = (label.Text ?? string.Empty).Trim();
                    if (labelText.Equals("Углы:", StringComparison.OrdinalIgnoreCase))
                    {
                        ts.Items.RemoveAt(ts.Items.Count - 1);
                        ts.Items.RemoveAt(ts.Items.Count - 1);
                    }
                }
            }
        }
    }
}

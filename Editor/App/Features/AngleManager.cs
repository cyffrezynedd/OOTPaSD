using System;
using System.Drawing;
using System.Windows.Forms;

namespace Editor
{
    public class AngleManager
    {
        private readonly PolygonAnglesGenerator generator;

        public int CurrentAngleCount { get; private set; }
        public ToolStripDropDownButton? AngleButton { get; private set; }

        public AngleManager(PolygonAnglesGenerator angleGenerator)
        {
            generator = angleGenerator
                ?? throw new ArgumentNullException(nameof(angleGenerator));
        }

        public ToolStripDropDownButton Initialize(ToolStrip ts, int initialAngle = 5)
        {
            if (ts == null)
                throw new ArgumentNullException(nameof(ts));

            CurrentAngleCount = initialAngle;
            AngleButton = generator.CreatePolygonAnglesMenu(ts, initialAngle, AngleClick);
            return AngleButton;
        }


        private void AngleClick(object? sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item && item.Tag is int angle)
            {
                CurrentAngleCount = angle;
                ParametersManager.SetAngle(angle);

                if (AngleButton != null)
                {
                    AngleButton.Text = angle.ToString();
                }
            }
        }
    }
}

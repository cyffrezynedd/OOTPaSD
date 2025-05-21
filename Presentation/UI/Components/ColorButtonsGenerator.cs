namespace Editor
{
    public class ColorButtonsGenerator
    {
        private int x;
        private int spaceTaken;
        private readonly int margin;
        private readonly int btnSize;
        private readonly int y;

        public Button Palette { get; private set; } = default!;
        public (Button StrokeIndicator, Button FillIndicator) Indicators { get; private set; }
        public List<Button> ColorButtons { get; private set; } = new List<Button>();
        public int CustomColorPlaced { get; private set; }

        public ColorButtonsGenerator(int someMargin = 3, int someBtnSize = 24, int someY = 0)
        {
            margin = someMargin;
            btnSize = someBtnSize;
            y = someY;
            spaceTaken = 0;
        }

        public void InitializeColorButtons(Panel panel, Color[] defaultColors)
        {
            if (spaceTaken == 0)
                throw new Exception("First create indicators and palette");
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));
            if (defaultColors == null)
                throw new ArgumentNullException(nameof(defaultColors));

            ColorButtons.Clear();
            x = margin;
            CustomColorPlaced = 0;

            foreach (var color in defaultColors)
            {
                var btn = new Button
                {
                    Size = new Size(btnSize, btnSize),
                    Location = new Point(x, y),
                    BackColor = color,
                };

                ColorButtons.Add(btn);
                panel.Controls.Add(btn);
                x += btnSize + 2 * margin;
            }
           
            while (x + btnSize + 2 * margin <= panel.Width - spaceTaken)
            {
                var btn = new Button
                {
                    Size = new Size(btnSize, btnSize),
                    Location = new Point(x, y),
                    BackColor = Color.White,
                };

                ColorButtons.Add(btn);
                panel.Controls.Add(btn);
                x += btnSize + 2 * margin;
                CustomColorPlaced++;
            }
        }

        public void CreateIndicatorsAndPalette(Panel panel)
        {
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));

            int posX = panel.Width;

            posX -= (2 * margin + btnSize);
            Button btnFill = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(btnSize, btnSize),
                BackColor = Color.White,
                Tag = "Fill"
            };
            btnFill.Location = new Point(posX, y);

            posX -= (2 * margin + btnSize);
            Button btnStroke = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(btnSize, btnSize),
                BackColor = Color.Black,
                Tag = "Stroke"
            };
            btnStroke.Location = new Point(posX, y);

            Indicators = (btnStroke, btnFill);
            panel.Controls.Add(btnStroke);
            panel.Controls.Add(btnFill);

            posX -= (2 * margin + btnSize);
            Palette = new Button
            {
                FlatStyle = FlatStyle.Flat,
                Size = new Size(btnSize, btnSize),
                Image = Properties.Resources.palette,
                FlatAppearance = { BorderSize = 0 },
            };
            Palette.Location = new Point(posX, y);
            panel.Controls.Add(Palette);

            spaceTaken = 3 * (2 * margin + btnSize);
        }
    }
}

namespace Editor
{
    public class ColorManager
    {
        public readonly Color[] DefaultColors =
        {
            Color.Black, Color.White, Color.Red, Color.Green,
            Color.Blue, Color.Yellow, Color.Orange, Color.Purple,
            Color.Brown, Color.Gray, Color.Pink,
        };
        public readonly int mainColorCount;
        public (Color Stroke, Color Fill) CurrentColors { get; private set; }

        private readonly ColorButtonsGenerator generator;
        private int customColorIndex; 
        private Button activeIndicator = default!;

        public ColorManager(ColorButtonsGenerator colorButtonsGenerator)
        {
            generator = colorButtonsGenerator ?? throw new ArgumentNullException(nameof(colorButtonsGenerator));
            CurrentColors = (Color.Black, Color.White);
            mainColorCount = DefaultColors.Length;
            customColorIndex = mainColorCount;
        }

        public void Initialize(Panel panel)
        {
            if (panel == null)
                throw new ArgumentNullException(nameof(panel));

            generator.CreateIndicatorsAndPalette(panel);
            generator.InitializeColorButtons(panel, DefaultColors);

            foreach (var btn in generator.ColorButtons)
            {
                btn.Click -= ColorButtonClick;
                btn.Click += ColorButtonClick;
            }

            var (btnStroke, btnFill) = generator.Indicators;
            btnStroke.Click -= IndicatorClick;
            btnStroke.Click += IndicatorClick;
            btnFill.Click -= IndicatorClick;
            btnFill.Click += IndicatorClick;

            generator.Palette.Click -= PaletteClick;
            generator.Palette.Click += PaletteClick;

            activeIndicator = generator.Indicators.StrokeIndicator;
            activeIndicator.Image = Properties.Resources.marker;
        }

        private void ColorButtonClick(object? sender, EventArgs e)
        {
            if (sender is Button selectedButton)
            {
                
                if (activeIndicator == null)
                    activeIndicator = generator.Indicators.StrokeIndicator;


                activeIndicator.BackColor = selectedButton.BackColor;

                if (activeIndicator.Tag?.Equals("Fill") == true)
                {
                    CurrentColors = (CurrentColors.Stroke, selectedButton.BackColor);
                }
                else if (activeIndicator.Tag?.Equals("Stroke") == true)
                {
                    CurrentColors = (selectedButton.BackColor, CurrentColors.Fill);
                }

                ParametersManager.SetColor(CurrentColors);
            }
        }

        private void IndicatorClick(object? sender, EventArgs e)
        {
            if (sender is Button indicator)
            {
                activeIndicator.Image = null;
                activeIndicator = indicator;
                activeIndicator.Image = Properties.Resources.marker;
            }
        }
        private void PaletteClick(object? sender, EventArgs e)
        {
            using (var colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    if (customColorIndex < generator.ColorButtons.Count)
                    {   
                        var btn = generator.ColorButtons[customColorIndex];
                        btn.BackColor = colorDialog.Color;


                        int totalCustomSlots = generator.CustomColorPlaced;
                        if (totalCustomSlots > 0)
                        {
                            customColorIndex = mainColorCount + ((customColorIndex - mainColorCount + 1) % totalCustomSlots);
                        }
                        else
                        {
                            customColorIndex = mainColorCount;
                        }
                    }
                }
            }
        }
    }
}

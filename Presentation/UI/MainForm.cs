namespace Editor
{
    public partial class MainForm : Form
    {
        private const int DEFAULT_WIDTH = 1;
        private ToolsManager toolsManager = new ToolsManager(new ToolButtonsGenerator());
        private StrokeManager strokeManager = new StrokeManager(new StrokeWidthsGenerator());
        private ColorManager colorManager = new ColorManager(new ColorButtonsGenerator());
        private PrimitiveTemplate currentItem = default!;
        private bool isDrawing = false, isDrawingPolyline = false;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            colorManager.Initialize(panelColor);
            toolsManager.Initialize(toolStrip, tsToolPensil);
            strokeManager.Initialize(tsStrokeValue, DEFAULT_WIDTH);
            ParametersManager.SetCurrentParameters(
                colorManager.CurrentColors,
                strokeManager.CurrentWidth,
                tsToolPensil);
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            if (currentItem != null)
            {
                currentItem.Draw(e.Graphics);
            }
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            var (selectedItem, style) = ParametersManager.GetInformation();
            if (!isDrawing && !isDrawingPolyline)
            {
                currentItem = PrimitiveFactory.CreateInstance(selectedItem, style, e.Location);
            }

            if (!selectedItem.Equals("Polyline"))
            {
                isDrawing = true;
                pictureBox.Invalidate();
            }
            else
            {
                isDrawingPolyline = true;
            }
        }


        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentItem != null)
            {
                currentItem.Update(e.Location);
            }
            pictureBox.Invalidate();
        }

        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentItem != null && !isDrawingPolyline)
            {
                isDrawing = false;
                currentItem.Update(e.Location);
            }
            else if (isDrawingPolyline && currentItem is Polyline polyline)
            {
                polyline.Add(e.Location);
            }
            pictureBox.Invalidate();
        }

        private void PictureBoxMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDrawingPolyline && currentItem is Polyline)
            {
                isDrawingPolyline = false;
            }
        }
    }
}

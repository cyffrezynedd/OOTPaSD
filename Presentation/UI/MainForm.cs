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
        private string primitiveName = default!;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            colorManager.Initialize(panelColor);
            toolsManager.Initialize(toolStrip, tsToolPencil);
            strokeManager.Initialize(tsStrokeValue, DEFAULT_WIDTH);
            ParametersManager.SetCurrentParameters(
                colorManager.CurrentColors,
                strokeManager.CurrentWidth,
                tsToolPencil);
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
            primitiveName = ParametersManager.CollectInformation(e.Location);
            if (!isDrawing && !isDrawingPolyline)
            {
                currentItem = PrimitiveFactory.CreateInstance(primitiveName);
            }

            if (!primitiveName.Equals("Polyline"))
            {
                isDrawing = true;
            }
            else
            {
                isDrawingPolyline = true;
            }
            pictureBox.Invalidate();
        }


        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentItem != null)
            {
                currentItem.Update(e.Location);
            }

            if (isDrawingPolyline && currentItem is Polyline polyline)
            {
                polyline.UpdatePreview(e.Location);
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

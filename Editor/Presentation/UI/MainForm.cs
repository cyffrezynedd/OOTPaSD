namespace Editor
{
    public partial class MainForm : Form
    {
        private const int DEFAULT_WIDTH = 1;
        private FileManagment fileManagment = new FileManagment();
        private ToolsManager toolsManager = new ToolsManager(new ToolButtonsGenerator());
        private StrokeManager strokeManager = new StrokeManager(new StrokeWidthsGenerator());
        private ColorManager colorManager = new ColorManager(new ColorButtonsGenerator());
        private History history = new History(new PrimitiveCollection());
        private PrimitiveTemplate? currentItem;
        private bool isDrawing = false, isDrawingPolyline = false;
        private string primitiveName = default!;

        public MainForm()
        {
            InitializeComponent();
            CenterToScreen();
            KeyPreview = true;
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
            history.Initialize(pictureBox.Width, pictureBox.Height);
        }

        private void MainFormKeyDown(object sender, KeyEventArgs e)
        {
            history.ProcessKeyDown(e);
            pictureBox.Invalidate();
        }

        private void BtnUndoClick(object sender, EventArgs e)
        {
            history.Undo();
            pictureBox.Invalidate();
        }

        private void BtnRedoClick(object sender, EventArgs e)
        {
            history.Redo();
            pictureBox.Invalidate();
        }

        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            history.Draw(e.Graphics);

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

            if (currentItem is not Polyline)
            {
                isDrawing = true;
            }
            else
            {
                isDrawingPolyline = true;
            }
        }

        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (currentItem != null)
            {
                if (isDrawing)
                {
                    currentItem.Update(e.Location);
                }

                if (isDrawingPolyline && currentItem is Polyline polyline)
                {
                    polyline.UpdatePreview(e.Location);
                }

                pictureBox.Invalidate();
            }

        }

        private void PictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing && currentItem != null && !isDrawingPolyline)
            {
                isDrawing = false;
                currentItem.Update(e.Location);
                history.Add(currentItem);
                currentItem = null;
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
                history.Add(currentItem);
            }
        }

        private void FileOpenClick(object sender, EventArgs e)
        {
            fileManagment.OpenFile(history);
            pictureBox.Invalidate();
        }

        private void FileSaveClick(object sender, EventArgs e)
        {
            fileManagment.SaveFile(history);
        }
    }
}

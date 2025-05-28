namespace Editor
{
    public class History
    {
        private PrimitiveCollection collection;
        private Stack<PrimitiveTemplate> undo;
        private Stack<PrimitiveTemplate> redo;
        private Bitmap savedImage = default!;

        public History(PrimitiveCollection newCollection)
        {
            collection = newCollection;
            undo= new Stack<PrimitiveTemplate>();
            redo = new Stack<PrimitiveTemplate>();
        }

        public void Initialize(int width, int height)
        {
            savedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(savedImage))
            {
                g.Clear(SystemColors.Control);
            }
        }

        public void Add(PrimitiveTemplate primitive)
        {
            collection.Add(primitive);

            undo.Push(collection.GetLast());
            redo.Clear();
            using (Graphics g = Graphics.FromImage(savedImage))
            {
                primitive.Draw(g);
            }
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(savedImage, 0, 0);
        }

        public void Undo()
        {
            if (undo.Count == 0)
            {
                return;
            }

            var primitive = undo.Pop();
            redo.Push(primitive);
            collection.RemoveLast();
            using (Graphics g = Graphics.FromImage(savedImage))
            {
                g.Clear(SystemColors.Control);
                collection.Draw(g);
            }
        }

        public void Redo()
        {
            if (redo.Count == 0)
            {
                return;
            }

            var primitive = redo.Pop();
            undo.Push(primitive);
            collection.Add(primitive);
            using (Graphics g = Graphics.FromImage(savedImage))
            {
                primitive.Draw(g);
            }
        }

        public void Clear()
        {
            undo.Clear();
            redo.Clear();
            collection.Clear();

            using (Graphics g = Graphics.FromImage(savedImage))
            {
                g.Clear(SystemColors.Control);
            }
        }

        public void ProcessKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo();
                e.Handled = true;
            }
        }

        public PrimitiveCollection GetCollection() => collection;
    }
}

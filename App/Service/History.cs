namespace Editor
{
    public class History
    {
        private PrimitiveCollection collection;
        private Stack<PrimitiveTemplate> undo;
        private Stack<PrimitiveTemplate> redo;


        public History(PrimitiveCollection newCollection)
        {
            collection = newCollection;
            undo = new Stack<PrimitiveTemplate>();
            redo = new Stack<PrimitiveTemplate>();
        }

        public void Add(PrimitiveTemplate primitive)
        {
            collection.Add(primitive);
            undo.Push(collection.GetLast());
            
            if (redo.Count > 0)
            {
                redo.Clear();
            }
        }

        public void Draw(Graphics graphics)
        {
            collection.Draw(graphics);
        }

        public void Undo()
        {
            if (undo.Count == 0)
            {
                return;
            }

            redo.Push(undo.Pop());
            collection.RemoveLast();
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
        }

        public void Clear()
        {
            undo.Clear();
            redo.Clear();
            collection.Clear();
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
    }
}

namespace Editor
{
    public class FileManagment
    {
        public void OpenFile(History history)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    var result = Deserialization.LoadFromFile(filePath);
                    history.Clear();
                    foreach (var primitive in result)
                    {
                        history.Add(primitive);
                    }
                }
            }
        }

        public void SaveFile(History history)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    Serialization.SaveToFile(history.GetCollection(), filePath);
                }
            }
        }
    }
}

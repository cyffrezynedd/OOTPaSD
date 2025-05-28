using Newtonsoft.Json;

namespace Editor
{
    public class FileManagment
    {
        public void OpenFile(History history)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "JSON Files (*.json)|*.json";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        var result = Deserialization.LoadFromFile(filePath);
                        history.Clear();
                        foreach (var primitive in result)
                        {
                            history.Add(primitive);
                        }
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show(ex.Message, "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void SaveFile(History history)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON Files (*.json)|*.json";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = saveFileDialog.FileName;
                        Serialization.SaveToFile(history.GetCollection(), filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

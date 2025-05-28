namespace Editor
{
    public class PluginManagment
    {
        public void OpenPlugin(ToolsManager toolsManager)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "DLL Files (*.dll)|*.dll";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string filePath = openFileDialog.FileName;
                        PrimitivePlugin.LoadPrimitivePlugin(filePath);

                        foreach (var pair in PluginHelper.MenuItems)
                        {
                            toolsManager.FigureMenuItems.TryAdd(pair.Key, pair.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while opening plugin: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

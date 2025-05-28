using System.Reflection;
using System.Security.Policy;

namespace Editor
{
    public static class PrimitivePlugin
    {
        public static void LoadPrimitivePlugin(string filepath)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(filepath);
            AssemblyName pluginAssemblyName = pluginAssembly.GetName() ??
                throw new ArgumentNullException($"{nameof(pluginAssembly)} does not have name");

            if (!PluginHelper.TryLoad(pluginAssemblyName))
            {
                MessageBox.Show("This plugin is already loaded", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var type = pluginAssembly.GetTypes()
                .Where((Type t) => !t.IsAbstract && typeof(PrimitiveTemplate).IsAssignableFrom(t)).First();

            string[] resourceNames = pluginAssembly.GetManifestResourceNames();
            string? imageResourceName = resourceNames
                .FirstOrDefault(rn => rn.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

            if (imageResourceName != null)
            {
                using (Stream? stream = pluginAssembly.GetManifestResourceStream(imageResourceName))
                {
                    if (stream != null)
                    {
                        Image image = Image.FromStream(stream);
                        PluginHelper.AddImage(type.Name, image);
                    }
                }
            }

            PrimitiveFactory.UpdateFactory(type);
            PluginHelper.AddMenuItem(type.Name);
        }
    }

    public static class PluginHelper
    {
        public static Dictionary<string, Image> Images { get; } = new Dictionary<string, Image>();

        public static Dictionary<string, ToolStripMenuItem> MenuItems { get; } = new Dictionary<string, ToolStripMenuItem>();

        public static Dictionary<string, AssemblyName> LoadedPlugins { get; } = new Dictionary<string, AssemblyName>();

        public static void AddImage(string key, Image image)
        {
            Images[key] = image;
        }

        public static void AddMenuItem(string name)
        {
            MenuItems.Add(name, new ToolStripMenuItem
            {
                Tag = name,
                Text = name,
                Image = Images.ContainsKey(name) ? Images[name] : Properties.Resources.figures
            });
        }

        public static bool TryLoad(AssemblyName pluginAssemblyName)
        {
            if (pluginAssemblyName == null || pluginAssemblyName.Name == null)
            {
                return false;
            }

            return LoadedPlugins.TryAdd(pluginAssemblyName.Name, pluginAssemblyName);
        }

    }
}

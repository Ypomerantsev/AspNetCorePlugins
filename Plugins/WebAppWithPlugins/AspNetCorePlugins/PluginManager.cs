using System.Collections.Generic;

namespace AspNetCorePlugins
{
    public class PluginManager
    {
        List<Plugin> _plugins { get; set; } = new List<Plugin>();

        public void Add(Plugin plugin)
        {
            _plugins.Add(plugin);
        }

        public IEnumerable<Plugin> Plugins
        {
            get
            {
                return _plugins.ToArray();
            }
        }

    }
}

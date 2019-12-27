using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace AspNetCorePlugins
{
    public static class PluginServiceExtensions
    {

        public static PluginManager AddPlugins(this IServiceCollection services, string pluginFolder)
        {
            var pluginManager = new PluginManager();
            services.AddSingleton(pluginManager);

            foreach (var folder in Directory.GetDirectories(pluginFolder))
            {
                // TODO: for now, I assume dll name equals to folder name
                var assemblyFileName = $"{Path.GetFileName(folder)}.dll";
                var assemblyPath = Path.Combine(folder, assemblyFileName);
                var assembly = loadAssembly(assemblyPath);
                var plugin = configureServices(assembly, services);
                pluginManager.Add(plugin);
            }
            return pluginManager;
        }

        static Assembly loadAssembly(string assemblyPath)
        {
            PluginLoadContext loadContext = new PluginLoadContext(assemblyPath);
            var assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(assemblyPath)));

            var dllPath = Path.GetDirectoryName(assemblyPath);

            var assemblyFiles = Directory.GetFiles(dllPath, "*.dll", SearchOption.AllDirectories);
            foreach (var assemblyFile in assemblyFiles)
            {
                Assembly.LoadFile(assemblyFile);
                if (!AssemblyLoadContext.Default.Assemblies.Any(a =>
                    Path.GetFileName(a.Location).ToLower() == Path.GetFileName(assemblyFile).ToLower()))
                {
                    AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFile);
                }
            }

            return AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
        }

        static Plugin configureServices(Assembly assembly, IServiceCollection services)
        {
            Plugin plugin = null;
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPluginStartup).IsAssignableFrom(type))
                {
                    IPluginStartup pluginStartup = Activator.CreateInstance(type) as IPluginStartup;
                    if (pluginStartup != null)
                    {
                        pluginStartup.ConfigureServices(services);
                        plugin = new Plugin
                        {
                            AreaName = pluginStartup.AreaName,
                            Assembly = assembly
                        };
                    }
                }
            }
            return plugin;
        }

    }
}

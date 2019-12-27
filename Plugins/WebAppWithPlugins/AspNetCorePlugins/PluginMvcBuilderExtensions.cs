using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCorePlugins
{
    public static class PluginMvcBuilderExtensions
    {

        public static IMvcBuilder AddPluginApplicationParts(
            this IMvcBuilder mvcBuilder,
            PluginManager pluginManager)
        {
            foreach (var plugin in pluginManager.Plugins)
            {
                addApplicationPart(mvcBuilder.PartManager, plugin);
            }
            return mvcBuilder;
        }

        static void addApplicationPart(ApplicationPartManager applicationPartManager, Plugin plugin)
        {
            var partFactory = ApplicationPartFactory.GetApplicationPartFactory(plugin.Assembly);

            foreach (var part in partFactory.GetApplicationParts(plugin.Assembly))
            {
                applicationPartManager.ApplicationParts.Add(part);
            }

            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(plugin.Assembly, throwOnError: true);
            foreach (var assembly in relatedAssemblies)
            {
                partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
                foreach (var part in partFactory.GetApplicationParts(assembly))
                {
                    applicationPartManager.ApplicationParts.Add(part);
                }
            }
        }

    }
}

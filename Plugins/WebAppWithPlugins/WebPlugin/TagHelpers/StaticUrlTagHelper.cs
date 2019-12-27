using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;

namespace WebPlugin.TagHelpers
{
    [HtmlTargetElement(Attributes = "rcl-static-url")]
    public class StaticUrlTagHelper : TagHelper
    {

        IActionContextAccessor _actionContextAccesor;
        IUrlHelper _urlHelper;

        public string RclStaticUrl { get; set; }

        public StaticUrlTagHelper(IActionContextAccessor actionContextAccesor, IUrlHelperFactory urlHelperFactory)
        {
            _actionContextAccesor = actionContextAccesor;
            _urlHelper = urlHelperFactory.GetUrlHelper(_actionContextAccesor.ActionContext);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var attributeName = getAttributeName(context.TagName);
            if (RclStaticUrl.StartsWith("/")) RclStaticUrl = RclStaticUrl.Substring(1);
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var staticFileBaseUrl = $"~/_content/{assemblyName}/" + RclStaticUrl;
            var url = _urlHelper.Content(staticFileBaseUrl);
            output.Attributes.SetAttribute(attributeName, url);
        }

        string getAttributeName(string tagName)
        {
            switch (tagName)
            {
                case "link":
                    return "href";
                case "script":
                    return "src";
                case "a":
                    return "href";
                case "img":
                    return "src";
                default:
                    return "href";
            }
        }

    }
}

using ExternalLibrary;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPlugin.Areas.Plugin.Pages
{
    public class IndexModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {
            var extClass = new ExternalClass();
            Message = extClass.GetMessage("1");
        }

    }
}

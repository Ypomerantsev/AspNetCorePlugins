using ExternalLibrary;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPlugin2.Areas.Plugin2.Pages
{
    public class IndexModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {
            var extClass = new ExternalClass();
            Message = extClass.GetMessage("2");
        }
    }
}

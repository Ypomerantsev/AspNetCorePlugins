using EFCorePlugin.Data;
using EFCorePlugin.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace EFCorePlugin.Areas.EFCore.Pages
{
    public class IndexModel : PageModel
    {

        DbEFCorePlugin _db;

        public Customer[] Customers { get; set; }

        public IndexModel(DbEFCorePlugin db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Customers = _db.Customers.ToArray();
        }

    }
}

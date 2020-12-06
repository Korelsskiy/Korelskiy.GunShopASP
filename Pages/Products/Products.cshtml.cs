using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korelskiy.ModelsForGunShop;
using Korelskiy.GunShopASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class ProductsModel : PageModel
    {
        private readonly IProductRepository _db;

        public IEnumerable<Product> Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public ProductsModel(IProductRepository db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Products = _db.Search(SearchTerm);
        }
    }
}

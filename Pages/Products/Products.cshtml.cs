using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShopASP.Models;
using Korelskiy.GunShopASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class ProductsModel : PageModel
    {
        private readonly IProductRepository _db;
        public IEnumerable<Product> Products { get; set; }
        public ProductsModel(IProductRepository db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Products = _db.GetAllProducts();
        }
    }
}

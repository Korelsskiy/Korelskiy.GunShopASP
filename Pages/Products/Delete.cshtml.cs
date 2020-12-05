using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public DeleteModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [BindProperty]
        public Product Product { get; set; }
        public IActionResult OnGet(int id)
        {
            Product = _productRepository.GetProduct(id);

            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            Product deletedProduct = _productRepository.Delete(Product.Id);

            if (deletedProduct == null)
            {
                return RedirectToPage("/NotFound");
            }

            return RedirectToPage("Products");
        }
    }
}

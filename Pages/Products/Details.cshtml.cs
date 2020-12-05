using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class DeatailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public DeatailsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

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
    }
}

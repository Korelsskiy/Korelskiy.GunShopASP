using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DeleteModel(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
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

            if (deletedProduct.PhotoPath != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", deletedProduct.PhotoPath);

                if (deletedProduct.PhotoPath != "noimage.png")
                {
                    System.IO.File.Delete(filePath);
                }


            }

            if (deletedProduct == null)
            {
                return RedirectToPage("/NotFound");
            }

            return RedirectToPage("Products");
        }
    }
}

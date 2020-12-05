using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korelskiy.ModelsForGunShop;
using Korelskiy.GunShopASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public Product Product { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }
        public string Message { get; set; }
        public EditModel(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int id)
        {
            Product = _productRepository.GetProduct(id);

            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();  
        }

        public IActionResult OnPost(Product product)
        {
            if (Photo != null)
            {
                if (product.PhotoPath != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", product.PhotoPath);
                    System.IO.File.Delete(filePath);
                }

                product.PhotoPath = ProcessUploadedFile();
            }

            Product = _productRepository.Update(product);

            TempData["SuccessMessage"] = $"{Product.Title} успешно обновлен";

            return RedirectToPage("Products");
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Вы включили оповещение на почту";
            }
            else
            {
                Message = "Вы выключили оповещение по почте";
            }

            Product = _productRepository.GetProduct(id);
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create)) // автоматически вызывает Dispose() и высвобождает ресурсы
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}

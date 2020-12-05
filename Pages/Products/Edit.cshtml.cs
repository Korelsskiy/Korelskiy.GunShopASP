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
        [BindProperty]
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
        public IActionResult OnGet(int? id) // при обращении к странице
        {
            if (id.HasValue)
            {
                Product = _productRepository.GetProduct(id.Value);
            }
            else
            {
                Product = new Product();
            }
           

            if (Product == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();  
        }


        public IActionResult OnPost() // при отправке формы со страницы
        {
            if (ModelState.IsValid)
            {

                if (Photo != null)
                {
                    if (Product.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Product.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    Product.PhotoPath = ProcessUploadedFile();
                }

                if (Product.Id > 0)
                {
                    Product = _productRepository.Update(Product);

                    TempData["SuccessMessage"] = $"{Product.Title} успешно обновлен";
                }
                else
                {
                    Product = _productRepository.Add(Product);

                    TempData["SuccessMessage"] = $"{Product.Title} успешно добавлен";
                }
               

                return RedirectToPage("Products");
            }
            
            return Page();
           
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "¬ы включили оповещение на почту";
            }
            else
            {
                Message = "¬ы выключили оповещение по почте";
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

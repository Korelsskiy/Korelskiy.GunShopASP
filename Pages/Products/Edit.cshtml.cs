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
        public IActionResult OnGet(int? id) // ��� ��������� � ��������
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


        public IActionResult OnPost() // ��� �������� ����� �� ��������
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

                    TempData["SuccessMessage"] = $"{Product.Title} ������� ��������";
                }
                else
                {
                    Product = _productRepository.Add(Product);

                    TempData["SuccessMessage"] = $"{Product.Title} ������� ��������";
                }
               

                return RedirectToPage("Products");
            }
            
            return Page();
           
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "�� �������� ���������� �� �����";
            }
            else
            {
                Message = "�� ��������� ���������� �� �����";
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

                using (var fs = new FileStream(filePath, FileMode.Create)) // ������������� �������� Dispose() � ������������ �������
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}

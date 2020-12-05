using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Korelskiy.GunShopASP.Services;
using GunShopASP.Models;

namespace Korelskiy.GunShopASP.Pages.Products
{
    public class DeatailsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public DeatailsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product Product { get; private set; }

        public void OnGet(int id)
        {
            Product = _productRepository.GetProduct(id);
        }
    }
}

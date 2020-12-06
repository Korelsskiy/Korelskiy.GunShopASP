using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korelskiy.GunShopASP.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent // используется архитектура MVC
    {
        private readonly IProductRepository _productRepository; // зависимость бд

        public HeadCountViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke(Cat? category = null)
        {
            var result = _productRepository.ProductCoundByCategory(category);

            return View(result);
        }
    }
}

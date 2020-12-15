using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Korelskiy.GunShopASP.Pages.Accounts
{
    public class AutoristaionModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        [BindProperty]
        public User User { get; set; }
        public AutoristaionModel(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public void OnGet()
        {
            User = new User();
        }

        public IActionResult OnPost()
        {
            User u = _userRepository.FindUser(User.Name, User.Password);

            if (u != null)
            {

                TempData["SuccessAutentification"] = $"{u.Name} успешно авторизован(а)";

                SiteStatus.ChangeUser(u);

                return RedirectToPage("/Index");
            }
            else
            {
                TempData["Autentification"] = $"¬ведены неверные данные, попробуйте еще раз";
            }

            return Page();
        }
    }
}

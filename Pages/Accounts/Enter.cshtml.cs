using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Korelskiy.GunShopASP.Services;
using Korelskiy.ModelsForGunShop;

namespace Korelskiy.GunShopASP.Pages.Accounts
{
    public class EnterModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        [BindProperty]
        public User User { get; set; }

        public EnterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void OnGet()
        {
            User = new User();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _userRepository.Add(User);

                TempData["SuccessRegistration"] = $"{User.Name} успешно зарегистрирован(а) (проверить в базе данных)";

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}

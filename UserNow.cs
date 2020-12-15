using Korelskiy.ModelsForGunShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Korelskiy.GunShopASP
{
    public static class SiteStatus
    {
        private static User _user = new User() { Name = "Не авторизован"}; 

        public static string GetUserName()
        {
            return _user.Name;
        }

        public static void ChangeUser(User user)
        {
            _user = user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class UserModel
    {
        public Int64 UserID { get; set; }
        public string UserUID { get; set; }
        public string UserRole { get; set; }
        public string AccessToken { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string IsUserLoggedIn { get; set; }
    }
    public class MobileIpAddress
    {
        public string UserIp { get; set; }
    }
    public class LoginModel: MobileIpAddress
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}

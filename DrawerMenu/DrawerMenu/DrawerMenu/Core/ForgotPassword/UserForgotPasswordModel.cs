using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class UserForgotPasswordModel
    {
        
        public string email { get; set; }
        public string reset_password_token { get; set; }
        public string reset_password_sent_at { get; set; }
    }
  
}

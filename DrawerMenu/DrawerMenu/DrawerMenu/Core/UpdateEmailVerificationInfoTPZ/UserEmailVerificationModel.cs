using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class UserEmailVerificationModel
    {
        public string uuid { get; set; }
        public string unconfirmed_email { get; set; }
        public string confirmation_token { get; set; }
    }
  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class UserReqRegisterationModel
    {
        public string uuid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string encrypted_password { get; set; }
        public string gender { get; set; }
        public string question { get; set; }
        public string TermsConditions { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string current_sign_in_ip { get; set; }
        public string last_sign_in_ip { get; set; }
        public string opt { get; set; }
    }
  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class UserProfileModel
    {
        public string uuid { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string country { get; set; }
        public string phone_number { get; set; }
        public string encrypted_password { get; set; }
        public string question { get; set; }
        public string is_active { get; set; }
        public Boolean UserRolemember { get; set; }
        public Boolean UserRoleAdmin { get; set; }
        public Boolean UserRolewarrior { get; set; }
        public Boolean UserRoleModerator { get; set; }
        public DateTime updated_at { get; set; }

      
        
    }
  
}

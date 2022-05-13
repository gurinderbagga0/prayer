using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class UserGetRoleDetailModel
    {
       
        public string Role { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UserGetRoleModel
    {
        public string UserId { get; set; }
      
    }

}

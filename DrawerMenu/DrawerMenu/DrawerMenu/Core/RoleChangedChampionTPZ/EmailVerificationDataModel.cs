using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class EmailVerificationDataModel
    {
    
        public string uuid { get; set; }
        public string email { get; set; }

        public string confirmation_token { get; set; }

        public string confirmation_sent_at { get; set; }
        
        public Int32 ChampionConfirmEmailID { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

   
}

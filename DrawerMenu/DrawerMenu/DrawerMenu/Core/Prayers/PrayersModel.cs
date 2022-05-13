using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
    public  class PrayersModel
    {
        public string RowNumber { get; set; }
        public string User_id { get; set; }
        public string user_name { get; set; }
        public int id { get; set; }
        public int isdeleted { get; set; }
        public string text { get; set; }
        public string title { get; set; }
        public string updated_at { get; set; }
        public int has_comments { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string created_at { get; set; }
        public string PrayerRequestId { get; set; }
        public string TotalPrayerPosted { get; set; }
        public string RequiredIDs { get; set; }
       
        public string Updateddate { get; set; }
        public Boolean IsDeleteButtonVisible { get; set; }
        public Boolean IsPostPrayerButtonVisible { get; set; }
        public Boolean IsOffensiveButtonVisible { get; set; }
        public Boolean IsWaitingApproval { get; set; }
        public Boolean IsApprovePrayerButtonVisible { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class UserGetSinglePrayerDetailModel
    {
        public Int64 Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string user_name { get; set; }
        public string OtherInformation { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string user_id { get; set; }
        
    }

    public class UserGetSinglePrayerModel
    {
        public string RequestedPostedUserId { get; set; }
        public string PrayerRequestID { get; set; }
        public string RequestPostedUserName { get; set; }
    }

}

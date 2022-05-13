using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
    public class PrayersCommentsModel
    {
        public string id { get; set; }
        public string uuid { get; set; }
        public string prayer_request_id { get; set; }
        public string text { get; set; }
        public string user_id { get; set; }
        public string response_type { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string warrior { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_name { get; set; }
        public string OtherInformation { get; set; }
        public string RequiredIDs { get; set; }
        public Boolean IsDeleteButtonVisible { get; set; }
        public Boolean IsGreenIconVisible { get; set; }

        public string OtherInfoforSpam { get; set; }
        public Boolean IsOffensiveButtonVisible { get; set; }
        public Int32 IsCommentWaitingApproval { get; set; }
        public Boolean IsWaitingApproval { get; set; }

        public Boolean IsApproveCommentButtonVisible { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class PostNotificationDataModel
    {
        public Int32 TimeId { get; set; }
        public string NotificationTime { get; set; }
        public string DayName { get; set; }
        public string UserType { get; set; }
        public string UuID { get; set; }
        public int IsReminderEnable { get; set; }

        public int IsPrayerUpdateNotificationEnable { get; set; }
        
        public bool Status { get; set; }
        public string Message { get; set; }
       
    }
    public class AdminBlockWordsViewClassModel
    {
     
        public Int32 Id { get; set; }
        public string strWord { get; set; }
        public int lngStatus { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }


}

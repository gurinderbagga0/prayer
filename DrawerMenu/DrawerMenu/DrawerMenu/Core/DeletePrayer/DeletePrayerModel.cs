using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class ApprovePrayerModel
    {

        public string PrayerRequestId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class DeletePrayerModel
    {

        public string PrayerRequestId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class DeleteCommentModel
    {

        public string CommentId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class ApproveCommentModel
    {

        public string CommentId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    //public class MobileIpAddress
    //{
    //    public string UserIp { get; set; }
    //}
    //public class LoginModel: MobileIpAddress
    //{
    //    public string UserName { get; set; }
    //    public string Password { get; set; }

    //}
}

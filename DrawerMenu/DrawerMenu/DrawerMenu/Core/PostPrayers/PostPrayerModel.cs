using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class PostPrayerModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }

        public string UserUId { get; set; }
        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UpdatePrayerModel
    {

        public string Title { get; set; }
        public string Description { get; set; }

        public string RequestPostedUserId { get; set; }
        public string PrayerRequestId { get; set; }

        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UpdatePrayerCommentModel
    {

        public Int64 CommentId { get; set; }
        public string CommentText { get; set; }
        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class InsertPrayerCommentModel
    {

        public string PrayerRequestId { get; set; }
        public string CommentText { get; set; }
        public string LoginUserId { get; set; }
        public string userRoleWarior { get; set; }
        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
    public class InsertUserMessageModel
    {
        public string LoginUserId { get; set; }
        public string UserMessageText { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        // public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        //  public string Message { get; set; }
    }

        
    public class InsertOffensiveCommentModel
    {
        public string PrayerRequestId { get; set; }
        public string PrayerCommentId { get; set; }

        public string PrayerCommentText { get; set; }
        public string LoginUserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        // public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        //  public string Message { get; set; }
    }
    public class InsertOffensivePrayerModel
    {
        public string PrayerRequestId { get; set; }
        public string LoginUserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmailAddress { get; set; }
        // public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        //  public string Message { get; set; }
    }

    public class InsertRestrictWordModel
    {
        public int Id { get; set; }
        public string strWord { get; set; }
        public string lngStatus { get; set; }
        public string Createdby { get; set; }
        public bool Status { get; set; }

        public string Message { get; set; }
        //  public string Message { get; set; }
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

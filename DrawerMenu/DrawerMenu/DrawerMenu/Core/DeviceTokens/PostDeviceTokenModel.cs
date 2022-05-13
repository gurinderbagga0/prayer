using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
   public class PostDeviceTokenModel
    {
    
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceSerialNo { get; set; }
        public string UuID { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public int IsNotificationEnable { get; set; }
    }
    public class UpdateDeviceTokenModel
    {
        public string DeviceSerialNo { get; set; }
        public string DeviceId { get; set; }
        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class UpdateDeviceDetailsModel
    {
        public string DeviceSerialNo { get; set; }
        public string UuID { get; set; }
        public Int32 StatusId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

}

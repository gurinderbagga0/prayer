using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
    interface IPrayersComments
    {
        Task<List<PrayersCommentsModel>> getAllPrayersComments(string PrayerRequestID, string MyRequestsUserid,Boolean IsDeleteButtonVisible, Int32 UserRoleAdmin,Boolean IsApproveCpmmentButtonVisible);
    }
}

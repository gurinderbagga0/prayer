using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
    interface IPrayers
    {
        Task<List<PrayersModel>> getAllPrayers(int pageIndex, string MyRequestsUserid, string WaitingForPrayers,Boolean IsPostPrayerButtonVisible, Boolean IsDeleteButtonVisible, string LoginUserId, Int32 UserRoleAdmin);
    }
}

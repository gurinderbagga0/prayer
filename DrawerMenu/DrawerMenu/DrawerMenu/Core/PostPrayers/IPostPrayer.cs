using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    interface IPostPrayer
    {
        Task<PostPrayerModel> PostNewPrayer(PostPrayerModel item);
        Task<UpdatePrayerModel> UpdatePrayer(UpdatePrayerModel item);
        Task<UpdatePrayerCommentModel> UpdatePrayerComment(UpdatePrayerCommentModel item);
        Task<InsertPrayerCommentModel> InsertPrayerComment(InsertPrayerCommentModel item);
        
    }
}

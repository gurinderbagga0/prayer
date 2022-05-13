using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    interface IDeletePrayer
    {
        Task<DeletePrayerModel> DeletePrayerRequest(DeletePrayerModel item);
        Task<DeleteCommentModel> DeleteComment(DeleteCommentModel item);
       
        
    }

    interface IApprovePrayer
    {
        Task<ApprovePrayerModel> ApprovePrayerRequest(ApprovePrayerModel item);
    }

    interface IApproveComment
    {
        Task<ApproveCommentModel> ApproveComment(ApproveCommentModel item);
    }
}

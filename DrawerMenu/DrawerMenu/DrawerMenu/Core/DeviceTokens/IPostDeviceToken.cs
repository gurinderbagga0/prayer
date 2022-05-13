using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    interface IPostDeviceToken
    {
        Task<PostDeviceTokenModel> PostDeviceDetails(PostDeviceTokenModel item);
        Task<UpdateDeviceTokenModel> UpdateDeviceDetails(UpdateDeviceTokenModel item);

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    interface IEmailVerificationData
    {
        Task<EmailVerificationDataModel> InsertEmailVerificationDataRoleChangedChampionAdminTPZ(EmailVerificationDataModel item);
       
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
    interface IAllUsers
    {
        Task<List<AllUsersModel>> getAllUsers();
    }
}

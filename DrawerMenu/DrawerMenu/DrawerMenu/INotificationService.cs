using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu
{
    public interface INotificationService
    {
        string Token { get; }
        void Register();
    }
}

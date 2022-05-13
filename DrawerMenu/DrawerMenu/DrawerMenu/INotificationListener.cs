using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu
{
    public interface INotificationListener
    {
        void OnRegister(string deviceToken);
        void OnMessage(JObject values);
    }

}

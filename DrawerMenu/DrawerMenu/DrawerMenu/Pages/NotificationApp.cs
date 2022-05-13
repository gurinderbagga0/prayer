using DrawerMenu.Pages;
using PushNotification.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DrawerMenu
{
    class NotificationApp : Application
    {
        public NotificationApp(string x, string y)
        {

            // The root page of your application
            MainPage = new NavigationPage(new MenuPageMemberRequest(x, y))
            {

                BarBackgroundColor = Color.FromHex("#D0D0D0"),
                BarTextColor = Color.White

            };


        }
      
    }
}

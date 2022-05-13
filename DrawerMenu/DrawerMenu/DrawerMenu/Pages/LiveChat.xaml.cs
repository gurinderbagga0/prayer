using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawerMenu.Pages
{

    public partial class LiveChat : ContentPage
    {
       
        // string URL = "https://secure.livechatinc.com/licence/6982601/open_chat.cgi?groups=35";
        string URL = "https://lc.chat/now/8827886/";
        // string URL = "https://www.theprayerzone.com";
        public LiveChat()
        {
            InitializeComponent();
            Browser.Source = URL;
           // Device.OpenUri(new Uri("https://lc.chat/now/8827886/"));
        }

    }
}

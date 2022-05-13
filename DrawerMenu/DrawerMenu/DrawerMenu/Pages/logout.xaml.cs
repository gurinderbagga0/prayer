using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class logout : ContentPage
    {
        public logout()
        {
            InitializeComponent();
            Application.Current.Properties["AccessToken"] = null;
            Application.Current.Properties["UserUID"] = null;
            Application.Current.Properties["UserRole"] = null;
            Application.Current.Properties["UserFirstName"] = null;
            Application.Current.Properties["UserLastName"] = null;
            Application.Current.Properties["UserEmail"] = null;
            Navigation.RemovePage(new MenuPage());
            Navigation.PushModalAsync(new MainPage("",""));
        }
    }
}

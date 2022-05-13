using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class SignUpChampionInfo : ContentPage
    {

        public SignUpChampionInfo()
        {
            InitializeComponent();
            //   stackHeading.Margin = new Thickness(10, 20);

            // Browser.Source = URL;
        }
        private void backClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        async void SignUpRequester_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpChampion());
        }
        async void LearnMore_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ViewMore());
        }

    }
}

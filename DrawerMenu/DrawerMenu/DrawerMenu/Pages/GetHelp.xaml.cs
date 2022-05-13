using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawerMenu.Pages
{

    public partial class GetHelp : ContentPage
    {
        string URL = "https://www.thehopeline.com/gethelp";
        public GetHelp()
        {
            NavigationPage.SetTitleIcon(this, "logoios.png");
            InitializeComponent();
            Browser.Source = URL;
        }
        private void backClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to

            Navigation.PopModalAsync();

        }



    }

}
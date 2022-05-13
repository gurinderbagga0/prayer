using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DrawerMenu.Pages
{

    public partial class TermsandConditions : ContentPage
    {
        string URL = "https://www.pray.thehopeline.com/termsandconditionsApp.html";
        public TermsandConditions()
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

        //private void forwardClicked(object sender, EventArgs e)
        //{
        //    if (Browser.CanGoForward)
        //    {
        //        Browser.GoForward();
        //    }
        //}

    }

}

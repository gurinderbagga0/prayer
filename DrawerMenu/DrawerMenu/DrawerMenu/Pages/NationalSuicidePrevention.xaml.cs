using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace DrawerMenu.Pages
{
    public partial class NationalSuicidePrevention : ContentPage
    {
        string URL = "https://suicidepreventionlifeline.org/";
        public NationalSuicidePrevention()
        {
            InitializeComponent();
            Browser.Source = URL;
        }
        private void backClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}
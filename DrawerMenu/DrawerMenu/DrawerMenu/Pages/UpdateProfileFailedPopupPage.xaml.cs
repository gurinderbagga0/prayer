using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class UpdateProfileFailedPopupPage : PopupPage
    {
        public UpdateProfileFailedPopupPage(string msgStatus)
        {
          
            InitializeComponent();
            if (msgStatus == "-1")
            {
               // FullmessageStatus.IsVisible = true;
                messageStatus.Text = "Username already in use, please choose another one.";
            }
            if (msgStatus == "-2")
            {
                messageStatus.Text = "Email Address already in use, please choose another one.";
            }
            if (msgStatus == "2")
            {
                //  stackmessage.BarBackgroundColor = Color.FromHex("#6EDA44");
                stackmessage.BackgroundColor = Color.Green;
                messageStatus.Text = "Your profile updated successfully. Please check your inbox to confirm your new email address.";
            }
            if (msgStatus == "1")
            {
                stackmessage.BackgroundColor = Color.Green;
                messageStatus.Text = "Your Profile has been Updated Successfully.";
            }
            if (msgStatus == "3")
            {
                stackmessage.BackgroundColor = Color.Green;
                messageStatus.Text = "Profile updated successfully and a confirmation email sent to the user.";
            }
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            HidePopup();
        }

        private async void HidePopup()
        {
            await Task.Delay(4000);
            await PopupNavigation.RemovePageAsync(this);
        }
    }
}

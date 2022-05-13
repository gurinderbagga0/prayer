using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using DrawerMenu.Pages;
using DrawerMenu.Core;
using UIKit;

namespace DrawerMenu
{
    public partial class LoginPopupPage : PopupPage
    {
        public LoginPopupPage()
        {
            InitializeComponent();
            UsernameEntry.Completed += (object sender, EventArgs e) => PasswordEntry.Focus();
            PasswordEntry.Completed += (object sender, EventArgs e) => LoginButton.Focus();
        }
        private async void OnTapForGotPasswordTapped(object sender, EventArgs args)
        {
            CloseAllPopup();
            var ForGotPasswordpage = new ForGotPasswordpopUp();
            await Navigation.PushPopupAsync(ForGotPasswordpage);
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            CloseAllPopup();
            var page = new SignUpPopupPage();
            await Navigation.PushPopupAsync(page);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            LoginButton.Scale = 0.3;
            LoginButton.Opacity = 0;

            UsernameEntry.TranslationX = PasswordEntry.TranslationX = -10;
            UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                UsernameEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                UsernameEntry.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                    await Task.WhenAll(
                        PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        PasswordEntry.FadeTo(1));
                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                LoginButton.ScaleTo(1),
                LoginButton.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();
            var currentHeight = FrameContainer.Height;
            await Task.WhenAll(
                UsernameEntry.FadeTo(0),
                PasswordEntry.FadeTo(0),
                LoginButton.FadeTo(0));

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);
            });

            await taskSource.Task;
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            string SerialNumber = "";
            // For IPhone build comment Android code

            //string Platform = Device.RuntimePlatform;
            //if (Platform.Contains("Android"))
            //{
            SerialNumber = Android.OS.Build.Serial;
            //}
            //else
            //{
            //  SerialNumber = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            // }

            //  String remoteComputerIPAddress = Request.UserHostAddress;

            // comment for android

            // SerialNumber = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                string UserName = UsernameEntry.Text.Trim();
                string Password = PasswordEntry.Text.Trim();
                CloseAllPopup();
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                User _user = new User();
                var _data = await _user.Login(new LoginModel { UserName = UserName, Password = Password, UserIp = "PhoneIP" });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.UserID > 0)
                {

                    // Code for updating Notification Devices table
                    // We are inserting the userid in the table so that send notification according to user

                    PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    var _dataUpdate = _PostDeviceToken.UpdateNotificationDevicesUserId(new UpdateDeviceDetailsModel { DeviceSerialNo = SerialNumber, UuID = Convert.ToString(_data.UserUID) });

                    Application.Current.Properties["AccessToken"] = _data.AccessToken;
                    //  string AccessToken = Convert.ToString(Application.Current.Properties["AccessToken"]);
                    //   var answer = await DisplayAlert("Question?", AccessToken, "Yes", "No");
                    Application.Current.Properties["UserUID"] = _data.UserUID;
                    Application.Current.Properties["UserRole"] = _data.UserRole;
                    Application.Current.Properties["UserFirstName"] = _data.UserFirstName;
                    Application.Current.Properties["UserLastName"] = _data.UserLastName;
                    Application.Current.Properties["UserEmail"] = _data.UserEmail;
                    if (Application.Current.Properties.ContainsKey("RequestPostedUserId") && Application.Current.Properties.ContainsKey("PrayerRequestId"))
                    {
                        string RequestedPostedUserId = Convert.ToString(Application.Current.Properties["RequestPostedUserId"]);
                        string PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestId"]);
                        if ((RequestedPostedUserId != null && RequestedPostedUserId != string.Empty) && (PrayerRequestId != null && PrayerRequestId != string.Empty))
                        {
                            await Navigation.PushModalAsync(new MenuPageMemberRequest(RequestedPostedUserId, PrayerRequestId));
                        }
                        else
                        {
                            NavigationPage.SetTitleIcon(this, "logo.png");
                            await Navigation.PushModalAsync(new MenuPage());
                        }
                    }
                    else
                    {
                        NavigationPage.SetTitleIcon(this, "logo.png");
                        await Navigation.PushModalAsync(new MenuPage());
                    }
                }
                else if (_data.Status == true && _data.UserID == -1)
                {
                    string msgStatus = "verifyEmail";
                    await Navigation.PushPopupAsync(new LoginFailedPopupPage(msgStatus));
                }
                else
                {
                    Application.Current.Properties["AccessToken"] = null;
                    Application.Current.Properties["UserUID"] = null;
                    Application.Current.Properties["UserRole"] = null;
                    Application.Current.Properties["UserFirstName"] = null;
                    Application.Current.Properties["UserLastName"] = null;
                    Application.Current.Properties["UserEmail"] = null;
                    string msgStatus = "0";
                    await Navigation.PushPopupAsync(new LoginFailedPopupPage(msgStatus));
                }

                //}


            }
            else
            {
                //  await Navigation.PushModalAsync(new Home());
            }

        }

        void Entry_Completed(object sender, EventArgs e)
        {
            //   UITextField obj = new UITextField();
            //  obj.ReturnKeyType = UIReturnKeyType.Next;
            //  var text = ((Entry)sender).Text; //cast sender to access the properties of the Entry
        }


        //public async Task Login(string UserName, string Password)
        //{

        //}
        public bool ValidateAllControls()
        {

            bool UserNameRequired = true;

            bool PasswordRequired = true;


            if (UsernameEntry.Text == "" || UsernameEntry.Text == null)
            {
                UsernameEntry.Placeholder = "User Name Required";
                UsernameEntry.PlaceholderColor = Color.Red;
                UserNameRequired = false;
            }
            else
            {
                UsernameEntry.PlaceholderColor = Color.Black;
                UserNameRequired = true;
            }


            if (PasswordEntry.Text == "" || PasswordEntry.Text == null)
            {
                PasswordEntry.Placeholder = "Password Required";
                PasswordEntry.PlaceholderColor = Color.Red;
                PasswordRequired = false;
            }
            else
            {
                PasswordRequired = true;
                PasswordEntry.PlaceholderColor = Color.Black;
            }

            return UserNameRequired && PasswordRequired;
        }
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

    }
}

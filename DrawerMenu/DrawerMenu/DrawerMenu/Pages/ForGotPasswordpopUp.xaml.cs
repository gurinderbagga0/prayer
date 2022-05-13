using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using DrawerMenu.Core;

namespace DrawerMenu.Pages
{
    public partial class ForGotPasswordpopUp : PopupPage
    {
        public ForGotPasswordpopUp()
        {
            InitializeComponent();
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
            var page = new ForGotPasswordpopUp();
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

            ForgotPasswordEntry.TranslationX = ForgotPasswordEntry.TranslationX = -10;
            // UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            //await Task.WhenAll(
            //    ForgotPasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //    ForgotPasswordEntry.FadeTo(1),
            //    (new Func<Task>(async () =>
            //    {
            //        await Task.Delay(200);
            //        await Task.WhenAll(
            //            PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //            PasswordEntry.FadeTo(1));

            //    }))());

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
                ForgotPasswordEntry.FadeTo(0),
                //  PasswordEntry.FadeTo(0),
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

        private async void OnForgotPasswordClick(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                string email = ForgotPasswordEntry.Text.Trim();
                string reset_password_token = Guid.NewGuid().ToString();
                string reset_password_sent_at = DateTime.Now.ToString();
                CloseAllPopup();
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                UserForgotPassword _ForgotPassword = new UserForgotPassword();
                var _data = await _ForgotPassword.ForgotPassword(new UserForgotPasswordModel { email = email, reset_password_token = reset_password_token, reset_password_sent_at = reset_password_sent_at });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true)
                {
                    string msgStatus = "ForgotPasswordSuccess";
                    await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
                }
                else
                {
                    string msgStatus = "ForgotPasswordFailed";
                    await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
                }
            }
            else
            {
                //  await Navigation.PushModalAsync(new Home());
            }

        }
        public bool ValidateEmailAddress(string EmailAddress)
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            bool IsValid = false;
            IsValid = (Regex.IsMatch(EmailAddress, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            return IsValid;
        }
        public bool ValidateAllControls()
        {

            bool ForgotPasswordEntryRequired = true;
            bool ValidForgotPasswordEntry = true;

            if (ForgotPasswordEntry.Text == "" || ForgotPasswordEntry.Text == null)
            {
                ForgotPasswordEntry.Placeholder = "Email address Required";
                ForgotPasswordEntry.PlaceholderColor = Color.Red;
                ForgotPasswordEntryRequired = false;
            }
            else
            {
                ForgotPasswordEntry.PlaceholderColor = Color.Black;
                ForgotPasswordEntryRequired = true;
                bool Valid = ValidateEmailAddress(ForgotPasswordEntry.Text.Trim());
                if (Valid == false)
                {
                    ForgotPasswordEntry.Placeholder = "Enter Valid Email Address";
                    ForgotPasswordEntry.BackgroundColor = (Color.FromHex("#F4C6C9"));
                    ValidForgotPasswordEntry = false;
                }
                else
                {
                    ForgotPasswordEntry.BackgroundColor = (Color.FromHex("#eaeaea"));
                    ForgotPasswordEntry.PlaceholderColor = Color.Black;
                    ValidForgotPasswordEntry = true;
                }
            }


            //if (PasswordEntry.Text == "" || PasswordEntry.Text == null)
            //{
            //    PasswordEntry.Placeholder = "Password Required";
            //    PasswordEntry.PlaceholderColor = Color.Red;
            //    PasswordRequired = false;
            //}
            //else
            //{
            //    PasswordRequired = true;
            //    PasswordEntry.PlaceholderColor = Color.Black;
            //}

            return ForgotPasswordEntryRequired && ValidForgotPasswordEntry;
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

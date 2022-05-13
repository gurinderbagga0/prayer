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
    public partial class DeleteNotificationtimeAdminpopUp : PopupPage
    {
        public DeleteNotificationtimeAdminpopUp()
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

            btnYes.Scale = 0.3;
            btnYes.Opacity = 0;

          //  ForgotPasswordEntry.TranslationX = ForgotPasswordEntry.TranslationX = -10;
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
                btnYes.ScaleTo(1),
                btnYes.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();
            var currentHeight = FrameContainer.Height;
            await Task.WhenAll(
                //     ForgotPasswordEntry.FadeTo(0),
                //  PasswordEntry.FadeTo(0),
                btnYes.FadeTo(0));

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

        private async void OnYesButtonClicked(object sender, EventArgs e)
        {

            if (Application.Current.Properties.ContainsKey("TimeId"))
            {
                string TimeId = Convert.ToString(Application.Current.Properties["TimeId"]);
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                PostNotificationData _DeleteNotificationTime = new PostNotificationData();
                var _data = await _DeleteNotificationTime.DeleteNotificationTime(new PostNotificationDataModel { TimeId = Convert.ToInt32(TimeId) });
                await Navigation.RemovePopupPageAsync(loadingPage);
                CloseAllPopup();
                Application.Current.Properties["TimeId"] = null;
                if (_data.Status == true)
                {
                    await Navigation.PushModalAsync(new MenuPage());
                }

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

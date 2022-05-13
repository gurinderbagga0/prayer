using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class AddPrayerRequestPopupPage : PopupPage
    {
        public AddPrayerRequestPopupPage()
        {
            InitializeComponent();
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            //try
            //{
            //    byte[] aaa = new byte[590489678];
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

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

            PostPrayerRequestButton.Scale = 0.3;
            PostPrayerRequestButton.Opacity = 0;

            //  UsernameEntry.TranslationX = PasswordEntry.TranslationX = -10;
            //  UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                PrayerTitleEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                PrayerTitleEntry.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    // await Task.Delay(200);
                    //   await Task.WhenAll(
                    //  PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                    //  PasswordEntry.FadeTo(1));

                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                PostPrayerRequestButton.ScaleTo(1),
                PostPrayerRequestButton.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();
            var currentHeight = FrameContainer.Height;
            await Task.WhenAll(
                PrayerTitleEntry.FadeTo(0),
                //PasswordEntry.FadeTo(0),
                PostPrayerRequestButton.FadeTo(0));

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

        private async void OnPostPrayerRequest(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                string Tiltle = PrayerTitleEntry.Text.Trim();
                string Description = PrayerRequestEntry.Text.Trim();
                string MyRequestsUserid = "";
                if (Application.Current.Properties.ContainsKey("UserUID"))
                { 
                    MyRequestsUserid = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                CloseAllPopup();
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                PostPrayer _PostPrayer = new PostPrayer();
                var _data = await _PostPrayer.PostNewPrayer(new PostPrayerModel { Title = Tiltle, Description = Description, UserUId= MyRequestsUserid });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.StatusId > 0)
                {
                    CloseAllPopup();
                    await Navigation.PushModalAsync(new MenuPage());
                }
                else
                {
                    string msgStatus = "PrayerTitleAlreadyExists";
                    await Navigation.PushPopupAsync(new LoginFailedPopupPage(msgStatus));
                }
            }
            else
            {
                //  await Navigation.PushModalAsync(new Home());
            }

        }
        public bool ValidateAllControls()
        {

            bool PrayerTitleRequired = true;

            bool PrayerDescriptionRequired = true;


            if (PrayerTitleEntry.Text == "" || PrayerTitleEntry.Text == null)
            {
                PrayerTitleValidation.Text = "Title Required.";
                // PrayerTitleEntry.PlaceholderColor = Color.Red;
                PrayerTitleRequired = false;
            }
            else
            {
                PrayerTitleEntry.PlaceholderColor = Color.Black;
                PrayerTitleRequired = true;
            }


            if (PrayerRequestEntry.Text == "" || PrayerRequestEntry.Text == null)
            {
                PrayerRequestValidation.Text = "Description Required.";
                // PasswordEntry.PlaceholderColor = Color.Red;
                PrayerDescriptionRequired = false;
            }
            else
            {
                PrayerDescriptionRequired = true;
              //  PasswordEntry.PlaceholderColor = Color.Black;
            }

            return PrayerTitleRequired && PrayerDescriptionRequired;
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

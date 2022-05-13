using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class UpdatePrayerRequestPopupPage : PopupPage
    {
        string PrayerRequestId = "";
        string RequestPostedUserId = "";
        string title = "";
        string Description = "";
        public UpdatePrayerRequestPopupPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("PrayerRequestId") && Application.Current.Properties.ContainsKey("RequestPostedUserId"))
            {
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestId"]);
                RequestPostedUserId = Convert.ToString(Application.Current.Properties["RequestPostedUserId"]);
                title = Convert.ToString(Application.Current.Properties["title"]);
                Description = Convert.ToString(Application.Current.Properties["Description"]);
                PrayerTitleEntry.Text = title;
                PrayerRequestEntry.Text = Description;
            }
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

        private async void OnUpdatePrayerRequest(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                Int64 status = 0;
                string user_id = RequestPostedUserId;
                string uuid = PrayerRequestId;
                string title = PrayerTitleEntry.Text.Trim();
                string text = PrayerRequestEntry.Text.Trim();
                PostPrayer _PostPrayer = new PostPrayer();
                CloseAllPopup();
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                var _data = await _PostPrayer.UpdatePrayer(new UpdatePrayerModel { Title = title, Description = text, RequestPostedUserId = user_id, PrayerRequestId = uuid });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.StatusId > 0)
                {
                    CloseAllPopup();
                    Application.Current.Properties["RequestPostedUserId"] = RequestPostedUserId;
                    Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                    Application.Current.Properties["title"] = null;
                    Application.Current.Properties["Description"] = null;
                    await Navigation.PushModalAsync(new MenuPageMemberRequest(RequestPostedUserId, PrayerRequestId));
                }
                else
                {
                    string msgStatus = "PrayerTitleAlreadyExists";
                    await Navigation.PushPopupAsync(new LoginFailedPopupPage(msgStatus));
                }
            }
            else
            {
                //  await Navigation.PushModalAsync(new ``Home());
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

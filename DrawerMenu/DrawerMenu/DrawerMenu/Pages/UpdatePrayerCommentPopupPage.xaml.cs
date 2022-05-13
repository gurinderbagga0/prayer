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
    public partial class UpdatePrayerCommentPopupPage : PopupPage
    {
        string CommentId = "";
        string commentText = "";

        public UpdatePrayerCommentPopupPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("CommentId") && Application.Current.Properties.ContainsKey("commentText"))
            {
                CommentId = Convert.ToString(Application.Current.Properties["CommentId"]);
                commentText = Convert.ToString(Application.Current.Properties["commentText"]);
                PrayerCommentEntry.Text = commentText;

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

            //   await Task.WhenAll(
            //   PrayerTitleEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //  PrayerTitleEntry.FadeTo(1),
            //   (new Func<Task>(async () =>
            //  {
            // await Task.Delay(200);
            //   await Task.WhenAll(
            //  PasswordEntry.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
            //  PasswordEntry.FadeTo(1));

            //  }))());

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
                //PrayerTitleEntry.FadeTo(0),
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

        private async void OnUpdatePrayerComment(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                Int64 CommentID = Convert.ToInt64(CommentId);
                string CommentText = PrayerCommentEntry.Text.Trim();
                PostPrayer _PostPrayer = new PostPrayer();
                CloseAllPopup();
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                var _data = await _PostPrayer.UpdatePrayerComment(new UpdatePrayerCommentModel { CommentId = CommentID, CommentText = CommentText });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true)
                {
                    string RequestPostedUserIdNewAddComment = Convert.ToString(Application.Current.Properties["RequestPostedUserIdNewAddComment"]);
                    string PrayerRequestIdNewAddComment = Convert.ToString(Application.Current.Properties["PrayerRequestIdNewAddComment"]);

                    Application.Current.Properties["CommentId"] = null;
                    Application.Current.Properties["commentText"] = null;
                    await Navigation.PushModalAsync(new MenuPageMemberRequest(RequestPostedUserIdNewAddComment, PrayerRequestIdNewAddComment));

                }

            }
            else
            {
                //  await Navigation.PushModalAsync(new Home());
            }

        }
        public bool ValidateAllControls()
        {

            bool PrayerCommentRequired = true;

            if (PrayerCommentEntry.Text == "" || PrayerCommentEntry.Text == null)
            {
                PrayerRequestValidation.Text = "Comment Required.";
                // PasswordEntry.PlaceholderColor = Color.Red;
                PrayerCommentRequired = false;
            }
            else
            {
                PrayerCommentRequired = true;
                //  PasswordEntry.PlaceholderColor = Color.Black;
            }

            return PrayerCommentRequired;
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

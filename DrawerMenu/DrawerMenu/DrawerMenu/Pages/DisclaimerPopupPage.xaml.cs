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
    public partial class DisclaimerPopupPage : PopupPage
    {
        public DisclaimerPopupPage()
        {
            InitializeComponent();
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("mailto:theprayerzone@thehopeline.com"));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            lblDisclaimerEmailto.Scale = 0.3;
            lblDisclaimerEmailto.Opacity = 0;

            //  UsernameEntry.TranslationX = PasswordEntry.TranslationX = -10;
            //  UsernameEntry.Opacity = PasswordEntry.Opacity = 0;
        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 400u;

            await Task.WhenAll(
                lblDisclaimerTitle.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                lblDisclaimerDescription.FadeTo(1),
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
                lblDisclaimerEmailto.ScaleTo(1),
                lblDisclaimerEmailto.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();
            var currentHeight = FrameContainer.Height;
            await Task.WhenAll(
                lblDisclaimerDescription.FadeTo(0),
                //PasswordEntry.FadeTo(0),
                lblDisclaimerEmailto.FadeTo(0));

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

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
using System.Text.RegularExpressions;
namespace DrawerMenu
{ 
    public partial class ContactUsPopupPage : PopupPage
    {
        public ContactUsPopupPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("UserFirstName"))
            {
                var UserFirstName = Application.Current.Properties["UserFirstName"];
                if (UserFirstName != null)
                {
                    FirstnameEntry.Text = Convert.ToString(UserFirstName);
                }
            }
            if (Application.Current.Properties.ContainsKey("UserLastName"))
            {
                var UserLastName = Application.Current.Properties["UserLastName"];
                if (UserLastName != null)
                {
                    LastnameEntry.Text = Convert.ToString(UserLastName);
                }
            }

            if (Application.Current.Properties.ContainsKey("UserEmail"))
            {
                var UserEmail = Application.Current.Properties["UserEmail"];
                if (UserEmail != null)
                {
                    EmailAddressEntry.Text = Convert.ToString(UserEmail);
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FrameContainer.HeightRequest = -1;

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

        }

        protected async override Task OnAppearingAnimationEnd()
        {
            var translateLength = 450u;

            await Task.WhenAll(
                lblDisclaimerTitle.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                lblContactUsDescription.FadeTo(1),
                (new Func<Task>(async () =>
                {


                }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                lblContactUsOtherDescription.ScaleTo(1),
                lblContactUsOtherDescription.FadeTo(1));
        }

        protected async override Task OnDisappearingAnimationBegin()
        {
            var taskSource = new TaskCompletionSource<bool>();
            var currentHeight = FrameContainer.Height;
            await Task.WhenAll(
                           lblContactUsOtherDescription.FadeTo(0));

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
        private async void OnTapGestureRecognizerNationalSuicidePrevention(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NationalSuicidePrevention());
        }
        private async void OnSubmit(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                PostPrayer _PostPrayer = new PostPrayer();
                string UserId = "";
                string FirstName = "";
                string LastName = "";
                string EmailAddress = "";
                string Message = "";
                FirstName = Convert.ToString(FirstnameEntry.Text);
                LastName = Convert.ToString(LastnameEntry.Text);
                EmailAddress = Convert.ToString(EmailAddressEntry.Text);
                UserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                Message = Convert.ToString(MessageEntry.Text);
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                var _data = await _PostPrayer.InsertUserMessage(new InsertUserMessageModel { LoginUserId = UserId, UserMessageText = Message, UserFirstName = FirstName, UserLastName = LastName, UserEmailAddress = EmailAddress });
                if (_data.Status == true)
                {
                    ErrMessage.Text = "Message posted successfully";
                    MessageEntry.Text = "";
                    stkmainStack.HeightRequest = 570;
                }
                else
                {
                    ErrMessage.Text = "There is some technical issue in posting the message. Please contact theprayerzone@thehopeline.com ";
                    stkmainStack.HeightRequest = 590;
                }
                await Navigation.RemovePopupPageAsync(loadingPage);
            }
        }
        public bool ValidateAllControls()
        {

            bool FirstNameRequired = true;
            bool EmailAddressRequired = true;
            bool ValidEmailAddress = true;
            bool MessageRequired = true;

            if (FirstnameEntry.Text == "" || FirstnameEntry.Text == null)
            {
                FirstNameValidation.Text = "  Required.";
                // PrayerTitleEntry.PlaceholderColor = Color.Red;
                FirstNameRequired = false;
            }
            else
            {
                FirstNameRequired = true;
                FirstNameValidation.Text = "";
            }


            if (EmailAddressEntry.Text == "" || EmailAddressEntry.Text == null)
            {
                EmailValidation.Text = "  Required.";
                EmailAddressRequired = false;
            }
            else
            {
                EmailValidation.Text = "";
                EmailAddressRequired = true;
                bool Valid = ValidateEmailAddress(EmailAddressEntry.Text.Trim());
                if (Valid == false)
                {
                    EmailValidation.Text = " Enter Valid Email Address";
                    ValidEmailAddress = false;
                }
                else
                {
                    EmailValidation.Text = "";
                    ValidEmailAddress = true;
                }
            }
            if (MessageEntry.Text == "" || MessageEntry.Text == null)
            {
                MessageValidation.Text = "  Required.";
                // PasswordEntry.PlaceholderColor = Color.Red;
                MessageRequired = false;
            }
            else
            {
                MessageValidation.Text = "";
                MessageRequired = true;
                //  PasswordEntry.PlaceholderColor = Color.Black;
            }

            return FirstNameRequired && EmailAddressRequired && MessageRequired && ValidEmailAddress;
        }
        public bool ValidateEmailAddress(string EmailAddress)
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            bool IsValid = false;
            IsValid = (Regex.IsMatch(EmailAddress, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            return IsValid;
        }
    }
}
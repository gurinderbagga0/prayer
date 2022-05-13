using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using DrawerMenu.Core;
using System.Net;
using Rg.Plugins.Popup.Extensions;

namespace DrawerMenu.Pages
{
    public partial class SignUpRequester : ContentPage
    {
        public SignUpRequester()
        {
           // string Platform = Device.RuntimePlatform;
            //if (!Platform.Contains("Android"))
            //{
            //NavigationPage.SetHasNavigationBar(this, true);
            //NavigationPage.SetTitleIcon(this, "logoios.png");
            //NavigationPage.SetHasBackButton(this, true);
            //  }
            InitializeComponent();
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                stackHeading.Margin = new Thickness(10, 30);
                // stackUserInfo.Spacing = 40;
            }
            // string IPAddress = Request.UserHostAddress;
        }
        void switcher_Toggled(object sender, ToggledEventArgs e)
        {

            //  label.Text = String.Format("Switch is now {0}", e.Value);
        }
        private async void OnTapGestureRecognizerTermsConditionsTapped(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new TermsandConditions());
        }
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            // Do something

        }
        public bool ValidateEmailAddress(string EmailAddress)
        {
            const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
            bool IsValid = false;
            IsValid = (Regex.IsMatch(EmailAddress, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            return IsValid;
        }
        private async void OnBacktoHomeRequester(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage("",""), true);
        }

        private async void OnSignUpButtonRequester(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
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
               // SerialNumber = UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
                // }


                string first_name = FirstNameRequesterEntryText.Text.Trim();
                string last_name = LastNameRequesterEntryText.Text.Trim();
                string user_name = UserNameRequesterEntryText.Text.Trim();
                string email = EmailEntryRequesterText.Text.Trim();
                string encrypted_password = PasswordRequesterEntryText.Text.Trim();
                string gender = "";

                if (GnderRequesterPicker.SelectedIndex == -1)
                {
                    gender = "";
                }
                else
                {
                    string GenderName = GnderRequesterPicker.Items[GnderRequesterPicker.SelectedIndex];
                    gender = GenderName;
                }
                string question = "";
                if (hearaboutusRequesterPicker.SelectedIndex == -1)
                {
                    question = "";
                }
                else
                {
                    string questionName = hearaboutusRequesterPicker.Items[hearaboutusRequesterPicker.SelectedIndex];
                    question = questionName;
                }
                string TermsConditions = "";

                var imageSender = (Image)chkreadTermconditionRequester;
                var source = chkreadTermconditionRequester.Source as FileImageSource;
                if (source.File == "Checked.png")
                {
                    TermsConditions = "1";
                }
                else
                {
                    TermsConditions = "0";
                }
                string current_sign_in_ip = "PhoneIP";
                string last_sign_in_ip = "PhoneIP";
                string opt = "0";
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                UserReqRegisteration _user = new UserReqRegisteration();
                var _data = await _user.RequesterRegistration(new UserReqRegisterationModel { first_name = first_name, last_name = last_name, user_name = user_name, email = email, encrypted_password = encrypted_password, gender = gender, question = question, TermsConditions = TermsConditions, current_sign_in_ip = current_sign_in_ip, last_sign_in_ip = last_sign_in_ip, opt = opt });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.UserID > 0)
                {
                    PostDeviceToken _PostDeviceToken = new PostDeviceToken();
                    var _dataUpdate = _PostDeviceToken.UpdateNotificationDevicesUserId(new UpdateDeviceDetailsModel { DeviceSerialNo = SerialNumber, UuID = Convert.ToString(_data.UserUID) });
                    Application.Current.Properties["AccessToken"] = _data.AccessToken;
                    Application.Current.Properties["UserUID"] = _data.UserUID;
                    Application.Current.Properties["UserRole"] = _data.UserRole;
                    Application.Current.Properties["UserFirstName"] = _data.UserFirstName;
                    Application.Current.Properties["UserLastName"] = _data.UserLastName;
                    Application.Current.Properties["UserEmail"] = _data.UserEmail;
                    await Navigation.PushModalAsync(new MenuPage());
                }
                else if (_data.Status == false && _data.UserID == -2)
                {
                    Application.Current.Properties["AccessToken"] = null;
                    Application.Current.Properties["UserUID"] = null;
                    Application.Current.Properties["UserRole"] = null;
                    Application.Current.Properties["UserFirstName"] = null;
                    Application.Current.Properties["UserLastName"] = null;
                    Application.Current.Properties["UserEmail"] = null;
                    string msgStatus = "-2";
                    await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
                }
                else if (_data.Status == false && _data.UserID == -1)
                {
                    Application.Current.Properties["AccessToken"] = null;
                    Application.Current.Properties["UserUID"] = null;
                    Application.Current.Properties["UserRole"] = null;
                    Application.Current.Properties["UserFirstName"] = null;
                    Application.Current.Properties["UserLastName"] = null;
                    Application.Current.Properties["UserEmail"] = null;
                    string msgStatus = "-1";
                    await Navigation.PushPopupAsync(new SignUpRequesterFailedPopupPage(msgStatus));
                }
            }




        }

        public bool ValidateAllControls()
        {
            bool FirstNameRequired = true;
            bool LastNameRequired = true;
            bool UserNameRequired = true;
            bool EmailAddressRequired = true;
            bool ValidEmailAddress = true;
            bool PasswordRequired = true;
            bool ValidPasswordLength = true;
            bool ValidPassConfirmPwdMatch = true;
            bool ConfirmPasswordRequired = true;
            bool genderRequired = true;
            bool hearaboutusRequired = true;
            bool TermConditionRequired = true;


            if (FirstNameRequesterEntryText.Text == "" || FirstNameRequesterEntryText.Text == null)
            {
                FirstNameRequesterEntryText.Placeholder = "First Name Required";
                FirstNameRequesterEntryText.PlaceholderColor = Color.Red;
                FirstNameRequired = false;
            }
            else
            {
                FirstNameRequesterEntryText.PlaceholderColor = Color.Black;
                FirstNameRequired = true;
            }
            if (LastNameRequesterEntryText.Text == "" || LastNameRequesterEntryText.Text == null)
            {
                LastNameRequesterEntryText.Placeholder = "Last Name Required";
                LastNameRequesterEntryText.PlaceholderColor = Color.Red;
                LastNameRequired = false;
            }
            else
            {
                LastNameRequesterEntryText.PlaceholderColor = Color.Black;
                LastNameRequired = true;
            }
            if (UserNameRequesterEntryText.Text == "" || UserNameRequesterEntryText.Text == null)
            {
                UserNameRequesterEntryText.Placeholder = "User Name Required";
                UserNameRequesterEntryText.PlaceholderColor = Color.Red;
                UserNameRequired = false;
            }
            else
            {
                UserNameRequesterEntryText.PlaceholderColor = Color.Black;
                UserNameRequired = true;
            }

            if (EmailEntryRequesterText.Text == "" || EmailEntryRequesterText.Text == null)
            {
                EmailEntryRequesterText.Placeholder = "Email Address Required";
                EmailEntryRequesterText.PlaceholderColor = Color.Red;
                EmailAddressRequired = false;
            }
            else
            {
                EmailAddressRequired = true;
                bool Valid = ValidateEmailAddress(EmailEntryRequesterText.Text.Trim());
                if (Valid == false)
                {
                    EmailEntryRequesterText.Placeholder = "Enter Valid Email Address";
                    EmailEntryRequesterText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                    ValidEmailAddress = false;
                }
                else
                {
                    EmailEntryRequesterText.BackgroundColor = (Color.FromHex("#eaeaea"));
                    EmailEntryRequesterText.PlaceholderColor = Color.Black;
                    ValidEmailAddress = true;
                }
            }
            if (PasswordRequesterEntryText.Text == "" || PasswordRequesterEntryText.Text == null)
            {
                PasswordRequesterEntryText.Placeholder = "Password Required";
                PasswordRequesterEntryText.PlaceholderColor = Color.Red;
                PasswordRequired = false;
            }
            else
            {
                PasswordRequired = true;
                PasswordRequesterEntryText.PlaceholderColor = Color.Black;
            }
            if (PasswordRequesterEntryText.Text != null && PasswordRequesterEntryText.Text.Length < 8)
            {
                PasswordRequesterEntryText.Text = "";
                PasswordRequesterEntryText.Placeholder = "Must be at least 8 characters";
                PasswordRequesterEntryText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                ValidPasswordLength = false;
            }
            else
            {
                GnderRequesterPicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                ValidPasswordLength = true;
            }


            if (PasswordConfirmationRequesterText.Text == "" || PasswordConfirmationRequesterText.Text == null)
            {
                PasswordConfirmationRequesterText.Placeholder = "Confirm Password Required";
                PasswordConfirmationRequesterText.PlaceholderColor = Color.Red;
                ConfirmPasswordRequired = false;
            }
            else
            {
                PasswordConfirmationRequesterText.PlaceholderColor = Color.Black;
                ConfirmPasswordRequired = true;
            }
            if ((PasswordRequesterEntryText.Text != null && PasswordConfirmationRequesterText.Text != null) && (PasswordRequesterEntryText.Text.Trim() != PasswordConfirmationRequesterText.Text.Trim()))
            {
                ValidPassConfirmPwdMatch = false;
                PasswordRequesterEntryText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                PasswordConfirmationRequesterText.BackgroundColor = (Color.FromHex("#F4C6C9"));
            }
            else
            {
                ValidPassConfirmPwdMatch = true;
                PasswordRequesterEntryText.BackgroundColor = (Color.FromHex("#eaeaea"));
                PasswordConfirmationRequesterText.BackgroundColor = (Color.FromHex("#eaeaea"));
            }



            if (GnderRequesterPicker.SelectedIndex == -1)
            {
                // GnderPicker.BackgroundColor = Color.Red;
                GnderRequesterPicker.BackgroundColor = (Color.FromHex("#F4C6C9"));
                genderRequired = false;
            }
            else
            {
                GnderRequesterPicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                genderRequired = true;
            }
            if (hearaboutusRequesterPicker.SelectedIndex == -1)
            {
                hearaboutusRequesterPicker.BackgroundColor = (Color.FromHex("#F4C6C9"));
                hearaboutusRequired = false;
            }
            else
            {
                hearaboutusRequesterPicker.BackgroundColor = (Color.FromHex("#eaeaea"));
                hearaboutusRequired = true;
            }

            var source = chkreadTermconditionRequester.Source as FileImageSource;
            var filename = source.File;
            if (filename == "not_Checked.png")
            {
                lblreadTermconditionRequester.BackgroundColor = (Color.FromHex("#F4C6C9"));
                TermConditionRequired = false;
            }
            else
            {
                lblreadTermconditionRequester.BackgroundColor = (Color.FromHex("#FFFFFF"));
                TermConditionRequired = true;
            }
            return FirstNameRequired && LastNameRequired && UserNameRequired && EmailAddressRequired && ValidEmailAddress && PasswordRequired && ValidPasswordLength && ValidPassConfirmPwdMatch && ConfirmPasswordRequired && genderRequired && hearaboutusRequired && TermConditionRequired;
        }

    }
}

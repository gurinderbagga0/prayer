using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class SignUpChampion : ContentPage
    {
        public SignUpChampion()
        {
            string Platform = Device.RuntimePlatform;
            InitializeComponent();
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
                stackHeading.Margin = new Thickness(10, 30);
            }
        }
        private async void OnTermsandconditionsTapped(object sender, EventArgs args)
        {

        }
        void switcher_Toggled(object sender, ToggledEventArgs e)
        {

            //  label.Text = String.Format("Switch is now {0}", e.Value);
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
        private async void OnTapGestureRecognizerTermsConditionsTapped(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new TermsandConditions());
        }
        void OnoptTextMessageTapped(object sender, EventArgs args)
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
        private async void OnBacktoHomeChampion(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage("",""), true);
        }
        private async void OnSignUpButton(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                string first_name = FirstNameChampionText.Text.Trim();
                string last_name = LastNameChampionText.Text.Trim();
                string user_name = UserNameChampionText.Text.Trim();
                string email = EmailChampionText.Text.Trim();
                string unconfirmed_email = EmailChampionText.Text.Trim();
                string encrypted_password = PasswordChampionText.Text.Trim();
                string gender = "";
                string phone_number = PhoneNumberText.Text.Trim();
                string street_address = StreetAddressText.Text.Trim();
                string city = CityText.Text.Trim();

                string state = "";
                if (StatePickerChampion.SelectedIndex == -1)
                {
                    state = "";
                }
                else
                {
                    string stateName = StatePickerChampion.Items[StatePickerChampion.SelectedIndex];
                    state = stateName;
                }
                string zipcode = Convert.ToString(ZipCodeText.Text);

                string Country = "";
                if (CountryChampion.SelectedIndex == -1)
                {
                    Country = "";
                }
                else
                {
                    string CountryName = CountryChampion.Items[CountryChampion.SelectedIndex];
                    Country = CountryName;
                }
                string question = "";
                if (hearaboutusPickerChampion.SelectedIndex == -1)
                {
                    question = "";
                }
                else
                {
                    string questionName = hearaboutusPickerChampion.Items[hearaboutusPickerChampion.SelectedIndex];
                    question = questionName;
                }


                // string state = StreetAddressText.Text.Trim();


                string TermsConditions = "";

                var imageSender = (Image)chkreadTermconditionChampion;
                var source = chkreadTermconditionChampion.Source as FileImageSource;
                if (source.File == "Checked.png")
                {
                    TermsConditions = "1";
                }
                else
                {
                    TermsConditions = "0";
                }

                string opt = "";

                var imageSenderopt = (Image)chkoptTextMessage;
                var sourceopt = chkoptTextMessage.Source as FileImageSource;
                if (sourceopt.File == "Checked.png")
                {
                    opt = "1";
                }
                else
                {
                    opt = "0";
                }


                string current_sign_in_ip = "PhoneIP";
                string last_sign_in_ip = "PhoneIP";

                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                UserChampionRegisteration _user = new UserChampionRegisteration();
                var _data = await _user.ChampionRegistration(new UserChampionRegisterationModel { first_name = first_name, last_name = last_name, user_name = user_name, email = email, unconfirmed_email = email, encrypted_password = encrypted_password, phone_number = phone_number, street_address = street_address, city = city, state = state, zipcode = zipcode, country = Country, question = question, TermsConditions = TermsConditions, opt = opt, current_sign_in_ip = current_sign_in_ip, last_sign_in_ip = last_sign_in_ip });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true && _data.UserID > 0)
                {
                    string msgStatus = "1";
                    await Navigation.PushModalAsync(new SignUpChampionFailedPopupPage(msgStatus));
                }
                else if (_data.Status == false && _data.UserID == -2)
                {
                    string msgStatus = "-2";
                    await Navigation.PushPopupAsync(new SignUpChampionFailedPopupPage(msgStatus));
                }
                else if (_data.Status == false && _data.UserID == -1)
                {

                    string msgStatus = "-1";
                    await Navigation.PushPopupAsync(new SignUpChampionFailedPopupPage(msgStatus));
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
            bool StreetAddressRequired = true;
            bool CityRequired = true;
            bool ZipCodeRequired = true;
            bool StateRequired = true;
            bool CountryRequired = true;
            bool hearaboutusRequired = true;
            bool TermConditionRequired = true;


            if (FirstNameChampionText.Text == "" || FirstNameChampionText.Text == null)
            {
                FirstNameChampionText.Placeholder = "First Name Required";
                FirstNameChampionText.PlaceholderColor = Color.Red;
                FirstNameRequired = false;
            }
            else
            {
                FirstNameChampionText.PlaceholderColor = Color.Black;
                FirstNameRequired = true;
            }
            if (LastNameChampionText.Text == "" || LastNameChampionText.Text == null)
            {
                LastNameChampionText.Placeholder = "Last Name Required";
                LastNameChampionText.PlaceholderColor = Color.Red;
                LastNameRequired = false;
            }
            else
            {
                LastNameChampionText.PlaceholderColor = Color.Black;
                LastNameRequired = true;
            }
            if (UserNameChampionText.Text == "" || UserNameChampionText.Text == null)
            {
                UserNameChampionText.Placeholder = "User Name Required";
                UserNameChampionText.PlaceholderColor = Color.Red;
                UserNameRequired = false;
            }
            else
            {
                UserNameChampionText.PlaceholderColor = Color.Black;
                UserNameRequired = true;
            }

            if (EmailChampionText.Text == "" || EmailChampionText.Text == null)
            {
                EmailChampionText.Placeholder = "Email Address Required";
                EmailChampionText.PlaceholderColor = Color.Red;
                EmailAddressRequired = false;
            }
            else
            {
                EmailAddressRequired = true;
                bool Valid = ValidateEmailAddress(EmailChampionText.Text.Trim());
                if (Valid == false)
                {
                    EmailChampionText.Placeholder = "Enter Valid Email Address";
                    EmailChampionText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                    ValidEmailAddress = false;
                }
                else
                {
                    EmailChampionText.BackgroundColor = (Color.FromHex("#eaeaea"));
                    EmailChampionText.PlaceholderColor = Color.Black;
                    ValidEmailAddress = true;
                }
            }
            if (PasswordChampionText.Text == "" || PasswordChampionText.Text == null)
            {
                PasswordChampionText.Placeholder = "Password Required";
                PasswordChampionText.PlaceholderColor = Color.Red;
                PasswordRequired = false;
            }
            else
            {
                PasswordRequired = true;
                PasswordChampionText.PlaceholderColor = Color.Black;
            }
            if (PasswordChampionText.Text != null && PasswordChampionText.Text.Length < 8)
            {
                PasswordChampionText.Text = "";
                PasswordChampionText.Placeholder = "Must be at least 8 characters";
                PasswordChampionText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                ValidPasswordLength = false;
            }
            else
            {
                PasswordChampionText.BackgroundColor = (Color.FromHex("#eaeaea"));
                ValidPasswordLength = true;
            }


            if (PasswordConfirmationChampionText.Text == "" || PasswordConfirmationChampionText.Text == null)
            {
                PasswordConfirmationChampionText.Placeholder = "Confirm Password Required";
                PasswordConfirmationChampionText.PlaceholderColor = Color.Red;
                ConfirmPasswordRequired = false;
            }
            else
            {
                PasswordConfirmationChampionText.PlaceholderColor = Color.Black;
                ConfirmPasswordRequired = true;
            }
            if ((PasswordChampionText.Text != null && PasswordConfirmationChampionText.Text != null) && (PasswordChampionText.Text.Trim() != PasswordConfirmationChampionText.Text.Trim()))
            {
                ValidPassConfirmPwdMatch = false;
                PasswordChampionText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                PasswordConfirmationChampionText.BackgroundColor = (Color.FromHex("#F4C6C9"));
            }
            else
            {
                ValidPassConfirmPwdMatch = true;
                PasswordChampionText.BackgroundColor = (Color.FromHex("#eaeaea"));
                PasswordConfirmationChampionText.BackgroundColor = (Color.FromHex("#eaeaea"));
            }


            if (StreetAddressText.Text == "" || StreetAddressText.Text == null)
            {
                StreetAddressText.Placeholder = "Street Address Required";
                StreetAddressText.PlaceholderColor = Color.Red;
                StreetAddressRequired = false;
            }
            else
            {
                StreetAddressText.PlaceholderColor = Color.Black;
                StreetAddressRequired = true;
            }


            if (CityText.Text == "" || CityText.Text == null)
            {
                CityText.Placeholder = "City Required";
                CityText.PlaceholderColor = Color.Red;
                CityRequired = false;
            }
            else
            {
                CityText.PlaceholderColor = Color.Black;
                CityRequired = true;
            }
            if (ZipCodeText.Text == "" || ZipCodeText.Text == null)
            {
                ZipCodeText.Placeholder = "Zip Required";
                ZipCodeText.PlaceholderColor = Color.Red;
                ZipCodeRequired = false;
            }
            else
            {
                ZipCodeText.PlaceholderColor = Color.Black;
                ZipCodeRequired = true;
            }


            if (StatePickerChampion.SelectedIndex == -1)
            {
                // GnderPicker.BackgroundColor = Color.Red;
                StatePickerChampion.BackgroundColor = (Color.FromHex("#F4C6C9"));
                StateRequired = false;
            }
            else
            {
                StatePickerChampion.BackgroundColor = (Color.FromHex("#eaeaea"));
                StateRequired = true;
            }

            if (CountryChampion.SelectedIndex == -1)
            {
                // GnderPicker.BackgroundColor = Color.Red;
                CountryChampion.BackgroundColor = (Color.FromHex("#F4C6C9"));
                CountryRequired = false;
            }
            else
            {
                CountryChampion.BackgroundColor = (Color.FromHex("#eaeaea"));
                CountryRequired = true;
            }

            if (hearaboutusPickerChampion.SelectedIndex == -1)
            {
                hearaboutusPickerChampion.BackgroundColor = (Color.FromHex("#F4C6C9"));
                hearaboutusRequired = false;
            }
            else
            {
                hearaboutusPickerChampion.BackgroundColor = (Color.FromHex("#eaeaea"));
                hearaboutusRequired = true;
            }

            var source = chkreadTermconditionChampion.Source as FileImageSource;
            var filename = source.File;
            if (filename == "not_Checked.png")
            {
                lblreadTermcondition.BackgroundColor = (Color.FromHex("#F4C6C9"));
                TermConditionRequired = false;
            }
            else
            {
                lblreadTermcondition.BackgroundColor = (Color.FromHex("#FFFFFF"));
                TermConditionRequired = true;
            }
            return FirstNameRequired && LastNameRequired && UserNameRequired && EmailAddressRequired && ValidEmailAddress && PasswordRequired && ValidPasswordLength && ValidPassConfirmPwdMatch && ConfirmPasswordRequired && StreetAddressRequired && CityRequired && ZipCodeRequired && StateRequired && CountryRequired && hearaboutusRequired && TermConditionRequired;
        }

    }
}

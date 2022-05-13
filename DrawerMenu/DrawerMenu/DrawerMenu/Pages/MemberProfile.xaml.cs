using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using DrawerMenu.Pages;
using DrawerMenu.Core;
using UIKit;
using DrawerMenu.Core.Prayers;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using Foundation;
using System.Text.RegularExpressions;

namespace DrawerMenu
{
    //public partial class MemberProfile : ContentPage
    //{

    //    public MemberProfile()
    //    {

    //        InitializeComponent();

    //    }
    //}

    public partial class MemberProfile : ContentPage
    {
        public MemberProfile()
        {
            string UserUId = "";
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
            }
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                string RoleName = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (RoleName.Contains("admin"))
                {
                    if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser") && Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]) != "")
                    {
                        UserUId = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                    }
                    else
                    {
                        UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                    }
                }
                else
                {
                    UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                if (RoleName.Contains("admin"))
                {
                    stackAdmin.IsVisible = true;
                    if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser") && Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]) != "")
                    {
                        UserUId = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                        GetUserRolesInformation(UserUId);
                    }
                    else
                    {
                        RoleName = Convert.ToString(Application.Current.Properties["UserRole"]);
                        chkAdmin.Source = "Checked.png";
                        if (RoleName.Contains("moderator"))
                        {
                            chkModerator.Source = "Checked.png";
                        }
                        if (RoleName.Contains("warrior"))
                        {
                            chkPrayerChampion.Source = "Checked.png";
                        }
                        if (RoleName.Contains("member"))
                        {
                            chkMember.Source = "Checked.png";
                        }
                    }

                }
                else
                {
                    stackAdmin.IsVisible = false;
                }
                GetUserProfileDetails(UserUId);
             //   SetReminderSwitch(UserUId);
             //   SetNotificationsSwitch(UserUId);

            }
        }

        //public async void SetNotificationsSwitch(string UserUId)
        //{
        //    if (UserUId != string.Empty)
        //    {
        //        PostDeviceToken objDeviceTokenData = new PostDeviceToken();
        //        List<PostDeviceTokenModel> _list = new List<PostDeviceTokenModel>();
        //        _list = await objDeviceTokenData.getNotificationEnableDisable(UserUId);
        //        if (_list != null)
        //        {
        //            if (_list.Count > 0)
        //            {
        //                Application.Current.Properties["PushNutificationsUserId"] = UserUId;
        //                Application.Current.Properties["PushNutificationToggled"] = "0";
        //                // PushNotificationSwitch.IsVisible = true;
        //                PushNotificationSwitch.IsToggled = true;
        //            }
        //            else
        //            {
        //                Application.Current.Properties["PushNutificationsUserId"] = UserUId;
        //                Application.Current.Properties["PushNutificationToggled"] = "0";
        //                // PushNotificationSwitch.IsVisible = false;
        //                PushNotificationSwitch.IsToggled = false;
        //            }
        //        }
        //        else
        //        {
        //            Application.Current.Properties["PushNutificationsUserId"] = UserUId;
        //            Application.Current.Properties["PushNutificationToggled"] = "0";
        //            //  PushNotificationSwitch.IsVisible = false;
        //            PushNotificationSwitch.IsToggled = false;
        //        }
        //    }
        //    Application.Current.Properties["PushNutificationsUserId"] = UserUId;
        //    Application.Current.Properties["PushNutificationToggled"] = "1";

        //}

        //public async void PushNotificationSwitch_Toggled(object sender, ToggledEventArgs e)
        //{
        //    string PushNutificationToggled = "";
        //    string PushNutificationsUserId = "";
        //    if (Application.Current.Properties.ContainsKey("PushNutificationToggled") && (Application.Current.Properties.ContainsKey("PushNutificationsUserId")))
        //    {
        //        PushNutificationToggled = Convert.ToString(Application.Current.Properties["PushNutificationToggled"]);
        //        PushNutificationsUserId = Convert.ToString(Application.Current.Properties["PushNutificationsUserId"]);
        //    }
        //    if (e.Value == true && PushNutificationToggled != "0")
        //    {
        //        PostDeviceToken _PostNotificationUserData = new PostDeviceToken();
        //        var _data = await _PostNotificationUserData.UpdatePushNotificationDetailsUsers(new PostDeviceTokenModel { IsNotificationEnable = 1, UuID = PushNutificationsUserId });
        //        // await Navigation.PushAsync(new PrayerRemainder());
        //    }
        //    else if (e.Value == false && PushNutificationToggled != "0")
        //    {
        //        var answer = await DisplayAlert("Question?", "Would you like to Set Push Notifications as OFF", "Yes", "No");
        //        if (answer)
        //        {
        //            // if yes make all user alarm Inactive
        //            PostDeviceToken _PostNotificationUserData = new PostDeviceToken();
        //            var _data = await _PostNotificationUserData.UpdatePushNotificationDetailsUsers(new PostDeviceTokenModel { IsNotificationEnable = 0, UuID = PushNutificationsUserId });
        //            //PostDeviceToken _PostDeviceToken = new PostDeviceToken();
        //            //var _data = _PostDeviceToken.UpdateDeviceDetails(new UpdateDeviceTokenModel { DeviceId = DeviceId });
        //        }
        //        else
        //        {
        //            // if No make all user alarm Active
        //            Application.Current.Properties["PushNutificationToggled"] = "0";
        //            PushNotificationSwitch.IsToggled = true;
        //        }
        //    }
        //    Application.Current.Properties["PushNutificationToggled"] = "1";

        //}

        //public async void SetReminderSwitch(string UserUId)
        //{
        //    if (UserUId != string.Empty)
        //    {
        //        PostNotificationData objNotificationData = new PostNotificationData();
        //        List<PostNotificationDataModel> _list = new List<PostNotificationDataModel>();
        //        _list = await objNotificationData.getAllNotificationDataUserTime(UserUId);
        //        if (_list != null)
        //        {
        //            if (_list.Count > 0)
        //            {
        //                Application.Current.Properties["PrayerReminderUserId"] = UserUId;
        //                Application.Current.Properties["Toggled"] = "0";
        //                SwitchRemainder.IsVisible = true;
        //                SetSwitchRemainder.IsToggled = true;

        //            }
        //            else
        //            {
        //                SwitchRemainder.IsVisible = false;
        //                Application.Current.Properties["PrayerReminderUserId"] = UserUId;
        //                Application.Current.Properties["Toggled"] = "0";
        //                SetSwitchRemainder.IsToggled = false;
        //            }
        //        }
        //        else
        //        {
        //            SwitchRemainder.IsVisible = false;
        //            Application.Current.Properties["PrayerReminderUserId"] = UserUId;
        //            Application.Current.Properties["Toggled"] = "0";
        //            SetSwitchRemainder.IsToggled = false;
        //        }
        //    }
        //    Application.Current.Properties["PrayerReminderUserId"] = UserUId;
        //    Application.Current.Properties["Toggled"] = "1";
        //}
        //public async void SetSwitchswitcher_Toggled(object sender, ToggledEventArgs e)
        //{
        //    string Toggled = "";
        //    string PrayerReminderUserId = "";
        //    if (Application.Current.Properties.ContainsKey("Toggled") && (Application.Current.Properties.ContainsKey("PrayerReminderUserId")))
        //    {
        //        Toggled = Convert.ToString(Application.Current.Properties["Toggled"]);
        //        PrayerReminderUserId = Convert.ToString(Application.Current.Properties["PrayerReminderUserId"]);
        //    }
        //    if (e.Value == true && Toggled != "0")
        //    {
        //        PostNotificationData _PostNotificationUserData = new PostNotificationData();
        //        var _data = await _PostNotificationUserData.UpdateReminderNotificationDetailsUsers(new PostNotificationDataModel { IsReminderEnable = 1, UuID = PrayerReminderUserId });
        //        var loadingPage = new LoadingPopupPage();
        //        await Navigation.PushPopupAsync(loadingPage);
        //        await Navigation.PushModalAsync(new PrayerRemainder());
        //        await Navigation.RemovePopupPageAsync(loadingPage);
        //    }
        //    else if (e.Value == false && Toggled != "0")
        //    {
        //        var answer = await DisplayAlert("Question?", "Would you like to Set Prayer Reminders as OFF", "Yes", "No");
        //        if (answer)
        //        {
        //            SwitchRemainder.IsVisible = false;
        //            // if yes make all user alarm Inactive
        //            PostNotificationData _PostNotificationUserData = new PostNotificationData();
        //            var _data = await _PostNotificationUserData.UpdateReminderNotificationDetailsUsers(new PostNotificationDataModel { IsReminderEnable = 0, UuID = PrayerReminderUserId });
        //            //PostDeviceToken _PostDeviceToken = new PostDeviceToken();
        //            //var _data = _PostDeviceToken.UpdateDeviceDetails(new UpdateDeviceTokenModel { DeviceId = DeviceId });
        //        }
        //        else
        //        {
        //            // if No make all user alarm Active
        //            Application.Current.Properties["Toggled"] = "0";
        //            SetSwitchRemainder.IsToggled = true;
        //        }
        //    }
        //    Application.Current.Properties["Toggled"] = "1";

        //}
        //private async void SetPrayerRemindersClicked(object sender, EventArgs e)
        //{
        //    // await Navigation.PushAsync(new PrayerRemainder());
        //    var loadingPage = new LoadingPopupPage();
        //    await Navigation.PushPopupAsync(loadingPage);
        //    await Navigation.PushModalAsync(new PrayerRemainder());
        //    await Navigation.RemovePopupPageAsync(loadingPage);
        //}

        protected override bool OnBackButtonPressed()
        {
            //DisplayAlert("Question?", "Are You sure you want to exit", "Yes", "No");
            //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            return false;
        }
        private async void OnUpdateProfileButton(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                string UserUniqueID = "";
                string password = "";
                string encrypted_password = "";
                string email = "";
                string user_name = "";
                string first_name = "";
                string last_name = "";
                string gender = "";
                string street_address = "";
                string city = "";
                string state = "";
                string zipcode = "";
                string Country = "";
                string question = "";
                string is_active = "";
                string UserRole = "";
                Boolean UserRolemember = false;
                Boolean UserRoleAdmin = false;
                Boolean UserRolewarrior = false;
                Boolean UserRoleModerator = false;
                if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
                {
                    UserUniqueID = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser") && Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]) != "")
                    {
                        UserUniqueID = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                    }
                }

                if (PasswordChampionText.Text != null && PasswordChampionText.Text != string.Empty)
                {
                    encrypted_password = PasswordChampionText.Text.Trim();
                    string mySalt = BCrypt.Net.BCrypt.GenerateSalt(12);
                    password = BCrypt.Net.BCrypt.HashPassword(encrypted_password, mySalt);
                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("UserPassword"))// Application.Current.Properties["UserPassword"])
                        if (Application.Current.Properties["UserPassword"] != null)
                        {
                            password = Convert.ToString(Application.Current.Properties["UserPassword"]);
                        }
                }
                if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser") && Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]) != "")
                {
                    if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser"))
                    {
                        UserUniqueID = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                    }
                    else
                    {
                        UserUniqueID = Convert.ToString(Application.Current.Properties["UserUID"]);
                    }
                }
                else
                {
                    UserUniqueID = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                string UserID = UserUniqueID;// Guid.NewGuid().ToString();

                if (EmailAddressText.Text != "" || EmailAddressText.Text != null)
                {
                    email = EmailAddressText.Text.Trim();
                }

                if (UserNameText.Text != "" || UserNameText.Text != null)
                {
                    user_name = UserNameText.Text.Trim();
                }

                if (FirstNameText.Text != "" || FirstNameText.Text != null)
                {
                    first_name = FirstNameText.Text.Trim();
                }

                if (LastNameText.Text != "" || LastNameText.Text != null)
                {
                    last_name = LastNameText.Text.Trim();
                }


                if (GnderPicker.SelectedIndex == -1)
                {
                    gender = "";
                }
                else
                {
                    string GenderName = GnderPicker.Items[GnderPicker.SelectedIndex];
                    gender = GenderName;
                }

                if (StreetAddressText.Text != "" || StreetAddressText.Text != null)
                {
                    street_address = StreetAddressText.Text.Trim();
                }

                if (CityText.Text != "" || CityText.Text != null)
                {
                    city = CityText.Text.Trim();
                }


                if (StatePickerChampion.SelectedIndex == -1)
                {
                    state = "";
                }
                else
                {
                    string stateName = StatePickerChampion.Items[StatePickerChampion.SelectedIndex];
                    state = stateName;
                }

                if (ZipCodeText.Text != "" || ZipCodeText.Text != null)
                {
                    zipcode = ZipCodeText.Text.Trim();
                    if (zipcode == "")
                    {
                        zipcode = "-1";
                    }
                }
                else
                {
                    zipcode = "-1";
                }



                if (CountryChampion.SelectedIndex == -1)
                {
                    Country = "";
                }
                else
                {
                    string CountryName = CountryChampion.Items[CountryChampion.SelectedIndex];
                    Country = CountryName;
                }
                string phone_number = "";
                if (PhoneNumberText.Text != "" || PhoneNumberText.Text != null)
                {
                    phone_number = PhoneNumberText.Text.Trim();
                }

                encrypted_password = password;

                if (hearaboutusPickerChampion.SelectedIndex == -1)
                {
                    question = "";
                }
                else
                {
                    string questionName = hearaboutusPickerChampion.Items[hearaboutusPickerChampion.SelectedIndex];
                    question = questionName;
                }

                DateTime updated_at = DateTime.Now;
                bool SendemailRoleChangedChampion = false;
                if (Application.Current.Properties.ContainsKey("UserRolewarrior"))
                {
                    //var imageSender = (Image)sender;
                    //var source = imageSender.Source as FileImageSource;
                    //imageSender.Source = "Checked.png";
                    //var filename = source.File;
                    var imageSender = (Image)chkPrayerChampion;
                    var source = chkPrayerChampion.Source as FileImageSource;
                    if (Convert.ToString(Application.Current.Properties["UserRolewarrior"]) != "warrior" && source != null && source.File == "Checked.png")
                    {
                        SendemailRoleChangedChampion = true;
                    }
                    else
                    {
                        SendemailRoleChangedChampion = false;
                    }
                }

                if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
                {
                    UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                }
                if (UserRole.Contains("admin"))
                {
                    var imageSender = (Image)chkActiveMember;
                    var sourceActiveMember = chkActiveMember.Source as FileImageSource;
                    if (sourceActiveMember != null && sourceActiveMember.File == "Checked.png")
                    {
                        is_active = "1";
                    }
                    else
                    {
                        is_active = "0";
                    }
                    var imageSenderRoleMember = (Image)chkMember;
                    var sourceRoleMember = chkMember.Source as FileImageSource;
                    if (sourceRoleMember != null && sourceRoleMember.File == "Checked.png")
                    {
                        UserRolemember = true;
                    }
                    var imageSenderRoleAdmin = (Image)chkAdmin;
                    var sourceRoleAdmin = chkAdmin.Source as FileImageSource;
                    if (sourceRoleAdmin != null && sourceRoleAdmin.File == "Checked.png")
                    {
                        UserRoleAdmin = true;
                    }
                    var imageSenderRoleChampion = (Image)chkPrayerChampion;
                    var sourceRoleChampion = chkPrayerChampion.Source as FileImageSource;
                    if (sourceRoleChampion != null && sourceRoleChampion.File == "Checked.png")
                    {
                        UserRolewarrior = true;
                    }
                    var imageSenderRoleModerator = (Image)chkModerator;
                    var sourceRoleModerator = chkModerator.Source as FileImageSource;
                    if (sourceRoleModerator != null && sourceRoleModerator.File == "Checked.png")
                    {
                        UserRoleModerator = true;
                    }
                }
                else
                {
                    is_active = "1";

                    if (UserID != string.Empty)
                    {
                        var loadingPageuser = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPageuser);
                        UserRoleDetails _UserUserRoleDetails = new UserRoleDetails();
                        var _datarole = await _UserUserRoleDetails.GetUserRoleDetails(new UserGetRoleModel { UserId = UserID });
                        await Navigation.RemovePopupPageAsync(loadingPageuser);
                        if (_datarole.Status == true)
                        {
                            string RoleName = Convert.ToString(_datarole.Role);
                            if (RoleName.Contains("member"))
                            {
                                chkMember.Source = "Checked.png";
                                UserRolemember = true;

                            }
                            else
                            {
                                UserRolemember = false;
                                chkMember.Source = "not_Checked.png";

                            }
                            if (RoleName.Contains("admin"))
                            {
                                UserRoleAdmin = true;
                                chkAdmin.Source = "Checked.png";

                            }
                            else
                            {
                                UserRoleAdmin = false;
                                chkAdmin.Source = "not_Checked.png";

                            }
                            if (RoleName.Contains("warrior"))
                            {
                                UserRolewarrior = true;
                                chkPrayerChampion.Source = "Checked.png";

                            }
                            else
                            {
                                UserRolewarrior = false;
                                chkPrayerChampion.Source = "not_Checked.png";

                            }
                            if (RoleName.Contains("moderator"))
                            {
                                UserRoleModerator = true;
                                chkModerator.Source = "Checked.png";

                            }
                            else
                            {
                                UserRoleModerator = false;
                                chkModerator.Source = "not_Checked.png";

                            }
                        }
                    }


                }
                bool Sendemail = false;
                if (Application.Current.Properties.ContainsKey("UserEmailaddress"))
                {
                    if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser"))
                    {
                        string UserUniqueIdLogin = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                        var imageSender = (Image)chkPrayerChampion;
                        var source = chkPrayerChampion.Source as FileImageSource;
                        if (Convert.ToString(Application.Current.Properties["UserEmailaddress"]) != null && Convert.ToString(Application.Current.Properties["UserEmailaddress"]) != String.Empty)
                        {
                            if (email != Convert.ToString(Application.Current.Properties["UserEmailaddress"]) && source != null && source.File == "Checked.png" && (UserUniqueIdLogin == UserUniqueID))
                            {

                                Sendemail = true;
                            }
                            else
                            {
                                Sendemail = false;
                            }
                        }
                    }
                }
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                UserProfile _userUserProfile = new UserProfile();
                var _data = await _userUserProfile.UpdateUserProfile(new UserProfileModel { uuid = UserID, email = email, user_name = user_name, first_name = first_name, last_name = last_name, gender = gender, street_address = street_address, city = city, state = state, zipcode = zipcode, country = Country, phone_number = phone_number, encrypted_password = encrypted_password, question = question, is_active = is_active, UserRolemember = UserRolemember, UserRoleAdmin = UserRoleAdmin, UserRolewarrior = UserRolewarrior, UserRoleModerator = UserRoleModerator, updated_at = updated_at });
                await Navigation.RemovePopupPageAsync(loadingPage);
                string msgStatus = "";
                if (_data.Status == false && _data.UserID == -1)
                {

                    msgStatus = "-1";
                    await Navigation.PushPopupAsync(new UpdateProfileFailedPopupPage(msgStatus));

                }
                else if (_data.Status == false && _data.UserID == -2)
                {

                    msgStatus = "-2";
                    await Navigation.PushPopupAsync(new UpdateProfileFailedPopupPage(msgStatus));
                }

                string userUniqueIDSearchUser = "";
                if (Application.Current.Properties.ContainsKey("userUniqueIDSearchUser"))
                {
                    userUniqueIDSearchUser = Convert.ToString(Application.Current.Properties["userUniqueIDSearchUser"]);
                }
                else
                {
                    userUniqueIDSearchUser = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                if (_data.Status == true && userUniqueIDSearchUser != string.Empty && SendemailRoleChangedChampion == true && _data.UserID > 0)
                {
                    bool result = false;
                    UserUniqueID = userUniqueIDSearchUser;
                    string uuid = UserUniqueID;
                    first_name = FirstNameText.Text.Trim();
                    email = EmailAddressText.Text.Trim();
                    string confirmation_token = Guid.NewGuid().ToString().Substring(0, 12);
                    string confirmation_sent_at = DateTime.Now.ToString();

                    var loadingPageEmailVerification = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPageEmailVerification);
                    InsertEmailVerificationData _InsertEmailVerificationData = new InsertEmailVerificationData();
                    var _dataEmail = await _InsertEmailVerificationData.InsertEmailVerificationDataRoleChangedChampionAdminTPZ(new EmailVerificationDataModel { uuid = uuid, email = email, confirmation_token = confirmation_token, confirmation_sent_at = confirmation_sent_at });
                    await Navigation.RemovePopupPageAsync(loadingPageEmailVerification);
                    if (_dataEmail.Status = true && _dataEmail.ChampionConfirmEmailID > 0)
                    {
                        msgStatus = "3";
                        await Navigation.PushPopupAsync(new UpdateProfileFailedPopupPage(msgStatus));
                    }
                }
                if (_data.UserID > 0)
                {
                    var imageSenderRoleChampion = (Image)chkPrayerChampion;
                    var sourceRoleChampion = chkPrayerChampion.Source as FileImageSource;
                    if (Sendemail == true && sourceRoleChampion != null && sourceRoleChampion.File == "Checked.png" && (Convert.ToString(Application.Current.Properties["UserUID"]) == userUniqueIDSearchUser))
                    {
                        first_name = FirstNameText.Text.Trim();
                        string unconfirmed_email = EmailAddressText.Text.Trim();
                        string confirmation_token = Guid.NewGuid().ToString().Substring(0, 12);

                        var loadingPageEmailVerification = new LoadingPopupPage();
                        await Navigation.PushPopupAsync(loadingPageEmailVerification);
                        UserEmailVerification _UserEmailVerification = new UserEmailVerification();
                        var _dataEmailVerification = await _UserEmailVerification.UpdateUserEmailVerificationModel(new UserEmailVerificationModel { uuid = UserID, unconfirmed_email = unconfirmed_email, confirmation_token = confirmation_token });
                        await Navigation.RemovePopupPageAsync(loadingPageEmailVerification);
                        if (_dataEmailVerification.Status == true)
                        {
                            Application.Current.Properties["userUniqueIDSearchUser"] = null;
                            msgStatus = "2";
                            await Navigation.PushPopupAsync(new UpdateProfileFailedPopupPage(msgStatus));
                        }
                    }
                    else if (SendemailRoleChangedChampion == false)
                    {
                        Application.Current.Properties["userUniqueIDSearchUser"] = null;
                        msgStatus = "1";
                        await Navigation.PushPopupAsync(new UpdateProfileFailedPopupPage(msgStatus));
                    }
                    Application.Current.Properties["userUniqueIDSearchUser"] = null;
                }
                Application.Current.Properties["userUniqueIDSearchUser"] = null;
            }
        }
        public async void GetUserRolesInformation(string UserUId)
        {
            if (UserUId != string.Empty)
            {
                var loadingPage = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPage);
                UserRoleDetails _UserUserRoleDetails = new UserRoleDetails();
                var _data = await _UserUserRoleDetails.GetUserRoleDetails(new UserGetRoleModel { UserId = UserUId });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true)
                {
                    string RoleName = Convert.ToString(_data.Role);
                    if (RoleName.Contains("member"))
                    {
                        chkMember.Source = "Checked.png";
                        //  ViewState["UserRolemember"] = "member";
                        Application.Current.Properties["UserRolemember"] = "member";
                    }
                    else
                    {
                        chkMember.Source = "not_Checked.png";
                        Application.Current.Properties["UserRolemember"] = null;
                    }
                    if (RoleName.Contains("admin"))
                    {
                        chkAdmin.Source = "Checked.png";
                        Application.Current.Properties["UserRoleAdmin"] = "admin";
                        // ViewState["UserRoleAdmin"] = "admin";
                    }
                    else
                    {
                        chkAdmin.Source = "not_Checked.png";
                        Application.Current.Properties["UserRoleAdmin"] = null;
                    }
                    if (RoleName.Contains("warrior"))
                    {
                        chkPrayerChampion.Source = "Checked.png";
                        Application.Current.Properties["UserRolewarrior"] = "warrior";
                        // chkRolePrayerChampion.Checked = true;
                        //  ViewState["UserRolewarrior"] = "warrior";
                    }
                    else
                    {
                        chkPrayerChampion.Source = "not_Checked.png";
                        Application.Current.Properties["UserRolewarrior"] = null;
                    }
                    if (RoleName.Contains("moderator"))
                    {
                        chkModerator.Source = "Checked.png";
                        Application.Current.Properties["UserRoleModerator"] = "moderator";
                        // chkRoleModerator.Checked = true;
                        //  ViewState["UserRoleModerator"] = "moderator";
                    }
                    else
                    {
                        chkModerator.Source = "not_Checked.png";
                        Application.Current.Properties["UserRoleModerator"] = null;
                    }
                }
            }
        }
        public async void GetUserProfileDetails(string UserId)
        {

            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            UserProfileDetails _UserProfileDetails = new UserProfileDetails();
            var _data = await _UserProfileDetails.GetUserProfileDetails(new UserGetProfileModel { UserId = UserId });
            await Navigation.RemovePopupPageAsync(loadingPage);
            if (_data.Status == true)
            {
                string Emailaddress = Convert.ToString(_data.email);
                Application.Current.Properties["UserEmailaddress"] = Emailaddress;
                //    ViewState["UserEmailaddress"] = Emailaddress;
                EmailAddressText.Text = Emailaddress;
                UserNameText.Text = _data.user_name;
                FirstNameText.Text = _data.first_name;
                LastNameText.Text = _data.last_name;
                string encrypted_password = _data.encrypted_password;
                Application.Current.Properties["UserPassword"] = encrypted_password;
                //   ViewState["UserPassword"] = encrypted_password;
                string Gender = Convert.ToString(_data.gender);
                if (Gender != null && Gender != String.Empty)
                {
                    GnderPicker.SelectedItem = Gender;
                }
                StreetAddressText.Text = Convert.ToString(_data.street_address);
                CityText.Text = Convert.ToString(_data.city);
                string State = Convert.ToString(_data.state);
                if (State != null && State != String.Empty)
                {
                    StatePickerChampion.SelectedItem = State;
                }

                if (Convert.ToString(_data.zipcode) != "-1")
                {
                    ZipCodeText.Text = Convert.ToString(_data.zipcode);
                }
                else
                {
                    ZipCodeText.Text = "";
                }
                if (Convert.ToString(_data.country) != "")
                {
                    CountryChampion.SelectedItem = _data.country;
                }

                PhoneNumberText.Text = Convert.ToString(_data.phone_number);
                //  hearaboutusPickerChampion.Text = Convert.ToString(ds.Tables[0].Rows[0]["question"].ToString());
                string optInTxtMessage = Convert.ToString(_data.opt);
                if (optInTxtMessage == "1")
                {
                    otpInTxtMessage.Text = "Yes";
                }
                else
                {
                    otpInTxtMessage.Text = "No";
                }
                string Active = Convert.ToString(_data.is_active);


                var imageSender = (Image)chkActiveMember;
                var source = chkActiveMember.Source as FileImageSource;
                if (Active == "1")
                {
                    chkActiveMember.Source = "Checked.png";
                }
                else
                {
                    chkActiveMember.Source = "not_Checked.png";
                }

            }


        }
        void OnActiveMemberTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            else if (filename == "not_Checked.png")
            {
                imageSender.Source = "Checked.png";
            }
            // Do something

        }
        void OnchkMemberTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            else if (filename == "not_Checked.png")
            {
                imageSender.Source = "Checked.png";
            }
            // Do something

        }
        void chkModeratorTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            else if (filename == "not_Checked.png")
            {
                imageSender.Source = "Checked.png";
            }
            // Do something

        }
        void chkPrayerChampionTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            else if (filename == "not_Checked.png")
            {
                imageSender.Source = "Checked.png";
            }
            // Do something

        }
        void chkAdminTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            imageSender.Source = "Checked.png";
            var filename = source.File;
            if (filename == "Checked.png")
            {
                imageSender.Source = "not_Checked.png";
            }
            else if (filename == "not_Checked.png")
            {
                imageSender.Source = "Checked.png";
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

        public bool ValidateAllControls()
        {
            bool FirstNameRequired = true;
            bool LastNameRequired = true;
            bool UserNameRequired = true;
            bool EmailAddressRequired = true;
            bool ValidEmailAddress = true;
            if (FirstNameText.Text == "" || FirstNameText.Text == null)
            {
                FirstNameText.Placeholder = "First Name Required";
                FirstNameText.PlaceholderColor = Color.Red;
                FirstNameRequired = false;
            }
            else
            {
                FirstNameText.PlaceholderColor = Color.Black;
                FirstNameRequired = true;
            }
            if (LastNameText.Text == "" || LastNameText.Text == null)
            {
                LastNameText.Placeholder = "Last Name Required";
                LastNameText.PlaceholderColor = Color.Red;
                LastNameRequired = false;
            }
            else
            {
                LastNameText.PlaceholderColor = Color.Black;
                LastNameRequired = true;
            }
            if (UserNameText.Text == "" || UserNameText.Text == null)
            {
                UserNameText.Placeholder = "User Name Required";
                UserNameText.PlaceholderColor = Color.Red;
                UserNameRequired = false;
            }
            else
            {
                UserNameText.PlaceholderColor = Color.Black;
                UserNameRequired = true;
            }

            if (EmailAddressText.Text == "" || EmailAddressText.Text == null)
            {
                EmailAddressText.Placeholder = "Email Address Required";
                EmailAddressText.PlaceholderColor = Color.Red;
                EmailAddressRequired = false;
            }
            else
            {
                EmailAddressRequired = true;
                bool Valid = ValidateEmailAddress(EmailAddressText.Text.Trim());
                if (Valid == false)
                {
                    EmailAddressText.Placeholder = "Enter Valid Email Address";
                    EmailAddressText.BackgroundColor = (Color.FromHex("#F4C6C9"));
                    ValidEmailAddress = false;
                }
                else
                {
                    EmailAddressText.BackgroundColor = (Color.FromHex("#eaeaea"));
                    EmailAddressText.PlaceholderColor = Color.Black;
                    ValidEmailAddress = true;
                }
            }
            return FirstNameRequired && LastNameRequired && UserNameRequired && EmailAddressRequired && ValidEmailAddress;
        }
    }
}

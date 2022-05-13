using DrawerMenu.Core;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DrawerMenu.Pages
{
    public partial class BlockWords : ContentPage
    {
        public BlockWords()
        {
            InitializeComponent();
            lblblockWordsText.Margin = new Thickness(10, 30);
            //  NavigationPage.SetTitleIcon(this, "logoios.png");
            BindBlockWords();
        }


        private async void OnEditWordClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            Int32 WordId = Convert.ToInt32(item.CommandParameter.ToString());
            Application.Current.Properties["EditWordId"] = WordId;
            PostNotificationData _PostNotificationData = new PostNotificationData();
            var _data = await _PostNotificationData.GetBlokWordById(new AdminBlockWordsViewClassModel { Id = WordId });

            if (_data.Status == true)
            {
                btnRestrictWords.Text = "Update";
                string word = Convert.ToString(_data.strWord);
                RestrictWordEntry.Text = word;

            }

        }
        private async void OnDeleteWordClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string wordID = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Would you like delete this Word", "Yes", "No");
            if (answer)
            {
                PostNotificationData _DeleteNotificationTime = new PostNotificationData();
                var _data = await _DeleteNotificationTime.DeleteBlockWord(new AdminBlockWordsViewClassModel { Id = Convert.ToInt32(wordID) });
                if (_data.Status == true)
                {
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    BindBlockWords();
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }

            }
            else
            {
                // if No make all user alarm Active

            }
        }
        public async void BindBlockWords()
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            string UserUId = "";
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                UserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
            }
            if (UserUId != string.Empty)
            {
                PostNotificationData objNotificationData = new PostNotificationData();
                List<AdminBlockWordsViewClassModel> _list = new List<AdminBlockWordsViewClassModel>();
                _list = await objNotificationData.getAllBlockWords();
                if (_list != null)
                {
                    NotificationTimeView.ItemsSource = _list;
                    if (_list.Count > 0)
                    {
                        NotificationTimeView.IsVisible = true;
                        lblblockWordsText.Text = "Following are the Block Words:";
                        //SwitchRemainder.IsToggled = true;
                    }
                    else
                    {
                        NotificationTimeView.IsVisible = false;
                        lblblockWordsText.Text = "There is no Block Words Click Add to set";
                        //  SwitchRemainder.IsToggled = false;
                    }
                }
                else
                {
                    NotificationTimeView.IsVisible = false;
                    NotificationTimeView.ItemsSource = null;
                    lblblockWordsText.Text = "There is no Block Words Click Add to set";
                }
            }
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddPrayerReminder());
            // await Navigation.PushAsync(new AddPrayerReminder());
        }
        private async void OnbtnRestrictWordsClicked(object sender, EventArgs e)
        {
            bool Valid = ValidateAllControls();
            if (Valid == true)
            {
                if (btnRestrictWords.Text == "Submit")
                {
                    string RestrictWord = RestrictWordEntry.Text.Trim();
                    string LoggedInUserUId = "";
                    if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
                    {
                        LoggedInUserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                    }
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    PostPrayer _PostPrayer = new PostPrayer();
                    var _data = await _PostPrayer.InsertRestrictWord(new InsertRestrictWordModel { strWord = RestrictWord, Createdby = LoggedInUserUId });

                    if (_data.Status == true && _data.Message == "Saved")
                    {
                        BindBlockWords();
                        RestrictWordEntry.Text = "";
                        await Navigation.RemovePopupPageAsync(loadingPage);
                    }
                    else if (_data.Status == true && _data.Message == "Already Exists")
                    {
                        RestictWordValidation.Text = "Already Exists";
                        await Navigation.RemovePopupPageAsync(loadingPage);
                    }
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
                if (btnRestrictWords.Text == "Update")
                {
                    int UpdatewordId = 0;
                    string RestrictWord = RestrictWordEntry.Text.Trim();
                    UpdatewordId = Convert.ToInt32(Application.Current.Properties["EditWordId"]);
                    string LoggedInUserUId = "";
                    if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
                    {
                        LoggedInUserUId = Convert.ToString(Application.Current.Properties["UserUID"]);
                    }
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    PostPrayer _PostPrayer = new PostPrayer();
                    var _data = await _PostPrayer.UpdateRestrictWordEntry(new InsertRestrictWordModel { Id = UpdatewordId, strWord = RestrictWord, Createdby = LoggedInUserUId });

                    if (_data.Status == true && _data.Message == "updated")
                    {
                        BindBlockWords();
                        RestrictWordEntry.Text = "";
                        Application.Current.Properties["EditWordId"] = null;
                        btnRestrictWords.Text = "Submit";
                        await Navigation.RemovePopupPageAsync(loadingPage);
                    }
                    else if (_data.Status == false && _data.Message == "Already Exists")
                    {
                        RestictWordValidation.Text = "Already Exists";
                        await Navigation.RemovePopupPageAsync(loadingPage);
                    }
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
        }
        public bool ValidateAllControls()
        {
            bool RestrictWordRequired = true;

            if (RestrictWordEntry.Text == "" || RestrictWordEntry.Text == null)
            {
                RestrictWordEntry.Placeholder = "Word Required";
                RestrictWordEntry.PlaceholderColor = Color.Red;
                RestrictWordRequired = false;
            }
            else
            {
                RestrictWordRequired = true;
            }
            return RestrictWordRequired;
        }
        async void BackItem_Clicked(object sender, EventArgs e)
        {

            Application.Current.Properties["userUniqueIDSearchUser"] = null;
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            await Navigation.PushModalAsync(new MenuPageNotifications());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
    }
}
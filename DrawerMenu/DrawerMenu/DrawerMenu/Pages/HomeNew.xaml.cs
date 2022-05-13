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

namespace DrawerMenu
{
    public partial class HomeNew : ContentPage
    {
        private AddPrayerRequestPopupPage _AddPrayerRequestPopup;
        static bool _lock = false;
        // ObservableCollection<ListClass> employees = new ObservableCollection<ListClass>();
        public HomeNew()
        {
            //  NavigationPage.SetHasNavigationBar(this, false);
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
            }
            InitializeComponent();
            ShowDataRoleWise();
            GetLiveCounter();
            string Request = "MyRequestsUserid";
            if (Application.Current.Properties.ContainsKey("Request"))
            {
                Request = Convert.ToString(Application.Current.Properties["Request"]);
                if (Request != "" && Request != string.Empty && Request == "PrayedForRequests")
                {
                    GetAllPrayersIhavePostedFor();
                }
                else
                {
                    if (Request != "" && Request != string.Empty)
                    {
                        BindingContext = new PrayersViewModel(Request);
                        PrayerView.RefreshCommand.Execute(null);
                        Application.Current.Properties["Request"] = null;
                    }
                    else
                    {
                        BindingContext = new PrayersViewModel("MyRequestsUserid");
                        PrayerView.RefreshCommand.Execute(null);
                        Application.Current.Properties["Request"] = null;
                    }
                }
            }
            else
            {
                BindingContext = new PrayersViewModel(Request);
                PrayerView.RefreshCommand.Execute(null);
                Application.Current.Properties["Request"] = null;
            }
            _AddPrayerRequestPopup = new AddPrayerRequestPopupPage();
        }
        public void ShowDataRoleWise()
        {
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("member"))
                {
                    lblAddPrayerReques.IsVisible = false;
                    lblWaitingText.WidthRequest = 310;
                }
                if (UserRole.Contains("member") && !UserRole.Contains("admin") && !UserRole.Contains("warrior"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }

                if (UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }
                if (UserRole.Contains("moderator"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }
                if (UserRole.Contains("member") && UserRole.Contains("warrior") && UserRole.Contains("admin") && UserRole.Contains("moderator"))
                {
                    lblAddPrayerReques.IsVisible = true;
                }
                if (!UserRole.Contains("member") && !UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("moderator"))
                {
                    lblAddPrayerReques.IsVisible = false;
                    lblWaitingText.WidthRequest = 310;
                }
            }
        }
        public async void GetAllPrayersIhavePostedFor()
        {
            Boolean IsPostPrayerButtonVisible = false;
            Boolean IsDeleteButtonVisible = false;
            string UserRole = "";
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {

                UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("warrior") || UserRole.Contains("admin"))
                {
                    IsPostPrayerButtonVisible = true;
                }
                else
                {
                    IsPostPrayerButtonVisible = false;
                }
                if (UserRole.Contains("admin") || UserRole.Contains("moderator"))
                {
                    IsDeleteButtonVisible = true;
                }
                else
                {
                    IsDeleteButtonVisible = false;
                }

            }

            string MyRequestsUserid = "";
            if (Application.Current.Properties.ContainsKey("UserUID"))
            {

                MyRequestsUserid = Convert.ToString(Application.Current.Properties["UserUID"]);
                Prayers objIhavePostedFor = new Prayers();
                List<PrayersModel> _list = new List<PrayersModel>();
                _list = await objIhavePostedFor.getAllPrayersIhavePsitedFor(MyRequestsUserid, IsPostPrayerButtonVisible, IsDeleteButtonVisible);
                PrayerView.ItemsSource = _list;
                lblloadMorePrayers.IsVisible = false;
                // lblloadMorePrayers.HeightRequest = 0;
            }
        }

        public async void GetLiveCounter()
        {

            UserStats _user = new UserStats();
            var _data = await _user.GetStats();
            if (_data.Status == true)
            {
                //  lblOfferedText.Text = _data.prayers_offered + " Offered";
                //   lblRequestedText.Text = _data.prayers_requested + " Requested";
                lblWaitingText.Text = _data.prayers_needed + " Waiting";
            }
            // lblloadMorePrayers.IsVisible = true;
            //  await Navigation.RemovePopupPageAsync(loadingPage);
        }


        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
           => ((ListView)sender).SelectedItem = null;

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            else
            {
                var _item = (PrayersModel)e.SelectedItem;
                string RequiredIDs = _item.RequiredIDs;
                if (RequiredIDs != string.Empty)
                {
                    string[] words = RequiredIDs.Split('@');
                    string RequestedPostedUserId = Convert.ToString(words[0]);
                    string PrayerRequestId = Convert.ToString(words[1]);
                    string IsWaitingApproval = Convert.ToString(words[3]);
                    Application.Current.Properties["IsWaitingApproval"] = IsWaitingApproval;
                    await Navigation.PushModalAsync(new MenuPageMemberRequest(RequestedPostedUserId, PrayerRequestId));
                }
                else
                {
                    ((ListView)sender).SelectedItem = null;
                }
            }


            //string RequestedPostedUserId = Convert.ToString(words[0]);
            //string PrayerRequestId = Convert.ToString(words[1]);

            //  

            // await DisplayAlert("Selected", e.SelectedItem.ToString(), "OK");

            //Deselect Item
            // ((ListView)sender).SelectedItem = null;
        }


        protected override bool OnBackButtonPressed()
        {
            //DisplayAlert("Question?", "Are You sure you want to exit", "Yes", "No");
            //Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            return false;
        }

        public class ListClass
        {
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string NumberOfPrayer { get; set; }
            public string UserName { get; set; }
            public string Title { get; set; }
            public string Detail { get; set; }

        }

        private async void WaitingTextClicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "WaitingForPrayers";
            await Navigation.PushModalAsync(new MenuPage());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }

        private async void AddPrayerRequestClicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        private async void OnTapGestureRecognizerTappedHome(object sender, EventArgs e)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            var filename = source.File;
            if (filename == "Home.png" || filename == "HomeSelected.png")
            {
                //HomeSelected.Source = "HomeSelected.png";
                //AddPrayerSelected.Source = "AddPrayer.png";
                //ProfileSelected.Source = "Profile.png";
            }

            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        private async void OnTapGestureRecognizerTappedAddPrayer(object sender, EventArgs e)
        {
            var imageSender = (Image)sender;
            var source = imageSender.Source as FileImageSource;
            var filename = source.File;
            if (filename == "AddPrayer.png" || filename == "AddPrayerSelected.png")
            {
                //HomeSelected.Source = "Home.png";
                //AddPrayerSelected.Source = "AddPrayerSelected.png";
                //ProfileSelected.Source = "Profile.png";

            }

            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        private async void PostPrayerClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string RequiredIDs = Convert.ToString(item.CommandParameter.ToString());
            string[] words = RequiredIDs.Split('@');
            string RequestedPostedUserId = Convert.ToString(words[0]);
            string PrayerRequestId = Convert.ToString(words[1]);
            string IsWaitingApproval = Convert.ToString(words[3]);
            Application.Current.Properties["IsWaitingApproval"] = IsWaitingApproval;

            //  Application.Current.Properties["RequestPostedUserId"] = RequestedPostedUserId;
            //    Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
            // await Navigation.PushAsync(new MemberPrayerRequest());
            await Navigation.PushModalAsync(new MenuPageMemberRequest(RequestedPostedUserId, PrayerRequestId));
        }


        private async void OnApprovePrayerClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string PrayerRequestId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Are you sure you want to Approve?", "Yes", "No");
            if (answer)
            {
                await Task.Delay(1000);
                var loadingPagedelete = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPagedelete);
                ApprovePrayer _ApprovePrayer = new ApprovePrayer();
                var _data = await _ApprovePrayer.ApprovePrayerRequest(new ApprovePrayerModel { PrayerRequestId = PrayerRequestId });
                await Navigation.RemovePopupPageAsync(loadingPagedelete);
                if (_data.Status == true)
                {
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    BindingContext = new PrayersViewModel("MyRequestsUserid");
                    PrayerView.RefreshCommand.Execute(null);
                    Application.Current.Properties["Request"] = null;
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
            else
            {
                // if No make all user alarm Active
            }

        }
        private async void OnDeleteRequestPrayerClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string PrayerRequestId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Are you sure you want to delete?", "Yes", "No");
            if (answer)
            {
                await Task.Delay(1000);
                var loadingPagedelete = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPagedelete);
                DeletePrayer _DeletePrayer = new DeletePrayer();
                var _data = await _DeletePrayer.DeletePrayerRequest(new DeletePrayerModel { PrayerRequestId = PrayerRequestId });
                await Navigation.RemovePopupPageAsync(loadingPagedelete);
                if (_data.Status == true)
                {
                    var loadingPage = new LoadingPopupPage();
                    await Navigation.PushPopupAsync(loadingPage);
                    BindingContext = new PrayersViewModel("MyRequestsUserid");
                    PrayerView.RefreshCommand.Execute(null);
                    Application.Current.Properties["Request"] = null;
                    await Navigation.RemovePopupPageAsync(loadingPage);
                }
            }
            else
            {
                // if No make all user alarm Active
            }

        }

        private async void OnSpamPrayerClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string PrayerRequestId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Report Content:", "Notify Administrator of Offensive Content.", "Yes", "No");
            if (answer)
            {
                await Task.Delay(1000);
                var loadingPage = new LoadingPopupPage();
                PostPrayer _PostPrayer = new PostPrayer();
                string UserId = "";
                string RequestId = "";
                string FirstName = "";
                string LastName = "";
                string EmailAddress = "";
                if (Application.Current.Properties.ContainsKey("UserFirstName"))
                {
                    var UserFirstName = Application.Current.Properties["UserFirstName"];
                    if (UserFirstName != null)
                    {
                        FirstName = Convert.ToString(UserFirstName);
                    }
                }
                if (Application.Current.Properties.ContainsKey("UserLastName"))
                {
                    var UserLastName = Application.Current.Properties["UserLastName"];
                    if (UserLastName != null)
                    {
                        LastName = Convert.ToString(UserLastName);
                    }
                }

                if (Application.Current.Properties.ContainsKey("UserEmail"))
                {
                    var UserEmail = Application.Current.Properties["UserEmail"];
                    if (UserEmail != null)
                    {
                        EmailAddress = Convert.ToString(UserEmail);
                    }
                }
                UserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                RequestId = Convert.ToString(PrayerRequestId);


                await Navigation.PushPopupAsync(loadingPage);
                var _data = await _PostPrayer.InsertOffensivePrayerRequest(new InsertOffensivePrayerModel { LoginUserId = UserId, PrayerRequestId = RequestId, UserFirstName = FirstName, UserLastName = LastName, UserEmailAddress = EmailAddress });
                await Navigation.RemovePopupPageAsync(loadingPage);
                if (_data.Status == true)
                {
                    await DisplayAlert("", "Thank you. The Administrator has been notified of the Offensive content.", "OK");
                }
                else
                {
                    await DisplayAlert("", "Thank you. There is some technical issue. Please contact theprayerzone@thehopeline.com.", "OK");
                }
            }
            else
            {
                // if No make all user alarm Active
            }

        }


        private async void OnTapLoadMoreTapped(object sender, EventArgs args)
        {
            //  PrayerView.RefreshCommand.Execute(null);
            _lock = false;
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            lblloadMorePrayers.IsVisible = false;
            PrayerView.RefreshCommand.Execute(null);
            while (!_lock)
            {
                await Task.Delay(1000);
                if (_lock == true)
                {
                    break;
                }

            }

            _lock = false;
            lblloadMorePrayers.IsVisible = true;
            await Navigation.RemovePopupPageAsync(loadingPage);

        }
        class PrayersViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<PrayersModel> _items = new ObservableCollection<PrayersModel>();
            public ObservableCollection<PrayersModel> Items
            {
                get { return _items; }
                set
                {
                    if (_items == value)
                        return;

                    _items = value;
                    OnPropertyChanged("Items");
                } //I have also tried with Title property
            }
            public PrayersViewModel(string Request)
            {

                RefreshDataCommand = new Command(
                    async () => await RefreshData(Request));
            }

            public ICommand RefreshDataCommand { get; }
            async Task RefreshData(string Request)
            {

                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                //Load Data Here
                //bindList(4,6);
                await callAPI(Request);

                IsBusy = false;
                //await Task.Delay(2000);


            }
            public async Task callAPI(string Request)
            {

                Boolean IsPostPrayerButtonVisible = false;
                Boolean IsDeleteButtonVisible = false;
                //   Boolean IsOffensiveButtonVisible = false;
                pageNo = pageNo + 1;
                Prayers objRestService = new Prayers();
                //var _data = await objRestService.GetNews("cnn");
                List<PrayersModel> _list = new List<PrayersModel>();
                string UserRole = "";
                string LoginUserId = "";
                Int32 UserRoleAdmin = 0;
                if (Application.Current.Properties.ContainsKey("UserUID"))
                {
                    LoginUserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                }
                if (Application.Current.Properties.ContainsKey("UserRole"))
                {

                    UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                    if (UserRole.Contains("warrior") || UserRole.Contains("admin"))
                    {
                        IsPostPrayerButtonVisible = true;
                    }
                    else
                    {
                        IsPostPrayerButtonVisible = false;
                    }
                    if (UserRole.Contains("admin") || UserRole.Contains("moderator"))
                    {
                        IsDeleteButtonVisible = true;
                    }
                    else
                    {
                        IsDeleteButtonVisible = false;
                    }
                    if (UserRole.Contains("admin"))
                    {
                        UserRoleAdmin = 1;
                    }
                }
                if (Request == "WaitingForPrayers")
                {

                    _list = await objRestService.getAllPrayers(PageNo, Request, "1", IsPostPrayerButtonVisible, IsDeleteButtonVisible, LoginUserId, UserRoleAdmin);

                }
                else if (Request == "MyRequestsUserid")
                {
                    _list = await objRestService.getAllPrayers(PageNo, Request, "0", IsPostPrayerButtonVisible, IsDeleteButtonVisible, LoginUserId, UserRoleAdmin);
                }
                else if (Request == "MyRequests")
                {
                    if (Application.Current.Properties.ContainsKey("UserUID"))
                    {
                        string MyRequestsUserid = "";
                        MyRequestsUserid = Convert.ToString(Application.Current.Properties["UserUID"]);
                        _list = await objRestService.getAllPrayers(PageNo, MyRequestsUserid, "0", IsPostPrayerButtonVisible, IsDeleteButtonVisible, LoginUserId, UserRoleAdmin);
                    }

                }

                //await Task.Delay(2000);
                //comment following code for paging
                // Items = new ObservableCollection<PrayersModel>();
                foreach (var item in _list)
                {
                    _items.Add(new PrayersModel
                    {
                        RowNumber = item.RowNumber,
                        User_id = item.User_id,
                        user_name = item.user_name,
                        id = item.id,
                        isdeleted = item.isdeleted,
                        text = item.text,
                        title = item.title,
                        updated_at = item.updated_at,
                        has_comments = item.has_comments,
                        first_name = item.first_name,
                        last_name = item.last_name,
                        email = item.email,
                        created_at = item.created_at,
                        PrayerRequestId = item.PrayerRequestId,
                        TotalPrayerPosted = item.TotalPrayerPosted,
                        RequiredIDs = item.RequiredIDs,
                        Updateddate = item.Updateddate,
                        IsDeleteButtonVisible = item.IsDeleteButtonVisible,
                        IsPostPrayerButtonVisible = item.IsPostPrayerButtonVisible,
                        IsOffensiveButtonVisible = item.IsOffensiveButtonVisible,
                        IsWaitingApproval = item.IsWaitingApproval,
                        IsApprovePrayerButtonVisible = item.IsApprovePrayerButtonVisible
                    }

                    );

                }
                //  Home.lblloadMorePrayers.IsVisible = false;
                _lock = true;
                if (_list.Count < 10)
                {
                    IsLoadMore = false;
                }
                else
                {
                    IsLoadMore = true;
                }
                objRestService = null;
                _list = null;

            }

            bool busy;
            public bool IsBusy
            {
                get { return busy; }
                set
                {
                    busy = value;
                    OnPropertyChanged();
                    ((Command)RefreshDataCommand).ChangeCanExecute();
                }
            }
            bool isLoadMore;
            public bool IsLoadMore
            {
                get { return isLoadMore; }
                set
                {
                    isLoadMore = value;
                    OnPropertyChanged("IsLoadMore");
                    //  ((Command)RefreshDataCommand).ChangeCanExecute();
                }
            }
            bool footer;
            public bool Footer
            {
                get { return footer; }
                set
                {
                    footer = value;
                    OnPropertyChanged("Footer");

                }
            }

            int pageNo;
            public int PageNo
            {
                get { return pageNo; }
                set
                {
                    if (pageNo == value)
                        return;

                    pageNo = value;
                    OnPropertyChanged("PageNo");
                } //I have also tried with Title property }
            }

            public object HttpContext { get; private set; }

            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}

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

namespace DrawerMenu
{
    public partial class MemberPrayerRequests : ContentPage
    {
        static bool Showmessage = false;
        static bool Hidemessage = false;
        private AddPrayerRequestPopupPage _AddPrayerRequestPopup;
        private UpdatePrayerRequestPopupPage _UpdatePrayerRequestPopup;
        private UpdatePrayerCommentPopupPage _UpdatePrayerCommentPopup;
        private AddPrayerCommentPopupPage _AddPrayerCommentPopupPage;
        string RequestPostedUserIdNewAddComment = "";
        string PrayerRequestIdNewAddComment = "";
        string RequestPostedUserId = "";
        string PrayerRequestId = "";

        double mainheight;
        public double MainHeight
        {
            get { return mainheight; }
            set
            {
                mainheight = value;
                // OnPropertyChanged("IsLoadMore");
                //  ((Command)RefreshDataCommand).ChangeCanExecute();
            }
        }
        public MemberPrayerRequests()
        {
            string Platform = Device.RuntimePlatform;
            NavigationPage.SetTitleIcon(this, "logoios.png");
            InitializeComponent();
            GetLiveCounter();
            _AddPrayerRequestPopup = new AddPrayerRequestPopupPage();
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
        private async void BackButtonClicked(object sender, EventArgs e)
        {
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            Application.Current.Properties["Request"] = "MyRequestsUserid";
            await Navigation.PushModalAsync(new MenuPage());
            await Navigation.RemovePopupPageAsync(loadingPage);
        }
        public async void GetLiveCounter()
        {
            string UserId = "";
            string UserRole = "";
            //var loadingPage = new LoadingPopupPage();
            //await Navigation.PushPopupAsync(loadingPage);
            UserStats _user = new UserStats();
            var _data = await _user.GetStats();
            if (_data.Status == true)
            {
                // lblOfferedText.Text = _data.prayers_offered + " Offered";
                // lblRequestedText.Text = _data.prayers_requested + " Requested";
                lblWaitingText.Text = _data.prayers_needed + " Waiting";
            }
            //  await Navigation.RemovePopupPageAsync(loadingPage);
            ShowDataRoleWise();
            if (Application.Current.Properties.ContainsKey("RequestPostedUserId") && Application.Current.Properties.ContainsKey("PrayerRequestId"))
            {
                RequestPostedUserId = Convert.ToString(Application.Current.Properties["RequestPostedUserId"]);
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestId"]);
                Application.Current.Properties["RequestPostedUserIdNewAddComment"] = RequestPostedUserId;
                Application.Current.Properties["PrayerRequestIdNewAddComment"] = PrayerRequestId;
                Application.Current.Properties["PrayerRequestDeleteApprove"] = PrayerRequestId;
                GetPrayerRequestDetails(RequestPostedUserId, PrayerRequestId);
            }
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                UserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                if (UserRole.Contains("admin") && UserRole.Contains("moderator"))
                {
                    btnDeleteCommentMain.IsVisible = true;
                }
                else
                {
                    btnDeleteCommentMain.IsVisible = false;
                }
                if (UserRole.Contains("warrior") && (UserId != RequestPostedUserId))
                {
                    btnAddComment.IsVisible = true;
                    btnAddCommentonprayer.IsVisible = true;
                }
                if (UserId == RequestPostedUserId)
                {
                    btnAddComment.IsVisible = true;
                    btnAddCommentonprayer.IsVisible = true;
                }
            }

            if (Application.Current.Properties.ContainsKey("IsWaitingApproval"))
            {
                string IsWaitingApproval = Convert.ToString(Application.Current.Properties["IsWaitingApproval"]);
                if (IsWaitingApproval != "")
                {
                    IsWaitingApproval = Convert.ToString(IsWaitingApproval);
                }
                if (IsWaitingApproval == "1" && UserId == RequestPostedUserId)
                {
                    btnAddCommentonprayer.IsVisible = false;
                    btnAddComment.IsVisible = false;
                    lblWaitingApproval.IsVisible = true;
                    lblWaitingApproval.Text = "Your Prayer is Waiting for Approval.....";
                }

                if (IsWaitingApproval == "1" && UserRole.Contains("admin"))
                {
                    btnAddCommentonprayer.IsVisible = false;
                    btnAddComment.IsVisible = false;
                    btnApproveRequest.IsVisible = true;

                }
            }
            Application.Current.Properties["RequestPostedUserId"] = null;
            Application.Current.Properties["PrayerRequestId"] = null;
            Application.Current.Properties["IsWaitingApproval"] = null;
        }

        private async void OnDeleteRequestPrayerClicked(object sender, EventArgs e)
        {

            if (Application.Current.Properties.ContainsKey("PrayerRequestDeleteApprove"))
            {
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestDeleteApprove"]);
                var DeletePrayerAdminpopUp = new DeleteSinglePrayerAdminpopUp();
                Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                await Navigation.PushPopupAsync(DeletePrayerAdminpopUp);
            }

        }
        private async void OnApprovePrayerClicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("PrayerRequestId"))
            {
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestDeleteApprove"]);
                var DeletePrayerAdminpopUp = new ApproveSinglePrayerAdminpopUp();
                Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                await Navigation.PushPopupAsync(DeletePrayerAdminpopUp);
            }

        }

        public async void GetPrayerRequestDetails(string RequestedPostedUserId, string PrayerRequestID)
        {
            lblPrayerTitle.Text = "";
            lblPrayerDescription.Text = "";
            string MyRequestsUserid = "";
            string RequestPostedUserName = "";
            if (Application.Current.Properties.ContainsKey("UserUID"))
            {
                MyRequestsUserid = Convert.ToString(Application.Current.Properties["UserUID"]);
                if (MyRequestsUserid == RequestedPostedUserId)
                {
                    RequestPostedUserName = "You";
                    spamPrayer.IsVisible = false;
                }
                else
                {
                    RequestPostedUserName = "";
                    spamPrayer.IsVisible = true;
                }
            }
            var loadingPage = new LoadingPopupPage();
            await Navigation.PushPopupAsync(loadingPage);
            UserSinglePrayerDetails _UserSinglePrayerDetails = new UserSinglePrayerDetails();
            var _data = await _UserSinglePrayerDetails.GetSinglePrayerDetails(new UserGetSinglePrayerModel { RequestedPostedUserId = RequestedPostedUserId, PrayerRequestID = PrayerRequestID, RequestPostedUserName = RequestPostedUserName });
            await Navigation.RemovePopupPageAsync(loadingPage);
            if (_data.Status == true)
            {
                lblPrayerTitle.Text = _data.Title;
                lblPrayerDescriptionwithoutquoteright.Text = _data.Text;
                lblPrayerDescription.Text = lblPrayerDescriptionquoteleft.Text + " " + _data.Text + " " + lblPrayerDescriptionquoteright.Text;
                lblPrayerOtheInfo.Text = _data.OtherInformation;
                if (MyRequestsUserid == string.Empty)
                {
                    MyRequestsUserid = _data.user_id;
                }
                BindComments(PrayerRequestID, MyRequestsUserid);


            }


        }

        private async void OnSpamCommentClicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("PrayerRequestSpamId"))
            {
                string PrayerRequestId = "";
                string PrayerCommentId = "";
                string CommentText = "";
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestSpamId"]);
                var item = (Xamarin.Forms.Button)sender;
                // PrayerCommentId = Convert.ToString(item.CommandParameter.ToString());

                string RequiredInfo = Convert.ToString(item.CommandParameter.ToString());
                if (RequiredInfo != string.Empty)
                {
                    string[] words = RequiredInfo.Split('@');
                    PrayerCommentId = Convert.ToString(words[0]);
                    CommentText = Convert.ToString(words[1]);
                }

                var answer = await DisplayAlert("Report Content:", "Notify Administrator of Offensive Content.", "Yes", "No");
                if (answer)
                {
                    string RequestId = "";
                    string CommentId = "";
                    string FirstName = "";
                    string LastName = "";
                    string EmailAddress = "";
                    await Task.Delay(1000);
                    var loadingPage = new LoadingPopupPage();
                    PostPrayer _PostPrayer = new PostPrayer();
                    string UserId = "";
                    RequestId = PrayerRequestId;
                    CommentId = PrayerCommentId;
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



                    await Navigation.PushPopupAsync(loadingPage);
                    var _data = await _PostPrayer.InsertOffensiveComments(new InsertOffensiveCommentModel { LoginUserId = UserId, PrayerRequestId = RequestId, PrayerCommentId = CommentId, PrayerCommentText = CommentText, UserFirstName = FirstName, UserLastName = LastName, UserEmailAddress = EmailAddress });
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




        }
        private async void OnSpamPrayerClicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("PrayerRequestSpamId"))
            {
                string PrayerRequestId = "";
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestSpamId"]);
                var answer = await DisplayAlert("Report Content:", "Notify Administrator of Offensive Content.", "Yes", "No");
                if (answer)
                {
                    string RequestId = "";
                    string FirstName = "";
                    string LastName = "";
                    string EmailAddress = "";
                    await Task.Delay(1000);
                    var loadingPage = new LoadingPopupPage();
                    PostPrayer _PostPrayer = new PostPrayer();

                    string UserId = "";

                    RequestId = PrayerRequestId;
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




        }
        public async void BindComments(string PrayerRequestID, string MyRequestsUserid)
        {

            string UserRole = "";
            Int32 UserRoleAdmin = 0;
            Boolean IsDeleteButtonVisible = false;
            Boolean IsApproveCommentButtonVisible = false;
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserLastName"))
            {
                UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
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
                    IsApproveCommentButtonVisible = true;
                }
                else
                {
                    UserRoleAdmin = 0;
                    IsApproveCommentButtonVisible = false;
                }
            }
            Application.Current.Properties["HasComments"] = null;
            BindingContext = new PrayersCommentsViewModel(PrayerRequestID, MyRequestsUserid, IsDeleteButtonVisible, UserRoleAdmin, IsApproveCommentButtonVisible);
            lstPrayerComments.RefreshCommand.Execute(null);

            lblPrayerTitleHeading.Text = "Prayer Request";
            lblPrayerCommentsHeading.Text = "Prayers/Comments";
            double lay = Convert.ToDouble(layout.GetSizeRequest(layout.Width, layout.Height).Request.Height);
            double lstview = lstPrayerComments.GetSizeRequest(layout.Width, layout.Height).Request.Height;
            double listview = lstPrayerComments.Height;

            MainHeight = lay + lstview + 10;
            layout.HeightRequest = MainHeight;

            //   lstPrayerComments.HeightRequest = .Length * lstPrayerComments.RowHeight;
            //  lstPrayerComments.RowHeight = Convert.ToInt32(MainHeight);
            while (!Showmessage)
            {
                await Task.Delay(1000);
                if (Showmessage == true)
                {
                    break;
                }


            }
            while (!Hidemessage)
            {
                await Task.Delay(1000);
                if (Hidemessage == true)
                {
                    break;
                }


            }

            if (Showmessage == true)
            {
                await Task.Delay(1000);
                lblNoComments.IsVisible = true;
            }
            if (Hidemessage == true)
            {
                await Task.Delay(1000);
                lblNoComments.IsVisible = false;
            }

        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {

            }
        }
        public void ShowDataRoleWise()
        {
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("warrior") && !UserRole.Contains("admin") && !UserRole.Contains("member"))
                {
                    lblAddPrayerReques.IsVisible = false;

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

                }

            }
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            else
            {
                var _item = (PrayersCommentsModel)e.SelectedItem;
                string CommentedPostedbyUserId = _item.user_id;
                string CommentId = _item.id;
                string commentText = _item.text;

                if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserRole"))
                {
                    string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                    string UserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                    if (UserId == CommentedPostedbyUserId || UserRole.Contains("admin") || UserRole.Contains("moderator"))
                    {
                        Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                        Application.Current.Properties["RequestPostedUserId"] = RequestPostedUserId;
                        Application.Current.Properties["title"] = null;
                        Application.Current.Properties["Description"] = null;
                        Application.Current.Properties["CommentId"] = CommentId;
                        Application.Current.Properties["commentText"] = commentText;
                        ((ListView)sender).SelectedItem = null;
                        _UpdatePrayerCommentPopup = new UpdatePrayerCommentPopupPage();
                        await Navigation.PushPopupAsync(_UpdatePrayerCommentPopup);
                    }
                }
                else
                {
                    ((ListView)sender).SelectedItem = null;
                }
            }
        }

        private async void OnApproveCommentClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string CommentId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Are you sure you want to Approve?", "Yes", "No");
            if (answer)
            {
                await Task.Delay(1000);
                var loadingPagedelete = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPagedelete);
                DeletePrayer _ApproveComment = new DeletePrayer();
                var _data = await _ApproveComment.ApproveComment(new ApproveCommentModel { CommentId = CommentId });
                await Navigation.RemovePopupPageAsync(loadingPagedelete);
                if (_data.Status == true)
                {
                    GetLiveCounter();
                }
            }
            else
            {
                // if No make all user alarm Active
            }
            //var DeleteCommentAdminpopUp = new DeleteCommentAdminpopUp();
            //Application.Current.Properties["CommentId"] = CommentId;
            //await Navigation.PushPopupAsync(DeleteCommentAdminpopUp);
        }
        private async void OnDeleteCommentClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            string CommentId = Convert.ToString(item.CommandParameter.ToString());
            var answer = await DisplayAlert("Question?", "Are you sure you want to delete?", "Yes", "No");
            if (answer)
            {
                await Task.Delay(1000);
                var loadingPagedelete = new LoadingPopupPage();
                await Navigation.PushPopupAsync(loadingPagedelete);
                DeletePrayer _DeleteComment = new DeletePrayer();
                var _data = await _DeleteComment.DeleteComment(new DeleteCommentModel { CommentId = CommentId });
                await Navigation.RemovePopupPageAsync(loadingPagedelete);
                if (_data.Status == true)
                {
                    GetLiveCounter();
                }
            }
            else
            {
                // if No make all user alarm Active
            }
            //var DeleteCommentAdminpopUp = new DeleteCommentAdminpopUp();
            //Application.Current.Properties["CommentId"] = CommentId;
            //await Navigation.PushPopupAsync(DeleteCommentAdminpopUp);
        }

        private async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            await Navigation.PushPopupAsync(_AddPrayerRequestPopup);
        }
        private async void OnAddCommentClicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("UserFirstName") && Application.Current.Properties.ContainsKey("UserRole"))
            {

                Int32 warrior = 0;
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                string LoginUserId = Convert.ToString(Application.Current.Properties["UserUID"]);
                PrayerRequestId = Convert.ToString(Application.Current.Properties["PrayerRequestIdNewAddComment"]);
                if (UserRole.Contains("warrior") && (LoginUserId != RequestPostedUserId))
                {
                    warrior = 1;
                }
                if (LoginUserId == RequestPostedUserId)
                {
                    warrior = 0;
                }
                Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                Application.Current.Properties["warrior"] = warrior;
                _AddPrayerCommentPopupPage = new AddPrayerCommentPopupPage();
                await Navigation.PushPopupAsync(_AddPrayerCommentPopupPage);



            }

        }

        private async void OnTapGestureRecognizerPrayerTitleTapped(object sender, EventArgs args)
        {

            string LoginUserId = Convert.ToString(Application.Current.Properties["UserUID"]);
            string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
            if ((PrayerRequestId != "" && LoginUserId == RequestPostedUserId) || UserRole.Contains("admin"))
            {
                Application.Current.Properties["PrayerRequestId"] = PrayerRequestId;
                Application.Current.Properties["RequestPostedUserId"] = RequestPostedUserId;
                Application.Current.Properties["title"] = lblPrayerTitle.Text.Trim();
                Application.Current.Properties["Description"] = lblPrayerDescriptionwithoutquoteright.Text.Trim();
                _UpdatePrayerRequestPopup = new UpdatePrayerRequestPopupPage();
                await Navigation.PushPopupAsync(_UpdatePrayerRequestPopup);
            }

        }
        class PrayersCommentsViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<PrayersCommentsModel> _items = new ObservableCollection<PrayersCommentsModel>();
            public ObservableCollection<PrayersCommentsModel> Items
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
            public PrayersCommentsViewModel(string PrayerRequestID, string MyRequestsUserid, Boolean IsDeleteButtonVisible, Int32 UserRoleAdmin, Boolean IsApproveCommentButtonVisible)
            {

                RefreshDataCommand = new Command(
                    async () => await RefreshData(PrayerRequestID, MyRequestsUserid, IsDeleteButtonVisible, UserRoleAdmin, IsApproveCommentButtonVisible));
            }

            public ICommand RefreshDataCommand { get; }
            async Task RefreshData(string PrayerRequestID, string MyRequestsUserid, Boolean IsDeleteButtonVisible, Int32 UserRoleAdmin, Boolean IsApproveCommentButtonVisible)
            {

                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                //Load Data Here
                //bindList(4,6);
                await callAPI(PrayerRequestID, MyRequestsUserid, IsDeleteButtonVisible, UserRoleAdmin, IsApproveCommentButtonVisible);

                IsBusy = false;
                //await Task.Delay(2000);


            }
            public async Task callAPI(string PrayerRequestID, string MyRequestsUserid, Boolean IsDeleteButtonVisible, Int32 UserRoleAdmin, Boolean IsApproveCommentButtonVisible)
            {
                PrayersComments objPrayersComments = new PrayersComments();
                List<PrayersCommentsModel> _list = new List<PrayersCommentsModel>();

                _list = await objPrayersComments.getAllPrayersComments(PrayerRequestID, MyRequestsUserid, IsDeleteButtonVisible, UserRoleAdmin, IsApproveCommentButtonVisible);
                if (_list != null)
                {
                    //await Task.Delay(2000);
                    //comment following code for paging
                    Items = new ObservableCollection<PrayersCommentsModel>();
                    foreach (var item in _list)
                    {
                        _items.Add(new PrayersCommentsModel
                        {
                            id = item.id,
                            uuid = item.uuid,
                            prayer_request_id = item.prayer_request_id,
                            text = item.text,
                            user_id = item.user_id,
                            response_type = item.response_type,
                            created_at = item.created_at,
                            updated_at = item.updated_at,
                            warrior = item.warrior,
                            first_name = item.first_name,
                            last_name = item.last_name,
                            user_name = item.user_name,
                            OtherInformation = item.OtherInformation,
                            IsDeleteButtonVisible = item.IsDeleteButtonVisible,
                            IsGreenIconVisible = item.IsGreenIconVisible,
                            OtherInfoforSpam = item.OtherInfoforSpam,
                            IsOffensiveButtonVisible = item.IsOffensiveButtonVisible,
                            IsCommentWaitingApproval = item.IsCommentWaitingApproval,
                            IsWaitingApproval = item.IsWaitingApproval,
                            IsApproveCommentButtonVisible = item.IsApproveCommentButtonVisible,
                        }

                        );

                    }
                }

                if (_list == null)
                {
                    Application.Current.Properties["HasComments"] = "HasNoComments";
                    IsNoComments = true;
                    IsListVisible = false;
                    Showmessage = true;

                }
                else
                {
                    Application.Current.Properties["HasComments"] = "HasComments";
                    IsNoComments = false;
                    IsListVisible = true;
                    Hidemessage = true;
                }
                objPrayersComments = null;
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


            bool isListvisible;
            public bool IsListVisible
            {
                get { return isListvisible; }
                set
                {
                    isListvisible = value;
                    OnPropertyChanged("IsListVisible");
                    //  ((Command)RefreshDataCommand).ChangeCanExecute();
                }
            }


            bool isNoComments;
            public bool IsNoComments
            {
                get { return isNoComments; }
                set
                {
                    isNoComments = value;
                    OnPropertyChanged("isNoComments");
                    //  ((Command)RefreshDataCommand).ChangeCanExecute();
                }
            }

            public object HttpContext { get; private set; }

            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }
    }
}


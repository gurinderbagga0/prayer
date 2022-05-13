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
    public partial class SearchUsersAdmin : ContentPage
    {
        private AddPrayerRequestPopupPage _AddPrayerRequestPopup;
        private ObservableCollection<AllUsersModel> Iitem;
        public SearchUsersAdmin()
        {
            //  NavigationPage.SetHasNavigationBar(this, false);
            string Platform = Device.RuntimePlatform;
            if (!Platform.Contains("Android"))
            {
                NavigationPage.SetTitleIcon(this, "logoios.png");
            }
            if (Application.Current.Properties.ContainsKey("UserRole"))
            {
                string UserRole = Convert.ToString(Application.Current.Properties["UserRole"]);
                if (UserRole.Contains("admin"))
                {
                    InitializeComponent();
                    ShowDataRoleWise();
                    GetLiveCounter();
                    BindingContext = new AllUsersViewModel();
                    lstAllUsersAdmin.RefreshCommand.Execute(null);
                }
                else
                {

                }
            }

        }
        private void FilterNames()
        {
            string filter = FirstNameChampionText.Text;
            lstAllUsersAdmin.BeginRefresh();
            if (Iitem == null)
            {
                Iitem = (ObservableCollection<AllUsersModel>)lstAllUsersAdmin.ItemsSource;
            }
            if (string.IsNullOrWhiteSpace(filter))
            {
                  lstAllUsersAdmin.ItemsSource = Iitem;
            }
            else
            {
                  lstAllUsersAdmin.ItemsSource = Iitem.Where(x => x.Name.ToLower().Contains(filter.ToLower()));
            }
            lstAllUsersAdmin.EndRefresh();
        }
        class AllUsersViewModel : INotifyPropertyChanged
        {
            private ObservableCollection<AllUsersModel> _items = new ObservableCollection<AllUsersModel>();
            public ObservableCollection<AllUsersModel> Items
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
            public AllUsersViewModel()
            {

                RefreshDataCommand = new Command(
                    async () => await RefreshData());
            }

            public ICommand RefreshDataCommand { get; }
            async Task RefreshData()
            {

                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;
                //Load Data Here
                //bindList(4,6);
                await callAPI();

                IsBusy = false;
                //await Task.Delay(2000);


            }
            public async Task callAPI()
            {
                Alluers objAllUsers = new Alluers();
                List<AllUsersModel> _list = new List<AllUsersModel>();

                _list = await objAllUsers.getAllUsers();
                if (_list != null)
                {

                    //await Task.Delay(2000);
                    //comment following code for paging
                    Items = new ObservableCollection<AllUsersModel>();
                    foreach (var item in _list)
                    {
                        _items.Add(new AllUsersModel
                        {
                            id = item.id,
                            uuid = item.uuid,
                            Name = item.Name,
                            created_at = item.created_at
                        }
                        );
                    }
                }
                objAllUsers = null;
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
      
        void OnSearchBarTextChanged(object sender, TextChangedEventArgs args)
        {
            FilterNames();
        }
        void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            FilterNames();
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
        public async void GetLiveCounter()
        {

            UserStats _user = new UserStats();
            var _data = await _user.GetStats();
            if (_data.Status == true)
            {
                lblWaitingText.Text = _data.prayers_needed + " Waiting";
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
                var _item = (AllUsersModel)e.SelectedItem;
                string UserId = _item.uuid;
                Application.Current.Properties["userUniqueIDSearchUser"] = UserId;
                await Navigation.PushAsync(new MemberProfile());
            }
            ((ListView)sender).SelectedItem = null;
        }
    }


}

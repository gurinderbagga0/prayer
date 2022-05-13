using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
   public class Prayers:IPrayers
    {
        HttpClient client;
        public Prayers()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<List<PrayersModel>> getAllPrayers(int pageIndex, string MyRequestsUserid, string WaitingForPrayers, Boolean IsPostPrayerButtonVisible, Boolean IsDeleteButtonVisible, string LoginUserId, Int32 UserRoleAdmin)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetAllActivePrayers?pageIndex="+ pageIndex + "&MyRequestsUserid="+ MyRequestsUserid + "&WaitingForPrayers="+ WaitingForPrayers + "&IsPostPrayerButtonVisible=" + IsPostPrayerButtonVisible + "&IsDeleteButtonVisible=" + IsDeleteButtonVisible + "&User_id="+LoginUserId+ "&UserRoleAdmin="+UserRoleAdmin+ " ");
             //   var response = await client.GetAsync(uri);
                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PrayersModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public async Task<List<PrayersModel>> getAllPrayersIhavePsitedFor(string UserIID,Boolean IsPostPrayerButtonVisible, Boolean IsDeleteButtonVisible)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetAllRequestIhavePostedFor?UserId=" + UserIID + "&IsPostPrayerButtonVisible=" + IsPostPrayerButtonVisible + "&IsDeleteButtonVisible=" + IsDeleteButtonVisible + "  ");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PrayersModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}

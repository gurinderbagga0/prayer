using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class DeletePrayer : IDeletePrayer
    {
        HttpClient client;
        public DeletePrayer()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<DeletePrayerModel> DeletePrayerRequest(DeletePrayerModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/DeletePrayerRequestTPZAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<DeletePrayerModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<ApproveCommentModel> ApproveComment(ApproveCommentModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/ApprovePrayerCommentTPZAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApproveCommentModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    

    public async Task<DeleteCommentModel> DeleteComment(DeleteCommentModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/DeletePrayerCommentTPZAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<DeleteCommentModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }

    public class ApprovePrayer : IApprovePrayer
    {
        HttpClient client;
        public ApprovePrayer()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<ApprovePrayerModel> ApprovePrayerRequest(ApprovePrayerModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/ApprovePrayerRequestTPZAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ApprovePrayerModel>(data);
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

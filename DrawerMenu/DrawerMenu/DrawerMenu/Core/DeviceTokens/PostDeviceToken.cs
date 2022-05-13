using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class PostDeviceToken : IPostDeviceToken
    {
        HttpClient client;
        public PostDeviceToken()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<PostDeviceTokenModel> PostDeviceDetails(PostDeviceTokenModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/PostDeviceDetails");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostDeviceTokenModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<UpdateDeviceDetailsModel> UpdateUserLoginDetailsUserId(UpdateDeviceDetailsModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateUserLoginDetailsUserId");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UpdateDeviceDetailsModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<UpdateDeviceDetailsModel> UpdateNotificationDevicesUserId(UpdateDeviceDetailsModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateNotificationDevicesUserId");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UpdateDeviceDetailsModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
       


        public async Task<List<PostDeviceTokenModel>> getNotificationEnableDisable(string UuID)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/getNotificationEnableDisable?UuId=" + UuID);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PostDeviceTokenModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<PostDeviceTokenModel> UpdatePushNotificationDetailsUsers(PostDeviceTokenModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdatePushNotificationDetailsUsers");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PostDeviceTokenModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<UpdateDeviceTokenModel> UpdateDeviceDetails(UpdateDeviceTokenModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateDeviceDetails");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UpdateDeviceTokenModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<List<PostDeviceTokenModel>> getPrayerUpdateNotificationEnableDisable(string UuID)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/getPrayerUpdateNotificationEnableDisable?UuId=" + UuID);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PostDeviceTokenModel>>(data);
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

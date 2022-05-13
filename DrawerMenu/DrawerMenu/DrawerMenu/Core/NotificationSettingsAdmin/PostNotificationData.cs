using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class PostNotificationData : INotificationSettings
    {
        HttpClient client;
        public PostNotificationData()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<PostNotificationDataModel> PostNotificationDetails(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint+ "User/PostPushNotificationAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<PostNotificationDataModel> DeleteNotificationTimeUser(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/DeleteNotificationDataUser");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<AdminBlockWordsViewClassModel> DeleteBlockWord(AdminBlockWordsViewClassModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/DeleteBlockWord");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<AdminBlockWordsViewClassModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<PostNotificationDataModel> UpdatePrayerUpdateNotificationDetailsUsers(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdatePrayerUpdateNotificationDetailsUsers");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<PostNotificationDataModel> UpdateReminderNotificationDetailsUsers(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateReminderNotificationDetailsUsers");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<PostNotificationDataModel> UpdateReminderNotificationUserDetailsTimeId(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateReminderNotificationUserDetailsTimeId");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<AdminBlockWordsViewClassModel> GetBlokWordById(AdminBlockWordsViewClassModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/GetBlokWordById");
                //var response = await client.PostAsync(uri, content).Result;
                var response = client.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<AdminBlockWordsViewClassModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<PostNotificationDataModel> GetReminderDetailsTimeId(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/GetReminderDetailsTimeId");
                //var response = await client.PostAsync(uri, content).Result;
                var response = client.PostAsync(uri,content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<PostNotificationDataModel> PostNotificationDetailsUsers(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/PostNotificationDetailsUsers");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<AdminBlockWordsViewClassModel>> getAllBlockWords()
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/getAllBlockWords");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<AdminBlockWordsViewClassModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<PostNotificationDataModel>> getAllNotificationDataUserTime(string UuID)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetAllNotificationUserTime?UuId=" + UuID);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PostNotificationDataModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<List<PostNotificationDataModel>> getAllNotificationSetDataAdmin()
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/getAllNotificationSetDataAdmin");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PostNotificationDataModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<List<PostNotificationDataModel>> getAllNotificationDataAdmin(string day)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetAllNotificationDataAdmin?day=" + day);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PostNotificationDataModel>>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<PostNotificationDataModel> DeleteNotificationTime(PostNotificationDataModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/DeleteNotificationDataTPZAdmin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostNotificationDataModel>(data);
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

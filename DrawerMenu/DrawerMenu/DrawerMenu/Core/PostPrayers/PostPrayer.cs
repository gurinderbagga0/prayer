using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class PostPrayer : IPostPrayer
    {
        HttpClient client;
        public PostPrayer()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<PostPrayerModel> PostNewPrayer(PostPrayerModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/PostNewPrayer");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PostPrayerModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<InsertRestrictWordModel> InsertRestrictWord(InsertRestrictWordModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/InsertRestrictWord");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<InsertRestrictWordModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public async Task<InsertOffensivePrayerModel> InsertOffensivePrayerRequest(InsertOffensivePrayerModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/InsertOffensivePrayerRequest");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<InsertOffensivePrayerModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<InsertOffensiveCommentModel> InsertOffensiveComments(InsertOffensiveCommentModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/InsertOffensiveComments");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<InsertOffensiveCommentModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<InsertUserMessageModel> InsertUserMessage(InsertUserMessageModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/InsertUserMessage");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<InsertUserMessageModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<InsertPrayerCommentModel> InsertPrayerComment(InsertPrayerCommentModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/InsertPrayerComment");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<InsertPrayerCommentModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }


        public async Task<InsertRestrictWordModel> UpdateRestrictWordEntry(InsertRestrictWordModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdateRestrictWordEntry");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<InsertRestrictWordModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public async Task<UpdatePrayerModel> UpdatePrayer(UpdatePrayerModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdatePrayer");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UpdatePrayerModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public async Task<UpdatePrayerCommentModel> UpdatePrayerComment(UpdatePrayerCommentModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UpdatePrayerComment");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<UpdatePrayerCommentModel>(data);
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

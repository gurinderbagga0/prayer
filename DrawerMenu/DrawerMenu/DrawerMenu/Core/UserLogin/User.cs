using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class User : IUser
    {
        HttpClient client;
        public User()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<UserModel> GetUserLoggedIn(UserModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/GetUserLoggedIn");
                var response = client.PostAsync(uri,content).Result;
              //  var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<UserModel>(data);
                }
                return null;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public async Task<UserModel> Login(LoginModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint+"User/UserLogin");
                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<UserModel>(data);
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

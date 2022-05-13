using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class UserStats : IUserStats
    {
        HttpClient client;
        public UserStats()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<UserStatsModel> GetStats()
        {
            try
            {
               
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint+ "User/GetAllLiveCounter");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    
                    return JsonConvert.DeserializeObject<UserStatsModel>(data);
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

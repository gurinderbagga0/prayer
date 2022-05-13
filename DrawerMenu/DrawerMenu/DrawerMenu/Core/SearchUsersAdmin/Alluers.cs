using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
   public class Alluers : IAllUsers
    {
        HttpClient client;
        public Alluers()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<List<AllUsersModel>> getAllUsers()
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetAllUsers");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<AllUsersModel>>(data);
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

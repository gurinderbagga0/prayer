﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core
{
    public class UserReqRegisteration : IUserReqRegisteration
    {

        HttpClient client;
        public UserReqRegisteration()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<UserModel> RequesterRegistration(UserReqRegisterationModel item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "User/UserSignUpRequester");
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

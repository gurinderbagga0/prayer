﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DrawerMenu.Core.Prayers
{
   public class PrayersComments : IPrayersComments
    {
        HttpClient client;
        public PrayersComments()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }
        public async Task<List<PrayersCommentsModel>> getAllPrayersComments(string PrayerRequestID, string MyRequestsUserid,Boolean IsDeleteButtonVisible,Int32 UserRoleAdmin, Boolean IsApproveCommentButtonVisible)
        {
            try
            {
                client.DefaultRequestHeaders.Add("X-Secret-Key", Common._key);
                var uri = new Uri(Common._endpoint + "Prayers/GetPrayerComments?PrayerRequestID=" + PrayerRequestID +"&MyRequestsUserid=" + MyRequestsUserid+ "&IsDeleteButtonVisible=" + IsDeleteButtonVisible+ "&IsApproveCommentButtonVisible=" + IsApproveCommentButtonVisible + "&UserRoleAdmin=" + UserRoleAdmin+" ");
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<List<PrayersCommentsModel>>(data);
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

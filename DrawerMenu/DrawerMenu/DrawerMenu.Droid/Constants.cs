using System;

namespace PushNotification
{
    public static class Constants
    {
        // Azure app specific URL and key
		public const string ApplicationURL = @"https://YOUR_APP_NAME.azure-mobile.net/";
		public const string ApplicationKey = @"AIzaSyB2xGRvCRmdjwlf39AC87mE6ONTy2a8Ra8";
		public const string SenderID = "210827963370"; // Google API Project Number

        // Azure app specific connection string and hub path
		public const string ConnectionString = "Endpoint=sb://quickstarthub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=YOUR_ACCESS_KEY";
        public const string NotificationHubPath = "QUICKSTARTHUB";
    }
}


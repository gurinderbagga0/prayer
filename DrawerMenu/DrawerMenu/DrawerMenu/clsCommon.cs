using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Locations;

namespace ThePrayerZone
{
	public enum CurrentProcess
	{
		CandleLight = 1,
		Donation = 2,
		Pushka = 3
	}

	public enum RecurringType
	{
		Monthly = 1,
		Weekly = 2,
		None = 3
	}

	public static class clsCommon
	{
		public static string PREFS_NAME = "ThePrayerZone_PrefsFile";
       
        public static String URL_String = "URL";
        public static String DonationId = "";
        public static string Device_Token = "";
		public static String EmailID_String = "EmailID";
		public static String Password_String = "Password";
		public static String FName_String = "FName";
		public static String LName_String = "LName";
		public static String UserID_String = "UserID";
		public static String MakePayment_String = "MakePayment";
		public static String CandleLightActive_String = "CandleLightActive";
		public static String CandleLightID_String = "CandleLightID";
		public static String HasDonationRecurring_String = "HasDonationRecurring";
		public static String DeviceId_String = "DeviceId";
		public static CurrentProcess objCurrentProcess = CurrentProcess.CandleLight;
		public static RecurringType CurrentRecurringType = RecurringType.None;
		public static string PaymentAmount = "";
		public static string DonationTime = "";

		public static string CandlelightTime = "";
		public static string CandlelightDate = "";
		public static string CandlelightType = "";
		public static string CandlelightZipcode = "";
		public static string CandlelightCity = "";
		public static string CandlelightState = "";
		public static string CandlelightCity_id = "";
		public static string CandlelightState_id = "";
		public static string Candlelight_tzid = "";
        public static string CommonUrlParamData = "";
		public static Activity Current_Activity;

		public static Address CurrentSelectedAddress;

		static public bool isValidSession { get; set; }

		static public bool showLockScreen { get; set; }

		static public bool isLockScreenFullCredentialCheck { get; set; }

		static public bool isLockScreenShowingPin { get; set; }

		static public Location userMostCurrentLocation { get; set; }

		static public Int32 intMaxNumberOfResults = 200;

		static public Int32 intTimeZoneOffSet { get; set; }

		static public Int32 intSecondsSinceLastUserInteraction { get; set; }

		public static string GetString (string key)
		{
			var prefs = Application.Context.GetSharedPreferences (PREFS_NAME, FileCreationMode.Private);
			return prefs.GetString (key, string.Empty);
		}

		public static void SaveString (string key, string value)
		{
			var prefs = Application.Context.GetSharedPreferences (PREFS_NAME, FileCreationMode.Private);
			var prefEditor = prefs.Edit ();
			prefEditor.PutString (key, value);
			prefEditor.Commit ();
		}

		public static bool IsValidEmail (string inputEmail)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
			                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
			                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex (strRegex);
			if (re.IsMatch (inputEmail))
				return (true);
			else
				return (false);
		}

		public static string CurrentDateTime_MMDDYYYY_HHMM ()
		{
			return DateTime.Now.Month.ToString ().PadLeft (2, '0') + "-" + DateTime.Now.Day.ToString ().PadLeft (2, '0') + "-" + DateTime.Now.Year.ToString () + " " + DateTime.Now.Hour.ToString ().PadLeft (2, '0') + ":" + DateTime.Now.Minute.ToString ().PadLeft (2, '0');
		}

		public static string ConvertDateTime_MMDDYYYY_HHMM (String datetime)
		{
			try {
				return datetime.Split (' ') [0].Split ('-') [1] + "/" + datetime.Split (' ') [0].Split ('-') [2] + "/" + datetime.Split (' ') [0].Split ('-') [0] + " " + datetime.Split (' ') [1].Split (':') [0] + ":" + datetime.Split (' ') [1].Split (':') [1];
			} catch {
				return "";
			}
		}

		public static string GetCreditCardType (string CreditCardNumber)
		{
			Regex regVisa = new Regex ("^4[0-9]{12}(?:[0-9]{3})?$");
			Regex regMaster = new Regex ("^5[1-5][0-9]{14}$");
			Regex regExpress = new Regex ("^3[47][0-9]{13}$");
			Regex regDiners = new Regex ("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
			Regex regDiscover = new Regex ("^6(?:011|5[0-9]{2})[0-9]{12}$");
			Regex regJSB = new Regex ("^(?:2131|1800|35\\d{3})\\d{11}$");

			if (regVisa.IsMatch (CreditCardNumber))
				return "VISA";
			if (regMaster.IsMatch (CreditCardNumber))
				return "MASTER CARD";
			if (regExpress.IsMatch (CreditCardNumber))
				return "AMERICAN EXPRESS";
			if (regDiners.IsMatch (CreditCardNumber))
				return "DINERS";
			if (regDiscover.IsMatch (CreditCardNumber))
				return "DISCOVER";
			if (regJSB.IsMatch (CreditCardNumber))
				return "JSB";
			return "Invalid";
		}
	}
}


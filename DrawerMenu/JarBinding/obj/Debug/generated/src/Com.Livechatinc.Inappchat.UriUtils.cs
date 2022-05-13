using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Livechatinc.Inappchat {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']"
	[global::Android.Runtime.Register ("com/livechatinc/inappchat/UriUtils", DoNotGenerateAcw=true)]
	public partial class UriUtils : global::Java.Lang.Object {

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/livechatinc/inappchat/UriUtils", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (UriUtils); }
		}

		protected UriUtils (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_getDataColumnForContentUri_Landroid_content_Context_Landroid_net_Uri_Ljava_lang_String_arrayLjava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']/method[@name='getDataColumnForContentUri' and count(parameter)=4 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.net.Uri'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='java.lang.String[]']]"
		[Register ("getDataColumnForContentUri", "(Landroid/content/Context;Landroid/net/Uri;Ljava/lang/String;[Ljava/lang/String;)Ljava/lang/String;", "")]
		public static unsafe string GetDataColumnForContentUri (global::Android.Content.Context p0, global::Android.Net.Uri p1, string p2, string[] p3)
		{
			if (id_getDataColumnForContentUri_Landroid_content_Context_Landroid_net_Uri_Ljava_lang_String_arrayLjava_lang_String_ == IntPtr.Zero)
				id_getDataColumnForContentUri_Landroid_content_Context_Landroid_net_Uri_Ljava_lang_String_arrayLjava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "getDataColumnForContentUri", "(Landroid/content/Context;Landroid/net/Uri;Ljava/lang/String;[Ljava/lang/String;)Ljava/lang/String;");
			IntPtr native_p2 = JNIEnv.NewString (p2);
			IntPtr native_p3 = JNIEnv.NewArray (p3);
			try {
				JValue* __args = stackalloc JValue [4];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (native_p2);
				__args [3] = new JValue (native_p3);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getDataColumnForContentUri_Landroid_content_Context_Landroid_net_Uri_Ljava_lang_String_arrayLjava_lang_String_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
				JNIEnv.DeleteLocalRef (native_p2);
				if (p3 != null) {
					JNIEnv.CopyArray (native_p3, p3);
					JNIEnv.DeleteLocalRef (native_p3);
				}
			}
		}

		static IntPtr id_getFilePathFromUri_Landroid_content_Context_Landroid_net_Uri_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']/method[@name='getFilePathFromUri' and count(parameter)=2 and parameter[1][@type='android.content.Context'] and parameter[2][@type='android.net.Uri']]"
		[Register ("getFilePathFromUri", "(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;", "")]
		public static unsafe string GetFilePathFromUri (global::Android.Content.Context p0, global::Android.Net.Uri p1)
		{
			if (id_getFilePathFromUri_Landroid_content_Context_Landroid_net_Uri_ == IntPtr.Zero)
				id_getFilePathFromUri_Landroid_content_Context_Landroid_net_Uri_ = JNIEnv.GetStaticMethodID (class_ref, "getFilePathFromUri", "(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				string __ret = JNIEnv.GetString (JNIEnv.CallStaticObjectMethod  (class_ref, id_getFilePathFromUri_Landroid_content_Context_Landroid_net_Uri_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_isDownloadsDocument_Landroid_net_Uri_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']/method[@name='isDownloadsDocument' and count(parameter)=1 and parameter[1][@type='android.net.Uri']]"
		[Register ("isDownloadsDocument", "(Landroid/net/Uri;)Z", "")]
		public static unsafe bool IsDownloadsDocument (global::Android.Net.Uri p0)
		{
			if (id_isDownloadsDocument_Landroid_net_Uri_ == IntPtr.Zero)
				id_isDownloadsDocument_Landroid_net_Uri_ = JNIEnv.GetStaticMethodID (class_ref, "isDownloadsDocument", "(Landroid/net/Uri;)Z");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				bool __ret = JNIEnv.CallStaticBooleanMethod  (class_ref, id_isDownloadsDocument_Landroid_net_Uri_, __args);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_isExternalStorageDocument_Landroid_net_Uri_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']/method[@name='isExternalStorageDocument' and count(parameter)=1 and parameter[1][@type='android.net.Uri']]"
		[Register ("isExternalStorageDocument", "(Landroid/net/Uri;)Z", "")]
		public static unsafe bool IsExternalStorageDocument (global::Android.Net.Uri p0)
		{
			if (id_isExternalStorageDocument_Landroid_net_Uri_ == IntPtr.Zero)
				id_isExternalStorageDocument_Landroid_net_Uri_ = JNIEnv.GetStaticMethodID (class_ref, "isExternalStorageDocument", "(Landroid/net/Uri;)Z");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				bool __ret = JNIEnv.CallStaticBooleanMethod  (class_ref, id_isExternalStorageDocument_Landroid_net_Uri_, __args);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_isMediaDocument_Landroid_net_Uri_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='UriUtils']/method[@name='isMediaDocument' and count(parameter)=1 and parameter[1][@type='android.net.Uri']]"
		[Register ("isMediaDocument", "(Landroid/net/Uri;)Z", "")]
		public static unsafe bool IsMediaDocument (global::Android.Net.Uri p0)
		{
			if (id_isMediaDocument_Landroid_net_Uri_ == IntPtr.Zero)
				id_isMediaDocument_Landroid_net_Uri_ = JNIEnv.GetStaticMethodID (class_ref, "isMediaDocument", "(Landroid/net/Uri;)Z");
			try {
				JValue* __args = stackalloc JValue [1];
				__args [0] = new JValue (p0);
				bool __ret = JNIEnv.CallStaticBooleanMethod  (class_ref, id_isMediaDocument_Landroid_net_Uri_, __args);
				return __ret;
			} finally {
			}
		}

	}
}

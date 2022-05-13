using System;
using System.Collections.Generic;
using Android.Runtime;

namespace Com.Livechatinc.Inappchat {

	// Metadata.xml XPath class reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment']"
	[global::Android.Runtime.Register ("com/livechatinc/inappchat/ChatWindowFragment", DoNotGenerateAcw=true)]
	public sealed partial class ChatWindowFragment : global::Android.App.Fragment {

		// Metadata.xml XPath class reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebChromeClient']"
		[global::Android.Runtime.Register ("com/livechatinc/inappchat/ChatWindowFragment$LCWebChromeClient", DoNotGenerateAcw=true)]
		public partial class LCWebChromeClient : global::Android.Webkit.WebChromeClient {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/livechatinc/inappchat/ChatWindowFragment$LCWebChromeClient", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (LCWebChromeClient); }
			}

			protected LCWebChromeClient (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_openFileChooser_Landroid_webkit_ValueCallback_;
#pragma warning disable 0169
			static Delegate GetOpenFileChooser_Landroid_webkit_ValueCallback_Handler ()
			{
				if (cb_openFileChooser_Landroid_webkit_ValueCallback_ == null)
					cb_openFileChooser_Landroid_webkit_ValueCallback_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr>) n_OpenFileChooser_Landroid_webkit_ValueCallback_);
				return cb_openFileChooser_Landroid_webkit_ValueCallback_;
			}

			static void n_OpenFileChooser_Landroid_webkit_ValueCallback_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
			{
				global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient __this = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Webkit.IValueCallback p0 = (global::Android.Webkit.IValueCallback)global::Java.Lang.Object.GetObject<global::Android.Webkit.IValueCallback> (native_p0, JniHandleOwnership.DoNotTransfer);
				__this.OpenFileChooser (p0);
			}
#pragma warning restore 0169

			static IntPtr id_openFileChooser_Landroid_webkit_ValueCallback_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebChromeClient']/method[@name='openFileChooser' and count(parameter)=1 and parameter[1][@type='android.webkit.ValueCallback&lt;android.net.Uri&gt;']]"
			[Register ("openFileChooser", "(Landroid/webkit/ValueCallback;)V", "GetOpenFileChooser_Landroid_webkit_ValueCallback_Handler")]
			public virtual unsafe void OpenFileChooser (global::Android.Webkit.IValueCallback p0)
			{
				if (id_openFileChooser_Landroid_webkit_ValueCallback_ == IntPtr.Zero)
					id_openFileChooser_Landroid_webkit_ValueCallback_ = JNIEnv.GetMethodID (class_ref, "openFileChooser", "(Landroid/webkit/ValueCallback;)V");
				try {
					JValue* __args = stackalloc JValue [1];
					__args [0] = new JValue (p0);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_openFileChooser_Landroid_webkit_ValueCallback_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "openFileChooser", "(Landroid/webkit/ValueCallback;)V"), __args);
				} finally {
				}
			}

			static Delegate cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_;
#pragma warning disable 0169
			static Delegate GetOpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Handler ()
			{
				if (cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_ == null)
					cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr>) n_OpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_);
				return cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_;
			}

			static void n_OpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
			{
				global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient __this = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Webkit.IValueCallback p0 = (global::Android.Webkit.IValueCallback)global::Java.Lang.Object.GetObject<global::Android.Webkit.IValueCallback> (native_p0, JniHandleOwnership.DoNotTransfer);
				string p1 = JNIEnv.GetString (native_p1, JniHandleOwnership.DoNotTransfer);
				__this.OpenFileChooser (p0, p1);
			}
#pragma warning restore 0169

			static IntPtr id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebChromeClient']/method[@name='openFileChooser' and count(parameter)=2 and parameter[1][@type='android.webkit.ValueCallback&lt;android.net.Uri&gt;'] and parameter[2][@type='java.lang.String']]"
			[Register ("openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;)V", "GetOpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Handler")]
			public virtual unsafe void OpenFileChooser (global::Android.Webkit.IValueCallback p0, string p1)
			{
				if (id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_ == IntPtr.Zero)
					id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;)V");
				IntPtr native_p1 = JNIEnv.NewString (p1);
				try {
					JValue* __args = stackalloc JValue [2];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (native_p1);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;)V"), __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_p1);
				}
			}

			static Delegate cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_;
#pragma warning disable 0169
			static Delegate GetOpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_Handler ()
			{
				if (cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_ == null)
					cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr>) n_OpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_);
				return cb_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_;
			}

			static void n_OpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1, IntPtr native_p2)
			{
				global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient __this = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebChromeClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Webkit.IValueCallback p0 = (global::Android.Webkit.IValueCallback)global::Java.Lang.Object.GetObject<global::Android.Webkit.IValueCallback> (native_p0, JniHandleOwnership.DoNotTransfer);
				string p1 = JNIEnv.GetString (native_p1, JniHandleOwnership.DoNotTransfer);
				string p2 = JNIEnv.GetString (native_p2, JniHandleOwnership.DoNotTransfer);
				__this.OpenFileChooser (p0, p1, p2);
			}
#pragma warning restore 0169

			static IntPtr id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebChromeClient']/method[@name='openFileChooser' and count(parameter)=3 and parameter[1][@type='android.webkit.ValueCallback&lt;android.net.Uri&gt;'] and parameter[2][@type='java.lang.String'] and parameter[3][@type='java.lang.String']]"
			[Register ("openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;Ljava/lang/String;)V", "GetOpenFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_Handler")]
			public virtual unsafe void OpenFileChooser (global::Android.Webkit.IValueCallback p0, string p1, string p2)
			{
				if (id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
					id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetMethodID (class_ref, "openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;Ljava/lang/String;)V");
				IntPtr native_p1 = JNIEnv.NewString (p1);
				IntPtr native_p2 = JNIEnv.NewString (p2);
				try {
					JValue* __args = stackalloc JValue [3];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (native_p1);
					__args [2] = new JValue (native_p2);

					if (GetType () == ThresholdType)
						JNIEnv.CallVoidMethod (((global::Java.Lang.Object) this).Handle, id_openFileChooser_Landroid_webkit_ValueCallback_Ljava_lang_String_Ljava_lang_String_, __args);
					else
						JNIEnv.CallNonvirtualVoidMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "openFileChooser", "(Landroid/webkit/ValueCallback;Ljava/lang/String;Ljava/lang/String;)V"), __args);
				} finally {
					JNIEnv.DeleteLocalRef (native_p1);
					JNIEnv.DeleteLocalRef (native_p2);
				}
			}

		}

		// Metadata.xml XPath class reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebViewClient']"
		[global::Android.Runtime.Register ("com/livechatinc/inappchat/ChatWindowFragment$LCWebViewClient", DoNotGenerateAcw=true)]
		public partial class LCWebViewClient : global::Android.Webkit.WebViewClient {

			internal static IntPtr java_class_handle;
			internal static IntPtr class_ref {
				get {
					return JNIEnv.FindClass ("com/livechatinc/inappchat/ChatWindowFragment$LCWebViewClient", ref java_class_handle);
				}
			}

			protected override IntPtr ThresholdClass {
				get { return class_ref; }
			}

			protected override global::System.Type ThresholdType {
				get { return typeof (LCWebViewClient); }
			}

			protected LCWebViewClient (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

			static Delegate cb_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_;
#pragma warning disable 0169
			static Delegate GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_Handler ()
			{
				if (cb_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_ == null)
					cb_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_ = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, IntPtr, bool>) n_ShouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_);
				return cb_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_;
			}

			static bool n_ShouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_ (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
			{
				global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebViewClient __this = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment.LCWebViewClient> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
				global::Android.Webkit.WebView p0 = global::Java.Lang.Object.GetObject<global::Android.Webkit.WebView> (native_p0, JniHandleOwnership.DoNotTransfer);
				global::Android.Webkit.IWebResourceRequest p1 = (global::Android.Webkit.IWebResourceRequest)global::Java.Lang.Object.GetObject<global::Android.Webkit.IWebResourceRequest> (native_p1, JniHandleOwnership.DoNotTransfer);
				bool __ret = __this.ShouldOverrideUrlLoading (p0, p1);
				return __ret;
			}
#pragma warning restore 0169

			static IntPtr id_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_;
			// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment.LCWebViewClient']/method[@name='shouldOverrideUrlLoading' and count(parameter)=2 and parameter[1][@type='android.webkit.WebView'] and parameter[2][@type='android.webkit.WebResourceRequest']]"
			[Register ("shouldOverrideUrlLoading", "(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z", "GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_Handler")]
			public virtual unsafe bool ShouldOverrideUrlLoading (global::Android.Webkit.WebView p0, global::Android.Webkit.IWebResourceRequest p1)
			{
				if (id_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_ == IntPtr.Zero)
					id_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_ = JNIEnv.GetMethodID (class_ref, "shouldOverrideUrlLoading", "(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z");
				try {
					JValue* __args = stackalloc JValue [2];
					__args [0] = new JValue (p0);
					__args [1] = new JValue (p1);

					bool __ret;
					if (GetType () == ThresholdType)
						__ret = JNIEnv.CallBooleanMethod (((global::Java.Lang.Object) this).Handle, id_shouldOverrideUrlLoading_Landroid_webkit_WebView_Landroid_webkit_WebResourceRequest_, __args);
					else
						__ret = JNIEnv.CallNonvirtualBooleanMethod (((global::Java.Lang.Object) this).Handle, ThresholdClass, JNIEnv.GetMethodID (ThresholdClass, "shouldOverrideUrlLoading", "(Landroid/webkit/WebView;Landroid/webkit/WebResourceRequest;)Z"), __args);
					return __ret;
				} finally {
				}
			}

		}

		internal static IntPtr java_class_handle;
		internal static IntPtr class_ref {
			get {
				return JNIEnv.FindClass ("com/livechatinc/inappchat/ChatWindowFragment", ref java_class_handle);
			}
		}

		protected override IntPtr ThresholdClass {
			get { return class_ref; }
		}

		protected override global::System.Type ThresholdType {
			get { return typeof (ChatWindowFragment); }
		}

		internal ChatWindowFragment (IntPtr javaReference, JniHandleOwnership transfer) : base (javaReference, transfer) {}

		static IntPtr id_ctor;
		// Metadata.xml XPath constructor reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment']/constructor[@name='ChatWindowFragment' and count(parameter)=0]"
		[Register (".ctor", "()V", "")]
		public unsafe ChatWindowFragment ()
			: base (IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
		{
			if (((global::Java.Lang.Object) this).Handle != IntPtr.Zero)
				return;

			try {
				if (GetType () != typeof (ChatWindowFragment)) {
					SetHandle (
							global::Android.Runtime.JNIEnv.StartCreateInstance (GetType (), "()V"),
							JniHandleOwnership.TransferLocalRef);
					global::Android.Runtime.JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, "()V");
					return;
				}

				if (id_ctor == IntPtr.Zero)
					id_ctor = JNIEnv.GetMethodID (class_ref, "<init>", "()V");
				SetHandle (
						global::Android.Runtime.JNIEnv.StartCreateInstance (class_ref, id_ctor),
						JniHandleOwnership.TransferLocalRef);
				JNIEnv.FinishCreateInstance (((global::Java.Lang.Object) this).Handle, class_ref, id_ctor);
			} finally {
			}
		}

		static IntPtr id_newInstance_Ljava_lang_Object_Ljava_lang_Object_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment']/method[@name='newInstance' and count(parameter)=2 and parameter[1][@type='java.lang.Object'] and parameter[2][@type='java.lang.Object']]"
		[Register ("newInstance", "(Ljava/lang/Object;Ljava/lang/Object;)Lcom/livechatinc/inappchat/ChatWindowFragment;", "")]
		public static unsafe global::Com.Livechatinc.Inappchat.ChatWindowFragment NewInstance (global::Java.Lang.Object p0, global::Java.Lang.Object p1)
		{
			if (id_newInstance_Ljava_lang_Object_Ljava_lang_Object_ == IntPtr.Zero)
				id_newInstance_Ljava_lang_Object_Ljava_lang_Object_ = JNIEnv.GetStaticMethodID (class_ref, "newInstance", "(Ljava/lang/Object;Ljava/lang/Object;)Lcom/livechatinc/inappchat/ChatWindowFragment;");
			try {
				JValue* __args = stackalloc JValue [2];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				global::Com.Livechatinc.Inappchat.ChatWindowFragment __ret = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment> (JNIEnv.CallStaticObjectMethod  (class_ref, id_newInstance_Ljava_lang_Object_Ljava_lang_Object_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
			}
		}

		static IntPtr id_newInstance_Ljava_lang_Object_Ljava_lang_Object_Ljava_lang_String_Ljava_lang_String_;
		// Metadata.xml XPath method reference: path="/api/package[@name='com.livechatinc.inappchat']/class[@name='ChatWindowFragment']/method[@name='newInstance' and count(parameter)=4 and parameter[1][@type='java.lang.Object'] and parameter[2][@type='java.lang.Object'] and parameter[3][@type='java.lang.String'] and parameter[4][@type='java.lang.String']]"
		[Register ("newInstance", "(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/String;Ljava/lang/String;)Lcom/livechatinc/inappchat/ChatWindowFragment;", "")]
		public static unsafe global::Com.Livechatinc.Inappchat.ChatWindowFragment NewInstance (global::Java.Lang.Object p0, global::Java.Lang.Object p1, string p2, string p3)
		{
			if (id_newInstance_Ljava_lang_Object_Ljava_lang_Object_Ljava_lang_String_Ljava_lang_String_ == IntPtr.Zero)
				id_newInstance_Ljava_lang_Object_Ljava_lang_Object_Ljava_lang_String_Ljava_lang_String_ = JNIEnv.GetStaticMethodID (class_ref, "newInstance", "(Ljava/lang/Object;Ljava/lang/Object;Ljava/lang/String;Ljava/lang/String;)Lcom/livechatinc/inappchat/ChatWindowFragment;");
			IntPtr native_p2 = JNIEnv.NewString (p2);
			IntPtr native_p3 = JNIEnv.NewString (p3);
			try {
				JValue* __args = stackalloc JValue [4];
				__args [0] = new JValue (p0);
				__args [1] = new JValue (p1);
				__args [2] = new JValue (native_p2);
				__args [3] = new JValue (native_p3);
				global::Com.Livechatinc.Inappchat.ChatWindowFragment __ret = global::Java.Lang.Object.GetObject<global::Com.Livechatinc.Inappchat.ChatWindowFragment> (JNIEnv.CallStaticObjectMethod  (class_ref, id_newInstance_Ljava_lang_Object_Ljava_lang_Object_Ljava_lang_String_Ljava_lang_String_, __args), JniHandleOwnership.TransferLocalRef);
				return __ret;
			} finally {
				JNIEnv.DeleteLocalRef (native_p2);
				JNIEnv.DeleteLocalRef (native_p3);
			}
		}

	}
}

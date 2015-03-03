using UnityEngine;
using System.Collections;
using System;

public class GrowthPushAndroid {

		private static GrowthPushAndroid instance = new GrowthPushAndroid();

		#if UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaObject growthPush;
		#endif

		private GrowthPushAndroid() {
				#if UNITY_ANDROID && !UNITY_EDITOR
					using(AndroidJavaClass gpclass = new AndroidJavaClass( "com.growthpush.GrowthPush" )) {
							growthPush = gpclass.CallStatic<AndroidJavaObject>("getInstance"); 
					}
				#endif
		}

		public static void Initialize(int applicationId, string secret, GrowthPush.Environment environment, bool debug, string senderId) {
			instance.PrivateInitialize(applicationId, secret, environment, debug, senderId);
		}

		public static void TrackEvent(string name, string val) {
			instance.PrivateTrackEvent(name, val);
		}

		public static void SetTag(string name, string val) {
			instance.PrivateSetTag(name, val);
		}

		public static void SetDeviceTags() {
			instance.PrivateSetDeviceTags();
		}

		private void PrivateInitialize(int applicationId, string secret, GrowthPush.Environment environment, bool debug, string senderId) {
				#if UNITY_ANDROID && !UNITY_EDITOR
					if (growthPush == null)
						return;
					AndroidJavaClass  environmentClass = new AndroidJavaClass("com.growthpush.model.Environment"); 
					AndroidJavaObject environmentObject = environmentClass.GetStatic<AndroidJavaObject>(environment == GrowthPush.Environment.Production ? "production" : "development"); 
					AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
					AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 
					growthPush.Call<AndroidJavaObject>("initialize", activity, applicationId, secret, environmentObject, debug);
					growthPush.Call<AndroidJavaObject>("register", senderId);
				#endif
		}

		private void PrivateTrackEvent(string name, string val) {
				#if UNITY_ANDROID && !UNITY_EDITOR
					if (growthPush == null)
						return;
					growthPush.Call("trackEvent", name, val);
				#endif
		}

		private void PrivateSetTag(string name, string val) {
				#if UNITY_ANDROID && !UNITY_EDITOR
					if (growthPush == null)
						return;
					growthPush.Call("setTag", name, val);
				#endif
		}

		private void PrivateSetDeviceTags() {
				#if UNITY_ANDROID && !UNITY_EDITOR
					if (growthPush == null)
						return;
					growthPush.Call("setDeviceTags");
				#endif
		}

}

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class GrowthPushIOS {

		#if UNITY_IPHONE
			[DllImport("__Internal")]
			private static extern void growthPushSetApplicationId(int applicationID, string secrect, int environment, bool debug);

			[DllImport("__Internal")] 
			private static extern void growthPushTrackEvent(string name, string val);

			[DllImport("__Internal")]
			private static extern void growthPushSetTag(string name, string val);

			[DllImport("__Internal")]
			private static extern void growthPushSetDeviceTags();

			[DllImport("__Internal")]
			private static extern void growthPushClearBadge();
		#endif

		public static void Initialize(int applicationID, string secrect, GrowthPush.Environment environment, bool debug) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					growthPushSetApplicationId(applicationID, secrect, (int)environment, debug);
				#endif
		}

		public static void TrackEvent(string name, string val) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					growthPushTrackEvent(name, val);
				#endif
		}

		public static void SetTag(string name, string val) {
				#if UNITY_IPHONE && !UNITY_EDITOR
					growthPushSetTag(name, val);
				#endif
		}

		public static void SetDeviceTags() {
				#if UNITY_IPHONE && !UNITY_EDITOR
					growthPushSetDeviceTags();
				#endif
		}

		public static void ClearBadge() {
				#if UNITY_IPHONE && !UNITY_EDITOR
					growthPushClearBadge();
				#endif
		}

};

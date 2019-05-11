#if UNITY_ANDROID && !UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class AndroidSample : Sample {
	AndroidJavaClass CreateNativeClass() => new AndroidJavaClass("com.konh.unityMarshalling.Samples");
	
	public override long GetStrLen(string str) {
		using ( var ajc = CreateNativeClass() ) {
			return ajc.CallStatic<int>("getStrLen", str);
		}
	}

	public override string ToString(int value) {
		using ( var ajc = CreateNativeClass() ) {
			return ajc.CallStatic<string>("toString", value);
		}
	}

	public override Dictionary<string, string> UseDictionary(Dictionary<string, string> dict) {
		using ( var map = new AndroidJavaObject("java.util.HashMap") ) {
			var putMethod = AndroidJNIHelper.GetMethodID(
				map.GetRawClass(), "put",
				"(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;"
			);
			var args = new object[2];
			foreach ( var pair in dict ) {
				using ( var key = new AndroidJavaObject("java.lang.String", pair.Key) )
				using ( var value = new AndroidJavaObject("java.lang.String", pair.Value) ) {
					args[0] = key;
					args[1] = value;
					AndroidJNI.CallObjectMethod(map.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
				}
			}
			using ( var ajc = CreateNativeClass() ) {
				ajc.CallStatic("useMap", map);
			}
			
			using ( var ajc = CreateNativeClass() ) {
				var content = ajc.CallStatic<string>("getContent", map);
				var result = new Dictionary<string, string>();
				var parts = content.Split(':', ',');
				for ( var i = 0; i < parts.Length; i += 2 ) {
					if ( (i + 1) >= parts.Length ) {
						break;
					}
					result.Add(parts[i], parts[i + 1]);
				}
				return result;
			}
		}
	}
}
#endif
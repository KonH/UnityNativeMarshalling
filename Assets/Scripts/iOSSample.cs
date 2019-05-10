#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

class DictMarshaller : ICustomMarshaler {
	static DictMarshaller _instance = new DictMarshaller();

	public IntPtr MarshalManagedToNative(object managedObj) {
		if ( !(managedObj is Dictionary<string, string> dict) ) {
			throw new ArgumentException(nameof(managedObj));
		}
		var ptr = iOSSample.Sample_CreateNsDictionary();
		foreach ( var pair in dict ) {
			iOSSample.Sample_AddPairToNsDictionary(ptr, pair.Key, pair.Value);
		}
		return ptr;
	}

	public object MarshalNativeToManaged(IntPtr pNativeData) {
		var result = new Dictionary<string, string>();
		var index = 0;
		while ( true ) {
			var keyPtr = iOSSample.Sample_GetKeyInNsDictionary(pNativeData, index);
			if ( keyPtr == IntPtr.Zero ) {
				break;
			}
			var key = Marshal.PtrToStringAuto(keyPtr);
			if ( key == null ) {
				throw new InvalidOperationException();
			}
			iOSSample.Sample_FreePtr(keyPtr);

			var valuePtr = iOSSample.Sample_GetValueInNsDictionary(pNativeData, key);
			var value = Marshal.PtrToStringAuto(valuePtr);
			iOSSample.Sample_FreePtr(valuePtr);
			
			result.Add(key, value);
			index++;
		}
		return result;
	}

	public void CleanUpManagedData(object managedObj) {}
	public void CleanUpNativeData(IntPtr pNativeData) {}
	public int  GetNativeDataSize() => -1;

	public static ICustomMarshaler GetInstance(string cookie) => _instance;
}

public class iOSSample : Sample {
	[DllImport("__Internal")]
	static extern long Sample_GetStrLen(string str);

	[DllImport("__Internal")]
	static extern IntPtr Sample_ToStringPtr(int value);

	[DllImport("__Internal")]
	public static extern void Sample_FreePtr(IntPtr ptr);

	[DllImport("__Internal")]
	public static extern IntPtr Sample_CreateNsDictionary();

	[DllImport("__Internal")]
	public static extern void Sample_AddPairToNsDictionary(IntPtr ptr, string key, string value);

	[DllImport("__Internal")]
	public static extern IntPtr Sample_GetKeyInNsDictionary(IntPtr dict, int index);
	
	[DllImport("__Internal")]
	public static extern IntPtr Sample_GetValueInNsDictionary(IntPtr dict, string key);

	// Unfortunately, unexpected MarshalDirectiveException (but why?)
	// [DllImport("__Internal")]
	// static extern void Sample_UseNsDictionary([In][Out][MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DictMarshaller))] Dictionary<string, string> dict);

	[DllImport("__Internal")]
	static extern IntPtr Sample_UseNsDictionary(IntPtr dict);

	public override long GetStrLen(string str) {
		return Sample_GetStrLen(str);
	}

	public override string ToString(int value) {
		var ptr = Sample_ToStringPtr(value);
		var str = Marshal.PtrToStringAuto(ptr);
		Sample_FreePtr(ptr);
		return str;
	}

	public override Dictionary<string, string> UseDictionary(Dictionary<string, string> dict) {
		var ptr = DictMarshaller.GetInstance("").MarshalManagedToNative(dict);
		var resultPtr = Sample_UseNsDictionary(ptr);
		return (Dictionary<string, string>)DictMarshaller.GetInstance("").MarshalNativeToManaged(resultPtr);
	}
}
#endif
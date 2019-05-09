#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class iOSSample : Sample {
	[DllImport("__Internal")]
	static extern long Sample_GetStrLen(string str);
	
	[DllImport("__Internal")]
	static extern IntPtr Sample_ToStringPtr(int value);

	[DllImport("__Internal")]
	static extern void Sample_FreePtr(IntPtr ptr);

	[DllImport("__Internal")]
	static extern IntPtr Sample_CreateNsDictionary();

	[DllImport("__Internal")]
	static extern void Sample_AddPairToNsDictionary(IntPtr ptr, string key, string value);
	
	[DllImport("__Internal")]
	static extern void Sample_PrintNsDictionary(IntPtr ptr);

	public override long GetStrLen(string str) {
		return Sample_GetStrLen(str);
	}
	
	public override string ToString(int value) {
		var ptr = Sample_ToStringPtr(value);
		var str = Marshal.PtrToStringAuto(ptr);
		Sample_FreePtr(ptr);
		return str;
	}

	public override void UseDictionary(Dictionary<string, string> dict) {
		var nsDict = Sample_CreateNsDictionary();
		foreach ( var pair in dict ) {
			Sample_AddPairToNsDictionary(nsDict, pair.Key, pair.Value);
		}
		Sample_PrintNsDictionary(nsDict);
	}
}
#endif
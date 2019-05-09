#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;

public class iOSSample : Sample {
	[DllImport("__Internal")]
	static extern long Sample_GetStrLen(string str);
	
	[DllImport("__Internal")]
	static extern IntPtr Sample_ToStringPtr(int value);

	[DllImport("__Internal")]
	static extern void Sample_FreePtr(IntPtr ptr);

	public override long GetStrLen(string str) {
		return Sample_GetStrLen(str);
	}
	
	public override string ToString(int value) {
		var ptr = Sample_ToStringPtr(value);
		var str = Marshal.PtrToStringAuto(ptr);
		Sample_FreePtr(ptr);
		return str;
	}
}
#endif
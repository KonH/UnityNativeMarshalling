public static class SampleWrapper {
	public static Sample Sample =
	#if UNITY_EDITOR
		new EditorSample();
	#elif UNITY_IPHONE
		new iOSSample();
	#endif
}
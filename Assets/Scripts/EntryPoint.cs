using UnityEngine;

public class EntryPoint : MonoBehaviour {
	void Start() {
		TestStrLens();
		TestToString();
	}

	void TestStrLens() {
		TestStrLen("Hello"); // 5
		TestStrLen("Ñɔβ😀"); // 1 + 1 + 1 + 2 = 5
	}
	
	void TestStrLen(string str) {
		Debug.Log($"Sample_GetStrLen/Unity: \"{str}\"");
		var result = SampleWrapper.Sample.GetStrLen(str);
		Debug.Log($"Sample_GetStrLen/Unity: Result is: {result} (expected: {str.Length})");
	}

	void TestToString() {
		var val = 99;
		Debug.Log($"Sample_ToString/Unity: {val}");
		var result = SampleWrapper.Sample.ToString(val);
		Debug.Log($"Sample_ToString/Unity: Result is: \"{result}\" (expected: \"{val.ToString()}\")");
	}
}

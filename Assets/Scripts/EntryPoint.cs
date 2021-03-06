﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour {
	void Start() {
		TestStrLens();
		TestToString();
		TestDictionary();
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

	void TestDictionary() {
		Debug.Log($"Sample_UseDictionary/Unity");
		var dict = new Dictionary<string, string> {
			{ "unity_key1", "value1" },
			{ "unity_key2", "value2" }
		};
		var result = SampleWrapper.Sample.UseDictionary(dict);
		Debug.Log($"Sample_UseDictionary/Unity: result: {string.Join("; ", result.Select(p => $"\"{p.Key}\" => \"{p.Value}\""))}");
	}
}

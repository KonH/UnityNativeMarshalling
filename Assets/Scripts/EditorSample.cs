using System.Collections.Generic;

public class EditorSample : Sample {
	public override long GetStrLen(string str) => str.Length;
	public override string ToString(int value) => value.ToString();
	public override Dictionary<string, string> UseDictionary(Dictionary<string, string> dict) => dict;
}
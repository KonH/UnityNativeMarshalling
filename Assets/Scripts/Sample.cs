using System.Collections.Generic;

public abstract class Sample {
	public abstract long GetStrLen(string str);
	public abstract string ToString(int value);
	public abstract Dictionary<string, string> UseDictionary(Dictionary<string, string> dict);
}
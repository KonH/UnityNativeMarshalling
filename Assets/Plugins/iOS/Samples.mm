extern "C" {
	NSString* CharPtrToNsString(const char* str) {
		NSLog(@"CharPtrToNsString/native: str: \"%s\"", str);
		return [NSString stringWithUTF8String:str];
	}
	
	char* NsStringToCharPtr(NSString* str) {
		auto utfStr = [str UTF8String];
		NSLog(@"NsStringToCharPtr/native: utfStr: \"%s\"", utfStr);
		// Lifetime of 'utfStr' is shorter
		auto result = (char*)malloc(strlen(utfStr) + 1);
		strcpy(result, utfStr);
		NSLog(@"NsStringToCharPtr/native: result: \"%s\"", result);
		return result;
	}
	
	size_t Sample_GetStrLen(const char* str) {
		NSLog(@"Sample_GetStrLen/native");
		NSLog(@"Sample_GetStrLen/native: str: \"%s\"", str);
		auto nsstr = CharPtrToNsString(str);
		NSLog(@"Sample_GetStrLen/native: nsstr: \"%@\"", nsstr);
		return [nsstr length];
	}
	
	char* Sample_ToStringPtr(int value) {
		NSLog(@"Sample_ToStringPtr/native");
		auto nsstr = [@(value) stringValue];
		return NsStringToCharPtr(nsstr);
	}
	
	void Sample_FreePtr(char* str) {
		NSLog(@"Sample_FreePtr/native");
		free(str);
	}
	
	NSMutableDictionary* Sample_CreateNsDictionary() {
		// Concerns about ARC at this place, possible memory leaks
		// NSMutableDictionary is created here, but not released
		// Pointer is passed to managed code and can't be counted by ARC (it isn't reference)
		// ARC don't permit to release manually
		// But according to profiling memory leaks doesn't happen (and it's strange)
		NSLog(@"Sample_CreateNsDictionary/native");
		auto dict = [[NSMutableDictionary alloc] init];
		NSLog(@"Sample_CreateNsDictionary/native: dict: %@", dict);
		return dict;
	}
	
	void Sample_AddPairToNsDictionary(NSMutableDictionary* dict, const char* key, const char* value) {
		NSLog(@"Sample_AddPairToNsDictionary/native");
		NSLog(@"Sample_AddPairToNsDictionary/native: %@, \"%s\", \"%s\"", dict, key, value);
		auto nsKey = CharPtrToNsString(key);
		auto valueId = CharPtrToNsString(value);
		[dict setValue:valueId forKey:nsKey];
	}
	
	NSMutableDictionary* Sample_UseNsDictionary(NSMutableDictionary* dict) {
		NSLog(@"Sample_UseNsDictionary/native");
		NSLog(@"Sample_UseNsDictionary/native: dict: %@", dict);
		Sample_AddPairToNsDictionary(dict, "nativekey1", "value");
		NSLog(@"Sample_UseNsDictionary/native: updated dict: %@", dict);
		return dict;
	}
	
	char* Sample_GetKeyInNsDictionary(NSMutableDictionary* dict, int index) {
		NSLog(@"Sample_GetKeyInNsDictionary/native");
		NSLog(@"Sample_GetKeyInNsDictionary/native: dict: %@, index: %d", dict, index);
		auto keys = [dict allKeys];
		NSLog(@"Sample_GetKeyInNsDictionary/native: keys: %@", keys);
		auto count = [keys count];
		NSLog(@"Sample_GetKeyInNsDictionary/native: count: %zd", count);
		if ( count > index ) {
			NSString* value = keys[index];
			return NsStringToCharPtr(value);
		}
		NSLog(@"Sample_GetKeyInNsDictionary/native: no more elements");
		return NULL;
	}
	
	char* Sample_GetValueInNsDictionary(NSMutableDictionary* dict, const char* key) {
		NSLog(@"Sample_GetValueInNsDictionary/native");
		NSLog(@"Sample_GetValueInNsDictionary/native: dict: %@, key: \"%s\"", dict, key);
		NSString* nsKey = CharPtrToNsString(key);
		NSString* value = [dict valueForKey:nsKey];
		return NsStringToCharPtr(value);
	}
}

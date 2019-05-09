extern "C" {
	size_t Sample_GetStrLen(const char* str) {
		NSLog(@"Sample_GetStrLen/native");
		NSLog(@"Sample_GetStrLen/native: str: \"%s\"", str);
		auto nsstr = [NSString stringWithUTF8String:str];
		NSLog(@"Sample_GetStrLen/native: nsstr: \"%@\"", nsstr);
		return [nsstr length];
	}
	
	char* Sample_ToStringPtr(int value) {
		NSLog(@"Sample_ToStringPtr/native");
		auto nsstr = [@(value) stringValue];
		NSLog(@"Sample_ToStringPtr/native: nsstr: \"%@\"", nsstr);
		auto utfStr = [nsstr UTF8String];
		NSLog(@"Sample_ToStringPtr/native: utfStr: \"%s\"", utfStr);
		// Lifetime of 'utfStr' is shorter
		auto result = (char*)malloc(strlen(utfStr) + 1);
		strcpy(result, utfStr);
		NSLog(@"Sample_ToStringPtr/native: result: \"%s\"", result);
		return result;
	}
	
	void Sample_FreePtr(char* str) {
		NSLog(@"Sample_FreePtr/native");
		free(str);
	}
}

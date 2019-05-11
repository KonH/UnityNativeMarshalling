package com.konh.unityMarshalling;

import android.util.Log;

import java.util.HashMap;

public class Samples {
    public static int getStrLen(String str) {
        Log.d("nativeSample", "getStrLen: \"" + str + "\"");
        return str.length();
    }

    public static String toString(int value) {
        Log.d("nativeSample", "toString: " + Integer.toString(value));
        return Integer.toString(value);
    }

    public static void useMap(HashMap<String, String> dict) {
        Log.d("nativeSample", "useMap");
        for ( String key : dict.keySet() ) {
            String value = dict.get(key);
            Log.d("nativeSample", "useDictionary: dict: \"" + key + "\" => \"" + value + "\"");
        }
        dict.put("nativekey1", "value");
    }
    
    public static String getContent(HashMap<String, String> dict) {
        Log.d("nativeSample", "getContent");
        String result = "";
        for ( String key : dict.keySet() ) {
            result += key + ":";
            String value = dict.get(key);
            result += value + ",";
        }
        return result;
    }
}
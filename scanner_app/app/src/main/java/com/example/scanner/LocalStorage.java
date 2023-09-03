package com.example.scanner;

import android.content.Context;
import android.content.SharedPreferences;

public class LocalStorage {
    private static SharedPreferences sharedPreferences;

    public static void init(Context context) {
        sharedPreferences = context.getSharedPreferences("localS", Context.MODE_PRIVATE);
    }

    public static void saveString(String key, String value){
        if(sharedPreferences != null){
            sharedPreferences.edit()
                            .putString(key, value)
                            .apply();
        }
    }

    public static String getString(String key){
        if(sharedPreferences != null){
            return sharedPreferences.getString(key, "");
        }
        return "";
    }
}

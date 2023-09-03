package com.example.scanner;

public class Utils {
    public static boolean isValidEAN(String value) {
        if(value.length() != 13){
            return false;
        }

        int check = value.charAt(12) - '0';
        int evenSum = 0;
        int oddSum = 0;

        for(int i = 1; i <= 12; i++){
            if(i % 2 == 0){
                evenSum += value.charAt(i - 1) - '0';
            } else {
                oddSum += value.charAt(i - 1) - '0';
            }
        }

        int totalSum = evenSum * 3 + oddSum;
        return ((10 - (totalSum % 10)) % 10) == check;
    }
}

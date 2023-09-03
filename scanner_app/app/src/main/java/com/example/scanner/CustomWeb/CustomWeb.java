package com.example.scanner.CustomWeb;

import android.content.Context;
import android.webkit.WebView;

import com.example.scanner.Client.Constants;

public class CustomWeb extends WebView implements IWeb {

    private final CustomWebClient customWebClient;
    private final IWeb iWeb;

    private short counter;
    private String search;

    public CustomWeb(Context context, IWeb iWeb) {
        super(context);

        this.iWeb = iWeb;
        counter = 0;

        customWebClient = new CustomWebClient(this);
        setWebViewClient(customWebClient);
    }

    public void setSearch(String search){
        this.search = search;
        counter++;
        customWebClient.setCounter(counter);
        loadUrl(Constants.MOTORA_URL + search);
    }

    @Override
    public void onReceiveValue(String value) {
        if(value.equals("null") && counter < 3){
            counter++;
            customWebClient.setCounter(counter);
            loadUrl(counter == 2 ? Constants.DUFY_URL + search : Constants.APA_URL + search);
            return;
        }
        counter = 0;
        customWebClient.setCounter(counter);
        iWeb.onReceiveValue(value);
    }
}

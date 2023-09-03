package com.example.scanner.CustomWeb;

import android.webkit.ValueCallback;
import android.webkit.WebView;
import android.webkit.WebViewClient;

public class CustomWebClient extends WebViewClient {

    private static final String SCRIPT_MOTORA = "(function() {" +
            " return document.querySelector('.products-list .title a').innerText; })();";

    private static final String SCRIPT_DUFFY = "(function() {" +
            " return document.querySelector('.productInline .productInline__main h2').innerText; })();";

    private static final String SCRIPT_APA = "(function() {" +
            " return document.querySelector('.cms-card .cms-post-content h4').innerText; })();";

    private final IWeb valueCallback;
    private short count;

    public CustomWebClient(IWeb valueCallback){
        this.valueCallback = valueCallback;
        count = 1;
    }

    public void setCounter(short value){
        count = value;
    }

    @Override
    public void onPageFinished(WebView view, String url) {
        super.onPageFinished(view, url);
        String script = SCRIPT_MOTORA;
        if(count == 2){
            script = SCRIPT_DUFFY;
        } else if(count == 3){
            script = SCRIPT_APA;
        }

        view.evaluateJavascript(script, new ValueCallback<String>() {
            @Override
            public void onReceiveValue(String value) {
                valueCallback.onReceiveValue(value);
            }
        });
    }
}

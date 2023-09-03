package com.example.scanner.Client;

public interface IDataSender {
    void onConnected();
    void onConnectionFiled();
    void onSendFailed();
    void onSend();
    void onPingSendFailed();
}

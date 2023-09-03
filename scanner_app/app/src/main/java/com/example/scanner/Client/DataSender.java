package com.example.scanner.Client;

import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class DataSender {
    private String serverIP;
    private int port;
    private Socket socket;
    private OutputStream outputStream;
    private final IDataSender listener;
    private boolean isConnecting;

    private final ExecutorService executor;

    public DataSender(IDataSender listener) {
        this.listener = listener;
        isConnecting = false;

        executor = Executors.newCachedThreadPool();
    }

    public void connect(String serverIP, int port) {
        disconnect();
        this.serverIP = serverIP;
        this.port = port;
        connect();
    }

    private void connect() {
        if (isConnecting) {
            return;
        }
        isConnecting = true;
        executor.execute(() -> {
            try {
                socket = new Socket(serverIP, port);
                socket.setKeepAlive(true);
                outputStream = socket.getOutputStream();
                listener.onConnected();
            } catch (IOException e) {
                e.printStackTrace();
                listener.onConnectionFiled();
            } finally {
                isConnecting = false;
            }
        });
    }

    public void sendData(String data) {
        executor.execute(() -> {
            try {
                if (outputStream != null) {
                    outputStream.write(data.getBytes());
                    outputStream.flush();
                    listener.onSend();
                } else {
                    listener.onSendFailed();
                }
            } catch (IOException e) {
                listener.onSendFailed();
                e.printStackTrace();
            }
        });
    }

    public void sendPing() {
        executor.execute(() -> {
            try {
                if (outputStream != null) {
                    outputStream.write("PING".getBytes());
                    outputStream.flush();
                } else {
                    listener.onPingSendFailed();
                }
            } catch (IOException e) {
                listener.onPingSendFailed();
                e.printStackTrace();
            }
        });
    }

    public void disconnect() {
        executor.execute(() -> {
            try {
                if (socket != null) {
                    socket.close();
                }
                if (outputStream != null) {
                    outputStream.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        });
    }
}
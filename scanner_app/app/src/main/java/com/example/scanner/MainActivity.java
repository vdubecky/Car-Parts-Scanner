package com.example.scanner;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.content.res.AppCompatResources;

import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.PowerManager;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.budiyev.android.codescanner.CodeScanner;
import com.budiyev.android.codescanner.CodeScannerView;
import com.example.scanner.Client.Constants;
import com.example.scanner.Client.DataSender;
import com.example.scanner.Dialogs.IConnectionDialog;
import com.example.scanner.Client.IDataSender;
import com.example.scanner.CustomWeb.CustomWeb;
import com.example.scanner.CustomWeb.IWeb;
import com.example.scanner.Dialogs.ConnectionDialog;
import com.example.scanner.Dialogs.IProductDialog;
import com.example.scanner.Dialogs.NewProductDialog;
import com.google.zxing.Result;


public class MainActivity extends AppCompatActivity implements IWeb, IConnectionDialog, IDataSender, IProductDialog {

    private int port;
    private String serverIP;

    private LinearLayout linearLayout;

    private TextView productName;
    private TextView productCode;
    private TextView connectionStatusText;

    private Button repeatConnection;
    private Button statusButton;

    private View progressBar;

    private CodeScanner mCodeScanner;
    private CustomWeb webView;

    private String result;
    private String product;

    private DataSender dataSender;

    private boolean isScanned;
    private boolean connecting;
    private boolean connected;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        requestPermissions(new String[]{android.Manifest.permission.CAMERA}, 1);

        webView = new CustomWeb(this, this);
        dataSender = new DataSender(this);

        initViews();
        initClickListeners();
        LocalStorage.init(this);
        initStatus();
        startScanRoutine();
    }

    private void initStatus(){
        serverIP = LocalStorage.getString(Constants.IP_KEY);
        String strPort = LocalStorage.getString(Constants.PORT_KEY);

        if(serverIP.equals("") || strPort.equals("")){
            connectionStatusText.setText(R.string.set_ip_text);
        } else {
            port = Integer.parseInt(strPort);
            tryConnect();
        }
    }

    private void initViews(){
        linearLayout = findViewById(R.id.buttonActionBar);
        productName = findViewById(R.id.productName);
        productCode = findViewById(R.id.productCode);
        connectionStatusText = findViewById(R.id.connectionStatusText);
        progressBar = findViewById(R.id.progressBar);
        repeatConnection = findViewById(R.id.repeatConnection);
        statusButton = findViewById(R.id.statusButton);

        repeatConnection.setVisibility(View.GONE);
    }

    private void initClickListeners(){
        findViewById(R.id.saveButton).setOnClickListener(v -> sendData());
        findViewById(R.id.repeatButton).setOnClickListener(v -> resetScan());
        findViewById(R.id.connectionStatus).setOnClickListener(v -> showConnectionStatus());
        findViewById(R.id.addManually).setOnClickListener(v -> showAddProductDialog());
        repeatConnection.setOnClickListener(v -> tryConnect());
    }

    private void resetScan(){
        setOutput("", "");
        isScanned = false;
        mCodeScanner.startPreview();
        linearLayout.setVisibility(View.INVISIBLE);
    }

    private void showConnectionStatus(){
        if(!connecting){
            showConnectionDialog();
        } else {
            Toast.makeText(getApplicationContext(), "Při připojování nelze měnit konfiguraci", Toast.LENGTH_SHORT).show();
        }
    }

    private void showAddProductDialog(){
        mCodeScanner.stopPreview();
        new NewProductDialog(this, MainActivity.this).show();
    }

    private void showConnectionDialog() {
        new ConnectionDialog(this, MainActivity.this).show();
    }

    private void startScanRoutine(){
        webView.getSettings().setJavaScriptEnabled(true);

        CodeScannerView scannerView = findViewById(R.id.scanner_view);
        mCodeScanner = new CodeScanner(this, scannerView);
        mCodeScanner.setDecodeCallback(this::onDecodedRoutine);
        mCodeScanner.startPreview();
    }

    private void sendData(){
        try {
            if(dataSender == null){
                Toast.makeText(getApplicationContext(), "Spojení se serverem nebylo navázáno", Toast.LENGTH_SHORT).show();
                return;
            }
            if(result.trim().length() == 0 || product.trim().length() == 0){
                Toast.makeText(getApplicationContext(), "Nesprávný formát dat", Toast.LENGTH_SHORT).show();
                return;
            }

            product.replace(';', '-');
            result.replace(';', '-');

            String resOutput = result + ";" + product;
            dataSender.sendData(resOutput);
        } catch (Exception e) {
            Toast.makeText(getApplicationContext(), e.getMessage(), Toast.LENGTH_SHORT).show();
            e.printStackTrace();
        }
    }

    private void onDecodedRoutine(Result res){
        runOnUiThread(() -> {
            if(Utils.isValidEAN(res.getText())){
                isScanned = true;
                result = res.getText();
                //onReceiveValue("Nějaky zvlastni autodil");
                progressBar.setVisibility(View.VISIBLE);
                webView.setSearch(result);
            } else {
                mCodeScanner.startPreview();
            }
        });
    }

    private void tryConnect() {
        connecting = true;
        connected = false;
        repeatConnection.setVisibility(View.GONE);
        dataSender.connect(serverIP, port);
        connectionStatusText.setText(String.format(getString(R.string.connecting_to), serverIP + ":" + port));
        statusButton.setBackground(AppCompatResources.getDrawable(this, R.drawable.rounded_indicator_offline));
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        if(requestCode == 1 && grantResults[0] == PackageManager.PERMISSION_GRANTED){
            mCodeScanner.startPreview();
        }
    }

    @Override
    public void onReceiveValue(String value) {
        isScanned = true;

        if((value.equals("null"))){
            setOutput("nenalezen", result);
        } else {
            setOutput(value.replace("\"", "").replace("\\n", " "), result);
        }

        progressBar.setVisibility(View.INVISIBLE);
        linearLayout.setVisibility(View.VISIBLE);
    }

    private void setOutput(String product, String code) {
        this.product = product;
        result = code;

        productName.setText(String.format(getString(R.string.product_title), product));
        productCode.setText(String.format(getString(R.string.product_code), code));
    }

    @Override
    public void onConnectClicked(String serverIP, int port) {
        this.port = port;
        this.serverIP = serverIP;

        LocalStorage.saveString(Constants.IP_KEY, serverIP);
        LocalStorage.saveString(Constants.PORT_KEY, String.valueOf(port));

        tryConnect();
    }

    @Override
    public void onConnected() {
        runOnUiThread(() -> {
            connecting = false;
            connected = true;
            connectionStatusText.setText(String.format(getString(R.string.connected_to), serverIP + ":" + port));
            statusButton.setBackground(AppCompatResources.getDrawable(MainActivity.this, R.drawable.rounded_indicator_online));
            repeatConnection.setVisibility(View.GONE);
        });
    }

    @Override
    public void onConnectionFiled() {
        runOnUiThread(() -> {
            connecting = false;
            connected = false;
            connectionStatusText.setText(String.format(getString(R.string.failed_to), serverIP + ":" + port));
            statusButton.setBackground(AppCompatResources.getDrawable(MainActivity.this, R.drawable.rounded_indicator_offline));
            repeatConnection.setVisibility(View.VISIBLE);
        });
    }

    @Override
    public void onSendFailed() {
        runOnUiThread(() -> {
            Toast.makeText(MainActivity.this, "Nepodařilo se odeslat data.", Toast.LENGTH_LONG).show();

            if(connecting || !connected){
                return;
            }

            connectionStatusText.setText(String.format(getString(R.string.connection_failed), serverIP + ":" + port));
            statusButton.setBackground(AppCompatResources.getDrawable(MainActivity.this, R.drawable.rounded_indicator_offline));
            repeatConnection.setVisibility(View.VISIBLE);
        });
    }

    @Override
    public void onSend() {
        runOnUiThread(() -> {
            isScanned = false;
            setOutput("", "");
            linearLayout.setVisibility(View.INVISIBLE);
            mCodeScanner.startPreview();
        });
    }

    @Override
    public void onPingSendFailed() {
        runOnUiThread(() -> {
            if(connecting || !connected){
                return;
            }
            connectionStatusText.setText(String.format(getString(R.string.connection_failed), serverIP + ":" + port));
            statusButton.setBackground(AppCompatResources.getDrawable(MainActivity.this, R.drawable.rounded_indicator_offline));
            repeatConnection.setVisibility(View.VISIBLE);
        });
    }

    @Override
    public void onAddClicked(String name, String code) {
        result = code;
        onReceiveValue(name);
    }

    @Override
    public void onAddCancel() {
        if(!isScanned){
            mCodeScanner.startPreview();
        }
    }

    @Override
    public void onResume() {
        if(!isScanned){
            mCodeScanner.startPreview();
        }

        if(dataSender != null && !connecting){
            dataSender.sendPing();
        }

        super.onResume();
    }

    @Override
    public void onPause() {
        mCodeScanner.releaseResources();
        super.onPause();
    }
}
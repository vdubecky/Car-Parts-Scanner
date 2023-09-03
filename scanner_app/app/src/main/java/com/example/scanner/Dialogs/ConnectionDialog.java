package com.example.scanner.Dialogs;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.example.scanner.R;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class ConnectionDialog extends Dialog {

    private final IConnectionDialog listener;
    private final Context context;
    private final Pattern pattern;

    public ConnectionDialog(IConnectionDialog listener, Context context){
        super(context);
        this.listener = listener;
        this.context = context;

        String ipAddressRegex = "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        pattern = Pattern.compile(ipAddressRegex);;
    }

    private AlertDialog buildDialog() {
        LayoutInflater inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.connection_dialog, null);

        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setView(view);
        return builder.create();
    }

    @Override
    public void show() {
        AlertDialog dialog = buildDialog();
        dialog.show();

        final EditText editTextIp = dialog.findViewById(R.id.editTextIp);
        final EditText editTextPort = dialog.findViewById(R.id.editTextPort);

        dialog.findViewById(R.id.btnConnect).setOnClickListener(v -> {
            String ipAddress = editTextIp.getText().toString();
            int port = 0;

            Matcher matcher = pattern.matcher(ipAddress);

            if(!matcher.matches()){
                Toast.makeText(getContext(), "Zadejte IP adresu v korektním formátu", Toast.LENGTH_SHORT).show();
                return;
            }

            try {
                port = Integer.parseInt(editTextPort.getText().toString());
            } catch (Exception ex){
                Toast.makeText(getContext(), "Port musí být celé číslo", Toast.LENGTH_SHORT).show();
                return;
            }

            listener.onConnectClicked(ipAddress, port);
            dialog.cancel();
        });

        dialog.findViewById(R.id.btnCancel).setOnClickListener(v -> dialog.dismiss());
    }
}

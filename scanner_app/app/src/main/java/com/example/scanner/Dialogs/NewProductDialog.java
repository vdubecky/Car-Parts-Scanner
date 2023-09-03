package com.example.scanner.Dialogs;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import com.example.scanner.R;

public class NewProductDialog extends Dialog {
    private final IProductDialog listener;
    private final Context context;

    public NewProductDialog(IProductDialog listener, Context context){
        super(context);
        this.listener = listener;
        this.context = context;
    }

    private AlertDialog buildDialog() {
        LayoutInflater inflater = LayoutInflater.from(context);
        View view = inflater.inflate(R.layout.new_product_dialog, null);

        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setView(view);
        return builder.create();
    }

    @Override
    public void show() {
        AlertDialog dialog = buildDialog();
        dialog.show();

        final EditText editTextName = dialog.findViewById(R.id.editTextName);
        final EditText editTextCode = dialog.findViewById(R.id.editTextCode);

        dialog.findViewById(R.id.btnConnect).setOnClickListener(v -> {
            String productName = editTextName.getText().toString();
            String productCode = editTextCode.getText().toString();

            if(productCode.trim().length() == 0 || productName.trim().length() == 0){
                Toast.makeText(getContext(), "Některé z vstupních polí je prázdné.", Toast.LENGTH_SHORT).show();
                return;
            }

            if(productCode.contains(";") || productName.contains(";")){
                Toast.makeText(getContext(), "Vstup nesmí obsahovat znak ';'", Toast.LENGTH_SHORT).show();
                return;
            }

            listener.onAddClicked(productName, productCode);
            dialog.dismiss();
        });

        dialog.findViewById(R.id.btnCancel).setOnClickListener(v -> dialog.cancel());
        dialog.setOnCancelListener(v -> listener.onAddCancel());
    }
}

package com.example.fince.gzsmobil;

import android.bluetooth.BluetoothDevice;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.UUID;

public class MainActivity extends AppCompatActivity implements AdapterView.OnItemClickListener {
    private ListView lvNewDevices;
    private BluetoothController bc;
    BluetoothConnectionService mBluetoothConnection;
    BluetoothDevice mBTDevice;
    TextView red,green;
    private static final UUID MY_UUID_INSECURE=
            UUID.fromString("8ce255c0-200a-11e0-ac64-0800200c9a66");
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        lvNewDevices=(ListView)findViewById(R.id.listView);
        lvNewDevices.setOnItemClickListener(MainActivity.this);
        red=(TextView)findViewById(R.id.textView5);
        green=(TextView)findViewById(R.id.textView6);
    }

    public void incomingMessage(String message)
    {
       setApple(message);
    }
    public MainActivity()
    {
        bc=new BluetoothController(this,this);

    }
    public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
        mBluetoothConnection=bc.cancelDiscovery(i,MainActivity.this,this);


    }
    public void drawView()
    {
        lvNewDevices.setAdapter(bc.getmDeviceListAdapter());
    }
    StringBuilder messages;
    public void setMessages(StringBuilder messages) {
        this.messages = messages;
        Log.i("TAG: sendMessages","Gelen Mesaj : "+messages);
    }
    public void btAcBasildi(View view)
    {
        bc.enableOrDisableBluetooth();
        if(!bc.isBtIsActive())
        {
            lvNewDevices.setAdapter(null);
        }

    }
    @RequiresApi(api = Build.VERSION_CODES.M)
    public void cihazlarTiklandi(View view)
    {
        if(bc.isBtIsActive())
        {
            bc.beDiscovered4Devices();
            bc.discoverDevices();



        }
        else
            Toast.makeText(getBaseContext(),"Bluetooth'u AÇ!",Toast.LENGTH_SHORT).show();
    }
    public void baglanArtik(View view)
    {
        mBTDevice=bc.getmBTDevice();
        if(mBTDevice != null)
        {
            if(mBluetoothConnection.isConnect() == false)
            {
                mBluetoothConnection.startBTConnection(mBTDevice,MY_UUID_INSECURE);
                while(!mBluetoothConnection.isConnect()){Log.d("","bekleniyor");};
                Log.i("","bağlandı");


            }

        }
        else
        {
            Toast.makeText(getBaseContext(),"PC'yi SEÇ!.",Toast.LENGTH_SHORT).show();
        }
    }
    public void setApple(final String message)
    {
        new Thread()
        {
            public void run()
            {
                MainActivity.this.runOnUiThread(new Runnable()
                {
                    public void run()
                    {

                        String[] parcalar=message.split("-");
                        if(parcalar[0].equals("K"))
                        {
                            red.setText(parcalar[1]);
                        }
                        else
                        {
                            green.setText(parcalar[1]);
                        }

                    }
                });
            }
        }.start();
    }
}

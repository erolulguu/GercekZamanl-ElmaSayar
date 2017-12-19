using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace elmalar
{
    class arduinoController
    {
        
        private arayuz View;
      

        public arduinoController(arayuz View)
        {
            this.View = View;
        }
        public void scan()
        {
            List<String> gecici = new List<string>();
            foreach (string portlar in System.IO.Ports.SerialPort.GetPortNames())
            {
                gecici.Add(portlar);
            }
            View.updateList(gecici);
        }
        public void baglan (String name)
        {
            try
            {
                View.serialPort1.PortName = name;
                View.serialPort1.BaudRate = 9600;
                View.serialPort1.Open();
                Console.WriteLine("Giriş Başarılı");
                Thread dinle = new Thread(ard_Dinle);
                dinle.IsBackground = true;
                dinle.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("Hata:" + e.Message);
            }
        }
        public void ard_Dinle()
        {
            while(true)
            {
                String message = View.serialPort1.ReadLine();
                Console.WriteLine(message);
                updateUI(message);
                

            }
        }
        private void updateUI(String message)
        {
            Func<int> del = delegate ()
            {
                String[] gecici = message.Split('-');
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\erol\source\repos\elmalar\elmalar\elmalar.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();


                
                if (gecici[0].Equals("K"))
                {

                    View.label6.Text = gecici[1];
                    SqlCommand ekle = new SqlCommand("UPDATE [table1] SET kapple=" + gecici[1] + " WHERE id1=1", con);
                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Kırmızı Elma Eklendi.");
                }
                else
                {
                    View.label7.Text = gecici[1];
                    SqlCommand ekle = new SqlCommand("UPDATE [table2] SET gapple=" + gecici[1] + " WHERE id=1", con);
                    ekle.ExecuteNonQuery();
                    MessageBox.Show("Yeşil Elma Eklendi.");
                }
                return 0;
            };
            View.Invoke(del);

        }
    }
}

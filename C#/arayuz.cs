using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GZSProjesi;

namespace elmalar
{
    public partial class arayuz : Form
        
    {
        arduinoController ac;
        private BluetoothServer bs;
        public arayuz()
        {
            InitializeComponent();
            ac = new arduinoController(this);
         
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void updateList (List<String> list)
        {
            comboBox1.Items.Clear();
            for (int i=0; i<list.Count; i++)
            {
                comboBox1.Items.Add(list[i]);
            }
        }

        private void arayuz_Load(object sender, EventArgs e)
        {
        
            ac.scan();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            bs = new BluetoothServer(this);
            bs.ConnectAsServer();
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String secilen = comboBox1.Text;
            ac.baglan(secilen);
            pictureBox1.Image = Image.FromFile( "C:\\Users\\erol\\Downloads\\tik.png");
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}

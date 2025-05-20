using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Database_Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.PerformClick();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //sol panel
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //sağ panel
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //menü labelı
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //anasayfa
            panel2.Controls.Clear();
            DashboardKontrol dash = new DashboardKontrol();
            dash.Dock = DockStyle.Fill;
            panel2.Controls.Add(dash);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ilanlarım
            panel2.Controls.Clear();
            ilanlarKontrol ilanlarim = new ilanlarKontrol();
            ilanlarim.Dock = DockStyle.Fill;
            panel2.Controls.Add(ilanlarim);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //rezervasyonlar
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ödeme
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //yorum ve puan
        }
    }
}

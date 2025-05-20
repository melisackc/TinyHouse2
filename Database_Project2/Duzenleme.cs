using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Database_Project2
{
    public partial class Duzenleme : Form
    {

        private int ilanID;
        private string connectionString = "Server=MELISA\\MSSQLSERVER01;Database=Database_project;Trusted_Connection=True;";
        private string secilenResimYolu = "";
        public Duzenleme(int id)
        {
            InitializeComponent();
            ilanID = id;
            this.Load += Duzenleme_Load;  // Load eventini bağla
        }

        private void Duzenleme_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Ilanlar WHERE ilanNumarasi = @ilanID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ilanID", ilanID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox1.Text = reader["ilanBasligi"].ToString();
                    textBox2.Text = reader["evTipi"].ToString();
                    textBox3.Text = reader["fiyat"].ToString();
                    textBox4.Text = reader["adres"].ToString();
                    textBox5.Text = reader["ilanSahibi"].ToString();
                    richTextBox1.Text = reader["aciklama"].ToString();                    
                    dateTimePicker1.Value = reader["tarih"] != DBNull.Value ? Convert.ToDateTime(reader["tarih"]) : DateTime.Now;
                    comboBox1.SelectedIndex = reader["durum"] != DBNull.Value && (bool)reader["durum"] ? 0 : 1;
                    pictureBox1.ImageLocation = reader["fotografYolu"].ToString();
                    pictureBox1.ImageLocation = secilenResimYolu;

                }

                conn.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ilan başlığı
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //Ev Tipi
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Fiyat
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        { 
            //Adres
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //ilan Sahibi
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aktif/Pasif
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //Açıklama
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Güncelle butonu
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Ilanlar SET 
                                 ilanBasligi = @baslik,
                                 evTipi = @evTipi,
                                 fiyat = @fiyat,
                                 adres = @adres,
                                 ilanSahibi = @sahip,
                                 aciklama = @aciklama,
                                 tarih = @tarih,
                                 durum = @durum,
                                 fotografYolu = @fotograf
                                 WHERE ilanNumarasi = @ilanID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ilanID", ilanID);
                cmd.Parameters.AddWithValue("@baslik", textBox1.Text);
                cmd.Parameters.AddWithValue("@evTipi", textBox2.Text);
                cmd.Parameters.AddWithValue("@fiyat", decimal.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@adres", textBox4.Text);
                cmd.Parameters.AddWithValue("@sahip", textBox5.Text);
                cmd.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@tarih", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@durum", comboBox1.SelectedIndex == 0); // 0: Aktif, 1: Pasif
                cmd.Parameters.AddWithValue("@fotograf", secilenResimYolu);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("İlan başarıyla güncellendi.");
                this.Close();
            }
        }
    

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Seç butonu            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                secilenResimYolu = ofd.FileName;
                pictureBox1.Image = Image.FromFile(secilenResimYolu);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Resim yükle
        }
    }
}

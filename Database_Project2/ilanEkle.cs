using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Database_Project2
{
    public partial class ilanEkle : Form
    {
        public ilanEkle()
        {
            InitializeComponent();
        }

        private void ilanEkle_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //fotoğraf ekleme
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ilan başlığı
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // ev tipi
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //fiyat
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //adres
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //ilan numarası
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //ilan sahibi
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //ev açıklaması
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ilan ver butonu
            // Boş alan kontrolü
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||  // ilan başlığı
                string.IsNullOrWhiteSpace(textBox3.Text) ||  // ev tipi
                string.IsNullOrWhiteSpace(textBox2.Text) ||  // fiyat
                string.IsNullOrWhiteSpace(textBox4.Text) ||  // adres
                //string.IsNullOrWhiteSpace(textBox5.Text) ||  // ilan numarası
                string.IsNullOrWhiteSpace(textBox6.Text) ||  // ilan sahibi
                string.IsNullOrWhiteSpace(richTextBox1.Text) || // açıklama
                pictureBox1.ImageLocation == null) // fotoğraf kontrolü
            {
                MessageBox.Show("Lütfen tüm alanları doldurun ve bir fotoğraf seçin.");
                return;
            }

            try
            {
                // Veritabanı bağlantısı (veritabanı adını kendine göre düzenle)
                string connectionString = "Server=MELISA\\MSSQLSERVER01;Database=Database_project;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Ilanlar (ilanBasligi, evTipi, fiyat, adres, ilanSahibi, aciklama, fotografYolu) " +
                                   "VALUES (@ilanBasligi, @evTipi, @fiyat, @adres,@ilanSahibi, @aciklama, @foto)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ilanBasligi", textBox1.Text);
                        cmd.Parameters.AddWithValue("@evTipi", textBox3.Text);
                        cmd.Parameters.AddWithValue("@fiyat", Convert.ToDecimal(textBox2.Text));
                        cmd.Parameters.AddWithValue("@adres", textBox4.Text);
                       // cmd.Parameters.AddWithValue("@ilanNumarasi", Convert.ToInt32(textBox5.Text));
                        cmd.Parameters.AddWithValue("@ilanSahibi", textBox6.Text);
                        cmd.Parameters.AddWithValue("@aciklama", richTextBox1.Text);
                        cmd.Parameters.AddWithValue("@foto", pictureBox1.ImageLocation);

                        int sonuc = cmd.ExecuteNonQuery();

                        if (sonuc > 0)
                        {
                            MessageBox.Show("İlan başarıyla eklendi.");
                            this.Close(); // Formu kapat
                        }
                        else
                        {
                            MessageBox.Show("Kayıt başarısız.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //seç butonu
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Bir fotoğraf seçiniz";
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}

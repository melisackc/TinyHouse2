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

namespace Database_Project2
{
    public partial class ilanlarKontrol : UserControl
    {
        public ilanlarKontrol()
        {
            InitializeComponent();
        }

        private void ilanlarKontrol_Load(object sender, EventArgs e)
        {
            LoadIlanlar();
        }

        private void LoadIlanlar()
        {
            string connectionString = "Server=MELISA\\MSSQLSERVER01;Database=Database_project;Trusted_Connection=True;";
            string query = "SELECT ilanNumarasi, ilanBasligi, evTipi, fiyat, adres FROM Ilanlar";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                // Kolon başlıklarını özelleştir (isteğe bağlı)
                dataGridView1.Columns["ilanNumarasi"].HeaderText = "ID";
                dataGridView1.Columns["ilanBasligi"].HeaderText = "Başlık";
                dataGridView1.Columns["evTipi"].HeaderText = "Ev Tipi";
                dataGridView1.Columns["fiyat"].HeaderText = "Fiyat";
                dataGridView1.Columns["adres"].HeaderText = "Adres";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //ilan verme 
            ilanEkle ilanEkleForm = new ilanEkle();
            ilanEkleForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ilan silme
            Silme silmeForm = new Silme(); // Silme formunu oluştur
            silmeForm.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*Duzenleme duzenleForm = new Duzenleme();
            duzenleForm.ShowDialog(); */
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen düzenlemek için bir ilan seçin.");
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ilanNumarasi"].Value);

            Duzenleme duzenleForm = new Duzenleme(ilanID);
            duzenleForm.ShowDialog();

            // İstersen düzenleme sonrası listeyi yenile
            LoadIlanlar();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

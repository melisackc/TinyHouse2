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
    public partial class Silme : Form
    {
        string connectionString = "Server=MELISA\\MSSQLSERVER01;Database=Database_project;Trusted_Connection=True;";
        public Silme()
        {
            InitializeComponent();
        }

        private void Silme_Load(object sender, EventArgs e)
        {
            IlanlariYukle();
        }

        private void IlanlariYukle()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ilanNumarasi, ilanBasligi, evTipi, fiyat FROM Ilanlar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Silme butonu
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek için bir ilan seçin.");
                return;
            }

            int ilanID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ilanNumarasi"].Value);

            DialogResult result = MessageBox.Show("Seçili ilanı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Ilanlar WHERE ilanNumarasi = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", ilanID);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("İlan başarıyla silindi.");
                IlanlariYukle(); // listeyi yenile
            }
        }
    }
    
}

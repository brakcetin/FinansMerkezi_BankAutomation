using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinansMerkezi
{
    public partial class viewFD : Form
    {
        public viewFD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewDisplay();
            string searchDate = dateTimePicker1.Value.ToString("dd / MM / yyyy");
            if (string.IsNullOrEmpty(searchDate))
            {
                MessageBox.Show("Lütfen bir tarih seçiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    string query = "SELECT * FROM fixed_deposit WHERE Start_Date=@startdate";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@startdate", searchDate);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // DataGridView'e verileri yükleyin
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);
                            dataGridView1.DataSource = dataTable;
                            ChangeNamesofColumns();
                        }
                    }
                }
            }

        }
        private void DataGridViewDisplay()
        {
            //datagridview1
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //satır ve sütun genişliğini otomatik ayarlar
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        }
        //sütun adları veri tabanında ingilizce olduğu için türkçeye çevrilir
        private void ChangeNamesofColumns()
        {
            //para çekme
            dataGridView1.Columns["Account_No"].HeaderText = "Hesap Numarası";
            dataGridView1.Columns["Liras"].HeaderText = "Yatırılacak Tutar (TL)";
            dataGridView1.Columns["Period"].HeaderText = "Süre (gün)";
            dataGridView1.Columns["Mode"].HeaderText = "Yöntem";
            dataGridView1.Columns["Interest_rate"].HeaderText = "Faiz (%)";
            dataGridView1.Columns["Maturity_date"].HeaderText = "Vade Sonu";
            dataGridView1.Columns["Maturity_Amount"].HeaderText = "Vade Miktarı";
            dataGridView1.Columns["Start_Date"].HeaderText = "Başlangış Tarihi";
        }
    }
}

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
            string searchDate = dateTimePicker1.Value.ToString("dd / MM / yyyy");
            if(string.IsNullOrEmpty(searchDate))
            {
                MessageBox.Show("Lütfen bir tarih seçiniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        }
                    }
                }
            }
            
        }
    }
}

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
    public partial class allUsers : Form
    {
        public allUsers()
        {
            InitializeComponent();
            loadUserAcc();
        }

        private void loadUserAcc()
        {
            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM useraccount";

                using(MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using(MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        dataGridView1.AutoGenerateColumns = false;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
    }
}

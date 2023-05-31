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
    public partial class admin_login : Form
    {
        public admin_login()
        {
            InitializeComponent();
        }


        private void login_btn_Click(object sender, EventArgs e)
        {
            string _server = "localhost";
            string _dbname = "deneme";
            string _user = "root";
            string _pass = "bB23aQkM18zcBxy";

            string MySqlBaglanti = $"SERVER={_server};DATABASE={_dbname};UID={_user};PWD={_pass}";



            if (string.IsNullOrEmpty(userTxt.Text) == true || string.IsNullOrEmpty(passTxt.Text) == true)
            {
                return;
            }
            else
            {

                using (var baglan = new MySqlConnection(MySqlBaglanti))
                {
                    try
                    {
                        baglan.Open();
                        string stm = "SELECT username,password FROM admin_table WHERE username =@Username AND password =@Password";
                        var cmd = new MySqlCommand(stm, baglan);

                        cmd.Parameters.AddWithValue("@Username", userTxt.Text);
                        cmd.Parameters.AddWithValue("@Password", passTxt.Text);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Kullanıcı doğrulandı, işlemler yapılabilir
                                while (reader.Read())
                                {
                                    // Verileri işleme
                                    string username = reader.GetString(0);
                                    string password = reader.GetString(1);
                                }
                                ClearFormFields();
                                Menu m1 = new Menu();
                                m1.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Yanlış kullanıcı adı ya da şifre girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                    }



                }
            }
        }
        private void ClearFormFields()
        {
            userTxt.Text = string.Empty;
            passTxt.Text = string.Empty;
        }
    }
}

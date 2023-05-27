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
    public partial class transferForm : Form
    {
        private Informations infos;
        public transferForm()
        {
            InitializeComponent();
            loaddate();
            infos = new Informations();
        }
        private void loaddate()
        {
            label2.Text = DateTime.UtcNow.ToString("dd / MM / yyyy");
        }

        private void detailsBtn_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                infos.AccountNo = Convert.ToDecimal(fromaccnoTxt.Text);
                //Hesap numarasını kullanarak bakiye ve ad-soyad bilgilerini sorgular ve ilgili TextBox'lara yazdırır
                string query = "SELECT Name, Balance FROM useraccount WHERE Account_No = @accountNo";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountNo", infos.AccountNo);

                    try
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nameTxt.Text = reader["Name"].ToString();
                                infos.Name = nameTxt.Text;
                                blncTxt.Text = reader["Balance"].ToString();
                                infos.Balance = Convert.ToDecimal(blncTxt.Text);
                            }
                            else
                            {
                                MessageBox.Show("Hesap numaranız bulunamadı", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Sorgu Hatası:", ex.Message);
                    }
                }
            }
        }

        private void sentBtn_Click(object sender, EventArgs e)
        {
            decimal new_fromaccno;
            string toaccno = toaccnoTxt.Text;
            decimal toamount;
            string date = label2.Text;

            //alanlar boş bırakıldığı durumda hata verir
            if (!decimal.TryParse(fromaccnoTxt.Text, out new_fromaccno))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(toamountTxt.Text, out toamount))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(toaccno))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (infos.AccountNo != new_fromaccno)
                {
                    MessageBox.Show("Hesap numarası değiştirilemez.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hata durumunda işlemi sonlandırır
                }
                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    string query1 = "SELECT Account_No FROM useraccount";
                    using (MySqlCommand command = new MySqlCommand(query1, connection))
                    {

                    }
                        string query2 = "INSERT INTO transfer (date, account_no, name, balance, totransfer) " +
                        "VALUES (@date, @accountNo, @Name, @balance,@totransfer)";
                    using (MySqlCommand command = new MySqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                        command.Parameters.AddWithValue("@Name", infos.Name);
                        command.Parameters.AddWithValue("@balance", infos.Balance);
                        command.Parameters.AddWithValue("@totransfer", toamount);
                        command.Parameters.AddWithValue("@date", date);

                        try     //yapılan işlem başarılı mı değil mi ona göre uyarı verir
                        {
                            // Sorguyu çalıştırın
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Mevzuat işlemi başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Yatırım işlemi gerçekleştirilemedi.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Sorgu Hatası: " + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace FinansMerkezi
{
    public partial class creditForm : Form
    {
        //Hesap numarasını pek çok class'ta ihtiyacımız olduğu için
        //Informations adlı bir class oluşturdum ve hesap numarasını oradan çağırıyorum
        private Informations infos; 
        public creditForm()
        {
            InitializeComponent();
            loaddate();
            loadmode();
            infos = new Informations();
        }

        private void loadmode()
        {
            modecomboBox.Items.Add("Nakit İşlemi");
            modecomboBox.Items.Add("Çek İşlemi");
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

                infos.AccountNo = Convert.ToDecimal(accnoTxt.Text);
                //Hesap numarasını kullanarak bakiye ve ad-soyad bilgilerini sorgular ve ilgili TextBox'lara yazdırır
                string query = "SELECT Name, Balance FROM useraccount WHERE Account_No = @accountNo";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@accountNo", infos.AccountNo);

                    try
                    {
                        using(MySqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                nameTxt.Text = reader["Name"].ToString();
                                infos.Name = nameTxt.Text;
                                oldblncTxt.Text = reader["Balance"].ToString();
                                infos.Balance = Convert.ToDecimal(oldblncTxt.Text);
                            }
                            else
                            {
                                MessageBox.Show("Hesap numaranız bulunamadı", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                            }
                        }
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show("Sorgu Hatası:", ex.Message);
                    }
                }
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            decimal new_accno;
            string mode = modecomboBox.Text;
            decimal depamount;
            string date = label2.Text;

            //alanlar boş bırakıldığı durumda hata verir
            if (!decimal.TryParse(accnoTxt.Text, out new_accno))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(damountTxt.Text, out depamount))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (modecomboBox.SelectedItem == null || string.IsNullOrEmpty(date))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (infos.AccountNo != new_accno)
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
                    string query = "INSERT INTO deposit (AccountNo, Name, OldBalance, Mode, DipAmount, Date) " +
                        "VALUES (@accountNo, @Name, @OldBalance,@mode, @dipamount, @date)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountNo", new_accno);
                        command.Parameters.AddWithValue("@Name", infos.Name);
                        command.Parameters.AddWithValue("@OldBalance", infos.Balance);
                        command.Parameters.AddWithValue("@mode", mode);
                        command.Parameters.AddWithValue("@dipamount", depamount);
                        command.Parameters.AddWithValue("@date", date);

                        try     //yapılan işlem başarılı mı değil mi ona göre uyarı verir
                        {
                            // Sorguyu çalıştırın
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Yatırım işlemi başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

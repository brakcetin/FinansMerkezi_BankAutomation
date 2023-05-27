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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

                infos.AccountNo = accnoTxt.Text;
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
            string new_accno = accnoTxt.Text;
            decimal new_oldbalance;
            string mode = modecomboBox.Text;
            decimal depamount;
            string date = label2.Text;
            string new_name = nameTxt.Text;

            //alanlar boş bırakıldığı durumda hata verir
            if (!decimal.TryParse(damountTxt.Text, out depamount))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!decimal.TryParse(oldblncTxt.Text, out new_oldbalance))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(new_accno) || string.IsNullOrEmpty(new_accno) ||
                modecomboBox.SelectedItem == null || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(new_name))
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
                if (infos.Name != new_name)
                {
                    MessageBox.Show("Ad-Soyad bilgileri değiştirilemez.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hata durumunda işlemi sonlandırır
                }
                if (infos.Balance != new_oldbalance)
                {
                    MessageBox.Show("Önceki bakiye değiştirilemez.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        command.Parameters.AddWithValue("@Name", new_name);
                        command.Parameters.AddWithValue("@OldBalance", new_oldbalance);
                        command.Parameters.AddWithValue("@mode", mode);
                        command.Parameters.AddWithValue("@dipamount", depamount);
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
                            MessageBox.Show("Sorgu Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
        }
    }
}

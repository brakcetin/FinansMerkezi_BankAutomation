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
            infos.AccountNo = 0;
            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                //Hesap numarası yerine girilen değerin boş olup olmadığını kontrol eder
                if (string.IsNullOrEmpty(accnoTxt.Text))
                {
                    MessageBox.Show("Detayları görmek için lütfen bir hesap numarası giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!decimal.TryParse(accnoTxt.Text, out decimal accno))
                {
                    MessageBox.Show("Hesap numarası rakamlardan oluşmak zorundadır!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                infos.AccountNo = accno;

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
            string date = DateTime.Now.ToString("dd / MM / yyyy HH:mm:ss");

            //alanlar boş bırakıldığı durumda hata verir
            if (modecomboBox.SelectedItem != null && !string.IsNullOrEmpty(modecomboBox.SelectedItem.ToString()))   //combobox boş mu değil mi, kontrol eder
            {
                mode = modecomboBox.SelectedItem.ToString();     //combobox'ı atar
            }
            else
            {
                // ComboBox'ta seçili bir öğe yoksa uygulanacak işlem
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(accnoTxt.Text) || string.IsNullOrEmpty(damountTxt.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                //veri setinde bulunan bir hesap numarası girilmiş mi diye kontrol eder
                new_accno = Convert.ToDecimal(accnoTxt.Text);
                if (infos.AccountNo != new_accno)
                {
                    MessageBox.Show("Hesap numarası değiştirilemez.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hata durumunda işlemi sonlandırır
                }
                //yatırılacak tutara sadece sayı girilsin istiyorum
                if (!decimal.TryParse(damountTxt.Text, out decimal depamount))
                {
                    MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    //veri setindeki useraccount tablosunda bulunan kullanıcıların bakiyelerini günceller
                    string query1 = "SELECT Balance FROM useraccount WHERE Account_No = @AccountNo";
                    using (MySqlCommand senderBalanceCommand = new MySqlCommand(query1, connection))
                    {
                        senderBalanceCommand.Parameters.AddWithValue("@AccountNo", new_accno);
                        infos.Balance = (decimal)senderBalanceCommand.ExecuteScalar();
                        decimal NewBalance = infos.Balance + depamount;

                        // Gönderen kişinin Balance değerini günceller
                        string updateSenderBalanceQuery = "UPDATE useraccount SET Balance = @NewBalance WHERE Account_No = @AccountNo";
                        using (MySqlCommand updateSenderBalanceCommand = new MySqlCommand(updateSenderBalanceQuery, connection))
                        {
                            updateSenderBalanceCommand.Parameters.AddWithValue("@NewBalance", NewBalance);
                            updateSenderBalanceCommand.Parameters.AddWithValue("@AccountNo", new_accno);
                            updateSenderBalanceCommand.ExecuteNonQuery();
                        }
                    }

                    string query = "INSERT INTO investment (AccountNo, Name, OldBalance, Mode, DipAmount, Date) " +
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

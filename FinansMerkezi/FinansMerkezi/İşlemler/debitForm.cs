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
    public partial class debitForm : Form
    {
        private Informations infos;
        public debitForm()
        {
            InitializeComponent();
            loaddate();
            loadmode();
            infos = new Informations();
        }
        private void loaddate()
        {
            dateLabel.Text = DateTime.Now.ToString("dd / MM / yyyy");
        }
        private void loadmode()
        {
            modecomboBox.Items.Add("Nakit İşlemi");
            modecomboBox.Items.Add("Çek İşlemi");
        }

        private void detailsBtn_Click(object sender, EventArgs e)
        {
            GetDetails();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void GetDetails()
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
                                return;
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.GetType().Name + Environment.NewLine + "Hata Detayı: " + ex.Message, "Sorgu Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        private void SaveData()
        {
            decimal new_accno = 0;
            string mode = string.Empty;
            string date = DateTime.Now.ToString("dd / MM / yyyy HH:mm:ss");

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

            if (string.IsNullOrEmpty(amountTxt.Text) || string.IsNullOrEmpty(accnoTxt.Text))
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
                if (!decimal.TryParse(amountTxt.Text, out decimal amount))
                {
                    MessageBox.Show("Geçerli bir para miktarı girin!", "Hata: Çekilecek Miktar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (infos.Balance > amount)
                {
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
                            decimal NewBalance = infos.Balance - amount;

                            // Gönderen kişinin Balance değerini günceller
                            string updateSenderBalanceQuery = "UPDATE useraccount SET Balance = @NewBalance WHERE Account_No = @AccountNo";
                            using (MySqlCommand updateSenderBalanceCommand = new MySqlCommand(updateSenderBalanceQuery, connection))
                            {
                                updateSenderBalanceCommand.Parameters.AddWithValue("@NewBalance", NewBalance);
                                updateSenderBalanceCommand.Parameters.AddWithValue("@AccountNo", new_accno);
                                updateSenderBalanceCommand.ExecuteNonQuery();
                            }
                        }

                        //veri setindeki debit isimli tabloya gerekli bilgileri girer
                        string query = "INSERT INTO debit (Date, AccountNo, Name, OldBalance, Mode, DebAmount) " +
                            "VALUES (@Date, @AccountNo, @Name, @OldBalance, @Mode, @DebAmount)";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@AccountNo", new_accno);
                            command.Parameters.AddWithValue("@Date", date);
                            command.Parameters.AddWithValue("@Mode", mode);
                            command.Parameters.AddWithValue("@Name", infos.Name);
                            command.Parameters.AddWithValue("@OldBalance", infos.Balance);
                            command.Parameters.AddWithValue("@DebAmount", amount);


                            try     //yapılan işlem başarılı mı değil mi ona göre uyarı verir
                            {
                                // Sorguyu çalıştırın
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Para çekme işleminiz başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFormFields();
                                }
                                else
                                {
                                    MessageBox.Show("İşlem gerçekleştirilemedi.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Hata: " + ex.GetType().Name + Environment.NewLine + "Hata Detayı: " + ex.Message, "Sorgu Hatası!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Yetersiz bakiye!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        private void ClearFormFields()
        {
            accnoTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            blncTxt.Text = string.Empty;
            modecomboBox.Items.Clear();
            amountTxt.Text = string.Empty;
        }
    }
}

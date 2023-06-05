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
    public partial class fdForm : Form
    {
        public fdForm()
        {
            InitializeComponent();
            loaddate();
            loadmode();
        }
        private void loaddate()
        {
            label2.Text = DateTime.Now.ToString("dd / MM / yyyy");
        }
        private void loadmode()
        {
            modecomboBox.Items.Add("Nakit İşlemi");
            modecomboBox.Items.Add("Çek İşlemi");
        }
        private void saveBtn_Click(object sender, EventArgs e)
        {
            string accnotxt = accnoTxt.Text;
            string mode = string.Empty;
            string liras_txt = lirasTxt.Text;
            string periodInput = periodTxt.Text;
            string period_txt = periodTxt.Text;
            string interest_txt = interestTxt.Text;
            string maturity_date = string.Empty;
            string start_date = DateTime.Now.ToString("dd / MM / yyyy");
            decimal senderNewBalance=0;
            decimal senderCurrentBalance;
            
            if (modecomboBox.SelectedItem != null && !string.IsNullOrEmpty(modecomboBox.SelectedItem.ToString()))   //combobox boş mu değil mi, kontrol eder
            {
                mode = modecomboBox.SelectedItem.ToString();     //combobox'ı atar
            }
            else
            {
                // ComboBox'ta seçili bir öğe yoksa uygulanacak işlem
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(period_txt) || string.IsNullOrEmpty(liras_txt) || string.IsNullOrEmpty(interest_txt) || string.IsNullOrEmpty(accnotxt))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //veri setinde bulunan bir hesap numarası girilmiş mi diye kontrol eder
                if (accnotxt == "1000000000")
                {
                    MessageBox.Show("Geçersiz hesap numarası!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (decimal.TryParse(accnoTxt.Text, out decimal accno)) //veri setinde bulunan bir hesap numarası girilmiş mi diye kontrol eder
                {
                    // Veritabanı bağlantısını açın
                    using (MySqlConnection connection = DataBaseHelper.GetConnection())
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        // Hesap numarasını kullanarak kaydın var olup olmadığını kontrol edin
                        string checkAccountQuery = "SELECT COUNT(*) FROM useraccount WHERE Account_No = @accountNo";
                        using (MySqlCommand checkAccountCommand = new MySqlCommand(checkAccountQuery, connection))
                        {
                            checkAccountCommand.Parameters.AddWithValue("@accountNo", accno);
                            decimal accountCount = Convert.ToDecimal(checkAccountCommand.ExecuteScalar());

                            if (accountCount == 0)
                            {
                                MessageBox.Show("Hesap numarası bulunamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Hata durumunda işlem yapmayı sonlandır
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz hesap numarası!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //yatırılacak tutara sadece sayı girilsin istiyorum
                if (!decimal.TryParse(lirasTxt.Text, out decimal liras))
                {
                    MessageBox.Show("Yatırılacak Tutar (TL) kısmına geçerli bir para miktarı girin!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Süre (gün) yerine girilen değerin hem boş olmadığını hem de sayı girilip girilmediğini kontrol eder
                if (int.TryParse(periodInput, out int period))
                {
                    maturity_date = (DateTime.UtcNow.AddDays(Convert.ToInt32(periodTxt.Text))).ToString("dd / MM / yyyy");
                }
                else
                {
                    MessageBox.Show("Geçerli bir süre girin!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(interestTxt.Text, out decimal interest))
                {
                    MessageBox.Show("Geçerli bir faiz oranı girin!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal maturity_amount = ((liras * period * interest) /
                (100 * 12 * 30)) + (liras);
                //100(yüzde), 12(ay sayısı), 30(gün sayısı)
                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    //veri setindeki useraccount tablosunda bulunan kullanıcıların bakiyelerini günceller
                    string query1 = "SELECT Balance FROM useraccount WHERE Account_No = @senderAccountNo";
                    using (MySqlCommand senderBalanceCommand = new MySqlCommand(query1, connection))
                    {
                        senderBalanceCommand.Parameters.AddWithValue("@senderAccountNo", accno);
                        senderCurrentBalance = (decimal)senderBalanceCommand.ExecuteScalar();
                        senderNewBalance = senderCurrentBalance - liras;

                        // Gönderen kişinin Balance değerini günceller
                        string updateSenderBalanceQuery = "UPDATE useraccount SET Balance = @senderNewBalance WHERE Account_No = @senderAccountNo";
                        using (MySqlCommand updateSenderBalanceCommand = new MySqlCommand(updateSenderBalanceQuery, connection))
                        {
                            updateSenderBalanceCommand.Parameters.AddWithValue("@senderNewBalance", senderNewBalance);
                            updateSenderBalanceCommand.Parameters.AddWithValue("@senderAccountNo", accno);
                            updateSenderBalanceCommand.ExecuteNonQuery();
                        }
                    }
                    //veri setindeki fixed_deposit isimli tabloya gerekli bilgileri girer
                    string query = "INSERT INTO fixed_deposit (Account_No, Mode, Liras, Period, Interest_rate, Maturity_date, Maturity_Amount, Start_Date) " +
                        "VALUES (@accountNo, @Mode, @Liras,@Period, @Interest_rate, @Maturity_date, @Maturity_Amount, @Start_Date)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountNo", accno);
                        command.Parameters.AddWithValue("@Mode", mode);
                        command.Parameters.AddWithValue("@Liras", liras);
                        command.Parameters.AddWithValue("@Period", period);
                        command.Parameters.AddWithValue("@Interest_rate", interest);
                        command.Parameters.AddWithValue("@Maturity_date", maturity_date);
                        command.Parameters.AddWithValue("@Maturity_Amount", maturity_amount);
                        command.Parameters.AddWithValue("@Start_Date", start_date);

                        try     //yapılan işlem başarılı mı değil mi ona göre uyarı verir
                        {
                            // Sorguyu çalıştırın
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Vadesiz Mevduat formunuz başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }
        private void ClearFormFields()
        {
            accnoTxt.Text = string.Empty;
            lirasTxt.Text = string.Empty;
            periodTxt.Text = string.Empty;
            modecomboBox.Items.Clear();
            interestTxt.Text = string.Empty;
        }
    }
}

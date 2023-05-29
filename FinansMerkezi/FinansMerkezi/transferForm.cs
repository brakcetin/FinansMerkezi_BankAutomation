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
            decimal senderNewBalance;
            decimal senderCurrentBalance;

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
                if (infos.Balance >= toamount)
                {
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
                            senderBalanceCommand.Parameters.AddWithValue("@senderAccountNo", new_fromaccno);
                            senderCurrentBalance = (decimal)senderBalanceCommand.ExecuteScalar();

                            // Gönderen kişinin yeni Balance değerini hesaplar
                            senderNewBalance = senderCurrentBalance - toamount;

                            // Gönderen kişinin Balance değerini günceller
                            string updateSenderBalanceQuery = "UPDATE useraccount SET Balance = @senderNewBalance WHERE Account_No = @senderAccountNo";
                            using (MySqlCommand updateSenderBalanceCommand = new MySqlCommand(updateSenderBalanceQuery, connection))
                            {
                                updateSenderBalanceCommand.Parameters.AddWithValue("@senderNewBalance", senderNewBalance);
                                updateSenderBalanceCommand.Parameters.AddWithValue("@senderAccountNo", new_fromaccno);
                                updateSenderBalanceCommand.ExecuteNonQuery();   
                            }
                        }

                        //veri setindeki useraccount tablosunda bulunan kullanıcıların bakiyelerini günceller
                        string receiverBalanceQuery = "SELECT Balance FROM useraccount WHERE Account_No = @receiverAccountNo";
                        using (MySqlCommand receiverBalanceCommand = new MySqlCommand(receiverBalanceQuery, connection))
                        {
                            receiverBalanceCommand.Parameters.AddWithValue("@receiverAccountNo", toaccno);
                            object receiverBalanceResult = receiverBalanceCommand.ExecuteScalar();

                            //Paranın gönderileceği hesap var mı yok kontrol eder
                            if (receiverBalanceResult != null && receiverBalanceResult != DBNull.Value)
                            {
                                decimal receiverCurrentBalance = (decimal)receiverBalanceResult;
                                // Alan kişinin yeni Balance değerini hesaplayın
                                decimal receiverNewBalance = receiverCurrentBalance + toamount;

                                // Alan kişinin Balance değerini güncelleyin
                                string updateReceiverBalanceQuery = "UPDATE useraccount SET Balance = @receiverNewBalance WHERE Account_No = @receiverAccountNo";
                                using (MySqlCommand updateReceiverBalanceCommand = new MySqlCommand(updateReceiverBalanceQuery, connection))
                                {
                                    updateReceiverBalanceCommand.Parameters.AddWithValue("@receiverNewBalance", receiverNewBalance);
                                    updateReceiverBalanceCommand.Parameters.AddWithValue("@receiverAccountNo", toaccno);
                                    updateReceiverBalanceCommand.ExecuteNonQuery();
                                }
                            }
                            else //hesap yoksa hata verir
                            {
                                MessageBox.Show("Alıcı hesap bulunamadı", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }


                        }

                        //veri tabanındaki transfer isimli tabloya gerekli bilgileri girer
                        string query2 = "INSERT INTO transfer (date, account_no, name, first_balance, totransfer, toaccount_no) " +
                            "VALUES (@date, @accountNo, @Name, @balance,@totransfer,@toaccount_no)";
                        using (MySqlCommand command = new MySqlCommand(query2, connection))
                        {
                            command.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                            command.Parameters.AddWithValue("@Name", infos.Name);
                            command.Parameters.AddWithValue("@balance", senderCurrentBalance);
                            command.Parameters.AddWithValue("@totransfer", toamount);
                            command.Parameters.AddWithValue("@date", date);
                            command.Parameters.AddWithValue("@toaccount_no", toaccno);

                            try     //yapılan işlem başarılı mı değil mi ona göre uyarı verir
                            {
                                // Sorguyu çalıştırın
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Transfer işlemi başarıyla tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFormFields();
                                }
                                else
                                {
                                    MessageBox.Show("Transfer işlemi gerçekleştirilemedi.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Sorgu Hatası: " + ex.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Yetersiz bakiye!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }
        private void ClearFormFields()
        {
            fromaccnoTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            blncTxt.Text = string.Empty;
            toaccnoTxt.Text = string.Empty;
            toamountTxt.Text = string.Empty;
        }
    }
}

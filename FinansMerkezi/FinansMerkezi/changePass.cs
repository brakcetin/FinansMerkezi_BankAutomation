using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FinansMerkezi
{
    public partial class changePass : Form
    {
        public changePass()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string username = usrnmTxt.Text;
            string oldpass = oldpassTxt.Text;
            string newpass = newpassTxt.Text;
            string repass = repassTxt.Text;

            if (string.IsNullOrEmpty(usrnmTxt.Text) || string.IsNullOrEmpty(oldpassTxt.Text) || string.IsNullOrEmpty(newpassTxt.Text) ||
                string.IsNullOrEmpty(newpassTxt.Text) || string.IsNullOrEmpty(repassTxt.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // admin_table tablosunda kullanıcı adı kontrolü
                    string passwordCheckQuery = "SELECT password FROM admin_table WHERE username = @username";
                    using (MySqlCommand passwordCheckCommand = new MySqlCommand(passwordCheckQuery, connection))
                    {
                        try
                        {
                            passwordCheckCommand.Parameters.AddWithValue("@username", username);
                            object result = passwordCheckCommand.ExecuteScalar();

                            // Eğer kullanıcı adına ait şifre bulunamadıysa
                            if (result == null)
                            {
                                MessageBox.Show("Böyle bir kullanıcı adı bulunamadı!", "Hata: Kullanıcı Adı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            string storedPassword = result.ToString();

                            // Eski şifre doğru mu kontrolü
                            if (!VerifyPassword(oldpass, storedPassword))
                            {
                                MessageBox.Show("Yanlış şifre girdiniz!", "Hata: Eski Şifre!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Yeni şifre eski şifre ile aynı mı kontrolü
                            if (oldpass == newpass)
                            {
                                MessageBox.Show("Yeni şifre, eski şifreyle aynı olamaz!", "Hata: Yeni Şifre!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Yeni şifre ve tekrar şifre uyum kontrolü
                            if (newpass != repass)
                            {
                                MessageBox.Show("Yeni şifreler uyuşmuyor!", "Hata: Yeni Şifre Tekrar!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Yeni şifreyi veritabanına kaydet
                            string updatePasswordQuery = "UPDATE admin_table SET password = @password WHERE username = @username";
                            using (MySqlCommand updatePasswordCommand = new MySqlCommand(updatePasswordQuery, connection))
                            {
                                updatePasswordCommand.Parameters.AddWithValue("@password", newpass);
                                updatePasswordCommand.Parameters.AddWithValue("@username", username);
                                updatePasswordCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Şifre başarıyla değiştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları:" + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
        }
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return string.Equals(inputPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }

    }
}

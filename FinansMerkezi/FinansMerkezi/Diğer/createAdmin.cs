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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinansMerkezi
{
    public partial class createAdmin : Form
    {
        public createAdmin()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string username = usrnmTxt.Text;
            string pass = passTxt.Text;
            string repass = repassTxt.Text;

            //boş alan var mı diye kontrol eder
            if (string.IsNullOrEmpty(usrnmTxt.Text) || string.IsNullOrEmpty(passTxt.Text) || string.IsNullOrEmpty(repassTxt.Text))
            {
                MessageBox.Show("Alanlar boş bırakılamaz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //tekrar girilen şifre ile uyum kontrolü
            if (pass != repass)
            {
                MessageBox.Show("Şifreler uyuşmuyor!", "Hata: Şifre Tekrar!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Kullanıcı adının mevcut olup olmadığını kontrol et
                string usernameCheckQuery = "SELECT COUNT(*) FROM admin_table WHERE username = @username";
                using (MySqlCommand usernameCheckCommand = new MySqlCommand(usernameCheckQuery, connection))
                {
                    try
                    {
                        usernameCheckCommand.Parameters.AddWithValue("@username", username);
                        int count = Convert.ToInt32(usernameCheckCommand.ExecuteScalar());

                        // Eğer kullanıcı adı zaten varsa uyarı ver
                        if (count > 0)
                        {
                            MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Hata: Kullanıcı Adı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları: " + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Yeni kullanıcıyı veritabanına kaydet
                string insertUserQuery = "INSERT INTO admin_table (username, password) VALUES (@username, @password)";
                using (MySqlCommand insertUserCommand = new MySqlCommand(insertUserQuery, connection))
                {
                    try
                    {
                        insertUserCommand.Parameters.AddWithValue("@username", username);
                        insertUserCommand.Parameters.AddWithValue("@password", pass);
                        insertUserCommand.ExecuteNonQuery();
                        MessageBox.Show("Yeni kullanıcı başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFormFields();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları: " + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ClearFormFields()
        {
            usrnmTxt.Text = string.Empty;
            passTxt.Text = string.Empty;
            repassTxt.Text = string.Empty;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.eye;
            passTxt.PasswordChar = '\0';
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.hide;
            passTxt.PasswordChar = '*';
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.eye;
            repassTxt.PasswordChar = '\0';
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.hide;
            repassTxt.PasswordChar = '*';
        }
    }
}

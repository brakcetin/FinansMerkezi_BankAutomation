using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace FinansMerkezi
{
    public partial class updateForm : Form
    {
        MemoryStream ms;
        string accountNo = string.Empty;
        string gender = string.Empty;
        string m_stat = string.Empty;
        string name = string.Empty;
        string motherName = string.Empty;
        string fatherName = string.Empty;
        string balanceText = string.Empty;
        DateTime dateOfBirth = DateTime.MinValue;
        string phoneno = string.Empty;
        string address = string.Empty;
        string district = string.Empty;
        string state;
        byte[] profile = null;

        public updateForm()
        {
            InitializeComponent();
            loadstate();
        }

        private void loadstate()
        {
            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT city_name FROM cities ORDER BY city_name ASC";

                List<string> cities = new List<string>();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string cityName = reader.GetString(0);
                            cities.Add(cityName);
                        }
                    }
                }

                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(cities.ToArray());
            }
        }

        private void detBtn_Click(object sender, EventArgs e)
        {
            string accountNo = accnoTxt.Text;

            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                string query = "SELECT * FROM useraccount WHERE Account_No = @accountNo";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountNo", accountNo);

                DataTable dt = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }

                dataGridView1.DataSource = dt;
                ChangeNamesofColumns();
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string UserName = nameTxt.Text;

            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                string query = "SELECT * FROM useraccount WHERE Name = @UserName";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", UserName);

                DataTable dt = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }

                dataGridView1.DataSource = dt;
                ChangeNamesofColumns();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                accountNo = row.Cells["Account_No"].Value.ToString();

                // Veri setinden ilgili hesap numarasına ait bilgileri getirme
                string query = "SELECT * FROM useraccount WHERE Account_No = @accountNo";
                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountNo", accountNo);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                // Bilgileri doldurma
                                name = reader["Name"].ToString();
                                motherName = reader["Mother_Name"].ToString();
                                fatherName = reader["Father_Name"].ToString();
                                balanceText = reader["Balance"].ToString();
                                dateOfBirth = reader.GetDateTime("DateofBirth");
                                phoneno = reader["PhoneNo"].ToString();
                                address = reader["Adress"].ToString();
                                district = reader["District"].ToString();
                                state = reader["State"].ToString();




                                if (reader["Gender"].ToString() == "Erkek")
                                {
                                    maleRadio.Checked = true;
                                    gender = "Erkek";
                                }

                                else if (reader["Gender"].ToString() == "Kadın")
                                {
                                    fmaleRadio.Checked = true;
                                    gender = "Kadın";
                                }
                                else if (reader["Gender"].ToString() == "Diğer")
                                {
                                    otherRadio.Checked = true;
                                    gender = "Diğer";
                                }
                                if (reader["Marriage_Status"].ToString() == "Evli")
                                {
                                    marriedRadio.Checked = true;
                                    m_stat = "Evli";
                                }
                                else if (reader["Marriage_Status"].ToString() == "Bekar")
                                {
                                    umarriedRadio.Checked = true;
                                    m_stat = "Bekar";
                                }

                                profile = (byte[])reader["Profile"]; // img değişkenine veri tabanından çektiğiniz byte dizisini atayın

                                if (profile != null && profile.Length > 0)
                                {
                                    using (ms = new MemoryStream(profile))
                                    {
                                        pictureBox1.Image = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    // Profil resmi yoksa, varsayılan bir resim atayabilirsiniz veya pictureBox1.Image özelliğini temizleyebilirsiniz
                                    pictureBox1.Image = null;
                                }


                                // Diğer kontrolleri doldurma işlemleri
                                accnoTxt.Text = accountNo;
                                nameTxt.Text = name;
                                momTxt.Text = motherName;
                                dadTxt.Text = fatherName;
                                blncTxt.Text = balanceText.ToString();
                                dateTimePicker1.Value = dateOfBirth;
                                phoneTxt.Text = phoneno;
                                addressTxt.Text = address;
                                distTxt.Text = district;
                                comboBox1.Text = state;

                            }
                        }
                    }
                }
            }
        }


        private void ChangeNamesofColumns()
        {
            dataGridView1.Columns["Account_No"].HeaderText = "Hesap Numarası";
            dataGridView1.Columns["Name"].HeaderText = "İsim";
            dataGridView1.Columns["DateofBirth"].HeaderText = "Doğum Tarihi";
            dataGridView1.Columns["PhoneNo"].HeaderText = "Telefon Numarası";
            dataGridView1.Columns["Adress"].HeaderText = "Adres";
            dataGridView1.Columns["District"].HeaderText = "İlçe";
            dataGridView1.Columns["State"].HeaderText = "Şehir";
            dataGridView1.Columns["Profile"].HeaderText = "Profil Resmi";
            dataGridView1.Columns["Gender"].HeaderText = "Cinsiyet";
            dataGridView1.Columns["Marriage_Status"].HeaderText = "Medeni Durum";
            dataGridView1.Columns["Mother_Name"].HeaderText = "Anne Adı";
            dataGridView1.Columns["Father_Name"].HeaderText = "Baba Adı";
            dataGridView1.Columns["Balance"].HeaderText = "Bakiye";
            dataGridView1.Columns["Date"].HeaderText = "Tarih";
        }

        private void imgBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog.FileName);
                pictureBox1.Image = img;
                ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            string newaccountNo = accnoTxt.Text;
            name = nameTxt.Text;
            motherName = momTxt.Text;
            balanceText = blncTxt.Text;
            dateOfBirth = dateTimePicker1.Value.Date;
            fatherName = dadTxt.Text;
            phoneno = phoneTxt.Text;
            address = addressTxt.Text;
            district = distTxt.Text;
            profile = GetProfileBytes();
            comboBox1.SelectedItem = state;

            if (maleRadio.Checked)
            {
                gender = "Erkek";
            }
            else if (fmaleRadio.Checked)
            {
                gender = "Kadın";
            }
            else if (otherRadio.Checked)
            {
                gender = "Diğer";
            }
            if (marriedRadio.Checked)
            {
                m_stat = "Evli";
            }
            else if (umarriedRadio.Checked)
            {
                m_stat = "Bekar";
            }
            if (string.IsNullOrEmpty(name) || dateOfBirth == DateTime.MinValue.Date || string.IsNullOrEmpty(phoneno) ||
    string.IsNullOrEmpty(address) || string.IsNullOrEmpty(district) || comboBox1.SelectedItem == null ||
    profile == null || string.IsNullOrEmpty(motherName) || string.IsNullOrEmpty(fatherName) ||
    string.IsNullOrEmpty(balanceText) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(m_stat))
            {
                MessageBox.Show("Alanlar boş bırakılamaz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (accountNo != newaccountNo)
                {
                    MessageBox.Show("Hesap numarası değiştirilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Hata durumunda işlemi sonlandırır
                }

                state = comboBox1.SelectedItem.ToString();
                if (!IsPhoneNumberValid(phoneno))
                {
                    MessageBox.Show("Lütfen geçerli bir telefon numarası girin.");
                    phoneTxt.Focus(); // Metin kutusuna odaklanma
                    phoneTxt.SelectAll(); // Tüm metni seçme
                    return;
                }

                if (!decimal.TryParse(balanceText, out decimal balance))
                {
                    MessageBox.Show("Lütfen geçerli bir bakiye girin.");
                    blncTxt.Focus(); // Metin kutusuna odaklanma
                    blncTxt.SelectAll(); // Tüm metni seçme
                    return; // İşlemi sonlandırma
                }


                // Veritabanında güncelleme işlemini gerçekleştirin
                string query = "UPDATE useraccount SET Name = @name,DateofBirth = @dateOfBirth, PhoneNo=@PhoneNo," +
                    "Adress=@Adress, District=@District, State=@State, Profile=@Profile, Gender=@Gender, Marriage_Status=@Marriage_Status," +
                    "Mother_Name = @Mother_Name, Father_Name=@Father_Name, Balance = @Balance  WHERE Account_No = @accountNo";
                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@accountNo", accountNo);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@dateofBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNo", phoneno);
                        command.Parameters.AddWithValue("@Adress", address);
                        command.Parameters.AddWithValue("@District", district);
                        command.Parameters.AddWithValue("@State", state);
                        command.Parameters.AddWithValue("@Profile", profile);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Marriage_Status", m_stat);
                        command.Parameters.AddWithValue("@Mother_Name", motherName);
                        command.Parameters.AddWithValue("@Father_Name", fatherName);
                        command.Parameters.AddWithValue("@Balance", balanceText);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla güncellendi.");
                            ClearFormFields();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt güncellenirken bir hata oluştu.");
                        }
                    }
                }
            }
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // Telefon numarası için regex deseni
            string pattern = @"^\d{10}$";

            // phoneNumber'ın desene uyup uymadığını kontrol etme
            bool isValid = Regex.IsMatch(phoneNumber, pattern);

            return isValid;
        }

        private byte[] GetProfileBytes()
        {
            if (ms != null)
            {
                return ms.ToArray();
            }
            return null;
        }
        private void ClearFormFields()
        {
            accnoTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
            phoneTxt.Text = string.Empty;
            addressTxt.Text = string.Empty;
            distTxt.Text = string.Empty;
            comboBox1.SelectedIndex = -1; // ComboBox seçimini temizler
            pictureBox1.Image = null;
            gender = string.Empty;
            m_stat = string.Empty;
            momTxt.Text = string.Empty;
            dadTxt.Text = string.Empty;
            blncTxt.Text = string.Empty;
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0 && accountNo != string.Empty)
            {

                DialogResult result = MessageBox.Show("Seçili kullanıcıyı silmek istediğinize emin misiniz?", "Kullanıcı Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM useraccount WHERE Account_No = @accountNo";

                    using (MySqlConnection connection = DataBaseHelper.GetConnection())
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@accountNo", accountNo);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Kullanıcı başarıyla silindi.");
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı silinirken bir hata oluştu.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçin.", "Kullanıcı Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

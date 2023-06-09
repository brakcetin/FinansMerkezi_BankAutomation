﻿using System;
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

            //Hesap numarası yerine girilen değerin boş olup olmadığını kontrol eder
            if (string.IsNullOrEmpty(accnoTxt.Text))
            {
                MessageBox.Show("Detayları görmek için lütfen bir hesap numarası giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(accnoTxt.Text, out decimal accno))
            {
                MessageBox.Show("Hesap numarası rakamlardan oluşmak zorundadır!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                string query = "SELECT * FROM useraccount WHERE Account_No = @accountNo";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@accountNo", accno);

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
            //Ad-Soyad yerine girilen değerin boş olup olmadığını kontrol eder
            if (string.IsNullOrEmpty(nameTxt.Text))
            {
                MessageBox.Show("Detayları görmek için Ad-Soyad bilgilerinizi giriniz!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        //sütun adları veri tabanında ingilizce olduğu için türkçeye çevrilir
        private void ChangeNamesofColumns()
        {
            dataGridView1.Columns["Account_No"].HeaderText = "Hesap Numarası";
            dataGridView1.Columns["Name"].HeaderText = "Ad-Soyad";
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
            name = CapitalizeFirstLetters(name);
            dateOfBirth = dateTimePicker1.Value.Date;
            phoneno = phoneTxt.Text;
            address = addressTxt.Text;
            address = CapitalizeFirstLetters(address);
            district = distTxt.Text;
            district = CapitalizeFirstLetters(district);
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

            //alanlar boş bırakıldığı durumda hata verir
            if (string.IsNullOrEmpty(name) || dateOfBirth == DateTime.MinValue.Date || string.IsNullOrEmpty(phoneno) ||
    string.IsNullOrEmpty(address) || string.IsNullOrEmpty(district) || comboBox1.SelectedItem == null ||
    profile == null || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(m_stat))
            {
                MessageBox.Show("Alanlar boş bırakılamaz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (accountNo != newaccountNo)
                {
                    MessageBox.Show("Hesap numarası değiştirilemez.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Hata durumunda işlemi sonlandırır
                }

                state = comboBox1.SelectedItem.ToString();
                if (!IsPhoneNumberValid(phoneno))
                {
                    MessageBox.Show("Lütfen geçerli bir telefon numarası girin.","Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    phoneTxt.Focus(); // Metin kutusuna odaklanma
                    phoneTxt.SelectAll(); // Tüm metni seçme
                    return;
                }


                // Veritabanında güncelleme işlemini gerçekleştirin
                string query = "UPDATE useraccount SET Name = @name,DateofBirth = @dateOfBirth, PhoneNo=@PhoneNo," +
                    "Adress=@Adress, District=@District, State=@State, Profile=@Profile, Gender=@Gender, Marriage_Status=@Marriage_Status WHERE Account_No = @accountNo";
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
                                MessageBox.Show("Kullanıcı başarıyla silindi.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı silinirken bir hata oluştu.","Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz kullanıcıyı seçin.", "Kullanıcı Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private string CapitalizeFirstLetters(string text)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string result = textInfo.ToTitleCase(text);
            return result;
        }
    }
}

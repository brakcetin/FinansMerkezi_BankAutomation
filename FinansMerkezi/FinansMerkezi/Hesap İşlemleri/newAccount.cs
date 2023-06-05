using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Xml.Linq;

namespace FinansMerkezi
{
    public partial class newAccount : Form
    {
        string gender = string.Empty;
        string m_stat = string.Empty;
        MemoryStream ms;

        public newAccount()
        {
            InitializeComponent();
            loaddate();
            loadaccount();
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

        private void loadaccount()
        {
            using (MySqlConnection connection = DataBaseHelper.GetConnection())
            {
                //hesap numarası kendiliğinden oluşur ve her kullanıcı ekledikçe 1 artarak gider
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT Account_No FROM useraccount ORDER BY Account_No DESC LIMIT 1";
                    object lastAccountNo = command.ExecuteScalar();
                    decimal newAccountNo = Convert.ToDecimal(lastAccountNo) + 1;

                    accnoTxt.Text = newAccountNo.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hesap numarası oluşturulurken bir hata oluştu: " + ex.Message,"Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void loaddate()
        {
            datelLbl.Text = DateTime.Now.ToString("dd / MM / yyyy");
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

            decimal newAccountNo = Convert.ToDecimal(accnoTxt.Text);
            string name = nameTxt.Text;
            name = CapitalizeFirstLetters(name); //ilk harflerini büyük yapar
            DateTime dateOfBirth = dateTimePicker1.Value.Date; //doğum tarihi
            string phoneNo = phoneTxt.Text;
            string address = addressTxt.Text;
            address = CapitalizeFirstLetters(address);
            string district = distTxt.Text;
            district = CapitalizeFirstLetters(district);
            string state; // ComboBox seçilen değeri alır
            byte[] profile = GetProfileBytes(); // Profil verisini byte dizisi olarak alır
            string motherName = momTxt.Text;
            motherName = CapitalizeFirstLetters(motherName);
            string fatherName = dadTxt.Text;
            fatherName = CapitalizeFirstLetters(fatherName);
            string date = DateTime.Now.ToString("dd / MM / yyyy HH:mm:ss");
            string balanceText = blncTxt.Text;

            if (string.IsNullOrEmpty(name) || dateOfBirth == DateTime.MinValue.Date || string.IsNullOrEmpty(phoneNo) ||
    string.IsNullOrEmpty(address) || string.IsNullOrEmpty(district) || comboBox1.SelectedItem == null ||
    profile == null || string.IsNullOrEmpty(motherName) || string.IsNullOrEmpty(fatherName) ||
    string.IsNullOrEmpty(balanceText) || string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(m_stat))
            {
                MessageBox.Show("Alanlar boş bırakılamaz", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                state = comboBox1.SelectedItem.ToString();
                if (!IsPhoneNumberValid(phoneNo))
                {
                    MessageBox.Show("Lütfen 10 haneli geçerli bir telefon numarası girin.","Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    phoneTxt.Focus(); // Metin kutusuna odaklanma
                    phoneTxt.SelectAll(); // Tüm metni seçme
                    return;
                }

                if (!decimal.TryParse(balanceText, out decimal balance))
                {
                    MessageBox.Show("Lütfen geçerli bir bakiye girin.","Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    blncTxt.Focus(); // Metin kutusuna odaklanma
                    blncTxt.SelectAll(); // Tüm metni seçme
                    return; // İşlemi sonlandırma
                }


                using (MySqlConnection connection = DataBaseHelper.GetConnection())
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = "INSERT INTO useraccount (Account_No, Name, DateofBirth, PhoneNo, Adress, District, State, Profile, Gender, Marriage_Status, Mother_Name, Father_Name, Balance, Date) " +
                                              "VALUES (@Account_No, @Name, @DateofBirth, @PhoneNo, @Adress, @District, @State, @Profile, @Gender, @Marriage_Status, @Mother_Name, @Father_Name, @Balance, @Date)";
                        command.Parameters.AddWithValue("@Account_No", newAccountNo);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@DateofBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@PhoneNo", phoneNo);
                        command.Parameters.AddWithValue("@Adress", address);
                        command.Parameters.AddWithValue("@District", district);
                        command.Parameters.AddWithValue("@State", state);
                        command.Parameters.AddWithValue("@Profile", profile);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@Marriage_Status", m_stat);
                        command.Parameters.AddWithValue("@Mother_Name", motherName);
                        command.Parameters.AddWithValue("@Father_Name", fatherName);
                        command.Parameters.AddWithValue("@Balance", balance);
                        command.Parameters.AddWithValue("@Date", date);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Yeni hesap başarıyla kaydedildi.","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFormFields();
                        }
                        else
                        {
                            MessageBox.Show("Hesap kaydedilirken bir hata oluştu.","Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hesap kaydedilirken bir hata oluştu: " + ex.Message,"Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

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

        private byte[] GetProfileBytes()
        {
            if (ms != null)
            {
                return ms.ToArray();
            }
            return null;
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // Telefon numarası için regex deseni
            string pattern = @"^\d{10}$";

            // phoneNumber'ın desene uyup uymadığını kontrol etme
            bool isValid = Regex.IsMatch(phoneNumber, pattern);

            return isValid;
        }

        //ilk harflerini büyük yapar
        private string CapitalizeFirstLetters(string text)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string result = textInfo.ToTitleCase(text);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace FinansMerkezi
{
    public partial class balanceSheet : Form
    {
        private Informations infos;
        public balanceSheet()
        {
            InitializeComponent();
            infos = new Informations();
        }

        private void balanceSheet_Load(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void balanceSheet_SizeChanged(object sender, EventArgs e)
        {
            ArrangeControls();
        }
        private void ArrangeControls()
        {
            int totalWidth = dataGridView1.Width + dataGridView2.Width + dataGridView3.Width;
            int spacing = 10; // İstediğiniz boşluk miktarını burada belirleyebilirsiniz
            int startX = (this.ClientSize.Width - totalWidth - spacing * 2) / 2; // DataGridView'lerin sol başlangıç noktası

            dataGridView1.Location = new Point(startX, dataGridView1.Location.Y);
            dataGridView2.Location = new Point(startX + dataGridView1.Width + spacing, dataGridView2.Location.Y);
            dataGridView3.Location = new Point(startX + dataGridView1.Width + dataGridView2.Width + spacing * 2, dataGridView3.Location.Y);
            int startX_lbl = dataGridView1.Left;
            int labelY = dataGridView1.Top - label2.Height - 10;

            label2.Location = new Point(startX_lbl, labelY);
            label3.Location = new Point(dataGridView2.Left, labelY);
            label4.Location = new Point(dataGridView3.Left, labelY);
            accnoLbl.Location = new Point(startX_lbl, labelY-100);
            accnoTxt.Location = new Point(startX_lbl, labelY - 80);
            showBtn.Location = new Point(startX_lbl + 240, labelY - 90);
        }

        private void showBtn_Click(object sender, EventArgs e)
        {
            infos.AccountNo = 0;
            DataGridViewDisplay();
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

                // useraccount tablosunda hesap numarası kontrolü
                string accountCheckQuery = "SELECT COUNT(*) FROM useraccount WHERE Account_No = @accountNo";
                using (MySqlCommand accountCheckCommand = new MySqlCommand(accountCheckQuery, connection))
                {
                    try
                    {
                        accountCheckCommand.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                        int accountCount = Convert.ToInt32(accountCheckCommand.ExecuteScalar());

                        if (accountCount == 0)
                        {
                            MessageBox.Show("Böyle bir hesap numarası bulunamadı!", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları:" + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                //Hesap numarasını kullanarak gerekli bilgileri sorgular ve yazdırır
                string debitquery = "SELECT * FROM debit WHERE AccountNo = @accountNo";
                using (MySqlCommand debitCheckCommand = new MySqlCommand(debitquery, connection))
                {
                    try
                    {
                        debitCheckCommand.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                        int debitCount = Convert.ToInt32(debitCheckCommand.ExecuteScalar());

                        if (debitCount == 0)
                        {
                            // DataGridView'in içerisine hata mesajını yazdırma
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Hata");
                            dataTable.Rows.Add("Bu hesapla daha önceden para çekme işlemi yapılmamış!");

                            dataGridView1.DataSource = dataTable;
                        }
                        else
                        {
                            using (MySqlDataReader reader = debitCheckCommand.ExecuteReader())
                            {
                                // DataGridView'e verileri yükleyin
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                dataGridView1.DataSource = dataTable;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları:"+ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Hesap numarasını kullanarak gerekli bilgileri sorgular ve yazdırır
                string investmentquery = "SELECT * FROM investment WHERE AccountNo = @accountNo";
                using (MySqlCommand investmentCheckCommand = new MySqlCommand(investmentquery, connection))
                {
                    try
                    {
                        investmentCheckCommand.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                        int debitCount = Convert.ToInt32(investmentCheckCommand.ExecuteScalar());

                        if (debitCount == 0)
                        {
                            // DataGridView'in içerisine hata mesajını yazdırma
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Hata");
                            dataTable.Rows.Add("Bu hesapla daha önceden para çekme işlemi yapılmamış!");

                            dataGridView2.DataSource = dataTable;
                        }
                        else
                        {
                            using (MySqlDataReader reader = investmentCheckCommand.ExecuteReader())
                            {
                                // DataGridView'e verileri yükleyin
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                dataGridView2.DataSource = dataTable;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları:" + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //Hesap numarasını kullanarak gerekli bilgileri sorgular ve yazdırır
                string transferquery = "SELECT * FROM transfer WHERE account_no = @accountNo";
                using (MySqlCommand transferCheckCommand = new MySqlCommand(transferquery, connection))
                {
                    try
                    {
                        transferCheckCommand.Parameters.AddWithValue("@accountNo", infos.AccountNo);
                        int debitCount = Convert.ToInt32(transferCheckCommand.ExecuteScalar());

                        if (debitCount == 0)
                        {
                            // DataGridView'in içerisine hata mesajını yazdırma
                            DataTable dataTable = new DataTable();
                            dataTable.Columns.Add("Hata");
                            dataTable.Rows.Add("Bu hesapla daha önceden para çekme işlemi yapılmamış!");

                            dataGridView3.DataSource = dataTable;
                        }
                        else
                        {
                            using (MySqlDataReader reader = transferCheckCommand.ExecuteReader())
                            {
                                // DataGridView'e verileri yükleyin
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                dataGridView3.DataSource = dataTable;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Veritabanı işleminde bir hata oluştu!\nHata Ayrıntıları:" + ex.ToString(), "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void DataGridViewDisplay()
        {
            //datagridview1
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //satır ve sütun genişliğini otomatik ayarlar
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);

            //datagridview2
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            //satır ve sütun genişliğini otomatik ayarlar
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);

            //datagridview3
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.Black;
            //satır ve sütun genişliğini otomatik ayarlar
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView3.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
        }
    }
}

namespace FinansMerkezi
{
    partial class creditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.accnoTxt = new System.Windows.Forms.TextBox();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.oldblncTxt = new System.Windows.Forms.TextBox();
            this.damountTxt = new System.Windows.Forms.TextBox();
            this.modecomboBox = new System.Windows.Forms.ComboBox();
            this.detailsBtn = new System.Windows.Forms.Button();
            this.updateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tarih:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(418, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = ".../.../...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Hesap Numarası:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "İsim:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Önceki Bakiye:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mevduat Yöntemi:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 396);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Mevduat Miktarı:";
            // 
            // accnoTxt
            // 
            this.accnoTxt.Location = new System.Drawing.Point(192, 129);
            this.accnoTxt.Name = "accnoTxt";
            this.accnoTxt.Size = new System.Drawing.Size(263, 22);
            this.accnoTxt.TabIndex = 7;
            // 
            // nameTxt
            // 
            this.nameTxt.Enabled = false;
            this.nameTxt.Location = new System.Drawing.Point(192, 195);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.ReadOnly = true;
            this.nameTxt.Size = new System.Drawing.Size(263, 22);
            this.nameTxt.TabIndex = 8;
            // 
            // oldblncTxt
            // 
            this.oldblncTxt.Enabled = false;
            this.oldblncTxt.Location = new System.Drawing.Point(192, 263);
            this.oldblncTxt.Name = "oldblncTxt";
            this.oldblncTxt.ReadOnly = true;
            this.oldblncTxt.Size = new System.Drawing.Size(263, 22);
            this.oldblncTxt.TabIndex = 9;
            // 
            // damountTxt
            // 
            this.damountTxt.Location = new System.Drawing.Point(192, 390);
            this.damountTxt.Name = "damountTxt";
            this.damountTxt.Size = new System.Drawing.Size(263, 22);
            this.damountTxt.TabIndex = 11;
            // 
            // modecomboBox
            // 
            this.modecomboBox.FormattingEnabled = true;
            this.modecomboBox.Location = new System.Drawing.Point(192, 322);
            this.modecomboBox.Name = "modecomboBox";
            this.modecomboBox.Size = new System.Drawing.Size(263, 24);
            this.modecomboBox.TabIndex = 12;
            // 
            // detailsBtn
            // 
            this.detailsBtn.Location = new System.Drawing.Point(495, 119);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new System.Drawing.Size(131, 38);
            this.detailsBtn.TabIndex = 13;
            this.detailsBtn.Text = "Detayları Göster";
            this.detailsBtn.UseVisualStyleBackColor = true;
            this.detailsBtn.Click += new System.EventHandler(this.detailsBtn_Click);
            // 
            // updateBtn
            // 
            this.updateBtn.Location = new System.Drawing.Point(192, 443);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(123, 46);
            this.updateBtn.TabIndex = 14;
            this.updateBtn.Text = "Güncelle";
            this.updateBtn.UseVisualStyleBackColor = true;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            // 
            // creditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(895, 564);
            this.Controls.Add(this.updateBtn);
            this.Controls.Add(this.detailsBtn);
            this.Controls.Add(this.modecomboBox);
            this.Controls.Add(this.damountTxt);
            this.Controls.Add(this.oldblncTxt);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.accnoTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "creditForm";
            this.Text = "Kredi Formu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox accnoTxt;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox oldblncTxt;
        private System.Windows.Forms.TextBox damountTxt;
        private System.Windows.Forms.ComboBox modecomboBox;
        private System.Windows.Forms.Button detailsBtn;
        private System.Windows.Forms.Button updateBtn;
    }
}
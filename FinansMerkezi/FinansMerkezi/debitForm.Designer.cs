﻿namespace FinansMerkezi
{
    partial class debitForm
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
            this.blncTxt = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.amountTxt = new System.Windows.Forms.TextBox();
            this.accnoTxt = new System.Windows.Forms.TextBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.detailsBtn = new System.Windows.Forms.Button();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.modecomboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // blncTxt
            // 
            this.blncTxt.Enabled = false;
            this.blncTxt.Location = new System.Drawing.Point(248, 260);
            this.blncTxt.Name = "blncTxt";
            this.blncTxt.ReadOnly = true;
            this.blncTxt.Size = new System.Drawing.Size(263, 22);
            this.blncTxt.TabIndex = 58;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(248, 441);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(123, 46);
            this.saveBtn.TabIndex = 55;
            this.saveBtn.Text = "Kaydet";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // amountTxt
            // 
            this.amountTxt.Location = new System.Drawing.Point(248, 390);
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(263, 22);
            this.amountTxt.TabIndex = 54;
            // 
            // accnoTxt
            // 
            this.accnoTxt.Location = new System.Drawing.Point(248, 129);
            this.accnoTxt.Name = "accnoTxt";
            this.accnoTxt.Size = new System.Drawing.Size(263, 22);
            this.accnoTxt.TabIndex = 53;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.Location = new System.Drawing.Point(418, 61);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(69, 25);
            this.dateLabel.TabIndex = 47;
            this.dateLabel.Text = ".../.../...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 31);
            this.label1.TabIndex = 46;
            this.label1.Text = "Tarih:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 396);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 16);
            this.label7.TabIndex = 52;
            this.label7.Text = "Çekilecek Miktar:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 16);
            this.label6.TabIndex = 51;
            this.label6.Text = "Para Çekme Yöntemi:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 16);
            this.label5.TabIndex = 50;
            this.label5.Text = "Bakiye:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 16);
            this.label4.TabIndex = 49;
            this.label4.Text = "Ad-Soyad:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 48;
            this.label3.Text = "Hesap Numarası:";
            // 
            // detailsBtn
            // 
            this.detailsBtn.Location = new System.Drawing.Point(551, 119);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new System.Drawing.Size(131, 38);
            this.detailsBtn.TabIndex = 59;
            this.detailsBtn.Text = "Detayları Göster";
            this.detailsBtn.UseVisualStyleBackColor = true;
            this.detailsBtn.Click += new System.EventHandler(this.detailsBtn_Click);
            // 
            // nameTxt
            // 
            this.nameTxt.Enabled = false;
            this.nameTxt.Location = new System.Drawing.Point(248, 195);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.ReadOnly = true;
            this.nameTxt.Size = new System.Drawing.Size(263, 22);
            this.nameTxt.TabIndex = 61;
            // 
            // modecomboBox
            // 
            this.modecomboBox.FormattingEnabled = true;
            this.modecomboBox.Location = new System.Drawing.Point(248, 322);
            this.modecomboBox.Name = "modecomboBox";
            this.modecomboBox.Size = new System.Drawing.Size(263, 24);
            this.modecomboBox.TabIndex = 62;
            // 
            // debitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(895, 564);
            this.Controls.Add(this.modecomboBox);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.detailsBtn);
            this.Controls.Add(this.blncTxt);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.amountTxt);
            this.Controls.Add(this.accnoTxt);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "debitForm";
            this.Text = "Para Çekme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox blncTxt;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox amountTxt;
        private System.Windows.Forms.TextBox accnoTxt;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button detailsBtn;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.ComboBox modecomboBox;
    }
}
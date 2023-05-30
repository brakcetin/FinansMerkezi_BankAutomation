namespace FinansMerkezi
{
    partial class fdForm
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
            this.periodTxt = new System.Windows.Forms.TextBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.interestTxt = new System.Windows.Forms.TextBox();
            this.accnoTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.modecomboBox = new System.Windows.Forms.ComboBox();
            this.lirasTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // periodTxt
            // 
            this.periodTxt.Location = new System.Drawing.Point(248, 325);
            this.periodTxt.Name = "periodTxt";
            this.periodTxt.Size = new System.Drawing.Size(263, 22);
            this.periodTxt.TabIndex = 43;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(248, 441);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(123, 46);
            this.saveBtn.TabIndex = 42;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // interestTxt
            // 
            this.interestTxt.Location = new System.Drawing.Point(248, 390);
            this.interestTxt.Name = "interestTxt";
            this.interestTxt.Size = new System.Drawing.Size(263, 22);
            this.interestTxt.TabIndex = 40;
            // 
            // accnoTxt
            // 
            this.accnoTxt.Location = new System.Drawing.Point(248, 129);
            this.accnoTxt.Name = "accnoTxt";
            this.accnoTxt.Size = new System.Drawing.Size(263, 22);
            this.accnoTxt.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(418, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 31;
            this.label2.Text = ".../.../...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 31);
            this.label1.TabIndex = 30;
            this.label1.Text = "Tarih:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 396);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 16);
            this.label7.TabIndex = 36;
            this.label7.Text = "Faiz (%):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Süre (gün):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "Yatırılacak Tutar (TL):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "Yöntem:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Hesap Numarası:";
            // 
            // modecomboBox
            // 
            this.modecomboBox.FormattingEnabled = true;
            this.modecomboBox.Location = new System.Drawing.Point(248, 192);
            this.modecomboBox.Name = "modecomboBox";
            this.modecomboBox.Size = new System.Drawing.Size(263, 24);
            this.modecomboBox.TabIndex = 44;
            // 
            // lirasTxt
            // 
            this.lirasTxt.Location = new System.Drawing.Point(248, 260);
            this.lirasTxt.Name = "lirasTxt";
            this.lirasTxt.Size = new System.Drawing.Size(263, 22);
            this.lirasTxt.TabIndex = 45;
            // 
            // fdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(895, 564);
            this.Controls.Add(this.lirasTxt);
            this.Controls.Add(this.modecomboBox);
            this.Controls.Add(this.periodTxt);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.interestTxt);
            this.Controls.Add(this.accnoTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "fdForm";
            this.Text = "Vadeli Mevduat Formu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox periodTxt;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox interestTxt;
        private System.Windows.Forms.TextBox accnoTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox modecomboBox;
        private System.Windows.Forms.TextBox lirasTxt;
    }
}
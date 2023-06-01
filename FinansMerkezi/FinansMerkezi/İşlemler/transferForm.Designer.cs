namespace FinansMerkezi
{
    partial class transferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(transferForm));
            this.sentBtn = new System.Windows.Forms.Button();
            this.detailsBtn = new System.Windows.Forms.Button();
            this.toamountTxt = new System.Windows.Forms.TextBox();
            this.blncTxt = new System.Windows.Forms.TextBox();
            this.nameTxt = new System.Windows.Forms.TextBox();
            this.fromaccnoTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toaccnoTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sentBtn
            // 
            this.sentBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(40)))), ((int)(((byte)(46)))));
            this.sentBtn.FlatAppearance.BorderSize = 2;
            this.sentBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sentBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sentBtn.Location = new System.Drawing.Point(273, 443);
            this.sentBtn.Name = "sentBtn";
            this.sentBtn.Size = new System.Drawing.Size(123, 46);
            this.sentBtn.TabIndex = 6;
            this.sentBtn.Text = "Gönder";
            this.sentBtn.UseVisualStyleBackColor = true;
            this.sentBtn.Click += new System.EventHandler(this.sentBtn_Click);
            // 
            // detailsBtn
            // 
            this.detailsBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(40)))), ((int)(((byte)(46)))));
            this.detailsBtn.FlatAppearance.BorderSize = 2;
            this.detailsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detailsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailsBtn.Location = new System.Drawing.Point(576, 119);
            this.detailsBtn.Name = "detailsBtn";
            this.detailsBtn.Size = new System.Drawing.Size(131, 38);
            this.detailsBtn.TabIndex = 1;
            this.detailsBtn.Text = "Detayları Göster";
            this.detailsBtn.UseVisualStyleBackColor = true;
            this.detailsBtn.Click += new System.EventHandler(this.detailsBtn_Click);
            // 
            // toamountTxt
            // 
            this.toamountTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(189)))), ((int)(((byte)(128)))));
            this.toamountTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toamountTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toamountTxt.Location = new System.Drawing.Point(273, 390);
            this.toamountTxt.Name = "toamountTxt";
            this.toamountTxt.Size = new System.Drawing.Size(263, 24);
            this.toamountTxt.TabIndex = 5;
            // 
            // blncTxt
            // 
            this.blncTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(189)))), ((int)(((byte)(128)))));
            this.blncTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.blncTxt.Enabled = false;
            this.blncTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blncTxt.Location = new System.Drawing.Point(273, 263);
            this.blncTxt.Name = "blncTxt";
            this.blncTxt.ReadOnly = true;
            this.blncTxt.Size = new System.Drawing.Size(263, 24);
            this.blncTxt.TabIndex = 3;
            // 
            // nameTxt
            // 
            this.nameTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(189)))), ((int)(((byte)(128)))));
            this.nameTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTxt.Enabled = false;
            this.nameTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameTxt.Location = new System.Drawing.Point(273, 195);
            this.nameTxt.Name = "nameTxt";
            this.nameTxt.ReadOnly = true;
            this.nameTxt.Size = new System.Drawing.Size(263, 24);
            this.nameTxt.TabIndex = 2;
            // 
            // fromaccnoTxt
            // 
            this.fromaccnoTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(189)))), ((int)(((byte)(128)))));
            this.fromaccnoTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fromaccnoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromaccnoTxt.Location = new System.Drawing.Point(273, 129);
            this.fromaccnoTxt.Name = "fromaccnoTxt";
            this.fromaccnoTxt.Size = new System.Drawing.Size(263, 24);
            this.fromaccnoTxt.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(418, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = ".../.../...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 31);
            this.label1.TabIndex = 15;
            this.label1.Text = "Tarih:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 396);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "Gönderilecek Miktar:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(45, 331);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(207, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Göndrilecek Hesap Numarası:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(45, 266);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 18);
            this.label5.TabIndex = 19;
            this.label5.Text = "Bakiye:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "İsim:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Gönderen Hesap Numarası:";
            // 
            // toaccnoTxt
            // 
            this.toaccnoTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(189)))), ((int)(((byte)(128)))));
            this.toaccnoTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toaccnoTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toaccnoTxt.Location = new System.Drawing.Point(273, 325);
            this.toaccnoTxt.Name = "toaccnoTxt";
            this.toaccnoTxt.Size = new System.Drawing.Size(263, 24);
            this.toaccnoTxt.TabIndex = 4;
            // 
            // transferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(229)))), ((int)(((byte)(156)))));
            this.ClientSize = new System.Drawing.Size(895, 564);
            this.Controls.Add(this.toaccnoTxt);
            this.Controls.Add(this.sentBtn);
            this.Controls.Add(this.detailsBtn);
            this.Controls.Add(this.toamountTxt);
            this.Controls.Add(this.blncTxt);
            this.Controls.Add(this.nameTxt);
            this.Controls.Add(this.fromaccnoTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "transferForm";
            this.Text = "Transfer İşlemleri";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sentBtn;
        private System.Windows.Forms.Button detailsBtn;
        private System.Windows.Forms.TextBox toamountTxt;
        private System.Windows.Forms.TextBox blncTxt;
        private System.Windows.Forms.TextBox nameTxt;
        private System.Windows.Forms.TextBox fromaccnoTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox toaccnoTxt;
    }
}
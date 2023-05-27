using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinansMerkezi
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void yeniHesapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newAccount newacc = new newAccount();
            newacc.MdiParent = this;
            newacc.Show();
        }

        private void kullanıcıEkleGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateForm up = new updateForm();
            up.MdiParent = this;
            up.Show();
        }

        private void tümHesaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allUsers allus = new allUsers();
            allus.MdiParent = this;
            allus.Show();
        }

        private void mevduatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creditForm crdfrm = new creditForm();
            crdfrm.MdiParent = this;
            crdfrm.Show();
        }

        private void paraÇekmekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debitForm dpf = new debitForm();
            dpf.MdiParent = this;
            dpf.Show();
        }

        private void transferİşlemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transferForm trans = new transferForm();
            trans.MdiParent = this;
            trans.Show();
        }

        private void vadeliMevduatFormuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fdForm fd = new fdForm();
            fd.MdiParent = this;
            fd.Show();
        }

        private void bilançoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            balanceSheet bal = new balanceSheet();
            bal.MdiParent = this;
            bal.Show();
        }

        private void vadeliMevduatGötürüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewFD viewFD = new viewFD();
            viewFD.MdiParent = this;
            viewFD.Show();
        }

        private void çıkışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void şifreDeğiştirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changePass chngpass = new changePass();
            chngpass.MdiParent = this;
            chngpass.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

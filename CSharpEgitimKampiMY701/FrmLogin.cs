using CSharpEgitimKampiMY701.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampiMY701
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        CSharpEgitimKampi701FinancialCRMDbEntities dataBase = new CSharpEgitimKampi701FinancialCRMDbEntities();        

        private void picBoxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegister frm = new FrmRegister();
            frm.Show();
            Hide();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            var user = dataBase.Tbl_Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.UserPassword == txtPassword.Text);

            int userId = dataBase.Tbl_Users.Where(x => x.Username == txtUsername.Text)
                                            .Select(y => y.UserId)
                                            .FirstOrDefault();

            if (user != null)
            {                
                FrmDashboard frm = new FrmDashboard();
                frm.userId = userId;
                frm.Show(); 
                this.Hide();
            }
            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}

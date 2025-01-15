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
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        CSharpEgitimKampi701FinancialCRMDbEntities dataBase = new CSharpEgitimKampi701FinancialCRMDbEntities();

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string passwordAgain = txtPasswordAgain.Text;

            Tbl_Users user = new Tbl_Users();
            user.Username = txtUsername.Text;
            user.UserNameSurname = txtNameSurname.Text;
            user.UserMail = txtMail.Text;
            user.UserPassword = txtPassword.Text;

            if(user.UserPassword == passwordAgain)
            {
                dataBase.Tbl_Users.Add(user);
                dataBase.SaveChanges();
                MessageBox.Show("Kullanıcı başarıyla eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Şifreleriniz aynı değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBoxExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

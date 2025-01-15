using CSharpEgitimKampiMY701.Models;
using CSharpEgitimKampiMY701.Timer;
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
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            // SharedTimer sınıfındaki TimerTick olayına abone ol
            SharedTimer.TimerTick += SharedTimer_TimerTick;
        }

        CSharpEgitimKampi701FinancialCRMDbEntities dataBase = new CSharpEgitimKampi701FinancialCRMDbEntities();
        public int userId;

        // SharedTimer Sınıfındaki Timer'ın Tick Olayı
        private void SharedTimer_TimerTick(object sender, EventArgs e)
        {
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            // Username
            var user = dataBase.Tbl_Users.FirstOrDefault(x => x.UserId == userId);

            if(user != null)
            {
                lblUserNameSurname.Text = user.UserNameSurname;
                txtUsername.Text = user.Username;
                txtPassword.Text = user.UserPassword;
                txtUserNameSurname.Text = user.UserNameSurname;
                txtMail.Text = user.UserMail;
            }

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(cbPassword.Text == "Göster")
            {
                cbPassword.Text = "Gizle";
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                cbPassword.Text = "Göster";
                txtPassword.UseSystemPasswordChar = true;
            }               
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if(txtNewPassword.Text == txtNewPasswordAgain.Text)
            {
                int id = userId;
                var values = dataBase.Tbl_Users.Find(id);

                if(values.UserPassword == txtOldPassword.Text)
                {
                    values.UserPassword = txtNewPassword.Text;
                    dataBase.SaveChanges();
                    MessageBox.Show("Şifreniz güncellenmiştir.\nYeni Şifreniz: " + txtNewPassword.Text + "\nLütfen uygulamayı yeniden başlatınız.", "Bilgilendirme", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Şifreniz yanlış!", "Hata", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Şifreler aynı değil!", "Hata", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }   
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBills frm = new FrmBills();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnSpendings_Click(object sender, EventArgs e)
        {
            FrmSpendings frm = new FrmSpendings();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnBankProcesses_Click(object sender, EventArgs e)
        {
            FrmBankProcesses frm = new FrmBankProcesses();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard dashboardForm = Application.OpenForms["FrmDashboard"] as FrmDashboard;
            if (dashboardForm != null)
            {
                dashboardForm.Show();
                this.Close();
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şu an zaten Ayarlar formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
               MessageBoxIcon.Warning);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmDashboard dashboardForm = Application.OpenForms["FrmDashboard"] as FrmDashboard;
            if (dashboardForm != null)
            {
                dashboardForm.Show();
                this.Close();
            }
        }        
    }
}

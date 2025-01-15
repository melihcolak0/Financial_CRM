using CSharpEgitimKampiMY701.Models;
using CSharpEgitimKampiMY701.Timer;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CSharpEgitimKampiMY701
{
    public partial class FrmBanks : Form
    {
        public FrmBanks()
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

        private void FrmBanks_Load(object sender, EventArgs e)
        {
            // Banka Hesap Bakiyelerini Getirme
            //var ziraatBankasiBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "Ziraat Bankası").
            //Select(y => y.BankBalance).FirstOrDefault();
            //var vakifBankBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "Vakıfbank").
            //Select(y => y.BankBalance).FirstOrDefault();
            //var isBankasiBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "İş Bankası").
            //Select(y => y.BankBalance).FirstOrDefault();
            //lblZiraatBankBalance.Text = ziraatBankasiBalance.ToString() + " ₺";
            //lblVakifbankBalance.Text = vakifBankBalance.ToString() + " ₺";
            //lblIsBankBalance.Text = isBankasiBalance.ToString() + " ₺";

            decimal? ziraatBankasiBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "Ziraat Bankası" && x.UserId == userId).
                Select(y => y.BankBalance).FirstOrDefault();
            lblZiraatBankBalance.Text = ziraatBankasiBalance.HasValue ? ziraatBankasiBalance.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            decimal? vakifBankBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "Vakıfbank" && x.UserId == userId).
                Select(y => y.BankBalance).FirstOrDefault();
            lblVakifbankBalance.Text = vakifBankBalance.HasValue ? vakifBankBalance.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            decimal? isBankasiBalance = dataBase.Tbl_Banks.Where(x => x.BankTitle == "İş Bankası" && x.UserId == userId).
                Select(y => y.BankBalance).FirstOrDefault();
            lblIsBankBalance.Text = isBankasiBalance.HasValue ? isBankasiBalance.Value.
                ToString("N2") + " ₺" : "0,00 ₺";


            // Son 5 Hesap Hareketini Getirme
            var bankProcess1 = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).OrderByDescending(x => x.BankProcessId).
                Take(1).FirstOrDefault();
            lblBankProcess1.Text = bankProcess1.Description + " - " + bankProcess1.Amount + " ₺";
            lblBankProcess1DateClock.Text = bankProcess1.ProcessDate.ToString();
            var bankProcess2 = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).OrderByDescending(x => x.BankProcessId).
                Take(2).Skip(1).FirstOrDefault();
            lblBankProcess2.Text = bankProcess2.Description + " - " + bankProcess2.Amount + " ₺";
            lblBankProcess2DateClock.Text = bankProcess2.ProcessDate.ToString();
            var bankProcess3 = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).OrderByDescending(x => x.BankProcessId).
                Take(3).Skip(2).FirstOrDefault();
            lblBankProcess3.Text = bankProcess3.Description + " - " + bankProcess3.Amount + " ₺";
            lblBankProcess3DateClock.Text = bankProcess3.ProcessDate.ToString();
            var bankProcess4 = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).OrderByDescending(x => x.BankProcessId).
                Take(4).Skip(3).FirstOrDefault();
            lblBankProcess4.Text = bankProcess4.Description + " - " + bankProcess4.Amount + " ₺";
            lblBankProcess4DateClock.Text = bankProcess4.ProcessDate.ToString();
            var bankProcess5 = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).OrderByDescending(x => x.BankProcessId).
                Take(5).Skip(4).FirstOrDefault();
            lblBankProcess5.Text = bankProcess5.Description + " - " + bankProcess5.Amount + " ₺";
            lblBankProcess5DateClock.Text = bankProcess5.ProcessDate.ToString();

            // Username
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();

            lblUserNameSurname.Text = user.ToString();

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            FrmDashboard dashboardForm = Application.OpenForms["FrmDashboard"] as FrmDashboard;
            if (dashboardForm != null)
            {
                dashboardForm.Show();
                this.Close();
            }
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBills frm = new FrmBills();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
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
            MessageBox.Show("Şu an zaten Bankalar formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }        
    }
}

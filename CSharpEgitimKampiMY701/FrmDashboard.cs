using CSharpEgitimKampiMY701.Models;
using CSharpEgitimKampiMY701.Timer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampiMY701
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();

            // SharedTimer sınıfındaki TimerTick olayına abone ol
            SharedTimer.TimerTick += SharedTimer_TimerTick;
        }

        CSharpEgitimKampi701FinancialCRMDbEntities dataBase = new CSharpEgitimKampi701FinancialCRMDbEntities();
        public int counter = 0;
        public int userId;

        // SharedTimer Sınıfındaki Timer'ın Tick Olayı
        private void SharedTimer_TimerTick(object sender, EventArgs e)
        {
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            var totalBalance = dataBase.Tbl_Banks.Where(y => y.UserId == userId).Sum(x => x.BankBalance);
            lblTotalBalance.Text = totalBalance.HasValue? totalBalance.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            var lastBankProcessDescription = dataBase.Tbl_BankProcesses.Where(z => z.UserId == userId).OrderByDescending(x => x.BankProcessId).Take(1).Select(y => y.Description).FirstOrDefault();
            lblLastBankProcessDescription.Text = lastBankProcessDescription.ToString();

            var lastBankProcessAmount = dataBase.Tbl_BankProcesses.Where(z => z.UserId == userId).OrderByDescending(x => x.BankProcessId).Take(1).Select(y => y.Amount).FirstOrDefault();
            lblLastBankProcessAmount.Text = lastBankProcessAmount.HasValue ? lastBankProcessAmount.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            var lastBankProcessDate = dataBase.Tbl_BankProcesses.Where(z => z.UserId == userId).OrderByDescending(x => x.BankProcessId).Take(1).Select(y => y.ProcessDate).FirstOrDefault();
            lblLastBankProcessDate.Text = lastBankProcessDate.ToString();

            // Chart 1
            var bankData = dataBase.Tbl_Banks.Where(y => y.UserId == userId)
                                             .Select(x => new
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();
            chart1.Series.Clear();
            var series = chart1.Series.Add("Bankalar");
            foreach(var data in bankData)
            {
                series.Points.AddXY(data.BankTitle, data.BankBalance);
            }

            // Chart2
            var billData = dataBase.Tbl_Bills.Where(y => y.UserId == userId)
                                             .Select(x => new
            {
                x.BillTitle,
                x.BillAmount
            }).ToList();
            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeColumn;            
            foreach(var data in billData)
            {
                series2.Points.AddXY(data.BillTitle, data.BillAmount);                
            }

            // Username
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();
                                          
            lblUserNameSurname.Text = user.ToString();


            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            if(counter % 4 == 1)
            {
                var electricBill = dataBase.Tbl_Bills.Where(x => x.BillTitle == "Elektrik Faturası" && x.UserId == userId).Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektrik Faturası";
                lblBillAmount.Text = electricBill.ToString() + " ₺";
            }
            if (counter % 4 == 2)
            {
                var electricBill = dataBase.Tbl_Bills.Where(x => x.BillTitle == "Doğalgaz Faturası" && x.UserId == userId).Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz Faturası";
                lblBillAmount.Text = electricBill.ToString() + " ₺";
            }
            if (counter % 4 == 3)
            {
                var electricBill = dataBase.Tbl_Bills.Where(x => x.BillTitle == "Su Faturası" && x.UserId == userId).Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su Faturası";
                lblBillAmount.Text = electricBill.ToString() + " ₺";
            }
            if (counter % 4 == 0)
            {
                var electricBill = dataBase.Tbl_Bills.Where(x => x.BillTitle == "İnternet Faturası" && x.UserId == userId).Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "İnternet Faturası";
                lblBillAmount.Text = electricBill.ToString() + " ₺";
            }            
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.userId = this.userId;
            frm.Show();
            Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBills frm = new FrmBills();
            frm.userId = this.userId;
            frm.Show();
            Hide();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.userId = this.userId;
            frm.Show();
            Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şu an zaten Dashboard formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnSpendings_Click(object sender, EventArgs e)
        {
            FrmSpendings frm = new FrmSpendings();
            frm.userId = this.userId;
            frm.Show();
            Hide();
        }

        private void btnBankProcesses_Click(object sender, EventArgs e)
        {
            FrmBankProcesses frm = new FrmBankProcesses();
            frm.userId=this.userId;
            frm.Show();
            Hide();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frm = new FrmSettings();
            frm.userId = this.userId;
            frm.Show();
            Hide();
        }
    }
}

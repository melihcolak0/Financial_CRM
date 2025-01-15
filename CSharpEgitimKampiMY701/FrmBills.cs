using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSharpEgitimKampiMY701.Models;
using CSharpEgitimKampiMY701.Timer;
namespace CSharpEgitimKampiMY701
{
    public partial class FrmBills : Form
    {
        public FrmBills()
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

        void ListBills()
        {
            var values = dataBase.Tbl_Bills.Where(y => y.UserId == userId).Select(x => new
            {
                x.BillId,
                x.BillTitle,
                x.BillAmount,
                x.BillPeriod
            }).ToList();
            dataGridView1.DataSource = values;
        }

        private void FrmBilling_Load(object sender, EventArgs e)
        {
            ListBills();

            // Username
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();

            lblUserNameSurname.Text = user.ToString();

            // İstatistik
            decimal? value = dataBase.Tbl_Bills.Where(x => x.UserId == userId).Sum(y => y.BillAmount);
            lblStatistics.Text = value.HasValue ? "Toplam Ödeme Miktarı: " + value.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListBills();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tbl_Bills bills = new Tbl_Bills();
            bills.BillTitle = txtTitle.Text;
            bills.BillAmount = decimal.Parse(txtAmount.Text);
            bills.BillPeriod = txtPeriod.Text;
            bills.UserId = userId;
            dataBase.Tbl_Bills.Add(bills);
            dataBase.SaveChanges();

            MessageBox.Show("Ödeme ekleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListBills();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var deletedValue = dataBase.Tbl_Bills.Find(id);
            dataBase.Tbl_Bills.Remove(deletedValue);
            dataBase.SaveChanges();

            MessageBox.Show("Ödeme silme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListBills();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = dataBase.Tbl_Bills.Find(id);

            values.BillTitle = txtTitle.Text;
            values.BillAmount = decimal.Parse(txtAmount.Text);
            values.BillPeriod = txtPeriod.Text;
            values.UserId = userId;
            dataBase.SaveChanges();

            MessageBox.Show("Ödeme güncelleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListBills();
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.userId = this.userId;
            frm.Show();
            Close();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPeriod.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.userId = this.userId;
            frm.Show();
            this.Close();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şu an zaten Faturalar formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
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

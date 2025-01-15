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
    public partial class FrmBankProcesses : Form
    {
        public FrmBankProcesses()
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

        void ListBankProcesses()
        {
            var values = dataBase.Tbl_BankProcesses.Where(y => y.UserId == userId).Select(x => new
            {
                x.BankProcessId,
                x.Description,
                x.ProcessDate,
                x.ProcessType,
                x.Amount,
                x.Tbl_Banks.BankTitle
            }).ToList();
            dataGridView1.DataSource = values;
        }

        private void FrmBankProcesses_Load(object sender, EventArgs e)
        {
            ListBankProcesses();

            // Username            
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();

            lblUserNameSurname.Text = user.ToString();

            // ComboBox'a Banka Çekme
            var values = dataBase.Tbl_Banks.Where(x => x.UserId == userId)
                                               .Select(y => new
                                               {
                                                   y.BankId,
                                                   y.BankTitle
                                               }).ToList();
            cmbBank.DisplayMember = "BankTitle";
            cmbBank.ValueMember = "BankId";
            cmbBank.DataSource = values;

            // İstatistik
            decimal? value = dataBase.Tbl_BankProcesses.Where(x => x.UserId == userId).Sum(y => y.Amount);            
            lblStatistics.Text = value.HasValue ? "Toplam Hareket Miktarı: " + value.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListBankProcesses();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tbl_BankProcesses tbl_BankProcesses = new Tbl_BankProcesses();
            tbl_BankProcesses.Description = txtDescription.Text;
            tbl_BankProcesses.ProcessDate = DateTime.Parse(mtxtDate.Text);
            tbl_BankProcesses.ProcessType = txtType.Text;
            tbl_BankProcesses.Amount = Convert.ToInt16(txtAmount.Text);
            tbl_BankProcesses.BankId = int.Parse(cmbBank.SelectedValue.ToString());
            tbl_BankProcesses.UserId = userId;
            dataBase.Tbl_BankProcesses.Add(tbl_BankProcesses);
            dataBase.SaveChanges();

            MessageBox.Show("Hareket ekleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
               MessageBoxIcon.Information);

            ListBankProcesses();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var deletedValue = dataBase.Tbl_BankProcesses.Find(id);
            dataBase.Tbl_BankProcesses.Remove(deletedValue);
            dataBase.SaveChanges();

            MessageBox.Show("Hareket silme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
               MessageBoxIcon.Information);

            ListBankProcesses();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = dataBase.Tbl_BankProcesses.Find(id);
            values.Description = txtDescription.Text;
            values.ProcessDate = DateTime.Parse(mtxtDate.Text);
            values.ProcessType = txtType.Text;
            values.Amount = decimal.Parse(txtAmount.Text);
            values.BankId = int.Parse(cmbBank.SelectedValue.ToString());
            dataBase.SaveChanges();

            MessageBox.Show("Hareket güncelleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
               MessageBoxIcon.Information);

            ListBankProcesses();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            mtxtDate.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtType.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbBank.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            FrmCategory frm = new FrmCategory();
            frm.userId = this.userId;
            frm.Show();
            Close();
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            FrmBanks frm = new FrmBanks();
            frm.userId = this.userId;
            frm.Show();
            Close();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            FrmBills frm = new FrmBills();
            frm.userId = this.userId;
            frm.Show();
            Close();
        }

        private void btnSpendings_Click(object sender, EventArgs e)
        {
            FrmSpendings frm = new FrmSpendings();
            frm.userId = this.userId;
            frm.Show();
            Close();
        }

        private void btnBankProcesses_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şu an zaten Banka Hareketleri formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
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
            FrmSettings frm = new FrmSettings();
            frm.userId = this.userId;
            frm.Show();
            Close();
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

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
    public partial class FrmSpendings : Form
    {
        public FrmSpendings()
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

        void ListSpendings()
        {
            var values = dataBase.Tbl_Spendings.Where(y => y.UserId == userId).Select(x => new
            {
                x.SpendingId,
                x.SpendingTitle,
                x.SpendingAmount,
                x.SpendingDate,
                x.Tbl_Categories.CategoryName
            }).ToList();
            dataGridView1.DataSource = values;
        }

        private void FrmSpendings_Load(object sender, EventArgs e)
        {
            ListSpendings();

            // Username
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();

            lblUserNameSurname.Text = user.ToString();

            // ComboBox'a Kategori Çekme
            var values = dataBase.Tbl_Categories.Where(x => x.UserId == userId)
                                               .Select(y => new
                                               {
                                                   y.CategoryId,
                                                   y.CategoryName
                                               }).ToList();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";
            cmbCategory.DataSource = values;

            // İstatistik
            decimal? value = dataBase.Tbl_Spendings.Where(x => x.UserId == userId).Sum(y => y.SpendingAmount);
            lblStatistics.Text = value.HasValue ? "Toplam Gider Miktarı: " + value.Value.
                ToString("N2") + " ₺" : "0,00 ₺";

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAmount.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            mtxtDate.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cmbCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListSpendings();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tbl_Spendings tbl_Spendings = new Tbl_Spendings();
            tbl_Spendings.SpendingTitle = txtTitle.Text;
            tbl_Spendings.SpendingAmount = decimal.Parse(txtAmount.Text);
            tbl_Spendings.SpendingDate = DateTime.Parse(mtxtDate.Text);
            tbl_Spendings.CategoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            tbl_Spendings.UserId = userId;
            dataBase.Tbl_Spendings.Add(tbl_Spendings);
            dataBase.SaveChanges();

            MessageBox.Show("Gider ekleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListSpendings();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var deletedValue = dataBase.Tbl_Spendings.Find(id);
            dataBase.Tbl_Spendings.Remove(deletedValue);
            dataBase.SaveChanges();

            MessageBox.Show("Gider silme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListSpendings();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var values = dataBase.Tbl_Spendings.Find(id);
            values.SpendingTitle = txtTitle.Text;
            values.SpendingAmount = decimal.Parse(txtAmount.Text);
            values.SpendingDate = DateTime.Parse(mtxtDate.Text);
            values.CategoryId = int.Parse(cmbCategory.SelectedValue.ToString());
            dataBase.SaveChanges();

            MessageBox.Show("Gider güncelleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListSpendings();
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

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            FrmDashboard dashboardForm = Application.OpenForms["FrmDashboard"] as FrmDashboard;
            if (dashboardForm != null)
            {
                dashboardForm.Show();
                this.Close();
            }
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
            Close();
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
            MessageBox.Show("Şu an zaten Giderler formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
               MessageBoxIcon.Warning);
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

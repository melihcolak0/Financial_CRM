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
    public partial class FrmCategory : Form
    {
        public FrmCategory()
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

        private void FrmCategory_Load(object sender, EventArgs e)
        {
            ListCategories();

            // Username
            var user = dataBase.Tbl_Users.Where(x => x.UserId == userId)
                                         .Select(y => y.UserNameSurname)
                                         .FirstOrDefault();

            lblUserNameSurname.Text = user.ToString();

            // Saat ve Tarih Kullanımı
            lblDateClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        void ListCategories()
        {
            var values = dataBase.Tbl_Categories.Where(y => y.UserId == userId).Select(x => new
            {
                x.CategoryId,
                x.CategoryName
            }).ToList();
            dataGridView1.DataSource = values;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            ListCategories();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tbl_Categories tbl_Categories = new Tbl_Categories();
            tbl_Categories.CategoryName = txtCategoryName.Text;
            tbl_Categories.UserId = userId;
            dataBase.Tbl_Categories.Add(tbl_Categories);
            dataBase.SaveChanges();

            MessageBox.Show("Kategori ekleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListCategories();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(txtId.Text);
            var deletedValue = dataBase.Tbl_Categories.Find(id);
            dataBase.Tbl_Categories.Remove(deletedValue);
            dataBase.SaveChanges();

            MessageBox.Show("Kategori silme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListCategories();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(txtId.Text);
            var values = dataBase.Tbl_Categories.Find(id);
            values.CategoryName = txtCategoryName.Text;
            dataBase.SaveChanges();

            MessageBox.Show("Kategori güncelleme işlemi başarılı.", "Bilgilendirme", MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ListCategories();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtCategoryName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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

        private void btnCategory_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Şu an zaten Kategoriler formundasınız.", "Bilgilendirme", MessageBoxButtons.OK,
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

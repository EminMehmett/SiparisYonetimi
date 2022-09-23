using SiparisYonetimi.Business.Managers;
using SiparisYonetimi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiparisYonetimi.WinFormsUI
{
    public partial class MusteriYonetimi : Form
    {
        public MusteriYonetimi()
        {
            InitializeComponent();
        }
        CustomerManager manager = new CustomerManager();
        private void MusteriYonetimi_Load(object sender, EventArgs e)
        {
            dgvMusteriler.DataSource = manager.GetAll();
        }
        void Temizle()
        {
            var nesneler = groupBox1.Controls.OfType<TextBox>();
            foreach (var item in nesneler)
            {
                item.Clear();
            }
            chbDurum.Checked = false;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Name = txtMusteriAdi.Text;
            customer.SurName = txtSoyadi.Text;
            customer.Phone = txtTelefon.Text;
            customer.Email = txtEmail.Text;
            customer.Adress = txtAdres.Text;
            customer.IsActive = chbDurum.Checked;
            if (string.IsNullOrWhiteSpace(txtMusteriAdi.Text) || string.IsNullOrWhiteSpace(txtSoyadi.Text))
            {
                MessageBox.Show("Müşteri Adı Boş Geçilemez!!");
                return;
            }
            try
            {
                manager.Add(customer);
                var sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    dgvMusteriler.DataSource = manager.GetAll();
                    MessageBox.Show("Kayıt Eklendi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void dgvMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvMusteriler.CurrentRow.Cells[0].Value);
                var kayit = manager.Find(id);

                txtMusteriAdi.Text = kayit.Name;
                txtSoyadi.Text = kayit.SurName;
                txtEmail.Text = kayit.Email;
                txtTelefon.Text = kayit.Phone;
                txtAdres.Text = kayit.Adress;
                chbDurum.Checked = kayit.IsActive;

                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMusteriAdi.Text) || string.IsNullOrWhiteSpace(txtSoyadi.Text))
            {
                MessageBox.Show("Müşteri Adı Boş Geçilemez!!");
                return;
            }
            int id = Convert.ToInt32(dgvMusteriler.CurrentRow.Cells[0].Value);
            Customer customer = manager.Find(id);

            customer.Name = txtMusteriAdi.Text;
            customer.SurName = txtSoyadi.Text;
            customer.Phone = txtTelefon.Text;
            customer.Email = txtEmail.Text;
            customer.Adress = txtAdres.Text;
            customer.IsActive = chbDurum.Checked;
            try
            {
                manager.Update(customer);
                var sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    dgvMusteriler.DataSource = manager.GetAll();
                    MessageBox.Show("Kayıt Güncellendi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvMusteriler.CurrentRow.Cells[0].Value);
                Customer customer = manager.Find(id);

                manager.Delete(customer);
                var sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    dgvMusteriler.DataSource = manager.GetAll();
                    MessageBox.Show("Kayıt Silindi!");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvMusteriler.DataSource = manager.GetAll(customer => customer.Name.Contains(txtAra.Text));
        }
    }
}

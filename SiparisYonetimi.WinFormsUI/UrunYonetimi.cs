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
    public partial class UrunYonetimi : Form
    {
        public UrunYonetimi()
        {
            InitializeComponent();
        }
        ProductManager manager = new ProductManager();
        CategoryManager categoryManager = new CategoryManager();
        BrandManager brandManager = new BrandManager();
        void Yükle()
        {
            dgvUrunler.DataSource = manager.GetAll();
            cbKategoriler.DataSource = categoryManager.GetAll();
            cbMarkalar.DataSource = brandManager.GetAll();
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
        void Kontrol()
        {
            if (string.IsNullOrWhiteSpace(txtUrunAdi.Text))
            {
                MessageBox.Show("Ürün Adı Boş Geçilemez!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFiyat.Text))
            {
                MessageBox.Show("Fiyat Boş Geçilemez!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtStok.Text))
            {
                MessageBox.Show("Stok Boş Geçilemez!");
                return;
            }
        }
        private void UrunYonetimi_Load(object sender, EventArgs e)
        {
            Yükle();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Kontrol();
            try
            {
                Product urun = new Product();
                urun.Name = txtUrunAdi.Text;
                urun.Description = txtAciklama.Text;
                urun.Price = Convert.ToDecimal(txtFiyat.Text);
                urun.Stock = int.Parse(txtStok.Text);
                urun.Image = txtResim.Text;
                urun.IsActive = chbDurum.Checked;
                urun.BrandId = (int)cbMarkalar.SelectedValue;
                urun.CategoryId = (int)cbKategoriler.SelectedValue;
                manager.Add(urun);
                int sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    Yükle();
                    MessageBox.Show("Kayıt Eklendi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu!");

            }
        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = (int)dgvUrunler.CurrentRow.Cells[0].Value;
                var urun = manager.Find(id);
                txtUrunAdi.Text = urun.Name;
                txtAciklama.Text = urun.Description;
                txtFiyat.Text = urun.Price.ToString();
                txtStok.Text = urun.Stock.ToString();
                txtResim.Text = urun.Image;
                chbDurum.Checked = urun.IsActive;
                cbKategoriler.SelectedValue = urun.CategoryId;
                cbMarkalar.SelectedValue = urun.BrandId;

                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Kontrol();
            try
            {
                int id = (int)dgvUrunler.CurrentRow.Cells[0].Value;
                var urun = manager.Find(id);
                urun.Name = txtUrunAdi.Text;
                urun.Description = txtAciklama.Text;
                urun.Price = Convert.ToDecimal(txtFiyat.Text);
                urun.Stock = int.Parse(txtStok.Text);
                urun.Image = txtResim.Text;
                urun.IsActive = chbDurum.Checked;
                urun.BrandId = (int)cbMarkalar.SelectedValue;
                urun.CategoryId = (int)cbKategoriler.SelectedValue;
                manager.Update(urun);
                int sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    Yükle();
                    MessageBox.Show("Kayıt Güncellendi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu!");

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Kontrol();
            try
            {
                int id = (int)dgvUrunler.CurrentRow.Cells[0].Value;
                var urun = manager.Find(id);
                manager.Delete(urun);

                var sonuc = manager.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    Yükle();
                    MessageBox.Show("Kayıt Silindi");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata oluştu!");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = manager.GetAll(m => m.Name.Contains(txtAra.Text) || m.Description.Contains(txtAra.Text));
        }
    }
}

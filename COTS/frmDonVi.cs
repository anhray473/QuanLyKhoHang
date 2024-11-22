using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS
{
    public partial class frmDonVi : DevExpress.XtraEditors.XtraForm
    {
        public frmDonVi()
        {
            InitializeComponent();
        }
        DONVI _donvi;
        CONGTY _congty;
        bool _them;
        string _madvi;

        private void frmDonVi_Load(object sender, EventArgs e)
        {
            _donvi = new DONVI();
            _congty = new CONGTY();
            loadCongTy();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            cboCty.SelectedIndexChanged += cboCty_SelectedIndexChanged;
            loadDviByCty();


        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtSDT.Enabled = t;
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
            checkKho.Enabled = t;
            txtKyHieu.Enabled = t;
        }
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtSDT.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
            checkKho.Checked = false;
            txtKyHieu.Text = "";
        }
        void loadCongTy()
        {
            cboCty.DataSource =_congty.getAll();
            cboCty.DisplayMember = "TenCTy";
            cboCty.ValueMember = "MaCTy";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            txtMa.Enabled = true;
            showHideControl(false);
            _enabled(true);
            _reset();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _donvi.delete(_madvi);
            }
            loadDviByCty();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_DONVI dvi = new tb_DONVI();
                dvi.MaCTy = cboCty.SelectedValue.ToString();
                dvi.MaDVi = txtMa.Text;
                dvi.TenDVi = txtTen.Text;
                dvi.SDT = txtSDT.Text;
                dvi.Fax = txtFax.Text;
                dvi.Email = txtEmail.Text;
                dvi.DiaChi = txtDiaChi.Text;
                dvi.Disabled = chkDisabled.Checked;
                dvi.KyHieu = txtKyHieu.Text;
                dvi.Kho = checkKho.Checked;
                _donvi.add(dvi);

            }
            else
            {
                tb_DONVI dvi = _donvi.getItem(_madvi);
                dvi.MaCTy = cboCty.SelectedValue.ToString();
                dvi.MaDVi = txtMa.Text;
                dvi.TenDVi = txtTen.Text;
                dvi.SDT = txtSDT.Text;
                dvi.Fax = txtFax.Text;
                dvi.Email = txtEmail.Text;
                dvi.DiaChi = txtDiaChi.Text;
                dvi.Disabled = chkDisabled.Checked;
                dvi.KyHieu = txtKyHieu.Text;
                dvi.Kho = checkKho.Checked;
                _donvi.update(dvi);

            }
            _them = false;
            loadDviByCty();
            _enabled(false);
            showHideControl(true);
            txtMa.Enabled =false;
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            txtMa.Enabled = false;
            _enabled(false);
            showHideControl(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDviByCty();
        }
        void loadDviByCty()
        {
            gcDanhSach.DataSource = _donvi.getAll(cboCty.SelectedValue.ToString());
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                cboCty.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MaCTy");
                _madvi = gvDanhSach.GetFocusedRowCellValue("MaDVi").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MaDVi").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenDVi").ToString();
                txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());
                txtKyHieu.Text=gvDanhSach.GetFocusedRowCellValue("KyHieu").ToString() ;
                checkKho.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Kho").ToString());
            }
        }
    }
}
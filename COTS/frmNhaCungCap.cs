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
    public partial class frmNhaCungCap : DevExpress.XtraEditors.XtraForm
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        NHACUNGCAP _ncc;
        bool _them;
        int _mancc;

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            _ncc = new NHACUNGCAP();
            loadData();
            txtMa.Enabled =false;
            showHideControl(true);
            _enabled(false);
        }
        void loadData()
        {
            gcDanhSach.DataSource = _ncc.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
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
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            txtMa.Enabled = false;
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
                _ncc.delete(_mancc);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_NHACUNGCAP ncc = new tb_NHACUNGCAP();
                ncc.TenNCC = txtTen.Text;
                ncc.SDT = txtSDT.Text;
                ncc.Fax = txtFax.Text;
                ncc.Email = txtEmail.Text;
                ncc.DiaChi = txtDiaChi.Text;
                ncc.Disabled = chkDisabled.Checked;
                _ncc.add(ncc);

            }
            else
            {
                tb_NHACUNGCAP ncc = _ncc.getItem(_mancc);
                ncc.TenNCC = txtTen.Text;
                ncc.SDT = txtSDT.Text;
                ncc.Fax = txtFax.Text;
                ncc.Email = txtEmail.Text;
                ncc.DiaChi = txtDiaChi.Text;
                ncc.Disabled = chkDisabled.Checked;
                _ncc.update(ncc);

            }
            _them = false;
            loadData();
            showHideControl(true);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _mancc = int.Parse(gvDanhSach.GetFocusedRowCellValue("MaNCC").ToString());
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MaNCC").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenNCC").ToString();
                txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());
            }
        }
    }
}
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
    public partial class frmNhomHH : DevExpress.XtraEditors.XtraForm
    {
        public frmNhomHH()
        {
            InitializeComponent();
        }
        NHOMHH _nhh;
        bool _them;
        int _idn;

        private void frmNhomHH_Load(object sender, EventArgs e)
        {
            _nhh = new NHOMHH();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void loadData()
        {
            gcDanhSach.DataSource = _nhh.getAll();
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
            txtMota.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtMota.Text = "";
            txtTen.Text = "";
            chkDisabled.Checked = false;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _nhh.delete(_idn);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_NHOMHH nhh = new tb_NHOMHH();
                nhh.TenNhom = txtTen.Text;
                nhh.Mota =txtMota.Text;
                nhh.Disabled = chkDisabled.Checked;
                _nhh.add(nhh);

            }
            else
            {
                tb_NHOMHH nhh = _nhh.getItem(_idn);
                nhh.TenNhom = txtTen.Text;
                nhh.Mota = txtMota.Text;
                nhh.Disabled = chkDisabled.Checked;
                _nhh.update(nhh);
            }
            _them = false;
            loadData();
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
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
                _idn = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDNhom").ToString());
                txtMota.Text = gvDanhSach.GetFocusedRowCellValue("Mota").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenNhom").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());

            }
        }
    }
}
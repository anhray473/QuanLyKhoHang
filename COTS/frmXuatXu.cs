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
    public partial class frmXuatXu : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatXu()
        {
            InitializeComponent();
        }
        XUATXU _xx;
        bool _them;
        int _id;

        private void frmXuatXu_Load(object sender, EventArgs e)
        {
            _xx = new XUATXU();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void loadData()
        {
            gcDanhSach.DataSource = _xx.getAll();
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
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            chkDisabled.Checked = false;

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _xx.delete(_id);
            }
            loadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_XUATXU xx = new tb_XUATXU();
                xx.Ten = txtTen.Text;
                xx.Disabled = chkDisabled.Checked;
                _xx.add(xx);

            }
            else
            {
                tb_XUATXU xx = _xx.getItem(_id);
                xx.Ten = txtTen.Text;
                xx.Disabled = chkDisabled.Checked;
                _xx.update(xx);
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
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("ID").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("Ten").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }
    }
}
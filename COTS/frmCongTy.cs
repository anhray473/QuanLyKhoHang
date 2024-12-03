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
    public partial class frmCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmCongTy()
        {
            InitializeComponent();
        }
        public frmCongTy(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }

        CONGTY _congty;
        bool _them;
        string _macty;
        int _right;
        tb_SYS_USER _user;
        private void frmCongTy_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            loadData();
            txtMa.Enabled = false;
            showHideControl(true);
            _enabled(false);
        }
        void loadData()
        {
            gcDanhSach.DataSource = _congty.getAll();
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
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _them =true;
            txtMa.Enabled = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _congty.delete(_macty);
            }
            loadData();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_CONGTY cty = new tb_CONGTY();
                cty.MaCTy = txtMa.Text;
                cty.TenCTy = txtTen.Text;
                cty.SDT = txtSDT.Text;
                cty.Fax = txtFax.Text;
                cty.Email = txtEmail.Text;
                cty.DiaChi = txtDiaChi.Text;
                cty.Disabled = chkDisabled.Checked;
                _congty.add(cty);
                
            }
            else
            {
                tb_CONGTY cty = _congty.getItem(_macty);
                cty.MaCTy = txtMa.Text;
                cty.TenCTy = txtTen.Text;
                cty.SDT = txtSDT.Text;
                cty.Fax = txtFax.Text;
                cty.Email = txtEmail.Text;
                cty.DiaChi = txtDiaChi.Text;
                cty.Disabled = chkDisabled.Checked;
                _congty.update(cty);

            }
            _them = false;
            loadData();
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them =false;
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
            //if (gvDanhSach.RowCount > 0)
            //{
            //    _macty=gvDanhSach.GetFocusedRowCellValue("MaCTy").ToString();
            //    txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MaCTy").ToString();
            //    txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenCTy").ToString();
            //    txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
            //    txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax").ToString() ;
            //    txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
            //    txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
            //    chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());
            //}
            try
            {
                if (gvDanhSach.RowCount > 0 && gvDanhSach.FocusedRowHandle >= 0)
                {
                    _macty = gvDanhSach.GetFocusedRowCellValue("MaCTy")?.ToString() ?? string.Empty;
                    txtMa.Text = _macty;
                    txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenCTy")?.ToString() ?? string.Empty;
                    txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT")?.ToString() ?? string.Empty;
                    txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax")?.ToString() ?? string.Empty;
                    txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email")?.ToString() ?? string.Empty;
                    txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi")?.ToString() ?? string.Empty;

                    var disabled = gvDanhSach.GetFocusedRowCellValue("Disabled");
                    chkDisabled.Checked = disabled != null && bool.Parse(disabled.ToString());
                }
                else
                {
                    MessageBox.Show("No data to display.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
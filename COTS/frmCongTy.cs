using BusinessLayer;
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

        CONGTY _congty;
        bool _them;
        string _macty;
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
            _them =true;
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
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _congty.delete(_macty);
            }
            loadData();
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                DataLayer.CONGTY cty = new DataLayer.CONGTY();
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
                DataLayer.CONGTY cty = _congty.getItem(_macty);
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
            if (gvDanhSach.RowCount > 0)
            {
                _macty=gvDanhSach.GetFocusedRowCellValue("MaCTy").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MaCTy").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenCTy").ToString();
                txtSDT.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax").ToString() ;
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (e.Column.Name == "Disabled" && bool.Parse(e.CellValue.ToString()) == true)
            //{
            //    Image img = Properties.Resources.cancel16x16;
            //    e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
            //    e.Handled = true;
            //}

        }
    }
}
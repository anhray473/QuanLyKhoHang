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
    public partial class frmDVT : DevExpress.XtraEditors.XtraForm
    {
        public frmDVT()
        {
            InitializeComponent();
        }
        DVT _dvt;
        bool _them;
        int _id;
        void loadData()
        {
            gcDanhSach.DataSource = _dvt.getAll();
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

        }
        void _reset()
        {
            //txtMa.Text = "";
            txtTen.Text = "";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
           // txtMa.Enabled = false;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            //txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _dvt.delete(_id);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_DVT dvt = new tb_DVT();
                dvt.Ten = txtTen.Text;
                _dvt.add(dvt);

            }
            else
            {
                tb_DVT dvt = _dvt.getItem(_id);
                dvt.Ten = txtTen.Text;
                _dvt.update(dvt);
            }
            _them = false;
            loadData();
            showHideControl(true);

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            //txtMa.Enabled = false;
            _enabled(false);
            showHideControl(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDVT_Load(object sender, EventArgs e)
        {
            _dvt = new DVT();
            loadData();
 //           txtMa.Enabled = false;
            showHideControl(true);
            _enabled(false);
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _id = int.Parse(gvDanhSach.GetFocusedRowCellValue("ID").ToString());
               // txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IDNhom").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("Ten").ToString();
            }
        }
    }
}
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
    public partial class frmNhapMua : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapMua()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        DONVI _donvi;
        NHACUNGCAP _nhacungcap;
        private void frmNhapMua_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _nhacungcap = new NHACUNGCAP();
            showHideControl(true);
            _enabled(false);
            loadCongTy();
            loadKho();
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged;
            loadDonVi();
            loadNCC();
        }

        private void CboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadKho();
            loadDonVi();

        }

        void loadCongTy()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TenCTy";
            cboCongTy.ValueMember = "MaCTy";
        }
        void loadDonVi()
        {
            cboDonVi.DataSource = _donvi.getKhoByCty(cboCongTy.SelectedValue.ToString());
            cboDonVi.DisplayMember = "TenDVi";
            cboDonVi.ValueMember = "MaDVi";
        }
        void loadKho()
        {
            cboKho.DataSource = _donvi.getKhoByCty(cboCongTy.SelectedValue.ToString());
            cboKho.DisplayMember = "TenDVi";
            cboKho.ValueMember = "MaDVi";
        }
        void loadNCC()
        {
            cboNCC.DataSource = _nhacungcap.getAll();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
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
            txtGhiChu.Enabled = t;
            cboDonVi.Enabled = t;
            cboNCC.Enabled = t;
            cboTrangThai.Enabled = t;
            dtNgay.Enabled = t;
        }
        void _reset()
        {
            txtGhiChu.Text = "";
            txtSoPhieu.Text = "";
            
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
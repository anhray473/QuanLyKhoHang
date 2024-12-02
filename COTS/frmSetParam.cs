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
    public partial class frmSetParam : DevExpress.XtraEditors.XtraForm
    {
        public frmSetParam()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        DONVI _donvi;
        private void frmSetParam_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            loadCongty();
            cboCty.SelectedValue = "CTKVN";
            cboCty.SelectedIndexChanged += CboCty_SelectedIndexChanged;
            loadDonvi();
        }

        private void CboCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDonvi();
        }
        void loadCongty()
        {
            cboCty.DataSource = _congty.getAll();
            cboCty.DisplayMember = "TenCTy";
            cboCty.ValueMember = "MaCTy";
        }
        void loadDonvi()
        {
            cboDvi.DataSource = _donvi.getAllCTy(cboCty.SelectedValue.ToString());
            cboDvi.DisplayMember = "TenDVi";
            cboDvi.ValueMember = "MaDVi";
            cboDvi.SelectedIndex = -1;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string macty = cboCty.SelectedValue.ToString();
            string madvi = (cboDvi.Text.Trim()=="")? "~": cboDvi.SelectedValue.ToString();
            SYS_PARAM _sysParam = new SYS_PARAM(macty, madvi);
            _sysParam.SaveFile();
            MessageBox.Show("Xác lập đơn vị sử dụng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
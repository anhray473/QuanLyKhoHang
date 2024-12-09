using BusinessLayer;
using COTS.Report;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class frmInBarcode : DevExpress.XtraEditors.XtraForm
    {
        public frmInBarcode()
        {
            InitializeComponent();
        }
        NHOMHH _nhom;
        HANGHOA _hanghoa;
        List<obj_INBARCODE> lst;


        private void btnInCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rptInBarcode rpt = new rptInBarcode();
            rpt.DataSource = lst;
            rpt.ShowPreviewDialog();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();

        }

        private void frmInBarcode_Load(object sender, EventArgs e)
        {
            _hanghoa = new HANGHOA();
            _nhom = new NHOMHH();
            lst = new List<obj_INBARCODE>();
            loadNhom();
            cboNhom.SelectedIndexChanged += CboNhom_SelectedIndexChanged;
            loadDanhMuc();
            lst=_hanghoa.getDanhMucInBarcode(int.Parse(cboNhom.SelectedValue.ToString()));
        }

        private void CboNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDanhMuc();
        }

        void loadNhom()
        {
            cboNhom.DataSource = _nhom.getAll();
            cboNhom.DisplayMember = "TenNhom";
            cboNhom.ValueMember = "IDNhom";
        }
        void loadDanhMuc()
        {
            gcDanhMuc.DataSource=_hanghoa.getDanhMucInBarcode(int.Parse(cboNhom.SelectedValue.ToString()));
        }
    }
}
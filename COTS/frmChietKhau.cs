using BusinessLayer;
using DevExpress.Office.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmChietKhau : DevExpress.XtraEditors.XtraForm
    {
        public frmChietKhau()
        {
            InitializeComponent();
        }
        public frmChietKhau(GridView gvChiTiet)
        {
            InitializeComponent();
            this._gvChiTiet = gvChiTiet;
        }
        GridView _gvChiTiet;
        private void btnCapNhat_Click(object sender, EventArgs e)
        {

            if (MYFUNCTIONS.sIsNumber(txtChietKhau.Text))
            {
                for(int i = 0; i < _gvChiTiet.RowCount; i++)
                {
                    _gvChiTiet.SetRowCellValue(i, "ChietKhau", txtChietKhau.Text);
                    _gvChiTiet.SetRowCellValue(i, "ThanhTien",double.Parse(_gvChiTiet.GetRowCellValue(i,"DonGia").ToString())* double.Parse(_gvChiTiet.GetRowCellValue(i, "SoLuong").ToString()) *(1-double.Parse(txtChietKhau.Text)/100));
                }
                _gvChiTiet.UpdateTotalSummary();
                this.Close();
            }
            else
            {
                MessageBox.Show("Chiết khẩu phải là số.", "Thông báo");
            }    
        }
    }
}
using BusinessLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
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
    public partial class frmTraHang : DevExpress.XtraEditors.XtraForm
    {
        public frmTraHang()
        {
            InitializeComponent();
        }
        public frmTraHang(List<obj_CHUNGTU_CT> lstChungTu, GridControl gcChiTiet)
        {
            InitializeComponent();
            this._lstChungTuCT = lstChungTu;
            this._gcChiTiet = gcChiTiet;
        }
        List<obj_CHUNGTU_CT> _lstChungTuCT;
        GridControl _gcChiTiet;

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            obj_CHUNGTU_CT obj;
            var item = _lstChungTuCT.FirstOrDefault(x => x.Code == txtCode.Text);
            if (item != null)
            {
                obj = new obj_CHUNGTU_CT();
                obj.Code = item.Code;
                obj.TenHang = item.TenHang;
                obj.DVT = item.DVT;
                obj.SoLuong = int.Parse("-"+txtSoLuong.Text);
                obj.DonGia = item.DonGia;
                obj.ThanhTien = obj.SoLuong * obj.DonGia;
                if(item.SoLuong <int.Parse(txtSoLuong.Text))
                {
                    MessageBox.Show("Số lượng nhập trả không được lớn hơn số lượng mua","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                _lstChungTuCT.Add(obj);
                _gcChiTiet.DataSource = _lstChungTuCT.ToList();
            }
            else
            {
                MessageBox.Show("Mã hàng không có trong đơn hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
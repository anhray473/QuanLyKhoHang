using BusinessLayer;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.TextEditController.Win32;
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
    public partial class frmBanLe : DevExpress.XtraEditors.XtraForm
    {
        public frmBanLe()
        {
            InitializeComponent();
        }
        public frmBanLe(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user; 
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        CONGTY _congty;
        DONVI _donvi;
        CHUNGTU _chungtu;
        CHUNGTU_CT _chungtuct;
        SYS_SEQUENCE _sequence;
        HANGHOA _hanghoa;
        tb_SYS_SEQUENCE _seq;
        Guid pkhoa;
        List<obj_CHUNGTU_CT> lstChungTuCT;
        private void frmBanLe_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _chungtu = new CHUNGTU();
            _chungtuct = new CHUNGTU_CT();
            _hanghoa = new HANGHOA();
            _sequence = new SYS_SEQUENCE();
            lstChungTuCT = new List<obj_CHUNGTU_CT>();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (gvChiTiet.RowCount == 0)
            {
                MessageBox.Show("Chi tiết đơn hàng không được rỗng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            luuthongtin();
            lstChungTuCT = new List<obj_CHUNGTU_CT>();
            gcChiTiet.DataSource = lstChungTuCT;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (gvChiTiet.RowCount == 0)
            {
                MessageBox.Show("Chi tiết đơn hàng không được rỗng","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            luuthongtin();
            lstChungTuCT = new List<obj_CHUNGTU_CT>();
            gcChiTiet.DataSource = lstChungTuCT;
        }

        private void btnCK_Click(object sender, EventArgs e)
        {
            frmChietKhau frm = new frmChietKhau(gvChiTiet);
            frm.ShowDialog();
        }

        private void btnTraHang_Click(object sender, EventArgs e)
        {
            frmTraHang frm = new frmTraHang(lstChungTuCT,gcChiTiet);
            frm.ShowDialog();
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int index = 0;
                if (!MYFUNCTIONS.sIsNumber(txtCode.Text))
                {
                    MessageBox.Show("Mã hàng không hợp lệ","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var hh=_hanghoa.getItem(txtCode.Text);
                if(hh==null)
                {
                    MessageBox.Show("Mã hàng không có trong danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                obj_CHUNGTU_CT ct = new obj_CHUNGTU_CT();
                obj_HANGHOA _hh =  new obj_HANGHOA();
                _hh = _hanghoa.getItemFull(txtCode.Text);
                ct.Code = _hh.Code;
                ct.TenHang = _hh.TenHang;
                ct.DVT = _hh.DVT;
                ct.SoLuong = 1;
                ct.DonGia = _hh.DonGia;
                ct.ThanhTien = ct.DonGia * ct.SoLuong;
                if(lstChungTuCT.Count>0)
                {
                    var item = lstChungTuCT.FirstOrDefault(x=> x.Code == txtCode.Text);
                    if (item != null)
                    {
                        index = lstChungTuCT.IndexOf(item);
                        lstChungTuCT[index].SoLuong = item.SoLuong + 1;
                        lstChungTuCT[index].ThanhTien = item.DonGia * lstChungTuCT[index].SoLuong;
                    }
                    else
                        lstChungTuCT.Add(ct);
                }
                else
                    lstChungTuCT.Add(ct);

                gcChiTiet.DataSource = lstChungTuCT.ToList();
                txtCode.Text = "";
            }
        }
        void Chungtu_Info(tb_CHUNGTU chungtu)
        {
            string madvi = "";
            if (MYFUNCTIONS._madvi == "~")
            {
                madvi = "CTKVN";
            }
            else
            {
                madvi = MYFUNCTIONS._madvi;
            }
            double _TONGCONG = 0;
            tb_DONVI dvi = _donvi.getItem(madvi);
            _seq = _sequence.getItem("BLE@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu);
            if (_seq == null)
            {
                _seq = new tb_SYS_SEQUENCE();
                _seq.SEQNAME = "BLE@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu;
                _seq.SEQVUALE = 1;
                _sequence.add(_seq);

            }
                chungtu.ID = Guid.NewGuid();
                chungtu.Ngay = DateTime.Now;
                chungtu.SCT = _seq.SEQVUALE.Value.ToString("000000") + @"/" + DateTime.Today.Year.ToString().Substring(2, 2) + @"/XNB/" + dvi.KyHieu;
                chungtu.NguoiTao = _user.IDUser;
                chungtu.NgayTao = DateTime.Now;
            
        
            chungtu.LCT = 4;
            //if (ChietKhau.text == "")
            //    chungtu.ChietKhau = null;
            //else chungtu.ChietKhau = int.Parse(txtCHietKhau.text);
            chungtu.MaCTy = MYFUNCTIONS._macty;
            chungtu.MaDVi = madvi;
            chungtu.MaDVi2 = "1";
            chungtu.TrangThai = 2;
           // chungtu.GhiChu = txtGhiChu.Text;
            chungtu.SoLuong = int.Parse(gvChiTiet.Columns["SoLuong"].SummaryItem.SummaryValue.ToString());

            for (int i = 0; i < gvChiTiet.RowCount; i++)
            {
                if (gvChiTiet.GetRowCellValue(i, "TenHang") == null)
                {
                    gvChiTiet.DeleteRow(i);
                    goto NEXT;
                }
                else
                {
                    _TONGCONG += double.Parse(gvChiTiet.GetRowCellValue(i, gvChiTiet.Columns["ThanhTien"]).ToString());
                }
            }
        NEXT:
            chungtu.TongTien = _TONGCONG;
            chungtu.NguoiSua = _user.IDUser;
            chungtu.NgaySua = DateTime.Now;
        }

        void ChungTuCT_Info(tb_CHUNGTU chungtu)
        {
            _chungtuct.deleteAll(chungtu.ID);
            for (int i = 0; i < gvChiTiet.RowCount; i++)
            {
                if (gvChiTiet.GetRowCellValue(i, "TenHang") == null)
                    gvChiTiet.DeleteRow(i);
                else
                {
                    tb_CHUNGTU_CT _ct = new tb_CHUNGTU_CT();
                    _ct.IDCT = Guid.Parse(Guid.NewGuid().ToString().ToUpper());
                    _ct.ID = chungtu.ID;
                    _ct.STT = i + 1; 
                    _ct.Ngay = DateTime.Now;
                    _ct.CODE = gvChiTiet.GetRowCellValue(i, "Code").ToString();
                    _ct.SoLuong = int.Parse(gvChiTiet.GetRowCellValue(i, "SoLuong").ToString());
                    _ct.DonGia = double.Parse(gvChiTiet.GetRowCellValue(i, "DonGia").ToString());
                    _ct.ThanhTien = double.Parse(gvChiTiet.GetRowCellValue(i, "ThanhTien").ToString());
                    _chungtuct.add(_ct);

                }
            }
        }
        private void luuthongtin()
        {
            try
            {
                tb_CHUNGTU ctu = new tb_CHUNGTU();
                Chungtu_Info(ctu);
                var resultCtu = _chungtu.add(ctu);
                pkhoa = resultCtu.ID;
                _sequence.update(_seq);

                ChungTuCT_Info(resultCtu);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra. " + ex.ToString());
            }
                
             
        }
        private void XuatReport(string _reportName, string _tieude)
        {
            //if(pkhoa !=null)
            //{
            //    Form frm = new Form();
            //    CrystalReportViewer Crv = new CrystalReportViewer();
            //    Crv.ShowGroupTreeButton = false;
            //    Crv.ShowParameterPanelButton = false;
            //    Crv.ToolPanelView = ToolPanelViewType.None;
            //    TableLogOnInfo Thongtin;
            //    ReportDocument doc = new ReportDocument();
            //    doc.Load(System.Windows.Forms.Application.StartupPath+"\\Reports\\"+_reportName+@".rpt");
            //    Thongtin = doc.Database.Tables[0].LogOnInfo;
            //    Thongtin.ConnectionInfo.ServerName = MYFUNCTIONS._srv;
            //    Thongtin.ConnectionInfo.DatabaseName = MYFUNCTIONS._db;
            //    Thongtin.ConnectionInfo.UserID = MYFUNCTIONS._us;
            //    Thongtin.ConnectionInfo.Password = MYFUNCTIONS._pw;
            //    doc.Database.Tables[0].ApplyLogOnInfo(Thongtin);
            //    try
            //    {
            //        doc.
            //    }
            
            //18}
        }

            
        
    }
}
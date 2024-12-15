using BusinessLayer;
using CrystalDecisions.ReportAppServer.ReportDefModel;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
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
using CrystalDecisions.CrystalReports.Engine;


namespace COTS
{
    public partial class frmXuatSi : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatSi()
        {
            InitializeComponent();
        }
        public frmXuatSi(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        bool _them;
        bool _sua = false;
        List<string> _listBarcode;
        string err = "";
        List<_TRANGTHAI> _trangthai;
        CONGTY _congty;
        DONVI _donvi;
        CHUNGTU _chungtu;
        CHUNGTU_CT _chungtuct;
        SYS_SEQUENCE _sequence;
        KHACHHANG _khachhang;
        HANGHOA _hanghoa;
        BindingSource _bdChungTuCT;
        BindingSource _bdChungTu;
        Guid _khoa;
        tb_SYS_SEQUENCE _seq;
        List<tb_CHUNGTU> _listChungTu;
        bool _IsImport;
        private void frmXuatSi_Load(object sender, EventArgs e)
        {
            _IsImport = false;
            _listBarcode = new List<string>();
            _chungtu = new CHUNGTU();
            _chungtuct = new CHUNGTU_CT();
            _hanghoa = new HANGHOA();
            _sequence = new SYS_SEQUENCE();
            _congty = new CONGTY();
            _donvi = new DONVI();
            _khachhang = new KHACHHANG();
            _bdChungTuCT = new BindingSource();
            _bdChungTu = new BindingSource();

            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.Value = DateTime.Now;

            _bdChungTu.PositionChanged += _bdChungTu_PositionChanged; ;
            loadCongTy();
            cboCongTy.SelectedValue = MYFUNCTIONS._macty;
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged; ;

            _trangthai = _TRANGTHAI.getList();
            cboTrangThai.DataSource = _trangthai;
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";

            //loadKho();
            loadDonVi();
            loadDonViXuat();
            _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());//Xem xét 14:30
            _bdChungTu.DataSource = _listChungTu;
            gcDanhSach.DataSource = _bdChungTu;


            xuatThongTin();
            cboDonVi.SelectedIndexChanged += CboDonVi_SelectedIndexChanged; ;
            showHideControl(true);
            contextMenuChiTiet.Enabled = false;
        }
        void loadCongTy()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TenCTy";
            cboCongTy.ValueMember = "MaCTy";
        }
        void loadDonVi()
        {
            cboDonVi.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            cboDonVi.DisplayMember = "TenDVi";
            cboDonVi.ValueMember = "MaDVi";
        }
        void loadDonViXuat()
        {
            cboDonViXuat.DataSource = _donvi.getAll(/*cboCongTy.SelectedValue.ToString()*/);
            cboDonViXuat.DisplayMember = "TenDVi";
            cboDonViXuat.ValueMember = "MaDVi";
        }
        void loadKhachHang()
        {
           // lkKhachHang.Properties.DataSource = _khachhang.getList();
            lkKhachHang.Properties.DisplayMember = "HOTEN";
            lkKhachHang.Properties.ValueMember = "IDKH";
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
            lkKhachHang.Enabled = t;
            cboTrangThai.Enabled = t;
            dtNgay.Enabled = t;
        }
        void _reset()
        {
            txtGhiChu.Text = "";
            txtSoPhieu.Text = "";

        }
        private void CboDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _bdChungTu_PositionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cboDonViXuat.SelectedValue = cboDonVi.SelectedValue;
            _bdChungTuCT.DataSource = _chungtuct.getListByIDFull(_khoa);
            gcChiTiet.DataSource = _bdChungTuCT;
            gvChiTiet.AddNewRow();
            tabChungTu.SelectedTabPage = pageChiTiet;
            gvChiTiet.OptionsBehavior.Editable = true;
            _them = true;
            _sua = false;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
                MessageBox.Show("Không có quyền thao tác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                tb_CHUNGTU cur = (tb_CHUNGTU)_bdChungTu.Current;
                if (cur.TrangThai == 1)
                {
                    _them = false;
                    _sua = true;
                    showHideControl(false);
                    _enabled(true);
                    tabChungTu.SelectedTabPage = pageChiTiet;
                    tabChungTu.TabPages[0].PageEnabled = false;
                    gvChiTiet.OptionsBehavior.Editable = true;
                    contextMenuChiTiet.Enabled = true;
                    cboDonVi.Enabled = false;

                    if (gvChiTiet.RowCount == 0)
                    {
                        List<V_CHUNGTU_CT> _lstChiTiet = new List<V_CHUNGTU_CT>();
                        _bdChungTu.DataSource = _lstChiTiet;
                        gcChiTiet.DataSource = _bdChungTu;
                        gvChiTiet.AddNewRow();
                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", 1);
                    }
                }
                else
                {
                    MessageBox.Show("Không được phép chỉnh sửa chứng từ đã hoàn tất.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                if (MessageBox.Show("Bạn có chắc muốn hủy phiếu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    tb_CHUNGTU cur = (tb_CHUNGTU)_bdChungTu.Current;
                    int index = _bdChungTu.Position;
                    _chungtu.delete(cur.ID, 1);
                    gvDanhSach.SetRowCellValue(index, "NguoiXoa", 0);//XOng user id thêm vào
                    lblXoa.Visible = true;
                }
                else
                    return;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            luuthongtin();
            _them = false;
            _sua = false;
            gvChiTiet.OptionsBehavior.Editable = false;
            contextMenuChiTiet.Enabled = false;
            tabChungTu.TabPages[0].PageEnabled = true;
            showHideControl(true);
            _enabled(false);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            _sua = false;
            showHideControl(true);
            _reset();
            _enabled(false);
            xuatThongTin();
            tabChungTu.TabPages[0].PageEnabled = true;
            tabChungTu.SelectedTabPage = pageDanhSach;
            gvChiTiet.OptionsBehavior.Editable = false;
            contextMenuChiTiet.Enabled = false;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            XuatReport("PHIEU_XUATNB", "Phiếu xuất nội bộ");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnXoaDong_Click(object sender, EventArgs e)
        {

        }

        private void mnXoaCT_Click(object sender, EventArgs e)
        {

        }

        private void mnImport_Click(object sender, EventArgs e)
        {

        }
        void Chungtu_Info(tb_CHUNGTU chungtu)
        {
            double _TONGCONG = 0;
            tb_DONVI dvi = _donvi.getItem(cboDonViXuat.SelectedValue.ToString());
            _seq = _sequence.getItem("XNB@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu);
            if (_seq == null)
            {
                _seq = new tb_SYS_SEQUENCE();
                _seq.SEQNAME = "XNB@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu;
                _seq.SEQVUALE = 1;
                _sequence.add(_seq);

            }
            if (_them)
            {
                chungtu.ID = Guid.NewGuid();
                chungtu.Ngay = dtNgay.Value;
                chungtu.SCT = _seq.SEQVUALE.Value.ToString("000000") + @"/" + DateTime.Today.Year.ToString().Substring(2, 2) + @"/XNB/" + dvi.KyHieu;
                chungtu.NguoiTao = _user.IDUser;
                chungtu.NgayTao = DateTime.Now;
            }
            chungtu.LCT = 2;
            chungtu.MaCTy = cboCongTy.SelectedValue.ToString();
            chungtu.MaDVi = cboDonViXuat.SelectedValue.ToString();
            chungtu.MaDVi2 = lkKhachHang.EditValue.ToString();
            chungtu.TrangThai = int.Parse(cboTrangThai.SelectedValue.ToString());
            chungtu.GhiChu = txtGhiChu.Text;
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
                    _ct.STT = i + 1; //int.Parce
                    _ct.Ngay = dtNgay.Value;
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
            err = "";

            tb_CHUNGTU ctu;
            if (gvChiTiet.RowCount == 0)
            {
                err += "Chi tiết phiếu xuất không được rỗng. \r\n";
                MessageBox.Show("Chi tiết phiếu xuất không được rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gvChiTiet.RowCount == 1 && gvChiTiet.GetRowCellValue(0, "Code") == null)
            {
                err += "Chi tiết phiếu xuất không được rỗng. \r\n";
                MessageBox.Show("Chi tiết phiếu xuất không được rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_them)
            {
                ctu = new tb_CHUNGTU();
                Chungtu_Info(ctu);
                var resultCtu = _chungtu.add(ctu);
                _sequence.update(_seq);

                ChungTuCT_Info(resultCtu);
                _bdChungTu.Add(resultCtu);
                _bdChungTu.MoveLast();
            }
            else
            {
                ctu = (tb_CHUNGTU)_bdChungTu.Current;
                ctu = _chungtu.getItem(ctu.ID);
                Chungtu_Info(ctu);
                var resultCtu = _chungtu.update(ctu);
                ChungTuCT_Info(resultCtu);

                _listChungTu = null;
                _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());

                _bdChungTu.DataSource = _listChungTu;
                gvDanhSach.ClearSorting();
                gvDanhSach.RefreshData();
                var obj = _bdChungTu.List.OfType<tb_CHUNGTU>().ToList().Find(c => c.SCT == resultCtu.SCT);
                _bdChungTu.Position = _bdChungTu.IndexOf(obj);
            }
            xuatThongTin();
            _them = false;

            tabChungTu.SelectedTabPage = pageDanhSach;
        }
        void xuatThongTin()
        {
            tb_CHUNGTU current = (tb_CHUNGTU)_bdChungTu.Current;
            if (current != null)
            {
                //tb_DONVI _dvi = _donvi.getItem(current.MaDVi);
                dtNgay.Value = current.Ngay.Value;
                txtSoPhieu.Text = current.SCT;
                txtGhiChu.Text = current.GhiChu;
                cboDonViXuat.SelectedValue = current.MaDVi;
                lkKhachHang.EditValue = current.MaDVi2;
                cboTrangThai.SelectedValue = current.TrangThai;

                if (current.NguoiXoa != null)
                {
                    lblXoa.Visible = true;
                    btnXoa.Enabled = false;
                }
                else
                {
                    lblXoa.Visible = false;
                    btnXoa.Enabled = true;
                }
                _bdChungTuCT.DataSource = _chungtuct.getListByIDFull(current.ID);//Xem xét lại
                gcChiTiet.DataSource = _bdChungTuCT;
                for (int i = 0; i < gvChiTiet.RowCount; i++)
                {
                    gvChiTiet.SetRowCellValue(i, "STT", i + 1);
                }

            }
        }
        private void XuatReport (string _reportName, string _tieude)
        {
        //    if (_khoa != null)
        //    {
        //        Form frm = new Form();
        //        CrystalReportViewer Crv = new CrystalReportViewer();
        //        Crv.ShowGroupTreeButton = false;
        //        Crv.ShowParameterPanelButton = false;
        //        Crv.ToolPanelView = ToolPanelViewType.None;
        //        TableLogOnInfo Thongtin;
        //        ReportDocument doc = new ReportDocument();
        //        doc.Load(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + _reportName + @".rpt");
        //        Thongtin = doc.Database.Tables[0].LogOnInfo;
        //        Thongtin.ConnectionInfo.ServerName = MYFUNCTIONS._srv;
        //        Thongtin.ConnectionInfo.DatabaseName = MYFUNCTIONS._db;
        //        Thongtin.ConnectionInfo.UserID = MYFUNCTIONS._us;
        //        Thongtin.ConnectionInfo.Password = MYFUNCTIONS._pw;
        //        doc.Database.Tables[0].ApplyLogOnInfo(Thongtin);
        //        try
        //        {
        //            doc.SetParameterValue("ID", "{" + _khoa.ToString() + "}");
        //            Crv.Dock = DockStyle.Fill;
        //            Crv.ReportSource = doc;
        //            frm.Controls.Add(Crv);
        //            Crv.Refresh();
        //            frm.Text = _tieude;
        //            frm.WindowState = FormWindowState.Maximized;
        //            frm.ShowDialog();
        //        }
        //        catch (Exception ex) 
        //        {
        //            MessageBox.Show("Lỗi : " + ex.ToString());
        //        }
        //    } 
        //    else
        //    {
        //        MessageBox.Show("Không có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        }
    }
}
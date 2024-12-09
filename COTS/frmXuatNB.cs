using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace COTS
{
    public partial class frmXuatNB : DevExpress.XtraEditors.XtraForm
    {
        public frmXuatNB()
        {
            InitializeComponent();
        }
        public frmXuatNB(tb_SYS_USER  user, int right)
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
        NHACUNGCAP _nhacungcap;
        HANGHOA _hanghoa;
        BindingSource _bdChungTuCT;
        BindingSource _bdChungTu;
        Guid _khoa;
        tb_SYS_SEQUENCE _seq;
        List<tb_CHUNGTU> _listChungTu;
        bool _IsImport;
        private void frmXuatNB_Load(object sender, EventArgs e)
        {
            _IsImport = false;
            _listBarcode = new List<string>();
            _chungtu = new CHUNGTU();
            _chungtuct = new CHUNGTU_CT();
            _hanghoa = new HANGHOA();
            _sequence = new SYS_SEQUENCE();
            _congty = new CONGTY();
            _donvi = new DONVI();
            _nhacungcap = new NHACUNGCAP();
            _bdChungTuCT = new BindingSource();
            _bdChungTu = new BindingSource();

            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.Value = DateTime.Now;

            _bdChungTu.PositionChanged += _bdChungTu_PositionChanged1;
            loadCongTy();
            cboCongTy.SelectedValue = MYFUNCTIONS._macty;
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged1;

            _trangthai = _TRANGTHAI.getList();
            cboTrangThai.DataSource = _trangthai;
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";

            //loadKho();
            loadDonVi();
            loadDonViNhap();
            loadDonViXuat();
            _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());//Xem xét 14:30
            _bdChungTu.DataSource = _listChungTu;
            gcDanhSach.DataSource = _bdChungTu;


            xuatThongTin();
            cboDonVi.SelectedIndexChanged += CboDonVi_SelectedIndexChanged1;
            showHideControl(true);
            contextMenuChiTiet.Enabled = false;
        }

        private void CboCongTy_SelectedIndexChanged1(object sender, EventArgs e)
        {
            loadDonVi();
        }

        private void _bdChungTu_PositionChanged1(object sender, EventArgs e)
        {
            if (! _them)
            {
                xuatThongTin();
            }
        }

        private void CboDonVi_SelectedIndexChanged1(object sender, EventArgs e)
        {
            _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());//Xem xét 14:30
            _bdChungTu.DataSource = _listChungTu;
            gcDanhSach.DataSource = _bdChungTu;
            xuatThongTin();
        }

        private void CboDonViXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDonVi();
        }

        private void _bdChungTu_PositionChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
        void loadDonViXuat()
        {
            cboDonViXuat.DataSource = _donvi.getAll(/*cboCongTy.SelectedValue.ToString()*/);
            cboDonViXuat.DisplayMember = "TenDVi";
            cboDonViXuat.ValueMember = "MaDVi";
        }
        void loadDonViNhap()
        {
            cboDonViNhap.DataSource = _donvi.getDonViByCty(cboCongTy.SelectedValue.ToString(), false);
            cboDonViNhap.DisplayMember = "TenDVi";
            cboDonViNhap.ValueMember = "MaDVi";
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
            cboDonViNhap.Enabled = t;
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
                    gvDanhSach.SetRowCellValue(index, "NguoiXoa",0);//XOng user id thêm vào
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
            _sua =false;
            gvChiTiet.OptionsBehavior.Editable = false;
            contextMenuChiTiet.Enabled =false;
            tabChungTu.TabPages[0].PageEnabled = true;
            showHideControl(true);
            _enabled(false);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            _sua = false;
            showHideControl(true) ;
            _reset();
            _enabled (false);
            xuatThongTin();
            tabChungTu.TabPages[0].PageEnabled=true;
            tabChungTu.SelectedTabPage = pageDanhSach;
            gvChiTiet.OptionsBehavior.Editable=false;
            contextMenuChiTiet .Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void btnIn_Click(object sender, EventArgs e)
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
            chungtu.MaDVi2 = cboDonViNhap.SelectedValue.ToString();
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
                cboDonViNhap.SelectedValue = current.MaDVi2;
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

        private void gvChiTiet_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvChiTiet.OptionsBehavior.Editable)
            {
                _IsImport = false;
                if (e.KeyData == Keys.Down)
                {
                    if(int.Parse(gvChiTiet.FocusedRowHandle.ToString())==(gvChiTiet.RowCount - 1))
                    {
                        if(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"TenHang")!=null)
                        {
                            gvChiTiet.AddNewRow();
                        }
                    }
                }
                if(e.KeyData == Keys.Up)
                {
                    if (int.Parse(gvChiTiet.FocusedRowHandle.ToString()) == (gvChiTiet.RowCount - 1))
                    {
                        if((gvChiTiet.FocusedValue == null&& gvChiTiet.RowCount>1)||(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"TenHang")==null&& gvChiTiet.RowCount >1))
                            gvChiTiet.DeleteSelectedRows();
                    }
                }
            }
            else
                e.Handled = false;
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if(e.Column.FieldName == "TrangThai")
            {
                if (e.CellValue.ToString() == "1")
                    e.DisplayText = "Chưa hoàn tất";
                else
                    e.DisplayText = "Đã hoàn tất";
            }
            if(e.Column.Name == "NguoiXoa"&&e.CellValue !=null)
            {
                Image img = Properties.Resources.cancel16x16;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled=true;
            }
        }

        private void gvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!_IsImport)
            {
                if (e.Column.FieldName == "Code")
                {
                    if (gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "Code").ToString().IndexOf(".") == 0)
                    {
                        _IsImport = true;
                        frmDanhMuc _popDM = new frmDanhMuc(gvChiTiet, gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "Code").ToString());
                        _popDM.ShowDialog();
                    }
                    else
                    {
                        tb_HANGHOA hh = _hanghoa.getItem(e.Value.ToString());
                        if (hh != null)
                        {
                            if (_hanghoa.checkExist(hh.Code))
                            {
                                List<string> s = new List<string>();
                                if (gvChiTiet.RowCount > 1)
                                {
                                    for (int i = 0; i < gvChiTiet.RowCount - 1; i++)
                                    {
                                        s.Add(gvChiTiet.GetRowCellValue(i, "Code").ToString());
                                    }
                                    if (s.Find(x => x.Equals(e.Value.ToString())) != null)
                                    {
                                        MessageBox.Show("Mã này đã có trong lưới nhập liệu", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    else
                                    {
                                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", hh.TenHang);
                                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", hh.DVT);
                                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", 1);
                                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", hh.DonGia);
                                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", hh.DonGia);
                                        gvChiTiet.UpdateTotalSummary();
                                    }
                                }
                                else
                                {
                                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", hh.TenHang);
                                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", hh.DVT);
                                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", 1);
                                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", hh.DonGia);
                                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", hh.DonGia);
                                    gvChiTiet.UpdateTotalSummary();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Mã tài sản này đã được nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã tài sản không chính xác. Kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        gvChiTiet.RefreshData();
                    }
                }
                //Thay đổi số lượng
                if (e.Column.FieldName == "SoLuong")
                {
                    if (gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang") != null)
                    {
                        double _soluong = double.Parse(e.Value.ToString());
                        if (_soluong != 0)
                        {
                            tb_HANGHOA hh = _hanghoa.getItem(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "Code").ToString());
                            if (gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia") != null)
                            {
                                double _trigiaTT = double.Parse(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia").ToString());
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", _trigiaTT * _soluong);
                                
                            }
                            else
                            {
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", 0);
                            }
                            gvChiTiet.UpdateTotalSummary();

                        }
                        else
                        {
                            MessageBox.Show("Số lượng tài sản không thể bằng 0", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                    gvChiTiet.RefreshData();
                }
                
            
            }
            
        }

        private void mnXoaDong_Click(object sender, EventArgs e)
        {
            int index = 0;
            if(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"Code")!=null)
            {
                if(_them)
                    gvChiTiet.DeleteSelectedRows();
                else
                {
                    index = gvChiTiet.FocusedRowHandle;
                    _listBarcode.Add(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"Code").ToString());
                    gvChiTiet.DeleteSelectedRows();

                }
                if (gvChiTiet.RowCount == 0)
                {
                    gvChiTiet.AddNewRow();
                    gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", 1);
                }
                else
                {
                    for(int i = 0;i<gvChiTiet.RowCount;i++)
                    {
                        gvChiTiet.FocusedRowHandle = i;
                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", i + 1);
                    }
                }
                gvChiTiet.FocusedRowHandle = index;
            }
            else
            {
                MessageBox.Show("Chưa chọn mẫu tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void mnXoaCT_Click(object sender, EventArgs e)
        {
            _listBarcode.Clear();
            for(int i = gvChiTiet.RowCount-1;i>=0;i--)
            {
                _listBarcode.Add(gvChiTiet.GetRowCellValue(i,"Code").ToString());
                gvChiTiet.DeleteRow(i);
            }
            gvChiTiet.AddNewRow();
            gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", 1);
        }

        private void mnImport_Click(object sender, EventArgs e)
        {
            exportExcel();
        }
        void exportExcel()
        {
            string filename = "";
            List<errExport> err = new List<errExport>();
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel 2000-2003 (.xls)|*.xls|Excel 2007 or higher (.xlsx)|*.xlsx";
            if(op.ShowDialog() == DialogResult.OK) 
            {
                SplashScreenManager.ShowForm(this, typeof(frmChoXuLy), true, true, false);
                _IsImport = true;
                List<string> s = new List<string>();
                List<string> _exist = new List<string>();
                if (gvChiTiet.RowCount > 1)
                {
                    if(gvChiTiet.GetRowCellValue(gvChiTiet.RowCount -1, "TenHang")!=null)
                    {
                        for(int i = 0;i< gvChiTiet.RowCount;i++)
                        {
                            _exist.Add(gvChiTiet.GetRowCellValue(i,"Code").ToString());
                        }
                    }
                    else
                    {
                        for (int i = 0; i < gvChiTiet.RowCount - 1; i++)
                            _exist.Add(gvChiTiet.GetRowCellValue(i, "Code").ToString());
                    }
                }
                filename = op.FileName;
                //đọc file excel
                //tạo đối tượng excel
                Excel.Application app = new Excel.Application();
                //Kết nối với tập tin excel
                Excel.Workbook wb = app.Workbooks.Open(filename);
                List<obj_CHUNGTU_CT> lstCTCT = new List<obj_CHUNGTU_CT> ();
                try
                {
                    //Kết nối với sheet cần đọc
                    Excel._Worksheet sheet = wb.Sheets["Sheet1"];
                    //Giới hạn đọc từ dòng cột nào đến dòng cột nào
                    Excel.Range range = sheet.UsedRange;
                    double tongdong = range.Rows.Count;
                    for(float i = 2; i <= range.Rows.Count; i++)
                    {
                        tb_HANGHOA hh = _hanghoa.getItem(range.Cells[i, 1].Value.ToString());
                        if (hh == null)
                        {
                            errExport _err = new errExport();
                            _err._barcode = range.Cells[i, 1].Value.ToString();
                            _err._soluong = int.Parse(range.Cells[i, 2].Value.ToString());
                            _err._errcode = "Barcode không tồn tại";
                            err.Add(_err);
                            continue;
                        }
                        else
                        {
                            if(_exist.Find(x=> x.Equals(hh.Code)) != null)
                            {
                                errExport _err = new errExport();
                                _err._barcode = range.Cells[i, 1].Value.ToString();
                                _err._soluong = int.Parse(range.Cells[i, 2].Value.ToString());
                                _err._errcode = "Trùng Barcode";
                                err.Add(_err);
                                continue;
                            }
                            else
                            {
                                s.Add(range.Cells[i, 1].Value.ToString() + "," + range.Cells[i, 2].Value.ToString());
                                _exist.Add(range.Cells[i, 1].Value.ToString());
                            }
                        }
                    }
                    foreach(string _validItem in s)
                    {
                        string[] item = _validItem.Split(',');
                        string _barcode = item[0].ToString();
                        double _soluong = double.Parse(item[1].ToString());
                        obj_HANGHOA _h = _hanghoa.getItemFull(_barcode);
                        if (gvChiTiet.RowCount > 1)
                        {
                            int mautin = gvChiTiet.RowCount;
                            gvChiTiet.SelectRow(mautin - 1);
                            if(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"TenHang")==null)
                            {
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", mautin);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "Code", _h.Code);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", _h.DVT);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", _h.TenHang);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", _soluong);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", _h.DonGia);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", _h.DonGia*_soluong);
                            }
                            else
                            {
                                gvChiTiet.AddNewRow();
                                gvChiTiet.SelectRow(mautin);
                                mautin++;
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", mautin);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "Code", _h.Code);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", _h.DVT);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", _h.TenHang);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", _soluong);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", _h.DonGia);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", _h.DonGia * _soluong);

                            }
                        }
                        else
                        {
                            if (gvChiTiet.RowCount == 0)
                                gvChiTiet.AddNewRow();
                            int mautin = gvChiTiet.RowCount;
                            gvChiTiet.SelectRow(mautin - 1);
                            if (gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang") == null)
                            {
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", mautin);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "Code", _h.Code);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", _h.DVT);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", _h.TenHang);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", _soluong);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", _h.DonGia);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", _h.DonGia * _soluong);
                            }
                            else
                            {
                                gvChiTiet.AddNewRow();
                                gvChiTiet.SelectRow(mautin);
                                mautin++;
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", mautin);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "Code", _h.Code);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DVT", _h.DVT);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "TenHang", _h.TenHang);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "SoLuong", _soluong);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia", _h.DonGia);
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", _h.DonGia * _soluong);
                            }
                        }
                    }
                    gvChiTiet.AddNewRow();
                    gvChiTiet.SelectRow(gvChiTiet.RowCount - 1);
                    gvChiTiet.DeleteSelectedRows();
                    _IsImport = false;
                    
                }
                catch (Exception ex)
                {
                    app.Workbooks.Close();
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show("Import không thành công kiểm tra lại đường dẫn và định dạng tệp" + ex.Message, "Thông báo");

                }
                finally
                {
                    wb.Close(true);
                    app.Quit();
                    releaseObject(app);
                }
            }
            //xuất mã lỗi ra excel
            if (err.Count != 0)
            {
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
                Excel._Worksheet sheet = null;
                try
                {
                    sheet = wb.ActiveSheet;
                    //Đặt tên sheet
                    sheet.Name = "Lỗi";
                    //Gôm 3 cột thành 1 cột
                    sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 3]].Merge();
                    //canh lề text
                    sheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    //bold
                    sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 3]].BorderAround(Type.Missing, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic);
                    sheet.Cells[1, 1].Value = "LỖI IMPORT";
                    sheet.Cells[1, 1].Font.Size = 20;
                    sheet.Cells[2, 1].Value = "BARCODE";
                    sheet.Cells[2, 2].Value = "SỐ LƯỢNG";
                    sheet.Cells[2, 3].Value = "LỖI";
                    //xUẤT DỮ liệu ra file và tương tác
                    for(int i = 1; i<= err.Count; i++)
                    {
                        sheet.Cells[i + 2, 1].Value = err.ElementAt(i - 1)._barcode;
                        sheet.Cells[i + 2, 2].Value = err.ElementAt(i - 1)._soluong;
                        sheet.Cells[i + 2, 3].Value = err.ElementAt(i - 1)._errcode;
                    }
                    //Lưu vào file xuất
                    string t = System.IO.Path.GetDirectoryName(filename) + @"\" + System.IO.Path.GetFileNameWithoutExtension(filename) + "_log.xlsx";
                    if (File.Exists(t))
                        File.Delete(t);
                    wb.SaveAs(t);


                }
                catch(Exception ex)
                {
                    SplashScreenManager.CloseForm(false);
                    MessageBox.Show(ex.ToString(),"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    wb.Close(true);
                    app.Quit();
                    releaseObject(wb);
                    releaseObject(app);
                    SplashScreenManager.CloseForm(false);
                }
                MessageBox.Show("Có lỗi phát sinh trong quá trình import.Xem chi tiết trong file log.", "Lỗi import", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Import dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch(Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object" + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if(gvDanhSach.RowCount>0)
            {
                tabChungTu.SelectedTabPage = pageChiTiet;
            }
        }

        private void tabChungTu_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if(_sua == false && tabChungTu.SelectedTabPage == pageChiTiet)
            {
                gvChiTiet.OptionsBehavior.Editable = false;
            }
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if(dtTuNgay.Value> dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void dtTuNgay_Leave(object sender, EventArgs e)
        {
            if(dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _listChungTu = _chungtu.getList(2,dtTuNgay.Value, dtDenNgay.Value.AddDays(1),cboDonVi.SelectedValue.ToString());
                _bdChungTu.DataSource = _listChungTu;
            }    
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if(dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ;
            }
        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());
                _bdChungTu.DataSource = _listChungTu;
            }
        }
        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDanhSach.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if(e.RowHandle<0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width)+20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvDanhSach); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText,e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(Width, gvDanhSach); }));
            }    
        }

        private void gvChiTiet_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvChiTiet.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvChiTiet); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvChiTiet); }));
            }
        }
    }
}
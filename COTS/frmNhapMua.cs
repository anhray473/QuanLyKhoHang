using BusinessLayer;
using DataLayer;
using DevExpress.Utils.DirectXPaint;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
        tb_SYS_USER _user;
        int _right;
        bool _them;
        bool _sua = false;
        bool _import;
        List<string> _listBarcode;
        string err = "";
        List<_TRANGTHAI> _trangthai;
        HANGHOA _hanghoa;
        CONGTY _congty;
        DONVI _donvi;
        NHACUNGCAP _nhacungcap;
        SYS_SEQUENCE _sequence;
        Guid _khoa;
        tb_SYS_SEQUENCE _seq;
        CHUNGTU _chungtu;
        CHUNGTU_CT _chungtuct;
        BindingSource _bdChungTuCT;
        BindingSource _bdChungTu;
        List<tb_CHUNGTU> _listChungTu;
        bool _IsImport;
        private void frmNhapMua_Load(object sender, EventArgs e)
        {
            _import = false;
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

            _bdChungTu.PositionChanged += _bdChungTu_PositionChanged;
            loadCongTy();
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged;

            _trangthai = _TRANGTHAI.getList();
            cboTrangThai.DataSource = _trangthai;
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";

            loadKho();
            loadNCC();
            loadKhoDanhSach();
            _listChungTu =_chungtu.getList(1, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboKho.SelectedValue.ToString());//Xem xét 14:30
            _bdChungTu.DataSource = _listChungTu;
            gcDanhSach.DataSource = _bdChungTu;


            xuatThongTin();
            cboDonVi.SelectedIndexChanged += CboDonVi_SelectedIndexChanged;
            cboKho.SelectedIndexChanged += CboKho_SelectedIndexChanged;
            showHideControl(true);
            //ContextMenuC
        }

        private void CboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            _listChungTu = _chungtu.getList(1, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboKho.SelectedValue.ToString());
            _bdChungTu.DataSource =_listChungTu;
            gcDanhSach.DataSource =_bdChungTu;
            xuatThongTin();
        }

        private void CboDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void _bdChungTu_PositionChanged(object sender, EventArgs e)
        {
            if (!_them)
            {
                xuatThongTin();
            }
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
        void loadKhoDanhSach()
        {
            cboKho.DataSource = _donvi.getKhoByCty(cboCongTy.SelectedValue.ToString());
            cboKho.DisplayMember = "TenDVi";
            cboKho.ValueMember = "MaDVi";
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
        void xuatThongTin()
        {
            tb_CHUNGTU current = (tb_CHUNGTU)_bdChungTu.Current;
            if(current != null)
            {
                //tb_DONVI _dvi = _donvi.getItem(current.MaDVi);
                dtNgay.Value = current.Ngay.Value;
                txtSoPhieu.Text = current.SCT;
                txtGhiChu.Text= current.GhiChu;
                cboDonVi.SelectedValue = current.MaDVi;
                cboNCC.SelectedValue = int.Parse(current.MaDVi2);
                cboTrangThai.SelectedValue = current.TrangThai;

                if(current.NguoiXoa != null)
                {
                    lblXoa.Visible = true;
                    btnXoa.Enabled = true;
                }
                else
                {
                    lblXoa.Visible =false;
                    btnXoa.Enabled = true;
                }
                _bdChungTuCT.DataSource = _chungtuct.getListByIDFull(current.ID);//Xem xét lại
                gcChiTiet.DataSource = _bdChungTuCT;
                for (int i=0; i< gvChiTiet.RowCount; i++)
                {
                    gvChiTiet.SetRowCellValue(i, "STT", i + 1);
                }

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
            if(_right ==1)
                MessageBox.Show("Không có quyền thao tác.", "Lỗi" , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
            else
            {
                tb_CHUNGTU cur = (tb_CHUNGTU)_bdChungTu.Current;
                if (cur.TrangThai == 1)
                {
                    _them = false;
                    _sua = true;
                    showHideControl(false);
                    _enabled(true);
                    tabChungTu.SelectedTabPage=pageChiTiet;
                    tabChungTu.TabPages[0].PageEnabled = false;
                    gvChiTiet.OptionsBehavior.Editable=true;
                    contextMenuChiTiet.Enabled = true;
                    cboDonVi.Enabled = false;

                    if(gvChiTiet.RowCount ==0)
                    {
                        List<V_CHUNGTU_CT> _lstChiTiet = new List<V_CHUNGTU_CT>();
                        _bdChungTu.DataSource = _lstChiTiet ;
                        gcChiTiet.DataSource = _bdChungTu ;
                        gvChiTiet.AddNewRow();
                        gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "STT", 1);
                    }    
                }
                else
                {
                    MessageBox.Show("Không được phép chỉnh sửa chứng từ đã hoàn tất.","Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    gvDanhSach.SetRowCellValue(index, "NguoiXoa", 0);
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
            c

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Chungtu_Info(tb_CHUNGTU chungtu)
        {
            double _TONGCONG = 0;
            tb_DONVI dvi = _donvi.getItem(cboDonVi.SelectedValue.ToString());
            _seq = _sequence.getItem("NHM@" + DateTime.Today.Year.ToString()+ "@"+dvi.KyHieu);
            if (_seq == null)
            {
                _seq = new tb_SYS_SEQUENCE();
                _seq.SEQNAME = "NHM@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu;
                _seq.SEQVUALE = 1;
                _sequence.add(_seq);

            }
            if (_them)
            {
                chungtu.ID = Guid.Parse(Guid.NewGuid().ToString().ToUpper());
                chungtu.Ngay = dtNgay.Value;
                chungtu.SCT = _seq.SEQVUALE.Value.ToString("000000")+@"/"+DateTime.Today.Year.ToString().Substring(2,2)+@"/NHM/"+dvi.KyHieu;
                chungtu.NguoiTao = 1; //IDUSER
                chungtu.NgayTao = DateTime.Now;
            }
            chungtu.LCT = 1;
            chungtu.MaCTy = cboCongTy.SelectedValue.ToString();
            chungtu.MaDVi = cboDonVi.SelectedValue.ToString();
            chungtu.MaDVi2 = cboNCC.SelectedValue.ToString();
            chungtu.TrangThai = int.Parse(cboTrangThai.SelectedValue.ToString());
            chungtu.GhiChu = txtGhiChu.Text;
            chungtu.SoLuong = int.Parse(gvChiTiet.Columns["SoLuong"].SummaryItem.SummaryValue.ToString());
            
            for(int i = 0; i< gvChiTiet.RowCount; i++)
            {
                if(gvChiTiet.GetRowCellValue(i, "TenHang") == null)
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
            chungtu.NguoiSua = 1; //UserID
            chungtu.NgaySua = DateTime.Now;
        }
        void ChungTuCT_Info(tb_CHUNGTU chungtu)
        {
            _chungtuct.deleteAll(chungtu.ID);
            for(int i = 0; i<gvChiTiet.RowCount-1; i++)
            {
                if (gvChiTiet.GetRowCellValue(i, "TenHang") == null)
                    gvChiTiet.DeleteRow(i);
                else
                {
                    tb_CHUNGTU_CT _ct = new tb_CHUNGTU_CT();
                    _ct.IDCT = Guid.Parse(Guid.NewGuid().ToString().ToUpper());
                    _ct.ID = chungtu.ID;
                    _ct.STT = i + 1; //int.parc...
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
            if(gvChiTiet.RowCount == 0)
            {
                err += "Chi tiết phiếu xuất không được rỗng. \r\n";
                MessageBox.Show("Chi tiết phiếu xuất không được rỗng.","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (gvChiTiet.RowCount == 1 && gvChiTiet.GetRowCellValue(0,"Code")==null )
            {
                err += "Chi tiết phiếu xuất không được rỗng. \r\n";
                MessageBox.Show("Chi tiết phiếu xuất không được rỗng.", "Lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                _listChungTu = _chungtu.getList(1, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());

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
                            MessageBox.Show("Mã tài sản không chính xác. Kiểm tra lại","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return ;
                        }
                        gvChiTiet.RefreshData();
                    }
                            

                }
                //Thay đổi số lượng
                if (e.Column.FieldName == "SoLuong")
                {
                    if(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"TenHang")!=null)
                    {
                        double _soluong = double.Parse(e.Value.ToString());
                        if (_soluong != 0)
                        {
                            tb_HANGHOA hh = _hanghoa.getItem(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "Code").ToString());
                            if (gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle,"DonGia")!=null)
                            {
                                double _trigiaTT = double.Parse(gvChiTiet.GetRowCellValue(gvChiTiet.FocusedRowHandle, "DonGia").ToString());
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle,  "ThanhTien",_trigiaTT* _soluong);
                            }
                            else
                            {
                                gvChiTiet.SetRowCellValue(gvChiTiet.FocusedRowHandle, "ThanhTien", 0);
                            }
                            gvChiTiet.UpdateTotalSummary();

                        }
                        else
                        {
                            MessageBox.Show("Số lượng tài sản không thể bằng 0","Lỗi nhập liệu", MessageBoxButtons.OK , MessageBoxIcon.Error);
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

        private void gvChiTiet_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if(!gvChiTiet.IsGroupRow(e.RowHandle))
            {
                if(e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle+1).ToString();
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText,e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_Size.Width)+20;
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
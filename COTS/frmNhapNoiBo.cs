using BusinessLayer;
using DataLayer;
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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace COTS
{
    public partial class frmNhapNoiBo : DevExpress.XtraEditors.XtraForm
    {
        public frmNhapNoiBo()
        {
            InitializeComponent();
        }
        public frmNhapNoiBo(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }

      
        tb_SYS_USER _user;
        int _right;
        List<_TRANGTHAI> _trangthai;
        CONGTY _congty;
        DONVI _donvi;
        CHUNGTU _chungtu;
        CHUNGTU_CT _chungtuct;
        SYS_SEQUENCE _sequence;
        HANGHOA _hanghoa;
        BindingSource _bdChungTuCT;
        BindingSource _bdChungTu;
        Guid _khoa;
        tb_SYS_SEQUENCE _seq;
        List<tb_CHUNGTU> _listChungTu;
        bool _them = false;
        private tb_SYS_USER user;
        private int right;

        private void frmNhapNoiBo_Load(object sender, EventArgs e)
        {
            _chungtu = new CHUNGTU();
            _chungtuct = new CHUNGTU_CT();
            _hanghoa = new HANGHOA();
            _sequence = new SYS_SEQUENCE();
            _congty = new CONGTY();
            _donvi = new DONVI();
            _bdChungTuCT = new BindingSource();
            _bdChungTu = new BindingSource();

            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.Value = DateTime.Now;

            _bdChungTu.PositionChanged += _bdChungTu_PositionChanged;
            loadCongTy();
            cboCongTy.SelectedValue = MYFUNCTIONS._macty;
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged;

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
            cboDonVi.SelectedIndexChanged += CboDonVi_SelectedIndexChanged;

        }

        private void CboDonVi_SelectedIndexChanged(object sender, EventArgs e)
        {
            _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());//Xem xét 14:30
            _bdChungTu.DataSource = _listChungTu;
            gcDanhSach.DataSource = _bdChungTu;
            xuatThongTin();
        }

        private void CboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDonVi();
        }

        private void _bdChungTu_PositionChanged(object sender, EventArgs e)
        {
            if (!_them)
            {
                xuatThongTin();
            }
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
        void loadDonViNhap()
        {
            cboDonViNhap.DataSource = _donvi.getDonViByCty(cboCongTy.SelectedValue.ToString(), false);
            cboDonViNhap.DisplayMember = "TenDVi";
            cboDonViNhap.ValueMember = "MaDVi";
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

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void btnTaoMa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
                MessageBox.Show("Không có quyền thao tác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                tb_CHUNGTU ctu;
                string madvi = "";
                if (MYFUNCTIONS._madvi == "~")
                    madvi = "KVN01";
                else
                    madvi = cboDonVi.SelectedValue.ToString();
                tb_DONVI dvi = _donvi.getItem(madvi);
                _seq = _sequence.getItem("NNB@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu);
                if (_seq == null)
                {
                    _seq = new tb_SYS_SEQUENCE();
                    _seq.SEQNAME = "NNB@" + DateTime.Today.Year.ToString() + "@" + dvi.KyHieu;
                    _seq.SEQVUALE = 1;
                    _sequence.add(_seq);

                }
                ctu = (tb_CHUNGTU)_bdChungTu.Current;
                ctu = _chungtu.getItem(ctu.ID);
                ctu.SCT2 = _seq.SEQVUALE.Value.ToString("000000") + @"/" + DateTime.Today.Year.ToString().Substring(2, 2) + @"/NNB/" + dvi.KyHieu;
                ctu.Ngay2 = DateTime.Now;
                var resultCtu = _chungtu.update(ctu);


                _listChungTu = null;
                _listChungTu = _chungtu.getList(2, dtTuNgay.Value, dtDenNgay.Value.AddDays(1), cboDonVi.SelectedValue.ToString());

                _bdChungTu.DataSource = _listChungTu;
                gvDanhSach.ClearSorting();
                gvDanhSach.RefreshData();
                var obj = _bdChungTu.List.OfType<tb_CHUNGTU>().ToList().Find(c => c.SCT == resultCtu.SCT);
                _bdChungTu.Position = _bdChungTu.IndexOf(obj);
                xuatThongTin();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void xuatThongTin()
        {
            tb_CHUNGTU current = (tb_CHUNGTU)_bdChungTu.Current;
            if (current != null)
            {
                dtNgay.Value = current.Ngay.Value;
                txtSoPhieu.Text = current.SCT;
                txtGhiChu.Text = current.GhiChu;
                cboDonViXuat.SelectedValue = current.MaDVi;
                cboDonViNhap.SelectedValue = int.Parse(current.MaDVi2);

                cboTrangThai.SelectedValue = current.TrangThai;

                if (current.SCT2 != null)
                {
                    btnTaoMa.Enabled = false;
                }
                else
                {
                    btnTaoMa.Enabled = false;
                }
                _bdChungTuCT.DataSource = _chungtuct.getListByIDFull(current.ID);//Xem xét lại
                gcChiTiet.DataSource = _bdChungTuCT;
                for (int i = 0; i < gvChiTiet.RowCount; i++)
                {
                    gvChiTiet.SetRowCellValue(i, "STT", i + 1);
                }

            }
        }
        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
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

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDanhSach.IsGroupRow(e.RowHandle))
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
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvDanhSach); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1));
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvDanhSach); }));
            }
        }
    }
}
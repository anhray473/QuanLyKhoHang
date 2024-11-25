using BusinessLayer;
using COTS.myPop;
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

namespace COTS
{
    public partial class frmDanhMuc : DevExpress.XtraEditors.XtraForm
    {
        public frmDanhMuc()
        {
            InitializeComponent();
        }
        string _chuoi;
        DevExpress.XtraGrid.Views.Grid.GridView _gvChitiet;
        CHUNGTU_CT _chungtuct;
        HANGHOA _hanghoa;
        public frmDanhMuc(DevExpress.XtraGrid.Views.Grid.GridView gvChiTiet, string chuoi)
        {
            InitializeComponent();
            this._chuoi = chuoi;
            this._gvChitiet = gvChiTiet;
        }
        private void frmDanhMuc_Load(object sender, EventArgs e)
        {
            _chungtuct = new CHUNGTU_CT();
            _hanghoa = new HANGHOA();
            if (_chuoi.Trim().Length == 1)
                gcDanhSach.DataSource = _hanghoa.getList();
            else
                gcDanhSach.DataSource = _hanghoa.getListByKeyword(_chuoi.Substring(1,_chuoi.Length-1).TrimStart().ToString());
            gvDanhSach.OptionsBehavior.Editable = false;

        }
        void Insert()
        {
            int[] _selectRow = gvDanhSach.GetSelectedRows();
            List<string> _selected = new List<string>();
            foreach(int item in _selectRow)
            {
                _selected.Add(gvDanhSach.GetRowCellValue(item, "Code").ToString());
            }
            List<errExport> _err = new List<errExport>();
            List<string> _valid = new List<string>();
            List<string> _exist = new List<string>();
            if (_gvChitiet.RowCount > 1)
            {
                if (_gvChitiet.GetRowCellValue(_gvChitiet.RowCount - 1, "TenHang") != null)
                {
                    for (int i = 0; i < _gvChitiet.RowCount; i++)
                    {
                        _exist.Add(_gvChitiet.GetRowCellValue(i, "Code").ToString()) ;
                    }
                        
                }
                else
                {
                    for(int i = 0; i<_gvChitiet.RowCount-1; i++)
                    {
                        _exist.Add(_gvChitiet.GetRowCellValue(i,"Code").ToString()) ;
                    }
                }
                for(int i = 0; i< _selected.Count; i++)
                {
                    tb_HANGHOA hh = _hanghoa.getItem(_selected[i]);
                    if (_exist.Contains(_selected[i])==true)
                    {
                        errExport err = new errExport();
                        err._barcode = _selected[i];
                        err._soluong = 1;
                        err._errcode = " Mã đã tồn tại trên lưới dữ liệu";
                        _err.Add(err);
                        continue;
                    }
                    else
                    {
                        _valid.Add(_selected[i]);
                        continue;
                    }
                }
                //Thêm những mã hợp lệ
                foreach (string _item in _valid)
                {
                    tb_HANGHOA hh =_hanghoa.getItem(_item);
                    if (_gvChitiet.RowCount > 1)
                    {
                        int mautin = _gvChitiet.RowCount;
                        _gvChitiet.SelectRow(mautin-1);
                        if (_gvChitiet.GetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang") == null)
                        {
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle,"STT", mautin);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "Code", hh.Code);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang", hh.TenHang);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DVT", hh.DVT);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "SoLuong", 1);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DonGia", hh.DonGia);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "ThanhTien", hh.DonGia);

                        }
                        else
                        {
                            _gvChitiet.AddNewRow();
                            _gvChitiet.SelectRow(mautin);
                            mautin++;
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "STT", mautin);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "Code", hh.Code);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang", hh.TenHang);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DVT", hh.DVT);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "SoLuong", 1);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DonGia", hh.DonGia);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "ThanhTien", hh.DonGia);
                        }
                    }
                    else
                    {
                        if (_gvChitiet.RowCount == 0)
                            _gvChitiet.AddNewRow();
                        int mautin = _gvChitiet.RowCount;
                        _gvChitiet.SelectRow(mautin - 1);
                        if (_gvChitiet.GetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang") == null)
                        {
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "STT", mautin);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "Code", hh.Code);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang", hh.TenHang);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DVT", hh.DVT);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "SoLuong", 1);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DonGia", hh.DonGia);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "ThanhTien", hh.DonGia);
                        }
                        else
                        {
                            _gvChitiet.AddNewRow();
                            _gvChitiet.SelectRow(mautin);
                            mautin++;
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "STT", mautin);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "Code", hh.Code);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "TenHang", hh.TenHang);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DVT", hh.DVT);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "SoLuong", 1);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "DonGia", hh.DonGia);
                            _gvChitiet.SetRowCellValue(_gvChitiet.FocusedRowHandle, "ThanhTien", hh.DonGia);
                        }
                    }
                }
                _gvChitiet.AddNewRow();
                _gvChitiet.SelectRow(_gvChitiet.RowCount - 1);
                _gvChitiet.DeleteSelectedRows();
                _gvChitiet.RefreshData();
                if (_err.Count > 0)
                {
                    popLoiImport _errPopup = new popLoiImport(_err);
                    _errPopup.ShowDialog();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Insert();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
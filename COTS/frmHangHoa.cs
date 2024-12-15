using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace COTS
{
    public partial class frmHangHoa : DevExpress.XtraEditors.XtraForm
    {
        public frmHangHoa()
        {
            InitializeComponent();
        }
        public frmHangHoa(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        //SYS_USER _User;
        tb_SYS_USER _user;
        int _right;
        bool _them;
        string _barcode;
        NHACUNGCAP _nhacc;
        DVT _dvt;
        NHOMHH _nhomhh;
        XUATXU _xuatxu;
        HANGHOA _hanghoa;
        SYS_SEQUENCE _sysSeq;
        tb_SYS_SEQUENCE _seq;
        List<obj_HANGHOA> _ListHH = new List<obj_HANGHOA>();

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            _nhacc = new NHACUNGCAP();
            _dvt = new DVT();
            _nhomhh = new NHOMHH();
            _xuatxu = new XUATXU();
            _hanghoa = new HANGHOA();
            _sysSeq = new SYS_SEQUENCE();
            showHideControl(true);
            _enabled(false);
            loadDVT();
            loadNhaCC();
            loadXuatXu();
            loadNhom();
            loadData();
            cboNhom.SelectedIndexChanged += CboNhom_SelectedIndexChanged;
        }

        private void CboNhom_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        void loadNhom()
        {
            cboNhom.DataSource =_nhomhh.getAll();
            cboNhom.DisplayMember = "TenNhom";
            cboNhom.ValueMember = "IDNhom";
        }
        void loadNhaCC()
        {
            cboNCC.DataSource = _nhacc.getAll();
            cboNCC.DisplayMember = "TenNCC";
            cboNCC.ValueMember = "MaNCC";
        }
        void loadXuatXu()
        {
            cboXuatXu.DataSource = _xuatxu.getAll();
            cboXuatXu.DisplayMember = "Ten";
            cboXuatXu.ValueMember = "ID";
        }
        void loadDVT()
        {
            cboDVT.DataSource = _dvt.getAll();
            cboDVT.DisplayMember = "Ten";
            cboDVT.ValueMember = "ID";
        }
        void loadData()
        {
            gcDanhSach.DataSource = _hanghoa.getListByNhom(int.Parse(cboNhom.SelectedValue.ToString()));
            gvDanhSach.OptionsBehavior.Editable = false;
            _ListHH= _hanghoa.getListByNhomFull(int.Parse(cboNhom.SelectedValue.ToString()));
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnExport.Visible = t;
        }
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtTenTat.Enabled = t;
            txtMoTa.Enabled = t;
            cboDVT.Enabled = t;
            spGia.Enabled = t;
            cboNCC.Enabled = t;
            cboXuatXu.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtCode.Text = "";
            txtTen.Text = "";
            txtTenTat.Text = "";
            txtMoTa.Text = "";
            spGia.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _them = false;
            showHideControl(false);
            _enabled(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _hanghoa.delete(_barcode);
                loadData();
            }
            else
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLuu_Click(object sender, EventArgs e)   
        {
            if (_them)
            {
                tb_HANGHOA hh = new tb_HANGHOA();
                _seq = _sysSeq.getItem("HH@" + DateTime.Now.Year.ToString() + "@" + cboNhom.SelectedValue.ToString());
                if (_seq == null)
                {
                    _seq = new tb_SYS_SEQUENCE();
                    _seq.SEQNAME = "HH@" + DateTime.Now.Year.ToString() + "@" + cboNhom.SelectedValue.ToString();
                    _seq.SEQVUALE = 1;
                    _sysSeq.add(_seq);
                }
                hh.Code = BarcodeEAN13.BuildEan13(DateTime.Now.Year.ToString() + cboNhom.SelectedValue.ToString() + _seq.SEQVUALE.Value.ToString("0000000"));
                hh.TenHang = txtTen.Text;
                hh.TenTat = txtTenTat.Text;
                hh.IDNhom = int.Parse(cboNhom.SelectedValue.ToString());
                hh.MoTa = txtMoTa.Text;
                hh.MaNCC = cboNCC.SelectedValue.ToString();
                hh.MaXX = int.Parse(cboXuatXu.SelectedValue.ToString());
                hh.DVT = cboDVT.Text;//int.Parse(cboDVT.SelectedValue.ToString());
                hh.Disabled = chkDisabled.Checked;
                hh.NgayTao = DateTime.Now;
                hh.NguoiTao = 1;
                hh.DonGia = float.Parse(spGia.Text);
                var _hh = _hanghoa.add(hh);
                txtCode.Text = _hh.Code;
                _sysSeq.update(_seq);
            }
            else
            {
                tb_HANGHOA hh = _hanghoa.getItem(_barcode);
                hh.TenHang = txtTen.Text;
                hh.TenTat = txtTenTat.Text;
                hh.IDNhom = int.Parse(cboNhom.SelectedValue.ToString());
                hh.MoTa = txtMoTa.Text;
                hh.MaNCC = cboNCC.SelectedValue.ToString();
                hh.MaXX = int.Parse(cboXuatXu.SelectedValue.ToString());
                hh.DVT = cboDVT.Text;//int.Parse(cboDVT.SelectedValue.ToString());
                hh.Disabled = chkDisabled.Checked;
                hh.DonGia = float.Parse(spGia.Text);           
                var _hh = _hanghoa.update(hh);
                txtCode.Text = _hh.Code;
            }
            _them = false;
            showHideControl(true);
            loadData();
            _enabled(false);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _reset();
            _enabled(false);
            loadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            _barcode = gvDanhSach.GetFocusedRowCellValue("Code").ToString();
            txtCode.Text = gvDanhSach.GetFocusedRowCellValue("Code").ToString();
            txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenHang").ToString();
            txtTenTat.Text = gvDanhSach.GetFocusedRowCellValue("TenTat").ToString();
            cboDVT.Text = gvDanhSach.GetFocusedRowCellValue("DVT").ToString();
            cboXuatXu.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MaXX");
            cboNCC.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MaNCC");
            txtMoTa.Text = gvDanhSach.GetFocusedRowCellValue("MoTa").ToString();
            spGia.Text = gvDanhSach.GetFocusedRowCellValue("DonGia").ToString();
            chkDisabled.Checked =bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            _export();
        }
        void _export()
        {
            string tenFile = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel 2000-2003 (.xls)|*.xls|Excel 2007 or higher (.xlsx)|*.xlsx";
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                SplashScreenManager.ShowForm(this,typeof(frmChoXuLy), true, true, false);
                tenFile = saveFile.FileName;
            }
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet sheet = null;
            try
            {
                sheet = wb.ActiveSheet;
                //Đặt tên sheet
                sheet.Name = "DM " + cboNhom.Text;
                sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 12]].Merge();
                //Canh lề text
                sheet.Cells[1, 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //Boder
                sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 12]].BorderAround(Type.Missing, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic);
                sheet.Cells[1, 1].Value = "DANH MỤC " + cboNhom.Text.ToUpper();
                sheet.Cells[1, 1].Font.Size = 20;
                sheet.Cells[2, 1].Value = "BARCODE";
                sheet.Cells[2, 2].Value = "TÊN HÀNG HÓA";
                sheet.Cells[2, 3].Value = "TÊN TẮT";
                sheet.Cells[2, 4].Value = "ĐVT";
                sheet.Cells[2, 5].Value = "ĐƠN GIÁ";
                sheet.Cells[2, 6].Value = "MÔ TẢ";
                sheet.Cells[2, 7].Value = "MÃ NHÓM";
                sheet.Cells[2, 8].Value = "TÊN NHÓM";
                sheet.Cells[2, 9].Value = "MÃ NCC";
                sheet.Cells[2, 10].Value = "TÊN NCC";
                sheet.Cells[2, 11].Value = "MÃ XUẤT XỨ";
                sheet.Cells[2, 12].Value = "XUẤT XỨ";
                //Xuất dữ liệu
                for(int i =1; i<=_ListHH.Count; i++)
                {
                    sheet.Cells[i + 2, 1].Value = _ListHH.ElementAt(i - 1).Code;
                    sheet.Cells[i + 2, 2].Value = _ListHH.ElementAt(i - 1).TenHang;
                    sheet.Cells[i + 2, 3].Value = _ListHH.ElementAt(i - 1).TenTat;
                    sheet.Cells[i + 2, 4].Value = _ListHH.ElementAt(i - 1).DVT;
                    sheet.Cells[i + 2, 5].Value = _ListHH.ElementAt(i - 1).DonGia;
                    sheet.Cells[i + 2, 6].Value = _ListHH.ElementAt(i - 1).MoTa;
                    sheet.Cells[i + 2, 7].Value = _ListHH.ElementAt(i - 1).IDNhom;
                    sheet.Cells[i + 2, 8].Value = _ListHH.ElementAt(i - 1).TenNhom;
                    sheet.Cells[i + 2, 9].Value = _ListHH.ElementAt(i - 1).MaNCC;
                    sheet.Cells[i + 2, 10].Value = _ListHH.ElementAt(i - 1).TenNCC;
                    sheet.Cells[i + 2, 11].Value = _ListHH.ElementAt(i - 1).MaXX;
                    sheet.Cells[i + 2, 12].Value = _ListHH.ElementAt(i - 1).TenXX;
                }
                //Save vào
                wb.SaveAs(tenFile);
                
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(true);
                MessageBox.Show(ex.ToString(), "Lỗi ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                wb.Close();
                app.Quit();
                releaseObject(wb);
                releaseObject(app);
                SplashScreenManager.CloseForm(true);
                MessageBox.Show("Xuất file thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
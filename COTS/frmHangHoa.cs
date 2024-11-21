using BusinessLayer;
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
    public partial class frmHangHoa : DevExpress.XtraEditors.XtraForm
    {
        public frmHangHoa()
        {
            InitializeComponent();
        }
        SYS_USER _User;
        int _right;
        bool _them;
        string _barcode;
        BusinessLayer.NHACUNGCAP _nhacc;
        BusinessLayer.DVT _dvt;
        BusinessLayer.NHOMHH _nhomhh;
        BusinessLayer.XUATXU _xuatxu;
        BusinessLayer.HANGHOA _hanghoa;
        BusinessLayer.SYS_SEQUENCE _sysSeq;
        DataLayer.SYS_SEQUENCE _seq;

        private void frmHangHoa_Load(object sender, EventArgs e)
        {
            _nhacc = new BusinessLayer.NHACUNGCAP();
            _dvt = new BusinessLayer.DVT();
            _nhomhh = new BusinessLayer.NHOMHH();
            _xuatxu = new BusinessLayer.XUATXU();
            _hanghoa = new BusinessLayer.HANGHOA();
            _sysSeq = new BusinessLayer.SYS_SEQUENCE();
            showHideControl(true);
            _enabled(false);
            loadDVT();
            loadNhaCC();
            loadXuatXu();
            loadNhom();
            loadData();
        }
        void loadNhom()
        {
            cboNhom.DataSource = _nhomhh.getAll();
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
            chkDisabled.Checked = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            //if(_right == 1)
            //{
            //    MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //if(_right == 1)
            //{
            //    MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            _them = true;
            showHideControl(false);
            _enabled(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if(_right == 1)
            //{
            //    MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
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
                DataLayer.HANGHOA hh = new DataLayer.HANGHOA();
                _seq = _sysSeq.getItem("HH@"+DateTime.Now.Year.ToString()+cboNhom.SelectedValue.ToString());
                if(_seq == null)
                {
                    _seq = new DataLayer.SYS_SEQUENCE();
                    _seq.SEQNAME = "HH@" + DateTime.Now.Year.ToString() + cboNhom.SelectedValue.ToString();
                    _seq.SEQVUALE = 1;
                    _sysSeq.add(_seq);
                }
                hh.Code = BarcodeEAN13.BuildEan13(DateTime.Now.Year.ToString() + cboNhom.SelectedValue.ToString()+_seq.SEQVUALE.Value.ToString("0000000"));
                hh.TenHang = txtTen.Text;
                hh.TenTat= txtTenTat.Text;
                hh.IDNhom = int.Parse(cboNhom.SelectedValue.ToString()) ;
                hh.MoTa = txtMoTa.Text;
                hh.MaNCC = int.Parse(cboNCC.SelectedValue.ToString()) ;
                hh.MaXX = int.Parse(cboXuatXu.SelectedValue.ToString()) ;
                hh.DVT = cboDVT.Text;   
                hh.Disabled = chkDisabled.Checked;
                hh.NgayTao = DateTime.Now;
                hh.DonGia = float.Parse(spGia.Text);
                txtCode.Text = hh.Code;
                var _hh= _hanghoa.add(hh);
                _sysSeq.update(_seq);

                MessageBox.Show(hh.Code);
            }
            else
            {
                DataLayer.HANGHOA hh = _hanghoa.getItem(_barcode);
            }
            _them = false;
            showHideControl(true);
            loadData();
            _enabled(true);
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

        }
    }
}
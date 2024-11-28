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

namespace Usermanagement
{
    public partial class frmGroup : DevExpress.XtraEditors.XtraForm
    {
        public frmGroup()
        {
            InitializeComponent();
        }

        public string _macty;
        public string _madvi;
        public int _idUS;
        public string _username;
        public string _fullname;
        public bool _them;
        SYS_USER _sysuser;
        tb_SYS_USER _user;
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];

        private void frmGroup_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            if (!_them)
            {
                var user = _sysuser.getItem(_idUS);
                txtTenNhom.Text = user.Username;
                _macty = user.MaCTy;
                _madvi = user.MaDVi;
                txtMoTa.Text = user.HoVaTen;
                txtTenNhom.ReadOnly = true;
            }
            else
            {
                txtMoTa.Text = "";
                txtTenNhom.Text = "";
            }
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenNhom.Text.Trim()=="")
            {
                MessageBox.Show("Chưa nhập tên nhóm. Tên nhóm nhập không dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhom.SelectAll();
                txtTenNhom.Focus();
                return;
            }
            saveData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void saveData()
        {
            if (_them)
            {
                bool checkUS = _sysuser.checkUserExist(_macty, _madvi, txtTenNhom.Text.Trim());
                if (checkUS)
                {
                    MessageBox.Show("Nhóm đã tồn tại. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenNhom.SelectAll();
                    txtTenNhom.Focus();
                    return;
                }
                _user = new tb_SYS_USER();
                _user.Username = txtTenNhom.Text.Trim();
                _user.HoVaTen = txtMoTa.Text;
                _user.Isgroup = true;
                _user.Disabled = false;
                _user.MaCTy = _macty;
                _user.MaDVi = _madvi;
                _sysuser.add(_user);
                

            }
            else
            {
                _user = _sysuser.getItem(_idUS);
                _user.HoVaTen = txtMoTa.Text;
                _sysuser.update(_user);
            }
            objMain.loadUser(_macty, _madvi);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmShowMembers frm = new frmShowMembers();
            frm._idGroup = _idUS;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();

        }
    }
}
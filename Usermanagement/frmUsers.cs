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
    public partial class frmUsers : DevExpress.XtraEditors.XtraForm
    {
        public frmUsers()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idUS;
        public string _username;
        public string _fullname;
        public bool _them;
        SYS_GROUP _sysgroup;
        SYS_USER _sysuser;
        tb_SYS_USER _user;
        VIEW_USER_IN_GROUP _vUserInGroup;
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        private void frmUsers_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            _sysgroup = new SYS_GROUP();
            if (!_them)
            {
                var user = _sysuser.getItem(_idUS);
                txtUsername.Text = user.Username;
                _macty = user.MaCTy;
                _madvi = user.MaDVi;
                txtHoTen.Text = user.HoVaTen;
                txtPass.Text = user.Password; //Encryptor.Encrypt(user.Password, "qwert@123!poiuy", true);
                txtRepass.Text = user.Password; //Encryptor.Encrypt(user.Password, "qwert@123!poiuy", true);
                chkDisabled.Checked = user.Disabled.Value;
                txtUsername.ReadOnly = true;
                loadGroupByUser(_idUS);
            }
            else
            {
                //txtUsername.Text = "";
                txtHoTen.Text = "";
                txtPass.Text = "";
                txtRepass.Text = "";
                
                chkDisabled.Checked = false;
            }
        }
        public void loadGroupByUser(int idUser)
        {
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            gcThanhVien.DataSource = _vUserInGroup.getGroupByUser(_macty, _madvi, idUser);
            gvThanhVien.OptionsBehavior.Editable = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên người dùng. Tên người dùng nhập không dấu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;
            }
            if (txtPass.Text != txtRepass.Text)
            {
                MessageBox.Show("Mật khẩu không trùng khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;
            }
            saveData();
            this.Close();
            MessageBox.Show("Tạo tài khoản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void saveData()
        {
            if (_them)
            {
                bool checkUS = _sysuser.checkUserExist(_macty, _madvi, txtUsername.Text.Trim());
                if (checkUS)
                {
                    MessageBox.Show("Tên người dùng đã tồn tại. Vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUsername.SelectAll();
                    txtUsername.Focus();
                    return;
                }
                _user = new tb_SYS_USER();
                _user.Username = txtUsername.Text.Trim();
                _user.HoVaTen = txtHoTen.Text;
                _user.Password = txtPass.Text;//Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.Isgroup = false;
                _user.Disabled = false;
                _user.MaCTy = _macty;
                _user.MaDVi = _madvi;
                _sysuser.add(_user);


            }
            else
            {
                _user = _sysuser.getItem(_idUS);
                _user.HoVaTen = txtHoTen.Text;
                _user.Password = txtPass.Text; //Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.Isgroup = false;
                _user.Disabled = chkDisabled.Checked;
                _user.MaCTy = _macty;
                _user.MaDVi = _madvi;
                _sysuser.update(_user);
            }
            objMain.loadUser(_macty, _madvi);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmShowGroups frm = new frmShowGroups();
            frm._idUser = _idUS;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnLoai_Click(object sender, EventArgs e)
        {
            _sysgroup.delGr(_idUS, int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUser").ToString()));
            loadGroupByUser(_idUS);
        }
    }
}
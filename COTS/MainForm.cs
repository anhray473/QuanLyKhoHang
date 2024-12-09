using BusinessLayer;
using DataLayer;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace COTS
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(tb_SYS_USER user)
        {
            InitializeComponent();
            this._user = user;
        }
        BusinessLayer.SYS_FUNC _func;
        tb_SYS_USER _user;
        SYS_GROUP _sysgroup;
        SYS_RIGHT _sysright;
        private void MainForm_Load(object sender, EventArgs e)
        {
            _func = new BusinessLayer.SYS_FUNC();
            _sysgroup = new SYS_GROUP();
            _sysright = new SYS_RIGHT();
            leftMenu();
        }

        void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach (var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.MoTa);
                navGroup.Tag = _pr.FUNC_Code;
                navGroup.Name = _pr.FUNC_Code;
                navGroup.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(navGroup);

                var _lsChild = _func.getChild(_pr.FUNC_Code);
                foreach (var _ch in _lsChild)
                {
                    NavBarItem navItem = new NavBarItem(_ch.MoTa);
                    navItem.Tag = _ch.FUNC_Code;
                    navItem.Name = _ch.FUNC_Code;
                    navItem.ImageOptions.SmallImageIndex = 0;
                    navGroup.ItemLinks.Add(navItem);
                }
                navMain.Groups[navGroup.Name].Expanded = true;
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();

            var _group = _sysgroup.getGroupByMember(_user.IDUser);
            var _uRight = _sysright.GetRIGHT(_user.IDUser, func_code);

            if (_group != null)
            {
                var _groupRight = _sysright.GetRIGHT(_group.Group, func_code);
                if (_uRight.User_RIGHT < _groupRight.User_RIGHT)
                    _uRight.User_RIGHT = _groupRight.User_RIGHT;
            }
            if (_uRight.User_RIGHT == 0)
            {
                MessageBox.Show("Không có quyền thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                switch (func_code)
                {
                    case "CONGTY":
                        {
                            //frmCongTy = frmConnect = new frmCongTy(_user, _uRight.User_RIGHT.Value);
                            //frmConnect.ShowDialog();
                            //break;
                            frmCongTy frm = new frmCongTy(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "DONVI":
                        {
                            frmDonVi frm = new frmDonVi(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "NHACUNGCAP":
                        {
                            frmNhaCungCap frm = new frmNhaCungCap(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "NHOMHH":
                        {
                            frmNhomHH frm = new frmNhomHH(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "DVT":
                        {
                            frmDVT frm = new frmDVT(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "XUATXU":
                        {
                            frmXuatXu frm = new frmXuatXu(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "HANGHOA":
                        {
                            frmHangHoa frm = new frmHangHoa(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "NHAPMUA":
                        {
                            frmNhapMua frm = new frmNhapMua(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "XUATNB":
                        {
                            frmXuatNB frm = new frmXuatNB(_user,_uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "NHAPNB":
                        {
                            frmNhapNoiBo frm = new frmNhapNoiBo(_user, _uRight.User_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }

                }

            }
        }

        private void btnInBarcode_Click(object sender, EventArgs e)
        {
            frmInBarcode frm = new frmInBarcode();
            frm.ShowDialog(this);
        }
    }
}

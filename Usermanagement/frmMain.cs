using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Usermanagement.MyComponent;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace Usermanagement
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        TreeViewCombo _treeView;
        CONGTY _congty;
        DONVI _donvi;
        string _macty;
        string _madvi;
        bool _isRoot;
        SYS_USER _sysuser;

        public void loadUser(string macty, string madvi)
        {
            _sysuser = new SYS_USER();
            gcUser.DataSource = _sysuser./*getAll()*/ getUserByDVi(macty, madvi);
            gvUser.OptionsBehavior.Editable = false;
        }
        void loadTreeView()
        {
            _treeView = new TreeViewCombo(pnNhom.Width, 300);
            _treeView.Font = new Font("Tahoma", 10, FontStyle.Regular);
            var lstCTY = _congty.getAll();
            foreach ( var item in lstCTY ) 
            {
                TreeNode ParentNode = new TreeNode();
                ParentNode.Text = item.MaCTy+" - " + item.TenCTy;
                ParentNode.Tag = item.MaCTy;
                ParentNode.Name = item.MaCTy;
                _treeView.TreeView.Nodes.Add(ParentNode);
                foreach(var dv in _donvi.getAll(item.MaCTy)) 
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = dv.MaDVi+" - "+ dv.TenDVi;
                    childNode.Tag = dv.MaCTy+ /*"."+*/ dv.MaDVi;
                    childNode.Name = dv.MaCTy + /*"." +*/ dv.MaDVi;
                    _treeView.TreeView.Nodes[ParentNode.Name].Nodes.Add(childNode);

                }
            }
            _treeView.TreeView.ExpandAll();
            pnNhom.Controls.Add( _treeView );
            _treeView.Width = pnNhom.Width;
            _treeView.Height = pnNhom.Height;
            _treeView.TreeView.AfterSelect += TreeView_AfterSelect;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _treeView.Text = _treeView.TreeView.SelectedNode.Text;
            if (_treeView.TreeView.SelectedNode.Parent==null)
            {
                _isRoot = true;
                _macty = _treeView.TreeView.SelectedNode.Tag.ToString();
                _madvi = "~";
            }
            else
            {
                _isRoot= false;
                _macty = _treeView.TreeView.SelectedNode.Name.Substring(0,4);
                _madvi = _treeView.TreeView.SelectedNode.Name.Substring(5,9);
            }
            loadUser(_macty, _madvi);
            _treeView.dropDown.Close();
            
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _sysuser = new SYS_USER();
            _isRoot = true ;
            loadTreeView();
            loadUser(_macty, _madvi);
        }

        private void btnNguoiDung_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmGroup frm = new frmGroup();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmUsers frm = new frmUsers();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm._idUS = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
            
            frm.ShowDialog();
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("Isgroup").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUS = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUsers frm = new frmUsers();
                frm._them = false;
                frm._idUS = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
                frm.ShowDialog();
            }
        }

        private void btnChucNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gvUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("Isgroup").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUS = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUsers frm = new frmUsers();
                frm._them = false;
                frm._idUS = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
                frm.ShowDialog();
            }
        }
    }
}

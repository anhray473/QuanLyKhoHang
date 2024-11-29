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
    public partial class frmShowGroups : DevExpress.XtraEditors.XtraForm
    {
        public frmShowGroups()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idUser;
        SYS_GROUP _sysGr;
        VIEW_USER_IN_GROUP _vUserInGroup;

        frmUsers objUser = (frmUsers)Application.OpenForms["frmUsers"];

        private void frmShowGroups_Load(object sender, EventArgs e)
        {
            _sysGr = new SYS_GROUP();
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            loadGroup();
        }
        void loadGroup()
        {
            gcNhom.DataSource = _vUserInGroup.getGroupByDonVi(_macty, _madvi);
            gvNhom.OptionsBehavior.Editable = false;
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_vUserInGroup.checkGroupByUser(_idUser, int.Parse(gvNhom.GetFocusedRowCellValue("IDUser").ToString())))
            {
                MessageBox.Show("Người dùng đã tồn tại trong nhóm. Vui lòng chọn nhóm khác.","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tb_SYS_GROUP gr = new tb_SYS_GROUP();
            gr.Group = int.Parse(gvNhom.GetFocusedRowCellValue("IDUser").ToString());
            gr.ThanhVien = _idUser;
            _sysGr.add(gr);
            objUser.loadGroupByUser(_idUser);
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
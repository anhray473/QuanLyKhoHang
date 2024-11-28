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
    public partial class frmShowMembers : DevExpress.XtraEditors.XtraForm
    {
        public frmShowMembers()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idGroup;
        SYS_GROUP _sysGr;
        VIEW_USER_NOTIN_GROUP _vNotGroup;

        frmGroup objGroup = (frmGroup)Application.OpenForms["frmGroup"];
        private void frmShowMembers_Load(object sender, EventArgs e)
        {
            _sysGr = new SYS_GROUP();
            _vNotGroup = new VIEW_USER_NOTIN_GROUP();
            loadUserNotInGroup();
        }
        void loadUserNotInGroup()
        {
            gcThanhVien.DataSource = _vNotGroup.getUserNotInGroup( _madvi ,_macty);
            gvThanhVien.OptionsBehavior.Editable = false;
           
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            tb_SYS_GROUP gr = new tb_SYS_GROUP();
            gr.Group = _idGroup;
            gr.ThanhVien = int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUser").ToString());
            _sysGr.add(gr);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
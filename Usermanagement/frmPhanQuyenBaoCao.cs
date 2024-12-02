using BusinessLayer;
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
    public partial class frmPhanQuyenBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenBaoCao()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;
        SYS_USER _sysuser;
        SYS_RIGHT_REP _sysrightrep;
        private void frmPhanQuyenBaoCao_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            _sysrightrep = new SYS_RIGHT_REP();
            loadUser();
            loadRepByUser();
        }
        void loadUser()
        {
            if (_madvi == null && _madvi == null)
            {
                gcUser.DataSource = _sysuser.getUserByDViFunc("CT01", "~");
                gvUser.OptionsBehavior.Editable = false;

            }
            else
            {
                gcUser.DataSource = _sysuser.getUserByDVi(_macty, _madvi);
                gvUser.OptionsBehavior.Editable = false;
            }
        }
        void loadRepByUser()
        {
            VIEW_SYS_RIGHT_REP _vFuncRight = new VIEW_SYS_RIGHT_REP();
            gcChucNang.DataSource = _vFuncRight.getRepByUser(_idUser);
            gvChucNang.OptionsBehavior.Editable = false;
            for (int i = 0; i < gvUser.RowCount; i++)
            {
                if (int.Parse(gvUser.GetRowCellValue(i, "IDUser").ToString()) == _idUser)
                {
                    gvUser.ClearSelection();
                    gvUser.FocusedRowHandle = i;
                }
            }
            gvChucNang.ClearSelection();
        }

        private void mnCamQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysrightrep.update(_idUser, int.Parse(gvChucNang.GetRowCellValue(i, "RepCode").ToString()), false);
                }
            }
            loadRepByUser();
        }

        private void mnToanQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysrightrep.update(_idUser, int.Parse(gvChucNang.GetRowCellValue(i, "RepCode").ToString()), true);
                }
            }
            loadRepByUser();
        }

        private void gvUser_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
            loadRepByUser();
        }
    }
}
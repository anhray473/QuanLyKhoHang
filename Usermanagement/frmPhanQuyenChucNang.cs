using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class frmPhanQuyenChucNang : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenChucNang()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;
        SYS_USER _sysuser;
   //     tb_SYS_RIGHT 
        SYS_RIGHT _sysright;
        private void frmPhanQuyenChucNang_Load(object sender, EventArgs e)
        {
            _sysright = new SYS_RIGHT();
            _sysuser = new SYS_USER();
            loadUser();
            loadFunByUser();
            gvChucNang.RowStyle += GvChucNang_RowStyle;
        }

        private void GvChucNang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view  = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool isRed = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["Isgroup"]));
                if (isRed)
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font  = new Font("Tahoma",12,FontStyle.Bold); 
                }
            }
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
        void loadFunByUser()
        {
            VIEW_FUNC_SYS_RIGHT _vFuncRight = new VIEW_FUNC_SYS_RIGHT();
            gcChucNang.DataSource = _vFuncRight.getFuncByUser(_idUser);
            gvChucNang.OptionsBehavior.Editable=false;
            for(int i=0;i<gvUser.RowCount;i++)
            {
                if(int.Parse(gvUser.GetRowCellValue(i,"IDUser").ToString()) == _idUser)
                {
                    gvUser.ClearSelection();
                    gvUser.FocusedRowHandle = i;
                }
            }
            gvChucNang.ClearSelection();
        }

        private void mnCamQuyen_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysright.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_Code").ToString(), 0);
                }
            }
            loadFunByUser();
        }

        private void mnChiXem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysright.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_Code").ToString(), 1);
                }
            }
            loadFunByUser();
        }

        private void mnToanQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysright.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_Code").ToString(), 2);
                }
            }
            loadFunByUser();
        }

        private void gvUser_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUser").ToString());
            loadFunByUser();
        }
    }
}
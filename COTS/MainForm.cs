using BusinessLayer;
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
        SYS_FUNC _func;

        private void MainForm_Load(object sender, EventArgs e)
        { 
            _func = new SYS_FUNC();
            leftMenu();
        }

        void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach(var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.MoTa);
                navGroup.Tag = _pr.FUNC_Code;
                navGroup.Name = _pr.FUNC_Code;
                navGroup.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(navGroup);

                var _lsChild = _func.getChild(_pr.FUNC_Code);
                foreach(var _ch in _lsChild)
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
        private void btnHeThong_Click(object sender, EventArgs e)
        {

        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

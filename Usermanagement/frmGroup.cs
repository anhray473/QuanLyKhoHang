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
    public partial class frmGroup : DevExpress.XtraEditors.XtraForm
    {
        public frmGroup()
        {
            InitializeComponent();
        }

        public string _macty;
        public string _madvi;
        SYS_USER _sysuser;

        private void frmGroup_Load(object sender, EventArgs e)
        {

        }
    }
}
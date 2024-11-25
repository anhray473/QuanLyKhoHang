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

namespace COTS.myPop
{
    public partial class popLoiImport : DevExpress.XtraEditors.XtraForm
    {
        public popLoiImport()
        {
            InitializeComponent();
        }
        List<errExport> _err;
        public popLoiImport(List<errExport> err) 
        {
            InitializeComponent();
            this._err = err;
        }

        private void popLoiImport_Load(object sender, EventArgs e)
        {
            gcErrors.DataSource = _err;
            gvErrors.OptionsBehavior.Editable = false;

        }
    }
}
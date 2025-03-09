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
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Tài khoản không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (username == "admin" && password == "123")
            {
                frmMain frmM = new frmMain();
                this.Hide();
                frmM.ShowDialog();
            }
            else if (username != "admin")
            {
                MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Mật khẩu không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
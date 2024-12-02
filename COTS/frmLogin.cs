using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COTS
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SYS_USER _sysUser;
        SYS_PARAM _sysParam;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //_sysUser = new SYS_USER();
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream fs = File.Open("sysparam.ini", FileMode.Open, FileAccess.Read);
            //_sysParam = (SYS_PARAM)bf.Deserialize(fs);
            //fs.Close();
            //MYFUNCTIONS._macty = _sysParam.macty;
            //MYFUNCTIONS._madvi = _sysParam.madvi;
            _sysUser = new SYS_USER();

            if (!File.Exists("sysparam.ini"))
            {
                MessageBox.Show("The file sysparam.ini does not exist.");
                return;
            }

            FileInfo fileInfo = new FileInfo("sysparam.ini");
            if (fileInfo.Length == 0)
            {
                MessageBox.Show("The file sysparam.ini is empty.");
                return;
            }

            try
            {
                using (FileStream fs = File.Open("sysparam.ini", FileMode.Open, FileAccess.Read))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _sysParam = (SYS_PARAM)bf.Deserialize(fs);
                }

                MYFUNCTIONS._macty = _sysParam.macty;
                MYFUNCTIONS._madvi = _sysParam.madvi;
            }
            catch (SerializationException ex)
            {
                MessageBox.Show($"Deserialization error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool us = _sysUser.checkUserExist(_sysParam.macty, _sysParam.madvi, txtUsername.Text.Trim());
            if(!us)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            string pass = txtPassword.Text;//Encryptor.Encrypt(txtPassword.Text, "qwert@123!poiuy", true);
            tb_SYS_USER user = _sysUser.getItem(txtUsername.Text.Trim(), _sysParam.macty, _sysParam.madvi);
            if (user.Password.Equals(pass))
            {
                MainForm frm =  new MainForm(user);
                frm.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Mật khẩu đăng nhập không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
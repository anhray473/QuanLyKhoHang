﻿using DataLayer;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace COTS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
       
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (File.Exists("connectdb.dba"))
            {
                string conStr = "";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open("connectdb.dba", FileMode.Open, FileAccess.Read); 
                connect cp = (connect)bf.Deserialize(fs);
                string servername = Encryptor.Encrypt(cp.servername, "qwertyuiop", true);
                string username = Encryptor.Encrypt(cp.username, "qwertyuiop", true);
                string password = Encryptor.Encrypt(cp.password, "qwertyuiop", true);
                string database = Encryptor.Encrypt(cp.database, "qwertyuiop", true);
                conStr = "Data Source = " + servername + "; Initial Catalog = " + database + "; User ID = " + username + "; Password = " + password + "; ";
                
                
                connoi = conStr;
                //myFunction.servername = servername;
                //myFunction.username = username;
                //myFunction.password = password;
                //myFunction.database = database;
                SqlConnection con = new SqlConnection(conStr);

                try
                {
                    con.Open();
                    Application.Run(new MainForm());
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối CSDL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fs.Close();
                }
                con.Close();
                fs.Close();


            }
            else
            {
                Application.Run(new frmConnect());
            }    
        }
        public static string connoi = "";
    }
}

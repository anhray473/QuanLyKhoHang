using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace COTS.Report
{
    public partial class rptInBarcode : DevExpress.XtraReports.UI.XtraReport
    {
        public rptInBarcode()
        {
            InitializeComponent();
            xrTen.DataBindings.Add("Text", this.DataSource, "TenTat");
            xrCode.DataBindings.Add("Text", this.DataSource, "Code");
            xrGia.DataBindings.Add("Text", this.DataSource, "DonGia");
        }

        private void xrGia_BeforePrint(object sender, CancelEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            string fileName = label.DataBindings[0].DataMember;
            double value = Convert.ToDouble(GetCurrentColumnValue(fileName));
            if(value == 0)
            {
                label.Text = "0 đ";
            }
            else
            {
                label.Text = string.Format("Giá: {0:N0} đ",value);
            }
        }
    }
}

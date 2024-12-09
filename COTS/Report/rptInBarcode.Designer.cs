namespace COTS.Report
{
    partial class rptInBarcode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.EAN13Generator eaN13Generator1 = new DevExpress.XtraPrinting.BarCode.EAN13Generator();
            DevExpress.XtraReports.UI.XRWatermark xrWatermark1 = new DevExpress.XtraReports.UI.XRWatermark();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrGia = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTen = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCode = new DevExpress.XtraReports.UI.XRBarCode();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrGia,
            this.xrTen,
            this.xrCode});
            this.Detail.HeightF = 129.3333F;
            this.Detail.MultiColumn.ColumnWidth = 150F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            // 
            // xrGia
            // 
            this.xrGia.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrGia.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100.5F);
            this.xrGia.Multiline = true;
            this.xrGia.Name = "xrGia";
            this.xrGia.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrGia.SizeF = new System.Drawing.SizeF(150F, 23F);
            this.xrGia.StylePriority.UseFont = false;
            this.xrGia.StylePriority.UseTextAlignment = false;
            this.xrGia.Text = "xrGia";
            this.xrGia.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrGia.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.xrGia_BeforePrint);
            // 
            // xrTen
            // 
            this.xrTen.Font = new DevExpress.Drawing.DXFont("Arial", 8F);
            this.xrTen.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTen.Multiline = true;
            this.xrTen.Name = "xrTen";
            this.xrTen.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrTen.SizeF = new System.Drawing.SizeF(150F, 25.50001F);
            this.xrTen.StylePriority.UseFont = false;
            this.xrTen.StylePriority.UseTextAlignment = false;
            this.xrTen.Text = "xrTen";
            this.xrTen.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrCode
            // 
            this.xrCode.AutoModule = true;
            this.xrCode.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.50001F);
            this.xrCode.Name = "xrCode";
            this.xrCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 100F);
            this.xrCode.SizeF = new System.Drawing.SizeF(150F, 74.99999F);
            this.xrCode.Symbology = eaN13Generator1;
            // 
            // rptInBarcode
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Version = "24.1";
            xrWatermark1.Id = "Watermark1";
            this.Watermarks.AddRange(new DevExpress.XtraPrinting.Drawing.Watermark[] {
            xrWatermark1});
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel xrGia;
        private DevExpress.XtraReports.UI.XRLabel xrTen;
        private DevExpress.XtraReports.UI.XRBarCode xrCode;
    }
}

namespace COTS.myPop
{
    partial class popLoiImport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcErrors = new DevExpress.XtraGrid.GridControl();
            this.gvErrors = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gcErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // gcErrors
            // 
            this.gcErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcErrors.Location = new System.Drawing.Point(0, 0);
            this.gcErrors.MainView = this.gvErrors;
            this.gcErrors.Name = "gcErrors";
            this.gcErrors.Size = new System.Drawing.Size(418, 235);
            this.gcErrors.TabIndex = 0;
            this.gcErrors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvErrors});
            // 
            // gvErrors
            // 
            this.gvErrors.GridControl = this.gcErrors;
            this.gvErrors.Name = "gvErrors";
            // 
            // popLoiImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 235);
            this.Controls.Add(this.gcErrors);
            this.Name = "popLoiImport";
            this.Text = "popLoiImport";
            this.Load += new System.EventHandler(this.popLoiImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvErrors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcErrors;
        private DevExpress.XtraGrid.Views.Grid.GridView gvErrors;
    }
}
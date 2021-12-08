
namespace CoffeeShopManager.GUI
{
    partial class FBill
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBill));
            this.USP_GetBillForReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.data_DataSet = new CoffeeShopManager.Data_DataSet();
            this.reportBill = new Microsoft.Reporting.WinForms.ReportViewer();
            this.uSP_GetBillForReportTableAdapter = new CoffeeShopManager.Data_DataSetTableAdapters.USP_GetBillForReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetBillForReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // USP_GetBillForReportBindingSource
            // 
            this.USP_GetBillForReportBindingSource.DataMember = "USP_GetBillForReport";
            this.USP_GetBillForReportBindingSource.DataSource = this.data_DataSet;
            // 
            // data_DataSet
            // 
            this.data_DataSet.DataSetName = "Data_DataSet";
            this.data_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportBill
            // 
            this.reportBill.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetBill";
            reportDataSource1.Value = this.USP_GetBillForReportBindingSource;
            this.reportBill.LocalReport.DataSources.Add(reportDataSource1);
            this.reportBill.LocalReport.ReportEmbeddedResource = "CoffeeShopManager.ReportBill.rdlc";
            this.reportBill.Location = new System.Drawing.Point(0, 0);
            this.reportBill.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportBill.Name = "reportBill";
            this.reportBill.ServerReport.BearerToken = null;
            this.reportBill.Size = new System.Drawing.Size(600, 366);
            this.reportBill.TabIndex = 0;
            // 
            // uSP_GetBillForReportTableAdapter
            // 
            this.uSP_GetBillForReportTableAdapter.ClearBeforeFill = true;
            // 
            // FBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.reportBill);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xuất hóa đơn";
            this.Load += new System.EventHandler(this.FBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetBillForReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_DataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportBill;
        private Data_DataSet data_DataSet;
        private Data_DataSetTableAdapters.USP_GetBillForReportTableAdapter uSP_GetBillForReportTableAdapter;
        private System.Windows.Forms.BindingSource USP_GetBillForReportBindingSource;
    }
}
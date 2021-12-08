
namespace CoffeeShopManager.GUI
{
    partial class FListBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FListBill));
            this.USP_GetListBillForReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.data_DataSet = new CoffeeShopManager.Data_DataSet();
            this.reportListBil = new Microsoft.Reporting.WinForms.ReportViewer();
            this.uSP_GetListBillForReportTableAdapter = new CoffeeShopManager.Data_DataSetTableAdapters.USP_GetListBillForReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetListBillForReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // USP_GetListBillForReportBindingSource
            // 
            this.USP_GetListBillForReportBindingSource.DataMember = "USP_GetListBillForReport";
            this.USP_GetListBillForReportBindingSource.DataSource = this.data_DataSet;
            // 
            // data_DataSet
            // 
            this.data_DataSet.DataSetName = "Data_DataSet";
            this.data_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportListBil
            // 
            this.reportListBil.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetListBill";
            reportDataSource1.Value = this.USP_GetListBillForReportBindingSource;
            this.reportListBil.LocalReport.DataSources.Add(reportDataSource1);
            this.reportListBil.LocalReport.ReportEmbeddedResource = "CoffeeShopManager.ReportListBill.rdlc";
            this.reportListBil.Location = new System.Drawing.Point(0, 0);
            this.reportListBil.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.reportListBil.Name = "reportListBil";
            this.reportListBil.ServerReport.BearerToken = null;
            this.reportListBil.Size = new System.Drawing.Size(728, 426);
            this.reportListBil.TabIndex = 0;
            // 
            // uSP_GetListBillForReportTableAdapter
            // 
            this.uSP_GetListBillForReportTableAdapter.ClearBeforeFill = true;
            // 
            // FListBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 426);
            this.Controls.Add(this.reportListBil);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FListBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo doanh thu";
            this.Load += new System.EventHandler(this.ReportListBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetListBillForReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.data_DataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportListBil;
        private Data_DataSet data_DataSet;
        private Data_DataSetTableAdapters.USP_GetListBillForReportTableAdapter uSP_GetListBillForReportTableAdapter;
        private System.Windows.Forms.BindingSource USP_GetListBillForReportBindingSource;
    }
}
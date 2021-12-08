using CoffeeShopManager.DTO;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager.GUI
{
    public partial class FBill : Form
    {
        private Table checkOutTable;
        private List<ReportParameter> parameters;
        public Table CheckOutTable { get => checkOutTable; set => checkOutTable = value; }
        public List<ReportParameter> Parameters { get => parameters; set => parameters = value; }

        public FBill(Table table, float totalPrice, int discount, float finalTotalPrice)
        {
            InitializeComponent();

            CheckOutTable = table;
            Parameters = new List<ReportParameter>();
            Parameters.Add(new ReportParameter("TotalPrice", totalPrice.ToString()));
            Parameters.Add(new ReportParameter("Discount", discount.ToString()));
            Parameters.Add(new ReportParameter("FinalTotalPrice", finalTotalPrice.ToString()));
        }

        private void FBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'data_DataSet.USP_GetBillForReport' table. You can move, or remove it, as needed.
            this.uSP_GetBillForReportTableAdapter.Fill(this.data_DataSet.USP_GetBillForReport, CheckOutTable.ID);
            this.reportBill.LocalReport.SetParameters(Parameters);
            this.reportBill.RefreshReport();
        }
    }
}

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
    public partial class FListBill : Form
    {
        private DateTime fromDate;
        private DateTime toDate;

        public DateTime FromDate { get => fromDate; set => fromDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }

        public FListBill(DateTime from, DateTime to)
        {
            InitializeComponent();
            FromDate = from;
            ToDate = to;
        }

       

        private void ReportListBill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'data_DataSet.USP_GetListBillForReport' table. You can move, or remove it, as needed.
            this.uSP_GetListBillForReportTableAdapter.Fill(this.data_DataSet.USP_GetListBillForReport, FromDate, ToDate);

            this.reportListBil.RefreshReport();
        }
    }
}

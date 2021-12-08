using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private BillDAO() { }

        public int GetUnpaidBillIDByTableID(int tableID)
        {
            string query = "SELECT * FROM dbo.Bill WHERE TableID = @tableID AND Status = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableID });

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }

            return -1;
        }

        //public bool InsertBill(int tableID)
        //{
        //    string query = "INSERT INTO Bill (DateCheckIn, DateCheckOut, TableID, Discount, Status) " +
        //        "VALUES (GETDATE(), null, @tableID, 0, 0)";

        //    return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID }) > 0;
        //}

        public int GetMaxBillID()
        {
            string query = "SELECT MAX(ID) FROM Bill";
            return (int)DataProvider.Instance.ExecuteScalar(query);
        }

        public bool DeleteUnpaidBill(int tableID)
        {
            string query = "EXEC USP_DeleteUnpaidBill @tableID";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID }) > 0;
        }

        public bool CheckOut(int billID, int discount, float totalPrice)
        {
            string query = "UPDATE Bill SET DateCheckOut = GETDATE(), Discount = @discount, TotalPrice = @totalPrice, Status = 1 WHERE ID = @billID";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { discount, totalPrice, billID }) > 0;
        }

        //public DataTable GetListBillByDate(DateTime checkIn, DateTime checkOut)
        //{
        //    string query = "USP_GetListBillByDate @checkIn, @checkOut";

        //    return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        //}

        // Phân trang hóa đơn
        public DataTable GetListBillByDateAndPage(DateTime checkIn, DateTime checkOut, int linesInAPage, int pageNumber)
        {
            string query = "USP_GetListBillByDateAndPage @checkIn, @checkOut, @linesInAPage, @pageNumber";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut, linesInAPage, pageNumber });
        }

        // Đếm tổng số bản ghi
        public int CountBillRecordByDate(DateTime checkIn, DateTime checkOut)
        {
            string query = "USP_CountBillRecordByDate @checkIn, @checkOut";

            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { checkIn, checkOut });
        }
    }
}

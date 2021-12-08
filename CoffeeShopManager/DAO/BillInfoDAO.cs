using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BillInfoDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo(int billID)
        {
            List<BillInfo> list = new List<BillInfo>();

            string query = "SELECT * FROM dbo.BillInfo WHERE BillID = @billID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { billID });

            foreach (DataRow row in data.Rows)
            {
                BillInfo billInfo = new BillInfo(row);
                list.Add(billInfo);
            }

            return list;
        }

        public bool InsertBillInfo(int tableID, int foodID, int amount)
        {
            string query = "EXEC USP_InsertBillInfo @tableID, @foodID, @amount";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID, foodID, amount }) > 0;
        }

        public bool DeleteBillInfo(int tableID, int foodID, int amount)
        {
            string query = "EXEC USP_DeleteBillInfo @billID, @foodID, @amount";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID, foodID, amount }) > 0;
        }

        public int CountBillInfoByFoodID(int foodID)
        {
            string query = "SELECT COUNT(*) FROM dbo.BillInfo WHERE FoodID = @foodID";

            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { foodID });
        }

    }
}

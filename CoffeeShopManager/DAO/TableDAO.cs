using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;
        public static int TableWidth = 100;
        public static int TableHeight = 100;

        public static TableDAO Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return instance;
            }
            set => instance = value; 
        }

        private TableDAO() { }

        public List<Table> GetTableList()
        {
            string query = "SELECT T.ID, T.Name, S.Status FROM TableFood as T " +
                "INNER JOIN TableStatus AS S ON T.StatusID = S.ID " +
                "WHERE S.Status != N'Đã xóa'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Table> tables = new List<Table>();
            foreach (DataRow row in data.Rows)
            {
                tables.Add(new Table(row));
            }

            return tables;
        }

        public bool SwitchTable(int tableID1, int tableID2)
        {
            string query = "USP_SwitchTable @tableID1, @tableID2";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID1, tableID2 }) > 0;
        }

        public bool MergeTable(int tableID1, int tableID2)
        {
            string query = "USP_MergeTable @tableID1, @tableID2";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { tableID1, tableID2 }) > 0;
        }

        public bool InsertTable(string name)
        {
            string query = "INSERT INTO TableFood VALUES( @name, dbo.UF_GetTableStatusID(N'Trống') )";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name }) > 0;
        }

        public bool UpdateTable(int id, string name, string status)
        {
            string query = "UPDATE TableFood SET Name = @name, StatusID = dbo.UF_GetTableStatusID( @status ) WHERE ID = @id";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, status, id }) > 0;
        }

        // Thực chất không xóa bàn mà chỉ đổi trạng thái là "Đã xóa"
        public bool DeleteTable(int id)
        {
            string query = "UPDATE TableFood SET StatusID = dbo.UF_GetTableStatusID( N'Đã xóa' ) WHERE ID = @id";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
        }
    }
}

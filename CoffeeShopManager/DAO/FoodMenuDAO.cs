using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class FoodMenuDAO
    {
        private static FoodMenuDAO instance;

        public static FoodMenuDAO Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodMenuDAO();
                }
                return instance;
            } 
            private set => instance = value; 
        }

        private FoodMenuDAO() { }

        public List<FoodMenu> GetListFoodMenuByTableID(int tableID)
        {
            List<FoodMenu> listFoodMenu = new List<FoodMenu>();

            string query = "SELECT f.Name AS FoodName, bi.Amount, f.Price, f.Price * bi.Amount AS TotalPrice " +
                "FROM BillInfo AS bi " +
                "INNER JOIN Bill AS b ON bi.BillID = b.ID " +
                "INNER JOIN Food AS f ON bi.FoodID = f.ID " +
                "WHERE b.TableID = @tableID AND b.Status = 0";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tableID });
            foreach (DataRow row in data.Rows)
            {
                FoodMenu foodMenu = new FoodMenu(row);
                listFoodMenu.Add(foodMenu);
            }

            return listFoodMenu;
        }
    }
}

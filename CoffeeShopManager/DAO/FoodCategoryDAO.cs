using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class FoodCategoryDAO
    {
        private static FoodCategoryDAO instance;

        public static FoodCategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodCategoryDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private FoodCategoryDAO() { }

        public List<FoodCategory> GetListFoodCategory()
        {
            List<FoodCategory> list = new List<FoodCategory>();

            string query = "SELECT fc.ID, fc.Name, COUNT(*) AS [NumberOfDishes] " +
                "FROM FoodCategory AS fc " +
                "INNER JOIN Food AS f ON f.CategoryID = fc.ID " +
                "GROUP BY fc.ID, fc.Name";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                FoodCategory foodCategory = new FoodCategory(row);
                list.Add(foodCategory);
            }

            return list;
        }
    }
}

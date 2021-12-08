using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FoodDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private FoodDAO() { }

        public List<Food> GetListFoodByFoodCategoryID(int categoryID)
        {
            List<Food> foods = new List<Food>();

            string query = "SELECT F.ID, F.Name, C.Name AS [Category], F.Price " +
                "FROM Food AS F INNER JOIN FoodCategory AS C ON F.CategoryID = C.ID " +
                "WHERE CategoryID = @categoryID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { categoryID });

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                foods.Add(food);
            }

            return foods;
        }

        public List<Food> GetListFood()
        {
            List<Food> foods = new List<Food>();

            string query = "SELECT F.ID, F.Name, C.Name AS [Category], F.Price " +
                "FROM Food AS F INNER JOIN FoodCategory AS C ON F.CategoryID = C.ID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                foods.Add(food);
            }

            return foods;
        }

        public bool InsertFood(string name, int categoryID, float price)
        {
            string query = "INSERT INTO dbo.Food(Name, CategoryID, Price) VALUES( @name, @categoryID, @price )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, categoryID, price }) > 0;
        }

        public bool UpdateFood(int id, string name, int categoryID, float price)
        {
            string query = "UPDATE dbo.Food SET Name = @name, CategoryID = @categoryID, Price = @price WHERE ID = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, categoryID, price, id }) > 0;
        }

        public bool DeleteFood(int id)
        {
            string query = "DELETE FROM dbo.Food WHERE ID = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> foods = new List<Food>();

            string query = "SELECT F.ID, F.Name, C.Name AS [Category], F.Price " +
                "FROM Food AS F INNER JOIN FoodCategory AS C ON F.CategoryID = C.ID " +
                "WHERE dbo.ConvertToUnsign(F.Name) LIKE dbo.ConvertToUnsign( @name )";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { string.Format("%{0}%", name) });
            
            foreach (DataRow row in data.Rows)
            {
                Food food = new Food(row);
                foods.Add(food);
            }

            return foods;
        }
    }
}

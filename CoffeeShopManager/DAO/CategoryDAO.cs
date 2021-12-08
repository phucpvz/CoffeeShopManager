using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();

            string query = "SELECT * FROM FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);
                list.Add(category);
            }

            return list;
        }

        public Category GetCategoryByID(int categoryID)
        {
            string query = "SELECT * FROM FoodCategory WHERE ID = @categoryID";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { categoryID });

            foreach (DataRow row in data.Rows)
            {
                Category category = new Category(row);
                return category;
            }

            return null;
        }

        public bool InsertCategory(string name)
        {
            string query = "INSERT INTO FoodCategory VALUES( @name )";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name }) > 0;
        }

        public bool UpdateCategory(int id, string name)
        {
            string query = "UPDATE FoodCategory SET Name = @name WHERE ID = @id";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { name, id }) > 0;
        }

        public int CountFoodByFoodCategoryID(int categoryID)
        {
            string query = "SELECT COUNT(*) FROM Food WHERE CategoryID = @categoryID";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { categoryID });
        }

        public bool DeleteCategory(int id)
        {
            string query = "DELETE FROM FoodCategory WHERE ID = @id";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
        }

    }
}

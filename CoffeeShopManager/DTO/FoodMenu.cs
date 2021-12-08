using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class FoodMenu
    {
        private string foodName;
        private int amount;
        private float price;
        private float totalPrice;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Amount { get => amount; set => amount = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }

        public FoodMenu(string foodName, int amount, float price, float totalPrice)
        {
            FoodName = foodName;
            Amount = amount;
            Price = price;
            TotalPrice = totalPrice;
        }

        public FoodMenu(DataRow row)
        {
            FoodName = row["FoodName"].ToString();
            Amount = (int)row["Amount"];
            Price = Convert.ToSingle(row["Price"]);
            TotalPrice = Convert.ToSingle(row["TotalPrice"]);
        }
    }
}

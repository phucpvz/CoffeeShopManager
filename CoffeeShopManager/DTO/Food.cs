using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class Food
    {
        private int iD;
        private string name;
        private string category;
        private float price;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public float Price { get => price; set => price = value; }

        public Food(int iD, string name, string category, float price)
        {
            ID = iD;
            Name = name;
            Category = category;
            Price = price;
        }

        public Food(DataRow row)
        {
            ID = (int)row["ID"];
            Name = row["Name"].ToString();
            Category = row["Category"].ToString();
            Price = Convert.ToSingle(row["Price"]);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class FoodCategory
    {
        private int iD;
        private string name;
        private int numberOfDishes;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int NumberOfDishes { get => numberOfDishes; set => numberOfDishes = value; }

        public FoodCategory(int iD, string name, int numOfDishes)
        {
            ID = iD;
            Name = name;
            NumberOfDishes = numOfDishes;
        }

        public FoodCategory(DataRow row)
        {
            ID = (int)row["ID"];
            Name = row["Name"].ToString();
            NumberOfDishes = (int)row["NumberOfDishes"];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class BillInfo
    {
        private int iD;
        private int billID;
        private int foodID;
        private int amount;

        public int ID { get => iD; set => iD = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Amount { get => amount; set => amount = value; }

        public BillInfo(int iD, int billID, int foodID, int amount)
        {
            ID = iD;
            BillID = billID;
            FoodID = foodID;
            Amount = amount;
        }

        public BillInfo(DataRow row)
        {
            ID = (int)row["ID"];
            BillID = (int)row["BillID"];
            FoodID = (int)row["FoodID"];
            Amount = (int)row["Amount"];
        }
    }
}

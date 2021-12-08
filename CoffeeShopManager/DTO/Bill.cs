using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class Bill
    {
        private int iD;
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int discount;
        private int status;

        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
        public int Status { get => status; set => status = value; }

        public Bill(int iD, DateTime? dateCheckIn, DateTime? dateCheckOut, int discount, int status)
        {
            ID = iD;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            Discount = discount;
            Status = status;
        }

        public Bill(DataRow row)
        {
            ID = (int)row["ID"];
            DateCheckIn = row["DateCheckIn"] as DateTime?;
            DateCheckOut = row["DateCheckOut"] as DateTime?;
            Discount = (int)row["Discount"];
            Status = (int)row["status"];
        }
    }
}

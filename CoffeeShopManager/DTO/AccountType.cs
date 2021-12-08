using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class AccountType
    {
        private int iD;
        private string name;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }

        public AccountType(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public AccountType(DataRow row)
        {
            ID = Convert.ToInt32(row["ID"]);
            Name = row["Name"].ToString();
        }
    }
}

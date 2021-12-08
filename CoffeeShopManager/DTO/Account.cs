using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DTO
{
    public class Account
    {
        private string username;
        private string displayName;
        private string type;

        public string Username { get => username; set => username = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Type { get => type; set => type = value; }

        public Account(string username, string displayName, string type)
        {
            Username = username;
            DisplayName = displayName;
            Type = type;
        }

        public Account(DataRow row)
        {
            Username = row["Username"].ToString();
            DisplayName = row["DisplayName"].ToString();
            Type = row["Type"].ToString();
        }
    }
}

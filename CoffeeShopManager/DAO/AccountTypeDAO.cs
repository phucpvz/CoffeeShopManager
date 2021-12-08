using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class AccountTypeDAO
    {
        private static AccountTypeDAO instance;

        public static AccountTypeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountTypeDAO();
                }

                return instance;
            }
            private set => instance = value;
        }

        private AccountTypeDAO() { }

        public List<AccountType> GetListAccountType()
        {
            List<AccountType> list = new List<AccountType>();

            string query = "SELECT * FROM AccountType";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                AccountType accountType = new AccountType(row);
                list.Add(accountType);
            }

            return list;
        }
    }
}

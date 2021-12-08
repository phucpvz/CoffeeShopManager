using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopManager.DAO
{
    public class AccountDAO
    {
        private string password;
        public string Password { get => password; set => password = value; }

        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }

                return instance;
            }
            private set => instance = value;
        }

        private AccountDAO() { }

        private void GenerateNewPassword()
        {
            Random rd = new Random();

            Password = rd.Next(1000, 10000).ToString();
        }

        // Kiểm tra có đã tồn tại tài khoản (quản trị viên) nào chưa
        //public bool ExistedAdminAccount()
        //{
        //    string query = "SELECT COUNT(*) FROM Account AS a INNER JOIN AccountType AS t ON a.TypeID = t.ID " +
        //        "WHERE t.Name = N'Administrator'";
        //    return (int)DataProvider.Instance.ExecuteScalar(query) > 0;
        //}

        // Đăng ký tài khoản quản trị viên đầu tiên
        //public bool SignUpAdminAccount(string username, string displayName, string password)
        //{
        //    string encryptedPass = GetEncryptedPassword(password);

        //    // Tìm mã của loại nhân viên quản trị
        //    int typeID = (int)DataProvider.Instance.ExecuteScalar("SELECT ID FROM AccountType WHERE Name = N'Administrator'");

        //    string query = "INSERT INTO dbo.Account(Username, DisplayName, Password, TypeID) " +
        //        "VALUES( @username, @displayName, @encryptedPass, @typeID )";
        //    return DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, displayName, encryptedPass, typeID }) > 0;
        //}

        // Đăng nhập
        public bool Login(string username, string password)
        {
            string encryptedPass = GetEncryptedPassword(password);

            string query = "SELECT COUNT(*) FROM dbo.Account " +
                "WHERE Username = @username AND Password = @encryptPass";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { username, encryptedPass }) > 0;

            /* Cách 2:
            string query = "USP_Login @username , @password";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password });
            return data.Rows.Count > 0;
            */
        }

        public Account GetAccountByUsername(string username)
        {
            string query = "SELECT A.Username, A.DisplayName, T.Name AS [Type] " +
                "FROM Account AS A INNER JOIN AccountType AS T ON A.TypeID = T.ID " +
                "WHERE Username = @username";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            foreach (DataRow row in data.Rows)
            {
                return new Account(row);
            }
            return null;
        }

        // Cập nhật thông tin cá nhân
        public bool UpdateProfile(string username, string displayName, string password, string newPass)
        {
            string encryptedPass = GetEncryptedPassword(password);
            string encryptedNewPass;

            // Mật khẩu mã hóa => Nếu không nhập mật khẩu mới thì phải cho là mật khẩu mới = mật khẩu hiện tại!
            if (string.IsNullOrEmpty(newPass))
            {
                encryptedNewPass = encryptedPass;
            }
            else
            {
                encryptedNewPass = GetEncryptedPassword(newPass);
            }

            string query = "USP_UpdateAccount @username, @displayName, @encryptedPass, @encryptedNewPass";

            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, displayName, encryptedPass, encryptedNewPass }) > 0;
        }

        public List<Account> GetListAccount()
        {
            string query = "SELECT A.Username, A.DisplayName, T.Name AS [Type] " +
                "FROM Account AS A INNER JOIN AccountType AS T ON A.TypeID = T.ID";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            List<Account> accounts = new List<Account>();
            foreach (DataRow row in data.Rows)
            {
                Account a = new Account(row);
                accounts.Add(a);
            }

            return accounts;
        }

        public bool InsertAccount(string username, string displayName, int typeID)
        {
            GenerateNewPassword();
            string encryptedPass = GetEncryptedPassword(Password);
            string query = "INSERT INTO dbo.Account(Username, DisplayName, Password, TypeID) VALUES( @username, @displayName, @encryptedPass, @typeID )";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { username, displayName, encryptedPass, typeID }) > 0;
        }

        public bool UpdateAccount(string username, string displayName, int typeID)
        {
            string query = "UPDATE dbo.Account SET DisplayName = @displayName, TypeID = @typeID WHERE Username = @username";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { displayName, typeID, username }) > 0;
        }

        public bool DeleteAccount(string username)
        {
            string query = "DELETE FROM dbo.Account WHERE Username = @username";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { username }) > 0;
        }

        public bool ResetPassword(string username)
        {
            GenerateNewPassword();
            string encryptedPass = GetEncryptedPassword(Password);
            string query = "UPDATE dbo.Account SET Password = @encryptedPass WHERE Username = @username";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { encryptedPass, username }) > 0;
        }

        // Mã hóa mật khẩu
        private string GetEncryptedPassword(string password)
        {
            byte[] tmp = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashData;

            using (var provider = new MD5CryptoServiceProvider())
            {
                hashData = provider.ComputeHash(tmp);
            }

            StringBuilder hashPass = new StringBuilder();
            foreach (byte item in hashData)
            {
                hashPass.Append(item);
            }

            return hashPass.ToString();
        }
    }
}

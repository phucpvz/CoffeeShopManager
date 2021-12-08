using CoffeeShopManager.DAO;
using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager.GUI
{
    public partial class FAccountProfile : Form
    {
        private Account loginAccount;
        public Account LoginAccount
        {
            get => loginAccount;
            set
            {
                loginAccount = value;
                ShowAccountInfo();
            }
        }

        private event EventHandler<AccountUpdatedEventArgs> accountUpdated;
        public event EventHandler<AccountUpdatedEventArgs> AccountUpdated
        {
            add { accountUpdated += value; }
            remove { accountUpdated -= value; }
        }

        private void ShowAccountInfo()
        {
            txbUsername.Text = LoginAccount.Username;
            txbDisplayName.Text = LoginAccount.DisplayName;
            txbPassword.Clear();
            txbNewPassword.Clear();
            txbRetypeNewPass.Clear();
        }

        public FAccountProfile(Account account)
        {
            InitializeComponent();

            LoginAccount = account;
        }

        private void UpdateAccount()
        {
            string username = txbUsername.Text;
            string displayName = txbDisplayName.Text;
            string password = txbPassword.Text;
            string newPass = txbNewPassword.Text;
            string retypeNewPass = txbRetypeNewPass.Text;

            if (displayName == "")
            {
                MessageBox.Show("Tên hiển thị không được để trống!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (retypeNewPass != newPass)
            {
                MessageBox.Show("Xác nhận mật khẩu mới không chính xác!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AccountDAO.Instance.UpdateProfile(username, displayName, password, newPass))
            {
                MessageBox.Show("Đã cập nhật thông tin cá nhân thành công!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoginAccount = AccountDAO.Instance.GetAccountByUsername(username);

                if (accountUpdated != null)
                {
                    accountUpdated(this, new AccountUpdatedEventArgs(LoginAccount));
                }

            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccount();
        }

    }

    public class AccountUpdatedEventArgs: EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }

        public AccountUpdatedEventArgs(Account account)
        {
            Acc = account;
        }
    }
}

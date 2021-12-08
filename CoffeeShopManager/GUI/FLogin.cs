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
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        private void ClearInfo()
        {
            txbUsername.Clear();
            txbPassword.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text;
            if (!CheckAccount(username, password))
            {
                MessageBox.Show("Tên người dùng hoặc mật khẩu của bạn không chính xác!",
                "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Account loginAccount = AccountDAO.Instance.GetAccountByUsername(username);

            FTableManager f = new FTableManager(loginAccount);
            Visible = false;    // Hide()
            f.ShowDialog();
            Visible = true;     // Show()
        }

        private bool CheckAccount(string username, string password)
        {
            try
            {
                return AccountDAO.Instance.Login(username, password);
            }
            catch (Exception)
            {
                MessageBox.Show("Không tìm thấy cơ sở dữ liệu! Chương trình sẽ kết thúc!",
                "Lỗi kỹ thuật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
                return false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //Close();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát khỏi ứng dụng không?",
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void FLogin_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                ClearInfo();
            }
        }
    }
}

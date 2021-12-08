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
    public partial class FAccount : Form
    {
        private event EventHandler accountAdded;
        public event EventHandler AccountAdded
        {
            add { accountAdded += value; }
            remove { accountAdded -= value; }
        }

        public FAccount()
        {
            InitializeComponent();

            LoadAccountTypeIntoComboBox(comboAccountType);
            ClearInput();
        }

        private void ClearInput()
        {
            txbUsername.Clear();
            txbDisplayName.Clear();
            comboAccountType.SelectedIndex = -1;
        }

        private void LoadAccountTypeIntoComboBox(ComboBox cb)
        {
            cb.DisplayMember = "Name";
            cb.DataSource = AccountTypeDAO.Instance.GetListAccountType();
        }

        private void OnAccountAdded()
        {
            if (accountAdded != null)
            {
                accountAdded(this, new EventArgs());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string displayName = txbDisplayName.Text;

            if (username == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (displayName == "")
            {
                MessageBox.Show("Tên hiển thị không được để trống!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AccountType type = comboAccountType.SelectedItem as AccountType;
            if (type == null)
            {
                MessageBox.Show("Hãy xác định loại tài khoản cần thêm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tên người dùng đã tồn tại chưa
            if (AccountDAO.Instance.GetAccountByUsername(username) != null)
            {
                MessageBox.Show("Tên người dùng đã tồn tại! Vui lòng đặt tên tài khoản khác!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AccountDAO.Instance.InsertAccount(username, displayName, type.ID))
            {
                MessageBox.Show("Tạo tài khoản thành công!\n\n" +
                    "Mật khẩu của tài khoản mới là: " + AccountDAO.Instance.Password,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OnAccountAdded();
                ClearInput();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi tạo tài khoản mới!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}

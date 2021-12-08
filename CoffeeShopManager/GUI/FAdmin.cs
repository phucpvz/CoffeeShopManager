using CoffeeShopManager.DAO;
using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager.GUI
{
    public partial class FAdmin : Form
    {
        private BindingSource tableBindings;
        private BindingSource categoryBindings;
        private BindingSource foodBindings;
        private BindingSource accountBindings;

        private event EventHandler tableChanged;
        public event EventHandler TableChanged
        {
            add { tableChanged += value; }
            remove { tableChanged -= value; }
        }

        private event EventHandler categoryChanged;
        public event EventHandler CategoryChanged
        {
            add { categoryChanged += value; }
            remove { categoryChanged -= value; }
        }

        private event EventHandler foodChanged;
        public event EventHandler FoodChanged
        {
            add { foodChanged += value; }
            remove { foodChanged -= value; }
        }

        private event EventHandler<AccountUpdatedEventArgs> loginAccountChanged;
        public event EventHandler<AccountUpdatedEventArgs> LoginAccountChanged
        {
            add { loginAccountChanged += value; }
            remove { loginAccountChanged -= value; }
        }

        private Account loginAccount;

        public Account LoginAccount { get => loginAccount; set => loginAccount = value; }

        public FAdmin(Account account)
        {
            InitializeComponent();

            LoginAccount = account;
            LoadData();
        }

        private void LoadData()
        {
            // Bàn ăn
            tableBindings = new BindingSource();
            dgrvTable.DataSource = tableBindings;
            LoadTableList();
            AddTableBinding();

            // Danh mục
            categoryBindings = new BindingSource();
            dgrvFoodCategory.DataSource = categoryBindings;
            LoadListCategory();
            AddCategoryBinding();

            // Thức ăn
            foodBindings = new BindingSource();
            dgrvFood.DataSource = foodBindings;
            LoadListFood();
            LoadCategoryIntoComboBox(comboFoodCategory);
            AddFoodBinding();
            CategoryChanged += FAdmin_CategoryChanged;

            // Doanh thu
            LoadDateTimePickerBill();
            // Gọi cho chắc ăn :)
            LoadPageNumber();
            LoadListBillByDateAndPage();

            // Tài khoản
            accountBindings = new BindingSource();
            dgrvAccount.DataSource = accountBindings;
            LoadAccountList();
            LoadAccountTypeIntoComboBox(comboAccountType);
            AddAccountBinding();
        }


        #region Table
        // ================================ Bàn ăn ================================
        private void LoadTableList()
        {
            tableBindings.DataSource = TableDAO.Instance.GetTableList();
        }

        private void AddTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dgrvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dgrvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        private void txbTableID_TextChanged(object sender, EventArgs e)
        {
            string tableStatus = dgrvTable.SelectedCells[0].OwningRow.Cells["Status"].Value.ToString();

            switch (tableStatus)
            {
                case "Trống":
                    radioEmpty.Checked = true;
                    panelTableStatus.Visible = true;
                    break;
                case "Bị hỏng":
                    radioBroken.Checked = true;
                    panelTableStatus.Visible = true;
                    break;
                default:
                    panelTableStatus.Visible = false;
                    break;
            }
        }

        private void OnTableChanged()
        {
            if (tableChanged != null)
            {
                tableChanged(this, new EventArgs());
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            if (TableDAO.Instance.InsertTable(name))
            {
                LoadTableList();
                OnTableChanged();
                MessageBox.Show("Thêm bàn ăn mới thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm bàn ăn!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);
            string name = txbTableName.Text;

            string status;
            if (panelTableStatus.Visible)
            {
                status = radioEmpty.Checked ? "Trống" : "Bị hỏng";
            }
            else
            {
                status = "Có người";
            }

            if (TableDAO.Instance.UpdateTable(id, name, status))
            {
                LoadTableList();
                OnTableChanged();
                MessageBox.Show("Đã sửa thông tin bàn ăn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa thông tin bàn ăn!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            // Nếu bàn ăn đang có người -> Không được xóa
            if (!panelTableStatus.Visible)
            {
                MessageBox.Show("Không thể xóa bàn ăn đang có người!",
                    "Hoạt động không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id = Convert.ToInt32(txbTableID.Text);
            string name = dgrvTable.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();

            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa bàn ăn có tên: {0}?\n\n" +
                "Tất cả thông tin hóa đơn liên quan đến bàn ăn này vẫn sẽ được lưu lại!", name),
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            if (TableDAO.Instance.DeleteTable(id))
            {
                LoadTableList();
                OnTableChanged();
                MessageBox.Show("Đã xóa bàn ăn thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa bàn ăn!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTableList();
        }
        #endregion

        #region FoodCategory
        // ================================ Loại món ================================
        private void LoadListCategory()
        {
            categoryBindings.DataSource = FoodCategoryDAO.Instance.GetListFoodCategory();
        }

        private void AddCategoryBinding()
        {
            txbCategoryID.DataBindings.Add(new Binding("Text", dgrvFoodCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCategoryName.DataBindings.Add(new Binding("Text", dgrvFoodCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
        }

        private void OnCategoryChanged()
        {
            if (categoryChanged != null)
            {
                categoryChanged(this, new EventArgs());
            }
        }

        private void FAdmin_CategoryChanged(object sender, EventArgs e)
        {
            LoadListFood();
            LoadCategoryIntoComboBox(comboFoodCategory);
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                LoadListCategory();
                OnCategoryChanged();
                MessageBox.Show("Thêm loại món ăn mới thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm loại món ăn mới!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            string name = dgrvFoodCategory.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            string newName = txbCategoryName.Text;

            if (CategoryDAO.Instance.UpdateCategory(id, newName))
            {
                LoadListCategory();
                OnCategoryChanged();
                MessageBox.Show(string.Format("Đã đổi tên loại món ăn từ \"{0}\" sang \"{1}\"!", name, newName),
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi đổi tên loại món ăn!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);
            // Không lấy tên từ TextBox thì người dùng có thể thay đổi tên ở đây trước đó
            string name = dgrvFoodCategory.SelectedCells[0].OwningRow.Cells["Name"].Value.ToString();
            int numberOfDishes = (int)dgrvFoodCategory.SelectedCells[0].OwningRow.Cells["NumberOfDishes"].Value;

            if (numberOfDishes > 0)
            {
                MessageBox.Show("Chỉ có thể xóa danh mục món ăn trống!\n\n" +
                    "Thử xóa tất cả món ăn trong danh mục này hoặc chuyển các món ăn sang danh mục khác để làm trống danh mục!",
                    "Hoạt động không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa danh mục món ăn có tên: " + name),
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                LoadListCategory();
                OnCategoryChanged();
                MessageBox.Show("Đã xóa danh mục món ăn tên " + name,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa danh mục món ăn!",
                    "Lỗi không xác định", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }
        #endregion

        #region Food
        // ================================ Thức ăn ================================
        private void LoadListFood()
        {
            foodBindings.DataSource = FoodDAO.Instance.GetListFood();
        }

        private void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DisplayMember = "Name";
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
        }

        private void AddFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text", dgrvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dgrvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            numericFoodPrice.DataBindings.Add(new Binding("Value", dgrvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        private void OnFoodChanged()
        {
            if (foodChanged != null)
            {
                foodChanged(this, new EventArgs());
            }
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            string categoryName = dgrvFood.SelectedCells[0].OwningRow.Cells["Category"].Value.ToString();

            int index = -1;
            int i = 0;
            foreach (Category item in comboFoodCategory.Items)
            {
                if (item.Name == categoryName)
                {
                    index = i;
                    break;
                }
                ++i;
            }

            comboFoodCategory.SelectedIndex = index;
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            Category category = comboFoodCategory.SelectedItem as Category;
            float price = Convert.ToSingle(numericFoodPrice.Value);

            if (FoodDAO.Instance.InsertFood(name, category.ID, price))
            {
                LoadListFood();
                OnFoodChanged();
                MessageBox.Show("Thêm món ăn thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Vì danh mục có liệt kê số món ăn từng loại
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm thức ăn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            Category category = comboFoodCategory.SelectedItem as Category;
            float price = Convert.ToSingle(numericFoodPrice.Value);

            if (FoodDAO.Instance.UpdateFood(id, name, category.ID, price))
            {
                LoadListFood();
                OnFoodChanged();
                MessageBox.Show("Sửa thông tin thức ăn thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa thông tin thức ăn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            string name = txbFoodName.Text;
            Category category = comboFoodCategory.SelectedItem as Category;
            float price = Convert.ToSingle(numericFoodPrice.Value);

            if (BillInfoDAO.Instance.CountBillInfoByFoodID(id) > 0)
            {
                MessageBox.Show("Không thể xóa món ăn đã có thông tin hóa đơn!",
                    "Hoạt động không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa thông tin của món ăn:\n" +
                "\nID: {0}\nTên món: {1}\nLoại món: {2}\nGiá: {3}", id, name, category.Name, price),
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            if (FoodDAO.Instance.DeleteFood(id))
            {
                LoadListFood();
                OnFoodChanged();
                MessageBox.Show("Đã xóa món ăn thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Vì danh mục có liệt kê số món ăn từng loại
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa món ăn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            List<Food> foods = FoodDAO.Instance.SearchFoodByName(txbSearchFood.Text);
            if (foods.Count == 0)
            {
                MessageBox.Show("Không tìm thấy món ăn nào có tên tương ứng!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foodBindings.DataSource = foods;
        }
        #endregion

        #region Bill
        // ================================ Doanh thu ================================
        private void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
            //dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        private void LoadListBillByDateAndPage()
        {
            DateTime fromDate = dtpkFromDate.Value;
            DateTime toDate = dtpkToDate.Value;
            int linesInAPage = (int)numericLineNumber.Value;

            string[] indexes = txbPageNumber.Text.Split('/');
            int currentPage = Convert.ToInt32(indexes[0]);
            int lastPage = Convert.ToInt32(indexes[1]);
            int firstPage = (lastPage == 0) ? 0 : 1;

            btnFirstPage.Enabled = btnPreviousPage.Enabled = (currentPage > firstPage);
            btnNextPage.Enabled = btnLastPage.Enabled = (currentPage < lastPage);

            dgrvBill.DataSource = BillDAO.Instance.GetListBillByDateAndPage(fromDate, toDate, linesInAPage, currentPage);
        }

        // Tính số trang (cập nhật)
        private int GetLastPageNumber()
        {
            int numRecords = BillDAO.Instance.CountBillRecordByDate(dtpkFromDate.Value, dtpkToDate.Value);

            int linesInAPage = (int)numericLineNumber.Value;

            return (int)Math.Ceiling(numRecords * 1.0 / linesInAPage);
        }

        private void LoadPageNumber()
        {
            int lastPage = GetLastPageNumber();
            int firstPage = (lastPage == 0) ? 0 : 1;
            txbPageNumber.Text = string.Format("{0}/{1}", firstPage, lastPage);
        }

        private void dtpkFromDate_ValueChanged(object sender, EventArgs e)
        {
            LoadPageNumber();
            LoadListBillByDateAndPage();
        }

        private void dtpkToDate_ValueChanged(object sender, EventArgs e)
        {
            LoadPageNumber();
            LoadListBillByDateAndPage();
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            LoadListBillByDateAndPage();
        }

        // Thay đổi số dòng trên 1 trang
        private void numericLineNumber_ValueChanged(object sender, EventArgs e)
        {
            LoadPageNumber();
            LoadListBillByDateAndPage();
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            LoadPageNumber();
            LoadListBillByDateAndPage();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            string[] indexes = txbPageNumber.Text.Split('/');
            int currentPage = Convert.ToInt32(indexes[0]);
            int lastPage = Convert.ToInt32(indexes[1]);
            txbPageNumber.Text = string.Format("{0}/{1}", currentPage - 1, lastPage);

            LoadListBillByDateAndPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            string[] indexes = txbPageNumber.Text.Split('/');
            int currentPage = Convert.ToInt32(indexes[0]);
            int lastPage = Convert.ToInt32(indexes[1]);
            txbPageNumber.Text = string.Format("{0}/{1}", currentPage + 1, lastPage);

            LoadListBillByDateAndPage();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            int lastPage = GetLastPageNumber();
            txbPageNumber.Text = string.Format("{0}/{0}", lastPage);

            LoadListBillByDateAndPage();
        }

        private void btnReportBill_Click(object sender, EventArgs e)
        {
            FListBill f = new FListBill(dtpkFromDate.Value, dtpkToDate.Value);
            f.ShowDialog();
        }
        #endregion

        #region Account
        // ================================ Tài khoản ================================
        private void OnLoginAccountChanged()
        {
            LoginAccount = AccountDAO.Instance.GetAccountByUsername(LoginAccount.Username);

            if (loginAccountChanged != null)
            {
                loginAccountChanged(this, new AccountUpdatedEventArgs(LoginAccount));
            }
        }

        private void LoadAccountList()
        {
            accountBindings.DataSource = AccountDAO.Instance.GetListAccount();
        }

        private void LoadAccountTypeIntoComboBox(ComboBox cb)
        {
            cb.DisplayMember = "Name";
            cb.DataSource = AccountTypeDAO.Instance.GetListAccountType();
        }

        private void AddAccountBinding()
        {
            txbUsername.DataBindings.Add(new Binding("Text", dgrvAccount.DataSource, "Username", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dgrvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
        }

        private void txbUsername_TextChanged(object sender, EventArgs e)
        {
            string type = dgrvAccount.SelectedCells[0].OwningRow.Cells["Type"].Value.ToString();

            int index = -1;
            int i = 0;
            foreach (AccountType item in comboAccountType.Items)
            {
                if (item.Name == type)
                {
                    index = i;
                    break;
                }
                ++i;
            }

            comboAccountType.SelectedIndex = index;
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccountList();
        }


        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            FAccount f = new FAccount();
            f.AccountAdded += F_AccountAdded;
            f.ShowDialog();
        }

        private void F_AccountAdded(object sender, EventArgs e)
        {
            LoadAccountList();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string displayName = txbDisplayName.Text;
            AccountType type = comboAccountType.SelectedItem as AccountType;

            // Không được thay đổi loại tài khoản của tài khoản hiện đang đăng nhập (Administrator -> Staff)
            if (username == LoginAccount.Username && type.Name != "Administrator")
            {
                MessageBox.Show("Không thể tự thay đổi loại của tài khoản hiện tại đang đăng nhập!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AccountDAO.Instance.UpdateAccount(username, displayName, type.ID))
            {
                LoadAccountList();
                if (username == LoginAccount.Username)
                {
                    OnLoginAccountChanged();
                }
                MessageBox.Show("Sửa thông tin tài khoản thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi sửa thông tin tài khoản!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;

            if (username == LoginAccount.Username)
            {
                MessageBox.Show("Không thể xóa tài khoản hiện tại đang đăng nhập!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa vĩnh viễn tài khoản này không?",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            if (AccountDAO.Instance.DeleteAccount(username))
            {
                LoadAccountList();
                MessageBox.Show("Xóa tài khoản thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa tài khoản!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;

            if (username == LoginAccount.Username)
            {
                MessageBox.Show("Không cần phải tự đặt lại mật khẩu cho tài khoản của mình!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn đặt lại mật khẩu của tài khoản này không?",
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            {
                return;
            }

            if (AccountDAO.Instance.ResetPassword(username))
            {
                MessageBox.Show("Đặt lại mật khẩu cho tài khoản thành công!\n\n" +
                    "Mật khẩu mới sau khi đặt lại là: " + AccountDAO.Instance.Password,
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi đặt lại mật khẩu cho tài khoản!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}

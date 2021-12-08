using CoffeeShopManager.DAO;
using CoffeeShopManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopManager.GUI
{
    public partial class FTableManager : Form
    {
        private Account loginAccount;
        private CultureInfo culture;

        public Account LoginAccount
        {
            get => loginAccount;
            set
            {
                loginAccount = value;
                Decentralize(loginAccount.Type);
            }
        }

        private void Decentralize(string type)
        {
            toolStripAdmin.Visible = type == "Administrator";
            toolStripAccount.Text = string.Format("Thông tin tài khoản ({0})", LoginAccount.DisplayName);
        }

        public CultureInfo Culture { get => culture; set => culture = value; }

        public FTableManager(Account account)
        {
            InitializeComponent();

            LoginAccount = account;
            Culture = new CultureInfo("vi-VN");

            LoadTables();
            LoadCategories();
            LoadComboBoxTable(comboSwitchTable);
        }

        private void LoadCategories()
        {
            List<Category> listFoodCategory = CategoryDAO.Instance.GetListCategory();
            comboFoodCategory.DisplayMember = "Name";
            comboFoodCategory.DataSource = listFoodCategory;
        }

        private void LoadFoodListByFoodCategoryID(int categoryID)
        {
            List<Food> listFood = FoodDAO.Instance.GetListFoodByFoodCategoryID(categoryID);
            comboFood.DisplayMember = "Name";
            comboFood.DataSource = listFood;
        }

        private void LoadComboBoxTable(ComboBox cb)
        {
            cb.DisplayMember = "Name";
            cb.DataSource = TableDAO.Instance.GetTableList().Where(t => t.Status != "Bị hỏng").ToList();
        }

        private void LoadTables()
        {
            flpanelTables.Controls.Clear();

            List<Table> tables = TableDAO.Instance.GetTableList();
            txbTableCount.Text = tables.Count.ToString();

            foreach (Table table in tables)
            {
                // Nếu bàn đã xóa thì không hiện lên
                if (table.Status == "Đã xóa")
                {
                    continue;
                }

                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = string.Format("{0}\n\n{1}", table.Name, table.Status);

                // Nếu bàn bị hỏng thì hiện lên nhưng không cho chọn bàn
                if (table.Status == "Bị hỏng")
                {
                    btn.Enabled = false;
                }
                else
                {
                    btn.Tag = table;
                    btn.Click += Btn_Click;

                    switch (table.Status)
                    {
                        case "Trống":
                            btn.BackColor = Color.LawnGreen;
                            break;
                        case "Có người":
                            btn.BackColor = Color.DeepSkyBlue;
                            break;
                    }
                }

                flpanelTables.Controls.Add(btn);
            }
        }

        private void ShowBill(int tableID)
        {
            listViewBill.Items.Clear();

            float totalPrice = 0;

            List<FoodMenu> listFoodMenu = FoodMenuDAO.Instance.GetListFoodMenuByTableID(tableID);
            foreach (FoodMenu foodMenu in listFoodMenu)
            {
                ListViewItem listItem = new ListViewItem(foodMenu.FoodName);
                listItem.SubItems.Add(foodMenu.Amount.ToString());
                listItem.SubItems.Add(foodMenu.Price.ToString());
                listItem.SubItems.Add(foodMenu.TotalPrice.ToString());
                totalPrice += foodMenu.TotalPrice;

                listViewBill.Items.Add(listItem);
            }

            //Thread.CurrentThread.CurrentCulture = Culture;
            txbTotalPrice.Text = totalPrice.ToString("c", Culture).Replace(",00", "");
        }



        #region Events
        private void Btn_Click(object sender, EventArgs e)
        {
            Table table = (sender as Button).Tag as Table;
            txbSelectedTable.Text = table.Name;
            listViewBill.Tag = (sender as Button).Tag;
            ShowBill(table.ID);
        }

        private void toolStripProfile_Click(object sender, EventArgs e)
        {
            FAccountProfile f = new FAccountProfile(LoginAccount);
            f.AccountUpdated += F_AccountUpdated;
            f.ShowDialog();
        }

        private void F_AccountUpdated(object sender, AccountUpdatedEventArgs e)
        {
            LoginAccount = e.Acc;
            toolStripAccount.Text = string.Format("Thông tin tài khoản ({0})", LoginAccount.DisplayName);
        }

        private void toolStripLogOut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripAdmin_Click(object sender, EventArgs e)
        {
            FAdmin f = new FAdmin(LoginAccount);
            f.TableChanged += F_TableChanged;
            f.CategoryChanged += F_CategoryChanged;
            f.FoodChanged += F_FoodChanged;
            f.LoginAccountChanged += F_LoginAccountChanged;
            f.ShowDialog();
        }

        private void F_CategoryChanged(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void F_TableChanged(object sender, EventArgs e)
        {
            LoadTables();
            LoadComboBoxTable(comboSwitchTable);
        }

        private void F_LoginAccountChanged(object sender, AccountUpdatedEventArgs e)
        {
            LoginAccount = e.Acc;
            toolStripAccount.Text = string.Format("Thông tin tài khoản ({0})", LoginAccount.DisplayName);
        }

        private void F_FoodChanged(object sender, EventArgs e)
        {
            Category category = (comboFoodCategory.SelectedItem as Category);
            LoadFoodListByFoodCategoryID(category.ID);

            LoadTables();

            Table table = listViewBill.Tag as Table;

            // Nếu người đã có bàn được chọn trước đó
            if (table != null)
            {
                // Hiển thị lại hóa đơn sau khi thêm, xóa, sửa món ăn
                ShowBill(table.ID);
            }
        }

        private void comboFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Category category = (comboFoodCategory.SelectedItem as Category);
            LoadFoodListByFoodCategoryID(category.ID);
        }

        // Thêm món cho bàn đã chọn
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;

            // Nếu người dùng chưa chọn bàn
            if (table == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn cần thêm món!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Food food = comboFood.SelectedItem as Food;

            // Nếu người dùng chưa chọn món
            if (food == null)
            {
                MessageBox.Show("Bạn chưa chọn món ăn cần thêm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int amount = (int)numericFoodCount.Value;

            // Thêm món cho hóa đơn (có hoặc chưa có) của bàn này
            BillInfoDAO.Instance.InsertBillInfo(table.ID, food.ID, amount);

            // Hiển thị lại hóa đơn sau khi thêm món ăn
            ShowBill(table.ID);
            LoadTables();
        }

        // Xóa món của bàn đã chọn
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;

            // Nếu người dùng chưa chọn bàn
            if (table == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn cần xóa món!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Food food = comboFood.SelectedItem as Food;

            // Nếu người dùng chưa chọn món
            if (food == null)
            {
                MessageBox.Show("Bạn chưa chọn món ăn cần xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int amount = (int)numericFoodCount.Value;

            // Xóa món cho hóa đơn (có hoặc chưa có) của bàn này
            BillInfoDAO.Instance.DeleteBillInfo(table.ID, food.ID, amount);

            // Hiển thị lại hóa đơn sau khi xóa món ăn
            ShowBill(table.ID);
            LoadTables();
        }

        // Xóa hóa đơn chưa thanh toán của bàn đã chọn
        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;

            // Nếu người dùng chưa chọn bàn
            if (table == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn cần xóa hóa đơn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int billID = BillDAO.Instance.GetUnpaidBillIDByTableID(table.ID);

            if (billID == -1)
            {
                MessageBox.Show("Bàn này chưa có hóa đơn để xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa hóa đơn của bàn này
            if (MessageBox.Show(string.Format("Bạn có chắc muốn xóa hóa đơn hiện tại của bàn [{0}] không?",
                table.Name), "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                BillDAO.Instance.DeleteUnpaidBill(table.ID);
                // Hiển thị lại hóa đơn sau khi xóa món ăn
                ShowBill(table.ID);
                LoadTables();
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = listViewBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn cần thanh toán hóa đơn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int billID = BillDAO.Instance.GetUnpaidBillIDByTableID(table.ID);

            if (billID == -1)
            {
                MessageBox.Show("Bàn này chưa có hóa đơn cần thanh toán!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int discount = (int)numericDiscount.Value;
            //float totalPrice = Convert.ToSingle(txbTotalPrice.Text.Split(',')[0].Replace(".", ""));
            float totalPrice = float.Parse(txbTotalPrice.Text, NumberStyles.Currency, Culture);
            float finalTotalPrice = totalPrice - totalPrice * discount / 100;

            if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho bàn [{0}] không?\n\n" +
                "Tổng tiền: {1}\n" +
                "Giảm giá: {2}%\n" +
                "Số tiền cuối cùng cần thanh toán: {3}",
                table.Name, txbTotalPrice.Text, discount, finalTotalPrice.ToString("c", Culture).Replace(",00", "")),
                    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                return;
            }

            //if (MessageBox.Show("Bạn có muốn in hóa đơn thanh toán không?",
            //    "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            //{
            FBill f = new FBill(table, totalPrice, discount, finalTotalPrice);
            f.ShowDialog();
            //}

            BillDAO.Instance.CheckOut(billID, discount, totalPrice);
            ShowBill(table.ID);
            LoadTables();
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            Table table1 = listViewBill.Tag as Table;
            Table table2 = comboSwitchTable.SelectedItem as Table;

            if (table1 == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn để đổi!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (table1.ID == table2.ID)
            {
                MessageBox.Show("Hãy chọn một bàn khác để đổi với bàn đã chọn!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có chắc muốn đổi hóa đơn hai bàn [{0}] và [{1}] không?", table1.Name, table2.Name),
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(table1.ID, table2.ID);
                LoadTables();
                ShowBill(table1.ID);
            }
        }

        private void btnMergeTable_Click(object sender, EventArgs e)
        {
            Table table1 = listViewBill.Tag as Table;
            Table table2 = comboSwitchTable.SelectedItem as Table;

            if (table1 == null)
            {
                MessageBox.Show("Bạn chưa chọn bàn để gộp!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (table1.ID == table2.ID)
            {
                MessageBox.Show("Hãy chọn một bàn khác để gộp bàn đã chọn vào bàn này!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(string.Format("Bạn có chắc muốn gộp hóa đơn bàn [{0}] vào [{1}] không?", table1.Name, table2.Name),
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                TableDAO.Instance.MergeTable(table1.ID, table2.ID);
                LoadTables();
                ShowBill(table1.ID);
            }
        }

        private void toolStripCheckOut_Click(object sender, EventArgs e)
        {
            btnCheckOut.PerformClick();
        }

        private void toolStripDeleteBill_Click(object sender, EventArgs e)
        {
            btnDeleteBill.PerformClick();
        }

        private void toolStripAddFood_Click(object sender, EventArgs e)
        {
            btnAddFood.PerformClick();
        }

        private void toolStripDeleteFood_Click(object sender, EventArgs e)
        {
            btnDeleteFood.PerformClick();
        }


        #endregion


    }
}

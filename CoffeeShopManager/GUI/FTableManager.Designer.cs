
namespace CoffeeShopManager.GUI
{
    partial class FTableManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTableManager));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCheckOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDeleteBill = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listViewBill = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbTotalPrice = new System.Windows.Forms.TextBox();
            this.btnCheckOut = new System.Windows.Forms.Button();
            this.numericDiscount = new System.Windows.Forms.NumericUpDown();
            this.btnSwitchTable = new System.Windows.Forms.Button();
            this.btnMergeTable = new System.Windows.Forms.Button();
            this.comboSwitchTable = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnDeleteBill = new System.Windows.Forms.Button();
            this.btnDeleteFood = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericFoodCount = new System.Windows.Forms.NumericUpDown();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.comboFood = new System.Windows.Forms.ComboBox();
            this.comboFoodCategory = new System.Windows.Forms.ComboBox();
            this.flpanelTables = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbTableCount = new System.Windows.Forms.TextBox();
            this.txbSelectedTable = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toolStripAddFood = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDeleteFood = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDiscount)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFoodCount)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Gold;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAdmin,
            this.toolStripAccount,
            this.toolStripFunction});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(913, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripAdmin
            // 
            this.toolStripAdmin.Name = "toolStripAdmin";
            this.toolStripAdmin.Size = new System.Drawing.Size(55, 20);
            this.toolStripAdmin.Text = "Admin";
            this.toolStripAdmin.Click += new System.EventHandler(this.toolStripAdmin_Click);
            // 
            // toolStripAccount
            // 
            this.toolStripAccount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProfile,
            this.toolStripLogOut});
            this.toolStripAccount.Name = "toolStripAccount";
            this.toolStripAccount.Size = new System.Drawing.Size(122, 20);
            this.toolStripAccount.Text = "Thông tin tài khoản";
            // 
            // toolStripProfile
            // 
            this.toolStripProfile.Name = "toolStripProfile";
            this.toolStripProfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.toolStripProfile.Size = new System.Drawing.Size(207, 22);
            this.toolStripProfile.Text = "Thông tin cá nhân";
            this.toolStripProfile.Click += new System.EventHandler(this.toolStripProfile_Click);
            // 
            // toolStripLogOut
            // 
            this.toolStripLogOut.Name = "toolStripLogOut";
            this.toolStripLogOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.toolStripLogOut.Size = new System.Drawing.Size(207, 22);
            this.toolStripLogOut.Text = "Đăng xuất";
            this.toolStripLogOut.Click += new System.EventHandler(this.toolStripLogOut_Click);
            // 
            // toolStripFunction
            // 
            this.toolStripFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripAddFood,
            this.toolStripDeleteFood,
            this.toolStripSeparator1,
            this.toolStripCheckOut,
            this.toolStripDeleteBill});
            this.toolStripFunction.Name = "toolStripFunction";
            this.toolStripFunction.Size = new System.Drawing.Size(77, 20);
            this.toolStripFunction.Text = "Chức năng";
            // 
            // toolStripCheckOut
            // 
            this.toolStripCheckOut.Name = "toolStripCheckOut";
            this.toolStripCheckOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toolStripCheckOut.Size = new System.Drawing.Size(206, 22);
            this.toolStripCheckOut.Text = "Thanh toán";
            this.toolStripCheckOut.Click += new System.EventHandler(this.toolStripCheckOut_Click);
            // 
            // toolStripDeleteBill
            // 
            this.toolStripDeleteBill.Name = "toolStripDeleteBill";
            this.toolStripDeleteBill.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.toolStripDeleteBill.Size = new System.Drawing.Size(206, 22);
            this.toolStripDeleteBill.Text = "Xóa hóa đơn";
            this.toolStripDeleteBill.Click += new System.EventHandler(this.toolStripDeleteBill_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listViewBill);
            this.panel2.Location = new System.Drawing.Point(481, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 285);
            this.panel2.TabIndex = 2;
            // 
            // listViewBill
            // 
            this.listViewBill.BackColor = System.Drawing.SystemColors.Window;
            this.listViewBill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewBill.GridLines = true;
            this.listViewBill.HideSelection = false;
            this.listViewBill.Location = new System.Drawing.Point(3, 3);
            this.listViewBill.Name = "listViewBill";
            this.listViewBill.Size = new System.Drawing.Size(413, 279);
            this.listViewBill.TabIndex = 0;
            this.listViewBill.UseCompatibleStateImageBehavior = false;
            this.listViewBill.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên món";
            this.columnHeader1.Width = 159;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Số lượng";
            this.columnHeader2.Width = 72;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Đơn giá";
            this.columnHeader3.Width = 77;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thành tiền";
            this.columnHeader4.Width = 101;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txbTotalPrice);
            this.panel3.Controls.Add(this.btnCheckOut);
            this.panel3.Controls.Add(this.numericDiscount);
            this.panel3.Location = new System.Drawing.Point(481, 387);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(419, 63);
            this.panel3.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(143, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Tổng tiền:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Giảm giá:";
            // 
            // txbTotalPrice
            // 
            this.txbTotalPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTotalPrice.ForeColor = System.Drawing.Color.Black;
            this.txbTotalPrice.Location = new System.Drawing.Point(204, 19);
            this.txbTotalPrice.Name = "txbTotalPrice";
            this.txbTotalPrice.ReadOnly = true;
            this.txbTotalPrice.Size = new System.Drawing.Size(100, 22);
            this.txbTotalPrice.TabIndex = 10;
            this.txbTotalPrice.Text = "0";
            this.txbTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.Location = new System.Drawing.Point(336, 19);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(75, 23);
            this.btnCheckOut.TabIndex = 5;
            this.btnCheckOut.Text = "Thanh toán";
            this.btnCheckOut.UseVisualStyleBackColor = true;
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            // 
            // numericDiscount
            // 
            this.numericDiscount.Location = new System.Drawing.Point(60, 22);
            this.numericDiscount.Name = "numericDiscount";
            this.numericDiscount.Size = new System.Drawing.Size(56, 20);
            this.numericDiscount.TabIndex = 4;
            this.numericDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSwitchTable
            // 
            this.btnSwitchTable.Location = new System.Drawing.Point(133, 3);
            this.btnSwitchTable.Name = "btnSwitchTable";
            this.btnSwitchTable.Size = new System.Drawing.Size(75, 23);
            this.btnSwitchTable.TabIndex = 9;
            this.btnSwitchTable.Text = "Đổi bàn";
            this.btnSwitchTable.UseVisualStyleBackColor = true;
            this.btnSwitchTable.Click += new System.EventHandler(this.btnSwitchTable_Click);
            // 
            // btnMergeTable
            // 
            this.btnMergeTable.Location = new System.Drawing.Point(133, 32);
            this.btnMergeTable.Name = "btnMergeTable";
            this.btnMergeTable.Size = new System.Drawing.Size(75, 23);
            this.btnMergeTable.TabIndex = 8;
            this.btnMergeTable.Text = "Gộp bàn";
            this.btnMergeTable.UseVisualStyleBackColor = true;
            this.btnMergeTable.Click += new System.EventHandler(this.btnMergeTable_Click);
            // 
            // comboSwitchTable
            // 
            this.comboSwitchTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSwitchTable.FormattingEnabled = true;
            this.comboSwitchTable.Location = new System.Drawing.Point(232, 22);
            this.comboSwitchTable.Name = "comboSwitchTable";
            this.comboSwitchTable.Size = new System.Drawing.Size(112, 21);
            this.comboSwitchTable.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnDeleteBill);
            this.panel4.Controls.Add(this.btnDeleteFood);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numericFoodCount);
            this.panel4.Controls.Add(this.btnAddFood);
            this.panel4.Controls.Add(this.comboFood);
            this.panel4.Controls.Add(this.comboFoodCategory);
            this.panel4.Location = new System.Drawing.Point(481, 27);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(419, 63);
            this.panel4.TabIndex = 4;
            // 
            // btnDeleteBill
            // 
            this.btnDeleteBill.Location = new System.Drawing.Point(233, 3);
            this.btnDeleteBill.Name = "btnDeleteBill";
            this.btnDeleteBill.Size = new System.Drawing.Size(97, 23);
            this.btnDeleteBill.TabIndex = 8;
            this.btnDeleteBill.Text = "Xóa hóa đơn";
            this.btnDeleteBill.UseVisualStyleBackColor = true;
            this.btnDeleteBill.Click += new System.EventHandler(this.btnDeleteBill_Click);
            // 
            // btnDeleteFood
            // 
            this.btnDeleteFood.Location = new System.Drawing.Point(336, 3);
            this.btnDeleteFood.Name = "btnDeleteFood";
            this.btnDeleteFood.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteFood.TabIndex = 7;
            this.btnDeleteFood.Text = "Xóa món";
            this.btnDeleteFood.UseVisualStyleBackColor = true;
            this.btnDeleteFood.Click += new System.EventHandler(this.btnDeleteFood_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(230, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Số lượng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên món:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Loại món:";
            // 
            // numericFoodCount
            // 
            this.numericFoodCount.Location = new System.Drawing.Point(283, 35);
            this.numericFoodCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericFoodCount.Name = "numericFoodCount";
            this.numericFoodCount.Size = new System.Drawing.Size(47, 20);
            this.numericFoodCount.TabIndex = 3;
            this.numericFoodCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAddFood
            // 
            this.btnAddFood.Location = new System.Drawing.Point(336, 32);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(75, 23);
            this.btnAddFood.TabIndex = 2;
            this.btnAddFood.Text = "Thêm món";
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // comboFood
            // 
            this.comboFood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFood.FormattingEnabled = true;
            this.comboFood.Location = new System.Drawing.Point(55, 39);
            this.comboFood.Name = "comboFood";
            this.comboFood.Size = new System.Drawing.Size(169, 21);
            this.comboFood.TabIndex = 1;
            // 
            // comboFoodCategory
            // 
            this.comboFoodCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFoodCategory.FormattingEnabled = true;
            this.comboFoodCategory.Location = new System.Drawing.Point(55, 3);
            this.comboFoodCategory.Name = "comboFoodCategory";
            this.comboFoodCategory.Size = new System.Drawing.Size(169, 21);
            this.comboFoodCategory.TabIndex = 0;
            this.comboFoodCategory.SelectedIndexChanged += new System.EventHandler(this.comboFoodCategory_SelectedIndexChanged);
            // 
            // flpanelTables
            // 
            this.flpanelTables.AutoScroll = true;
            this.flpanelTables.BackColor = System.Drawing.Color.Silver;
            this.flpanelTables.Location = new System.Drawing.Point(12, 96);
            this.flpanelTables.Name = "flpanelTables";
            this.flpanelTables.Size = new System.Drawing.Size(463, 354);
            this.flpanelTables.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Bàn thay đổi:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbTableCount);
            this.panel1.Controls.Add(this.txbSelectedTable);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnMergeTable);
            this.panel1.Controls.Add(this.btnSwitchTable);
            this.panel1.Controls.Add(this.comboSwitchTable);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 63);
            this.panel1.TabIndex = 6;
            // 
            // txbTableCount
            // 
            this.txbTableCount.Location = new System.Drawing.Point(366, 22);
            this.txbTableCount.Name = "txbTableCount";
            this.txbTableCount.ReadOnly = true;
            this.txbTableCount.Size = new System.Drawing.Size(70, 20);
            this.txbTableCount.TabIndex = 12;
            // 
            // txbSelectedTable
            // 
            this.txbSelectedTable.Location = new System.Drawing.Point(6, 22);
            this.txbSelectedTable.Name = "txbSelectedTable";
            this.txbSelectedTable.ReadOnly = true;
            this.txbSelectedTable.Size = new System.Drawing.Size(112, 20);
            this.txbSelectedTable.TabIndex = 2;
            this.txbSelectedTable.Text = "Chưa chọn bàn!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Bàn đang chọn:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(363, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Số lượng bàn:";
            // 
            // toolStripAddFood
            // 
            this.toolStripAddFood.Name = "toolStripAddFood";
            this.toolStripAddFood.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.toolStripAddFood.Size = new System.Drawing.Size(206, 22);
            this.toolStripAddFood.Text = "Thêm món";
            this.toolStripAddFood.Click += new System.EventHandler(this.toolStripAddFood_Click);
            // 
            // toolStripDeleteFood
            // 
            this.toolStripDeleteFood.Name = "toolStripDeleteFood";
            this.toolStripDeleteFood.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.toolStripDeleteFood.Size = new System.Drawing.Size(206, 22);
            this.toolStripDeleteFood.Text = "Xóa món";
            this.toolStripDeleteFood.Click += new System.EventHandler(this.toolStripDeleteFood_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // FTableManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(913, 463);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flpanelTables);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FTableManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần mềm quản lý quán cà phê";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDiscount)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFoodCount)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripAdmin;
        private System.Windows.Forms.ToolStripMenuItem toolStripAccount;
        private System.Windows.Forms.ToolStripMenuItem toolStripProfile;
        private System.Windows.Forms.ToolStripMenuItem toolStripLogOut;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listViewBill;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.ComboBox comboFood;
        private System.Windows.Forms.ComboBox comboFoodCategory;
        private System.Windows.Forms.NumericUpDown numericFoodCount;
        private System.Windows.Forms.Button btnSwitchTable;
        private System.Windows.Forms.Button btnMergeTable;
        private System.Windows.Forms.ComboBox comboSwitchTable;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.NumericUpDown numericDiscount;
        private System.Windows.Forms.FlowLayoutPanel flpanelTables;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txbTotalPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbTableCount;
        private System.Windows.Forms.TextBox txbSelectedTable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDeleteFood;
        private System.Windows.Forms.Button btnDeleteBill;
        private System.Windows.Forms.ToolStripMenuItem toolStripFunction;
        private System.Windows.Forms.ToolStripMenuItem toolStripCheckOut;
        private System.Windows.Forms.ToolStripMenuItem toolStripDeleteBill;
        private System.Windows.Forms.ToolStripMenuItem toolStripAddFood;
        private System.Windows.Forms.ToolStripMenuItem toolStripDeleteFood;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
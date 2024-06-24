using Quản_lý_quán_caffe.DAO;
using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Quản_lý_quán_caffe.DTO.Menu;

namespace Quản_lý_quán_caffe
{
    public partial class TableManager : Form
    {
        public TableManager()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            fLogin fLogin = new fLogin();
            this.Hide();
            fLogin.ShowDialog();
            this.Show();
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
        }

        void LoadFoodListByCategoryID(int id)
        {   
            List<Product> listProduct = ProductDAO.Instance.GetFoodListByCategoryID(id);
            cbFood.DataSource = listProduct;
            cbFood.DisplayMember = "Name";

        }

        void LoadTable() {

            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList) { 
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight};
                btn.Text= item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                 
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    case "Có người":
                        btn.BackColor = Color.Red;
                        break;
                    default:
                        btn.BackColor = Color.Gray;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();


            List<Menu> listBillInfo  = MenuDAO.Instance.GetListMenuByTable(id);

            float totalPrice = 0;
            foreach (Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice+= item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c",culture);


        }
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) return;
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;   

            LoadFoodListByCategoryID(id);

        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!");
                return;
            }

            Product selectedProduct = cbFood.SelectedItem as Product;
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a food item.");
                return;
            }

            int idBill = BillDAO.Instance.GetUncheckBillIDByTable(table.ID);
            int foodID = selectedProduct.ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, 1);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }

            ShowBill(table.ID);
            LoadTable();
        }


        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUncheckBillIDByTable(table.ID);
            int discount = (int)nmDiscount.Value;
            double totalPrice;

            if (double.TryParse(txbTotalPrice.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("vi-VN"), out totalPrice))
            {
                double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
                if (idBill != -1)
                {
                    if (MessageBox.Show("Xác nhận thanh toán hóa đơn cho " + table.Name + "\nTổng số tiền = " + finalTotalPrice, "Thông báo ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                        ShowBill(table.ID);
                        LoadTable();
                    }
                }
            }
        }
    }
}

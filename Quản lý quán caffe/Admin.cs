using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Quản_lý_quán_caffe.DAO;
using Quản_lý_quán_caffe.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Quản_lý_quán_caffe
{
    public partial class Admin : Form
    {
        BindingSource productList = new BindingSource();
        BindingSource categoryList = new BindingSource();
        BindingSource tableList = new BindingSource();
        BindingSource employeeList = new BindingSource();
        public Admin()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            dtgvProduct.DataSource = productList;
            dtgvCategory.DataSource = categoryList;
            dtgvTable.DataSource = tableList;
            dtgvEmployee.DataSource = employeeList;
            LoadListCategory();
            LoadListBillByDate(dtpkFromDay.Value, dtpkToDay.Value);
            LoadListProduct();
            AddProductBinding();
            LoadCategroByToCombobox(cbFoodCategory);
            AddCategoryBinding();
            LoadListTable();
            AddTableBinding();
            LoadListEmployee();
            AddEmployeeBinding();
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        void LoadListTable()
        {
            tableList.DataSource = TableDAO.Instance.LoadTableList();
        }

        void LoadListCategory()
        {
            categoryList.DataSource = CategoryDAO.Instance.GetListCategory();
        }

         void LoadListBillByDate(DateTime checkin,  DateTime checkout)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(checkin, checkout);
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDay.Value, dtpkToDay.Value);
        }

        void LoadListProduct()
        {
            productList.DataSource = ProductDAO.Instance.GetListProduct();
        }

        void LoadListEmployee()
        {
            employeeList.DataSource = EmployeeDAO.Instance.GetEmployeeList();
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListProduct();
        }
        void AddProductBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text",dtgvProduct.DataSource,"Name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvProduct.DataSource, "ID",true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvProduct.DataSource, "Price",true, DataSourceUpdateMode.Never));
            cbStatusFood.DataBindings.Add(new Binding("Text", dtgvProduct.DataSource, "Status", true, DataSourceUpdateMode.Never));

        }

        void AddEmployeeBinding()
        {
            txbEmployeeID.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource,"ID", true, DataSourceUpdateMode.Never));
            txbEmployeeName.DataBindings.Add(new Binding("Text",dtgvEmployee.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbEmployeeTime.DataBindings.Add(new Binding("Text",dtgvEmployee.DataSource,"Time", true, DataSourceUpdateMode.Never));
            txbEmployeePhoneNumber.DataBindings.Add(new Binding("Text",dtgvEmployee.DataSource,"Phonenumber", true, DataSourceUpdateMode.Never));
            txbEmployeeBankNumber.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "Banknumber", true, DataSourceUpdateMode.Never));
            txbEmployeeBankName.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "Bankname", true, DataSourceUpdateMode.Never));
            txbEmployeeSalary.DataBindings.Add(new Binding("Text", dtgvEmployee.DataSource, "Salary", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbCategoryID.DataBindings.Add(new Binding("Text", dtgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));

        }

        void AddTableBinding()
        {   
            txbTableName.DataBindings.Add(new Binding("Text",dtgvTable.DataSource, "Name", true,DataSourceUpdateMode.Never));
            txbTableID.DataBindings.Add(new Binding("Text",dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status", true, DataSourceUpdateMode.Never));
        }

        void LoadCategroByToCombobox(ComboBox cb)
        {
             cbFoodCategory.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if(dtgvProduct.SelectedCells.Count > 0)
            {
                int id = (int)dtgvProduct.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
                Category category = CategoryDAO.Instance.GetCategryByID(id);
                cbFoodCategory.SelectedItem = category;
                int index = -1;
                int i = 0;
                foreach (Category item in cbFoodCategory.Items) {
                    if (item.ID == category.ID) { 
                        index = i; break;
                    }
                    i++;
                }
                cbFoodCategory.SelectedIndex = index;   
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (ProductDAO.Instance.InsertFroduct(name, categoryID, price)) {
                MessageBox.Show("Thêm món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text; ;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            string status = cbStatusFood.Text;

            if (ProductDAO.Instance.UpdateFroduct(id,name, categoryID, price,status))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;
            int id = Convert.ToInt32(txbCategoryID.Text);

            string status = cbStatusFood.Text;

            if (CategoryDAO.Instance.UpdateCategory(id, name))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            LoadListCategory();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCategoryID.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListCategory();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (ProductDAO.Instance.DeleteFroduct(id))
            {
                MessageBox.Show("Xóa  món thành công");
                LoadListProduct();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            string status = cbTableStatus.Text;

            if (TableDAO.Instance.InsertTable(name, status))
            {
                MessageBox.Show("Thêm thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableID.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbTableID.Text);

            string status = cbTableStatus.Text;

            if (TableDAO.Instance.UpdateTable(id, name, status))
            {
                MessageBox.Show("Sửa thành công");
                LoadListTable();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnShowEmployee_Click(object sender, EventArgs e)
        {
            LoadListEmployee();
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string name = txbEmployeeName.Text;
            string time = txbEmployeeTime.Text;
            string salary = txbEmployeeSalary.Text;
            string phonenumber = txbEmployeePhoneNumber.Text;
            string banknumber = txbEmployeeBankNumber.Text;
            string bankname = txbEmployeeBankName.Text;


            if (EmployeeDAO.Instance.InsertEmployee(name, time, salary, phonenumber, banknumber, bankname))
            {
                MessageBox.Show("Thêm thành công");
                LoadListEmployee();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            string name = txbEmployeeName.Text;
            string time = txbEmployeeTime.Text;
            string salary = txbEmployeeSalary.Text;
            string phonenumber = txbEmployeePhoneNumber.Text;
            string banknumber = txbEmployeeBankNumber.Text;
            string bankname = txbEmployeeBankName.Text;

            int id = Convert.ToInt32(txbEmployeeID.Text);

            if (EmployeeDAO.Instance.UpdateEmployee(name, time, salary, phonenumber, banknumber, bankname,id))
            {
                MessageBox.Show("Sửa thành công");
                LoadListEmployee();
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbEmployeeID.Text);

            if (EmployeeDAO.Instance.DeleteEmployee(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListEmployee();
               
            }
            else
            {
                MessageBox.Show("Thất bại");
            }
        }
    }
}   

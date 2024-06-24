using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DAO
{
    public  class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new CategoryDAO();
                return instance;
            }
            private set
            {
                CategoryDAO.instance = value;
            }
        }

        private CategoryDAO() { }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query = "select * from Category";
            DataTable data = DataProvider.instance.ExecuteQuery(query);
            foreach(DataRow row in data.Rows )
            {
                Category category = new Category(row);
                list.Add(category);

            }

            return list;

        }

        public Category GetCategryByID(int id)
        {
            Category category = null;
            string query = "select * from Category where id = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }

        public bool InsertCategory(string name)
        {
            string query = "exec USP_InsertCategory N' " + name + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateCategory(int idCategory, string name)
        {
            string query = "exec USP_UpdateCategory  N'" + name + "',"  + idCategory;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory(int idProduct)
        {
            string query = "exec USP_DeleteProduct " + idProduct;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

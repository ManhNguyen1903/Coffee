using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance;

        public static ProductDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ProductDAO();
                return instance;
            }
            private set
            {
                ProductDAO.instance = value;
            }
        }

        private ProductDAO() { }

        public List<Product> GetFoodListByCategoryID(int id)
        {
            List<Product> list = new List<Product>();
            string query = "select * from Product where category_id =" + id + "and status = N'Còn hàng'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow row in data.Rows)
            {
                Product product = new Product(row);
                list.Add(product);
            }

            return list;
        }

        public List<Product> GetListProduct()
        {
            List<Product> list = new List<Product>();
            string query = "select * from Product";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                Product product = new Product(row);
                list.Add(product);
            }

            return list;
        }

        public bool InsertFroduct(string name, int id, float price)
        {
            string query = "exec USP_InsertProduct N' " + name + "'," + id + "," + price ;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateFroduct(int idProduct,string name, int id, float price, string status)
        {
            string query = "exec USP_UpdateProduct  N'"+ name + "'," + id + "," + price +"," + idProduct + ", N'" + status + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteFroduct(int idProduct)
        {
            string query = "exec USP_DeleteProduct " + idProduct ;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

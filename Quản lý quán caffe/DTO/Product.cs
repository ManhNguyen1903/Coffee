using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DTO
{
    public class Product
    {   
        public Product(int id, string name, int category_id, int price, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.CategoryID = category_id;
            this.Status = status;
        }
        public Product(DataRow row)
        {
            this.ID = (int) row["id"];
            this.Name = row["name"].ToString();
            this.Price = (int)row["price"];
            this.CategoryID = (int)row["category_id"];
            this.Status = row["status"].ToString();
        }
        private string status;
        private int price;
        private int categoryID;
        private string name;
        private int iD;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public int Price { get => price; set => price = value; }
        public string Status { get => status; set => status = value; }
    }
}

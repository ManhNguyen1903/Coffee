using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = Quản_lý_quán_caffe.DTO.Menu;

namespace Quản_lý_quán_caffe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        public static MenuDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuDAO();
                return instance;
            }
            private set
            {
                MenuDAO.instance = value;
            }
        }
        public MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> list = new List<Menu>();

            string query = "SELECT Product.name,BillInfo.count,Product.price,Product.price*BillInfo.count as totalPrice FROM Bill, BillInfo, Product WHERE BillInfo.idBill = Bill.id and BillInfo.idFood = Product.id and Bill.status = 0 and Bill.idTable = " + id;

            DataTable dataTable = DataProvider.instance.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                Menu menu = new Menu(row);
                list.Add(menu);
            }
            return list;
        }
    }
}

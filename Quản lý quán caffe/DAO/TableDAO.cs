using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DAO
{
    public  class TableDAO
    {
        private static TableDAO instance;
        public static TableDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TableDAO();
                return instance;
            }
            private set
            {
                TableDAO.instance = value;
            }
        }

        public static int TableWidth = 80;
        public static int TableHeight = 80;
        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");

            foreach (DataRow item in data.Rows) {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public bool InsertTable(string name, string status)
        {
            string query = "exec USP_InsertTable N' " + name + "', N'" + status + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateTable(int idTable, string name, string status)
        {
            string query = "exec USP_UpdateTable  N'" + name + "'," + idTable + ", N'" + status + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteTable(int idTable)
        {
            string query = "exec USP_DeleteTable " + idTable;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}

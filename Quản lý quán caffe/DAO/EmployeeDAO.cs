using Quản_lý_quán_caffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO instance;
        public static EmployeeDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new EmployeeDAO();
                return instance;
            }
            private set
            {
                EmployeeDAO.instance = value;
            }
        }

        private EmployeeDAO() { }

        public List<Employee> GetEmployeeList()
        {
            List<Employee> employees = new List<Employee>();

            string query = "select * from Employee";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Employee employee = new Employee(item);
                employees.Add(employee);
            }
            return employees;
        }

        public bool InsertEmployee(string name, string time, string salary, string phonenumber, string banknumber, string bankname)
        {
            string query = "exec USP_InsertEmployee N'" + name +"', N'" + time + "',N'" + salary +"','" + phonenumber +"', '" + banknumber +"'," +bankname;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool UpdateEmployee(string name, string time, string salary, string phonenumber, string banknumber, string bankname, int idEmployee)
        {
            string query = "exec USP_UpdateEmployee N'" + name + "', N'" + time + "',N'" + salary + "','" + phonenumber + "', '" + banknumber + "'," + bankname + "," +idEmployee;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteEmployee(int idEmployee)
        {
            string query = "exec USP_DeleteEmployee " + idEmployee;
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}   

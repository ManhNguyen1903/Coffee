using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_quán_caffe.DTO
{
    public class Employee
    {   
        public Employee(int id, string name, string time, string salary, string phonenumber, string banknumber, string bankname) {
            this.ID = id;
            this.Name = name;
            this.Time = time;
            this.Salary = salary;
            this.Phonenumber = phonenumber;
            this.Banknumber = banknumber;
            this.Bankname = bankname;
        }
        public Employee(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["fullname"].ToString();
            this.Time = row["time"].ToString();
            this.Salary = row["salary"].ToString();
            this.Phonenumber = row["phonenumber"].ToString();
            this.Banknumber = row["banknumber"].ToString();
            this.Bankname = row["bankname"].ToString();
        }


        private int iD;
        private string name;
        private string time;
        private string salary;
        private string phonenumber;
        private string banknumber;
        private string bankname;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        
        public string Time { get => time; set => time = value; }
        public string Salary { get => salary; set => salary = value; }
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }
        public string Banknumber { get => banknumber; set => banknumber = value; }
        public string Bankname { get => bankname; set => bankname = value; }
    }
}

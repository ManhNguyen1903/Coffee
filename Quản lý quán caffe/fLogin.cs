using Quản_lý_quán_caffe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quản_lý_quán_caffe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SignUp f = new SignUp();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {   
            string userName = txbUsername.Text;
            string passWord = txbPassword.Text;
            if (Login(userName, passWord)) {
                Admin admin = new Admin();
                this.Hide();
                admin.ShowDialog();
                this.Show();
            }
            else
                {
                MessageBox.Show("Sai toàn khoản hoặc mật khẩu.");
                }
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
    }
}

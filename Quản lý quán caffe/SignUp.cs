using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Quản_lý_quán_caffe
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {   
            string fullname = txbFullName.Text;
            string username = txbUsername.Text;
            string password = txbPassword.Text;
            string confirmPassword = txbConfirmPassword.Text;

            string connectionSTR = @"Data Source=LENOVO\MSSQLSERVER01;Initial Catalog=DB_Coffee;Integrated Security=True;Encrypt=False";
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            // Lưu thông tin tài khoản vào cơ sở dữ liệu
            try
            {   
                using (SqlConnection connection = new SqlConnection(connectionSTR))
                {
                    connection.Open();
                    string query = "INSERT INTO Account (fullname, username, password) VALUES (@FullName, @Username, @Password)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", fullname);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        int result = command.ExecuteNonQuery();

                        // Kiểm tra xem có bao nhiêu hàng bị ảnh hưởng
                        if (result < 0)
                        {
                            MessageBox.Show("Error inserting data into Database!");
                        }
                        else
                        {
                            MessageBox.Show("Registration successful!");

                            Admin admin = new Admin();
                            this.Hide();
                            admin.ShowDialog();
                            this.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}

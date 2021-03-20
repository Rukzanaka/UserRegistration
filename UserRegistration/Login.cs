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

namespace UserRegistration
{
    public partial class Login : Form
    {
        string connectionString = DataBaseConnection.connection;
        public Login()
        {
            InitializeComponent();
        }
        private void linkHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            Student LoggedStudent = LoginUser(txtEmail.Text, txtPassword.Text);
            if (LoggedStudent != null)
            {
                HomePage homePage = new HomePage(LoggedStudent);
                homePage.Show();
            }
        }
        private Student LoginUser(string Email,string Password)
        {
          
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("UserLogin", connection);
                command.CommandType = CommandType.StoredProcedure; ;


                command.Parameters.AddWithValue("Email", txtEmail.Text);
                command.Parameters.AddWithValue("Password", txtPassword.Text);


                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                Student student = new Student();

                student.Id       = Convert.ToInt32(reader["Id"]);
                student.Name     = reader["Name"].ToString();
                student.Email    = reader["Email"].ToString();
                student.Password = reader["Password"].ToString();
                student.Color    = reader["Color"].ToString();
                student.Left     = reader["Left"].ToString();
                student.Top      = reader["Top"].ToString();

                return student;

        }

        private void checkboxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(checkboxShowPassword.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}

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
using System.Text.RegularExpressions;

namespace UserRegistration
{
    public partial class Registration : Form
    {
        string connectionString = DataBaseConnection.connection;
        public Registration()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text == "" || txtEmail.Text == "" || txtPassword.Text == "" || txtConformPassword.Text == "")
            {
                lblError.Text = "Please fill all fields";
            }
            else if(txtPassword.Text != txtConformPassword.Text) 
            {
                lblError.Text = "Password does not match please correct";
            }
          else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand command = new SqlCommand("UserRegistration", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("Name", txtName.Text);
                command.Parameters.AddWithValue("Email", txtEmail.Text);
                command.Parameters.AddWithValue("Password", txtPassword.Text);
                command.Parameters.AddWithValue("ConformPassword", txtConformPassword.Text);
               

                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Registration success , Please login");

                txtName.Text = txtEmail.Text = txtPassword.Text = txtConformPassword.Text = "";
            }
        }
      
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
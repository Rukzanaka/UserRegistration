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
 
    public partial class HomePage : Form
    {
        Student _student;
        public HomePage(Student student)
        {
            InitializeComponent();
            _student = student;
        }
        private void HomePage_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.FromArgb(Convert.ToInt32(_student.Color));
            label3.Text = "Welcome";

            this.Text = txtName.Text  = _student.Name;
            txtEmail.Text = _student.Email;
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            SetColor(_student.Id);
        }
        private void SetColor(int studentId)
        {
            colorDialog1.ShowDialog();
            this.BackColor = Color.FromArgb(colorDialog1.Color.ToArgb());

            SqlConnection connection = new SqlConnection(DataBaseConnection.connection);
            connection.Open();

            SqlCommand command = new SqlCommand("SetColor", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Id", studentId);
            command.Parameters.AddWithValue("Color", colorDialog1.Color.ToArgb().ToString());

            command.ExecuteNonQuery();
        }
    }
}

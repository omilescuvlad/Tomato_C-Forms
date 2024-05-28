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

namespace proiect_mtp
{
    public partial class login : Form
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=user;Integrated Security=True";
        public login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT role, userID FROM userTable WHERE username=@Username AND password=@Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", username_text.Text);
                    cmd.Parameters.AddWithValue("@Password", password_text.Text);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string role = result.ToString();
                        MessageBox.Show($"Login successful!");

                        if (role == "client")
                        { 
                            this.Hide();
                            client_menu foodForm = new client_menu();
                            foodForm.FormClosed += (s, args) => this.Close(); 
                            foodForm.Show(); 
                        }
                        if (role == "manager")
                        {
                            this.Hide();
                            manager_menu editFoodForm = new manager_menu();
                            editFoodForm.FormClosed += (s, args) => this.Close(); 
                            editFoodForm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class client_menu : Form
    {
        public client_menu()
        {
            InitializeComponent();
        }
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=menu;Integrated Security=True";

        private void client_menu_Load(object sender, EventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT foodID, nume, pret, calorii FROM menuTablee";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    foodDataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void SearchData(string searchTerm)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT foodID, nume, pret, calorii FROM menuTablee WHERE nume LIKE @searchTerm";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    foodDataGridView.DataSource = dataTable;
                    if (dataTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Itemul este disponibil");
                    }
                    else
                    {
                        MessageBox.Show("Itemul nu a fost găsit");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchData(searchTextBox.Text);
        }
    }
}

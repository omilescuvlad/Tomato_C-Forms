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
    public partial class manager_menu : Form
    {
        public manager_menu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void manager_menu_Load(object sender, EventArgs e)
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
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=menu;Integrated Security=True";

        private void LoadData()
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
                finally
                {
                    conn.Close();
                }
            }
        }



        private void foodDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            
                if (foodDataGridView.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = foodDataGridView.SelectedRows[0];
                    nameTextBox.Text = row.Cells["nume"].Value.ToString();
                    priceTextBox.Text = row.Cells["pret"].Value.ToString();
                    caloriesTextBox.Text = row.Cells["calorii"].Value.ToString();
                }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (foodDataGridView.SelectedRows.Count > 0)
                {
                    int foodID = Convert.ToInt32(foodDataGridView.SelectedRows[0].Cells["foodID"].Value);
                    string nume = nameTextBox.Text;
                    int pret = Convert.ToInt32(priceTextBox.Text);
                    int calorii = Convert.ToInt32(caloriesTextBox.Text);

                    try
                    {
                        conn.Open();
                        string query = "UPDATE menuTablee SET nume = @nume, pret = @pret, calorii = @calorii WHERE foodID = @foodID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@foodID", foodID);
                        cmd.Parameters.AddWithValue("@nume", nume);
                        cmd.Parameters.AddWithValue("@pret", pret);
                        cmd.Parameters.AddWithValue("@calorii", calorii);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data updated successfully.");
                        LoadData();
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
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (foodDataGridView.SelectedRows.Count > 0)
                {
                    int foodID = Convert.ToInt32(foodDataGridView.SelectedRows[0].Cells["foodID"].Value);

                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM menuTablee WHERE foodID = @foodID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@foodID", foodID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Item deleted successfully.");
                        LoadData();
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
        }
       
    }
}

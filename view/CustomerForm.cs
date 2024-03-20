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

namespace gestion_stock.view
{
    public partial class CustomerForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();
        }

     

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Client", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void dvgclient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModule = new CustomerModuleForm();
                customerModule.labelCustomerId.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                customerModule.textCustomerLastName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModule.textCustomerFirstName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModule.textCustomerAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                customerModule.textCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                customerModule.textCustomerCountry.Text = dgvCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();
                customerModule.textCustomerCity.Text = dgvCustomer.Rows[e.RowIndex].Cells[6].Value.ToString();
                customerModule.textCustomerEmail.Text = dgvCustomer.Rows[e.RowIndex].Cells[7].Value.ToString();

                customerModule.btnSave.Enabled = false;
                customerModule.btnUpdate.Enabled = true;
                customerModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Etes vous sûre de vouloir supprimer ce client?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM Client WHERE ID_Client LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Client a été supprimé avec succée.");
                }
            }
            LoadCustomer();
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            CustomerModuleForm moduleForm = new CustomerModuleForm();
            moduleForm.ShowDialog();
            LoadCustomer();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void disconnectButton_Click(object sender, EventArgs e)
        {
            // Get a reference to the 'menu' form and call its disconnection method
            var menuForm = Application.OpenForms["menu"] as menu;
            if (menuForm != null)
            {
                menuForm.DisconnectUser();
            }
            this.Close(); // Close the current form after disconnecting
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {

        }
    }
}

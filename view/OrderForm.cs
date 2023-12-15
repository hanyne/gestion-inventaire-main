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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Commande", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[4].ToString(), dr[5].ToString(), dr[7].ToString(), dr[8].ToString(), dr[11].ToString());
/*                total += Convert.ToInt32(dr[11].ToString());*/
            }
            dr.Close();
            con.Close();

            labelQty.Text = i.ToString();
            labelTotal.Text = total.ToString();
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                OrderModuleForm orderModule = new OrderModuleForm();
                orderModule.labelOrderId.Text = dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString();
               /* orderModule.orderDate.Text = dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString();*/
                orderModule.textCustomerId.Text = dgvOrder.Rows[e.RowIndex].Cells[2].Value.ToString();
                orderModule.textCustomerFirstName.Text = dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString();
                orderModule.textCustomerPhone.Text = dgvOrder.Rows[e.RowIndex].Cells[4].Value.ToString();
                orderModule.textProductName.Text = dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString();
                orderModule.textProductQuantity.Text = dgvOrder.Rows[e.RowIndex].Cells[6].Value.ToString();

                orderModule.btnSave.Enabled = false;
                orderModule.btnUpdate.Enabled = true;
                orderModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Etes vous sûre de vouloir supprimer cette commande?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM Commande WHERE ID_Commande LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Commande a été supprimé avec succée.");
                }
            }
            LoadOrder();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

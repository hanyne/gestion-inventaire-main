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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        int qty = 0;

        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Client WHERE CONCAT(ID_Client, Nom_Client, Prenom_Client) LIKE '%" + textSearchCustomer.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[4].ToString());
            }
            dr.Close();
            con.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Produit WHERE CONCAT(Id_Produit, Nom_Produit, Prix_Produit, Quantite_Produit) LIKE '%" + textSearchProduct.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void textProductQuantity_ValueChanged(object sender, EventArgs e)
        {
            textProductQuantity.ValueChanged -= textProductQuantity_ValueChanged;

            GetQty();
            if (Convert.ToInt16(textProductQuantity.Value) > qty)
            {
                MessageBox.Show("Quantité dans le stock n'est pas suffisente !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textProductQuantity.Value = textProductQuantity.Value - 1;
                return;
            }
            if (Convert.ToInt16(textProductQuantity.Value) > 0)
            {
                int total = Convert.ToInt16(textProductPrice.Text) * Convert.ToInt16(textProductQuantity.Value);
                textTotal.Text = total.ToString();
            }

            textProductQuantity.ValueChanged += textProductQuantity_ValueChanged;

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textCustomerId.Text = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
            textCustomerLastName.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            textCustomerFirstName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            textCustomerPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            textProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            textProductPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
            textProductCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            qty = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (textCustomerId.Text == "")
                {
                    MessageBox.Show("Séléctionnez un client s'il vous plait", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textProductId.Text == "")
                {
                    MessageBox.Show("Séléctionnez un produit s'il vous plait", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Etes vous sure que vous voulez ajouter cette commande?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO Commande(DATE_Commande, ID_Client, Nom_Client, Prenom_Client, Telephone_Client, Id_Produit, Nom_Produit, Quantite_Produit, Prix_Produit, Id_Categorie, totale)VALUES(@DATE_Commande, @ID_Client, @Nom_Client, @Prenom_Client, @Telephone_Client, @Id_Produit, @Nom_Produit, @Quantite_Produit, @Prix_Produit, @Id_Categorie, @totale)", con);
                    cm.Parameters.AddWithValue("@DATE_Commande", orderDate.Value);
                    cm.Parameters.AddWithValue("@ID_Client", textCustomerId.Text);
                    cm.Parameters.AddWithValue("@Nom_Client", textCustomerLastName.Text);
                    cm.Parameters.AddWithValue("@Prenom_Client", textCustomerFirstName.Text);
                    cm.Parameters.AddWithValue("@Telephone_Client", textCustomerPhone.Text);
                    cm.Parameters.AddWithValue("@Id_Produit", textProductId.Text);
                    cm.Parameters.AddWithValue("@Nom_Produit", textProductName.Text);
                    cm.Parameters.AddWithValue("@Quantite_Produit", textProductQuantity.Text);
                    cm.Parameters.AddWithValue("@Prix_Produit", textProductPrice.Text);
                    cm.Parameters.AddWithValue("@Id_Categorie", textProductCategory.Text);
                    cm.Parameters.AddWithValue("@totale", textTotal.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Commande à été enregistré avec succée.");

                    cm = new SqlCommand("UPDATE Produit SET Quantite_Produit=(Quantite_Produit-@Quantite_Produit) WHERE Id_Produit LIKE '" + textProductId.Text + "' ", con);
                    cm.Parameters.AddWithValue("@Quantite_Produit", Convert.ToInt16(textProductQuantity.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textCustomerFirstName.Clear();
            textCustomerLastName.Clear();
            textCustomerId.Clear();
            textCustomerPhone.Clear();
            textProductCategory.Clear();
            textProductId.Clear();
            textProductName.Clear();
            textProductPrice.Clear();
            textProductQuantity.Value = 1;
            orderDate.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void textProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez mettre à jour cette commande?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE Commande SET DATE_Commande=@DATE_Commande, ID_Client=@ID_Client, Nom_Client=@Nom_Client, Prenom_Client=@Prenom_Client, Telephone_Client=@Telephone_Client, Id_Produit=@Id_Produit, Nom_Produit=@Nom_Produit, Quantite_Produit=@Quantite_Produit, Prix_Produit=@Prix_Produit, Id_Categorie=@Id_Categorie, totale=@totale WHERE ID_Commande LIKE '" + labelOrderId.Text + "'", con);
                    cm.Parameters.AddWithValue("@DATE_Commande", orderDate.Value);
                    cm.Parameters.AddWithValue("@ID_Client", textCustomerId.Text);
                    cm.Parameters.AddWithValue("@Nom_Client", textCustomerLastName.Text);
                    cm.Parameters.AddWithValue("@Prenom_Client", textCustomerFirstName.Text);
                    cm.Parameters.AddWithValue("@Telephone_Client", textCustomerPhone.Text);
                    cm.Parameters.AddWithValue("@Id_Produit", textProductId.Text);
                    cm.Parameters.AddWithValue("@Nom_Produit", textProductName.Text);
                    cm.Parameters.AddWithValue("@Quantite_Produit", textProductQuantity.Text);
                    cm.Parameters.AddWithValue("@Prix_Produit", textProductPrice.Text);
                    cm.Parameters.AddWithValue("@Id_Categorie", textProductCategory.Text);
                    cm.Parameters.AddWithValue("@totale", textTotal.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Mise à jour du commande a été complit avec succée!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT Quantite_Produit FROM Produit WHERE Id_Produit='" + textProductId.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void orderDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

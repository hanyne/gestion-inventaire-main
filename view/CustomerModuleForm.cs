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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez ajouter ce client?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO Client(Nom_Client, Prenom_Client, Adresse_Client, Telephone_Client, Pays_Client, Ville_Client, Email)VALUES(@Nom_Client,@Prenom_Client,@Adresse_Client,@Telephone_Client,@Pays_Client,@Ville_Client,@Email)", con);
                    cm.Parameters.AddWithValue("@Nom_Client", textCustomerFirstName.Text);
                    cm.Parameters.AddWithValue("@Prenom_Client", textCustomerLastName.Text);
                    cm.Parameters.AddWithValue("@Adresse_Client", textCustomerAddress.Text);
                    cm.Parameters.AddWithValue("@Telephone_Client", textCustomerPhone.Text);
                    cm.Parameters.AddWithValue("@Pays_Client", textCustomerCountry.Text);
                    cm.Parameters.AddWithValue("@Ville_Client", textCustomerCity.Text);
                    cm.Parameters.AddWithValue("@Email", textCustomerEmail.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Client à été enregistré avec succée.");
                    Clear();
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
            textCustomerCity.Clear();
            textCustomerEmail.Clear();
            textCustomerCountry.Clear();
            textCustomerAddress.Clear();
            textCustomerPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez mettre à jour ce client?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE Client SET Nom_Client=@Nom_Client, Prenom_Client=@Prenom_Client, Adresse_Client=@Adresse_Client, Telephone_Client=@Telephone_Client, Pays_Client=@Pays_Client, Ville_Client=@Ville_Client, Email=@Email WHERE ID_Client LIKE '" + labelCustomerId.Text + "'", con);
                    cm.Parameters.AddWithValue("@Nom_Client", textCustomerFirstName.Text);
                    cm.Parameters.AddWithValue("@Prenom_Client", textCustomerLastName.Text);
                    cm.Parameters.AddWithValue("@Adresse_Client", textCustomerAddress.Text);
                    cm.Parameters.AddWithValue("@Telephone_Client", textCustomerPhone.Text);
                    cm.Parameters.AddWithValue("@Pays_Client", textCustomerCountry.Text);
                    cm.Parameters.AddWithValue("@Ville_Client", textCustomerCity.Text);
                    cm.Parameters.AddWithValue("@Email", textCustomerEmail.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Mise à jour du client a été complit avec succée!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void CustomerModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class SupplierModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();

        public SupplierModuleForm()
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
                if (MessageBox.Show("Etes vous sure que vous voulez ajouter ce fournisseur?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO Fournisseur(Nom_Fournisseur,Adresse_Fournisseur,Telephone_Fournisseur,Email_Fournisseur)VALUES(@Nom_Fournisseur,@Adresse_Fournisseur,@Telephone_Fournisseur,@Email_Fournisseur)", con);
                    cm.Parameters.AddWithValue("@Nom_Fournisseur", textNomFournisseur.Text);
                    cm.Parameters.AddWithValue("@Adresse_Fournisseur", textAdresseFournisseur.Text);
                    cm.Parameters.AddWithValue("@Telephone_Fournisseur", textTelephoneFournisseur.Text);
                    cm.Parameters.AddWithValue("@Email_Fournisseur", textEmailFournisseur.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Fournisseur à été ajouté avec succée.");
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
            textNomFournisseur.Clear();
            textAdresseFournisseur.Clear();
            textEmailFournisseur.Clear();
            textTelephoneFournisseur.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez mettre à jour ce fournisseur?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE Fournisseur SET Nom_Fournisseur=@Nom_Fournisseur, Adresse_Fournisseur=@Adresse_Fournisseur, Telephone_Fournisseur=@Telephone_Fournisseur, Email_Fournisseur=@Email_Fournisseur WHERE ID_Fournisseur LIKE '" + labelSupplierId.Text + "'", con);
                    cm.Parameters.AddWithValue("@Nom_Fournisseur", textNomFournisseur.Text);
                    cm.Parameters.AddWithValue("@Adresse_Fournisseur", textAdresseFournisseur.Text);
                    cm.Parameters.AddWithValue("@Telephone_Fournisseur", textTelephoneFournisseur.Text);
                    cm.Parameters.AddWithValue("@Email_Fournisseur", textEmailFournisseur.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Mise à jour du fournisseur a été complit avec succée!");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void SupplierModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}

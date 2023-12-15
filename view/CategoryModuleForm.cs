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
    public partial class CategoryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public CategoryModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez ajouter cette catégorie?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO Categorie(Nom_categorie)VALUES(@Nom_categorie)", con);
                    cm.Parameters.AddWithValue("@Nom_categorie", textNomCategorie.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Catégorie à été ajouté avec succée.");
                    textNomCategorie.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez mettre à jour cette catégorie?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE Categorie SET Nom_categorie=@Nom_categorie WHERE ID_CATEGORIE LIKE '" + labelCategoryId.Text + "'", con);
                    cm.Parameters.AddWithValue("@Nom_categorie", textNomCategorie.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Mise à jour du catégorie a été complit avec succée!");
                    this.Dispose();
                    textNomCategorie.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CategoryModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}

using gestion_stock.classe;
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
    public partial class CategoryForm : Form
    {
        public static CategoryForm CategorieList;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public static CategoryForm Instance
        {
            get
            {
                if (CategorieList == null)
                {
                    CategorieList = new CategoryForm();

                }
                return CategorieList;
            }
        }

        private void dvgclient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm categoryModule = new CategoryModuleForm();
                categoryModule.labelCategoryId.Text = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
                categoryModule.textNomCategorie.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();

                categoryModule.btnSave.Enabled = false;
                categoryModule.btnUpdate.Enabled = true;
                categoryModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Etes vous sûre de vouloir supprimer cette catégorie?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM Categorie WHERE ID_CATEGORIE LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Catégorie a été supprimé avec succée.");
                }
            }
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Categorie", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            CategoryModuleForm moduleForm = new CategoryModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadCategory();
        }

        private void btnmodifierclient_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textrecherche_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnsupprimerclient_Click(object sender, EventArgs e)
        {

        }
    }
}

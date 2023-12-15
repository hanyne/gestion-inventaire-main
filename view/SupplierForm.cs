using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gestion_stock.view
{
    public partial class SupplierForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public SupplierForm()
        {
            InitializeComponent();
            LoadSupplier();
        }

        public void LoadSupplier()
        {
            int i = 0;
            dgvSupplier.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Fournisseur", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvSupplier.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            SupplierModuleForm moduleForm = new SupplierModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadSupplier();
        }

        private void dgvSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvSupplier.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                SupplierModuleForm supplierModule = new SupplierModuleForm();
                supplierModule.labelSupplierId.Text = dgvSupplier.Rows[e.RowIndex].Cells[0].Value.ToString();
                supplierModule.textNomFournisseur.Text = dgvSupplier.Rows[e.RowIndex].Cells[1].Value.ToString();
                supplierModule.textAdresseFournisseur.Text = dgvSupplier.Rows[e.RowIndex].Cells[2].Value.ToString();
                supplierModule.textTelephoneFournisseur.Text = dgvSupplier.Rows[e.RowIndex].Cells[3].Value.ToString();
                supplierModule.textEmailFournisseur.Text = dgvSupplier.Rows[e.RowIndex].Cells[4].Value.ToString();

                supplierModule.btnSave.Enabled = false;
                supplierModule.btnUpdate.Enabled = true;
                supplierModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Etes vous sûre de vouloir supprimer ce fournisseur?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM Fournisseur WHERE ID_Fournisseur LIKE '" + dgvSupplier.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Fournisseur a été supprimé avec succée.");
                }
            }
            LoadSupplier();
        }
    }
}

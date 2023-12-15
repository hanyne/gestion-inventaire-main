using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_stock.view
{
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM Produit", con);
            con.Open();
            dr = cm.ExecuteReader();

            while (dr.Read())
            {
                i++;
                // Assuming dr[4] contains image data as byte[] or DBNull

                if (dr[4] != DBNull.Value)
                {
                    byte[] imageData = (byte[])dr[4]; // Assuming dr[4] contains the byte array of image data

                    // Create an Image object from the byte array
                    Image img = null;
                    if (imageData != null && imageData.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            img = Image.FromStream(ms);

                            // Resize the image to fit the DataGridView cell size (e.g., 50x50)
                            int cellWidth = dgvProduct.Columns[4].Width;
                            int cellHeight = dgvProduct.RowTemplate.Height;
                            img = ResizeImage(img, cellWidth, cellHeight);
                        }
                    }

                    // Add other data to the DataGridView
                    dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), null, dr[5].ToString());

                    // Set the image to the specific cell in DataGridView
                    DataGridViewImageCell cell = new DataGridViewImageCell();
                    cell.Value = img;

                    dgvProduct.Rows[i - 1].Cells[4] = cell; // Assuming the image column index is 4
                }
                else
                {
                    // If the image data is NULL in the database, handle it accordingly
                    // For example, add a placeholder or handle the empty image
                    dgvProduct.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), null, dr[5].ToString());
                }
            }

            dr.Close();
            con.Close();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModule = new ProductModuleForm();

                productModule.labelProductID.Text = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                productModule.textProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModule.textProductQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModule.textProductPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();

                productModule.comboCat.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();

                productModule.btnSave.Enabled = false;
                productModule.btnUpdate.Enabled = true;
                productModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Etes vous sûre de vouloir supprimer ce produit?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM Produit WHERE Id_Produit LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Produit a été supprimé avec succés.");
                }
            }
            LoadProduct();
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            ProductModuleForm moduleForm = new ProductModuleForm();
            moduleForm.ShowDialog();
            LoadProduct();
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

        private void btnmodifierclient_Click(object sender, EventArgs e)
        {

        }

        private void btnsupprimerclient_Click(object sender, EventArgs e)
        {

        }

        // Méthode pour redimensionner l'image
        private Image ResizeImage(Image imgToResize, int newWidth, int newHeight)
        {
            Bitmap bitmap = new Bitmap(newWidth, newHeight);
            using (Graphics graphic = Graphics.FromImage(bitmap))
            {
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(imgToResize, 0, 0, newWidth, newHeight);
            }
            return bitmap;
        }
    }
}

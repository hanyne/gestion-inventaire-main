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
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\eki\Desktop\gestion-inventaire c#\gestion-inventaire-main\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCat.Items.Clear();
            cm = new SqlCommand("SELECT ID_CATEGORIE, Nom_categorie FROM Categorie", con);
            con.Open();
            dr = cm.ExecuteReader();

            while (dr.Read())
            {
                comboCat.Items.Add(dr[0].ToString() + "- " + dr[1].ToString());
            }

            dr.Close();
            con.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez ajouter ce produit?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (productPicture.Image != null) // Vérifie si une image est chargée dans le PictureBox
                    {
                        System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                        productPicture.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] imageBytes = memoryStream.ToArray();

                        cm = new SqlCommand("INSERT INTO Produit(Nom_Produit, Quantite_Produit, Prix_Produit, Image_Produit, ID_CATEGORIE)VALUES(@Nom_Produit,@Quantite_Produit,@Prix_Produit, @Image_Produit, @ID_CATEGORIE)", con);
                        cm.Parameters.AddWithValue("@Nom_Produit", textProductName.Text);
                        cm.Parameters.AddWithValue("@Quantite_Produit", Convert.ToInt16(textProductQuantity.Text));
                        cm.Parameters.AddWithValue("@Prix_Produit", Convert.ToDouble(textProductPrice.Text));
                        cm.Parameters.AddWithValue("@Image_Produit", imageBytes);
                        string[] categoryId = comboCat.Text.Split('-');
                        cm.Parameters.AddWithValue("@ID_CATEGORIE", categoryId[0]);
                        con.Open();
                        cm.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Produit à été enregistré avec succée.");
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Veuillez charger une image pour le produit.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Clear()
        {
            textProductName.Clear();
            textProductPrice.Clear();
            textProductQuantity.Clear();
            comboCat.Text = "";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Etes vous sure que vous voulez mettre à jour ce produit?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    productPicture.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] imageBytes = memoryStream.ToArray();

                    cm = new SqlCommand("UPDATE Produit SET Nom_Produit=@Nom_Produit, Quantite_Produit=@Quantite_Produit, Prix_Produit=@Prix_Produit, ID_CATEGORIE=@ID_CATEGORIE, Image_Produit=@Image_Produit WHERE Id_Produit LIKE '" + labelProductID.Text + "'", con);
                    cm.Parameters.AddWithValue("@Nom_Produit", textProductName.Text);
                    cm.Parameters.AddWithValue("@Quantite_Produit", textProductQuantity.Text);
                    cm.Parameters.AddWithValue("@Prix_Produit", textProductPrice.Text);
                    cm.Parameters.AddWithValue("@Image_Produit", imageBytes);
                    cm.Parameters.AddWithValue("@ID_CATEGORIE", comboCat.Text[0]);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Mise à jour du produit a été complit avec succée!");
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
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Charger l'image
                    Image selectedImage = Image.FromFile(openFileDialog1.FileName);

                    // Redimensionner l'image à une taille plus petite (par exemple, 200x200 pixels)
                    int newWidth = 200;
                    int newHeight = 200;
                    Image resizedImage = ResizeImage(selectedImage, newWidth, newHeight);

                    // Afficher l'image redimensionnée dans votre PictureBox
                    productPicture.Image = resizedImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite lors du chargement de l'image : " + ex.Message);
                }
            }
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



        private void comboCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProductModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace gestion_stock.classe
{
    internal class Cls_Categorie
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Ed\1ing\Sem1\C#\Projects\gestion_stock\gestion_stock.mdf;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        /*public bool Ajouter_Categorie(string Nom, string Prenom, string Adresse, string Telephone, string Email, string Pays, string Ville)
        {
            cm = new SqlCommand("INSERT INTO tbCategory(nom_categorie)VALUES(@nom_categorie)", con);
            cm.Parameters.AddWithValue("@nom_categorie", .Text);
            con.Open();
            cm.ExecuteNonQuery();
            con.Close();
        }*/
    }
}

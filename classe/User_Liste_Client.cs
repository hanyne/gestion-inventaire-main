using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_stock.classe

{
   
    public partial class User_Liste_Client : UserControl
    {
        public static User_Liste_Client Userclient;
        private dbStockContext db;
        //Creer une instance pour le usercontrole
        public static User_Liste_Client Instance
        {
            get
            {
                if (Userclient == null) { 
                 Userclient = new User_Liste_Client();
                
                }
                return Userclient;
            }
        }
        
       public User_Liste_Client()
        {
            InitializeComponent();
            db = new dbStockContext();
        }
        ///Ajouter dans la datagridview
       
        public void Actualisedatagrid()
        {
            db=new dbStockContext();
            dvgclient.Rows.Clear();//vider datagrid view
            foreach(var S in db.Clients)
            {
                //ajouter les lignes dans datagrid
                dvgclient.Rows.Add(false, S.ID_Client, S.Nom_Client, S.Prenom_Client, S.Adresse_Client, S.Telephone_Client, S.Email, S.Ville_Client, S.Pays_Client);
                
            }
        }
        //verfier combien de ligne est selectionner 
        public string SelectVerif()
        {
            int Nbrselect = 0;
            for (int i = 0; i<dvgclient.Rows.Count;i++ )
            {
                if ((bool)dvgclient.Rows[i].Cells[0].Value==true)
                {
                    Nbrselect++;
                }
            }
            if(Nbrselect == 0 ) {
                return " Selectionner le client Que vous-voulez modifier ";
            }
            if (Nbrselect > 1)
            {
                return " Selectionner seulement 1 client pour modifier ";

            }
            return null;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            view.Ajoute_Modifier_client frmclient = new view.Ajoute_Modifier_client(this);
            if (SelectVerif() == null)
            {
                for (int i = 0;i<dvgclient.Rows.Count;i++)
                    if ((bool)dvgclient.Rows[i].Cells[0].Value == true)
                    {
                        frmclient.IDselect = (int)dvgclient.Rows[i].Cells[1].Value;
                        frmclient.textnomclient.Text = dvgclient.Rows[i].Cells[2].Value.ToString();
                        frmclient.textPrenom.Text = dvgclient.Rows[i].Cells[3].Value.ToString();
                        frmclient.textAdresse.Text = dvgclient.Rows[i].Cells[4].Value.ToString();
                        frmclient.textTelephone.Text = dvgclient.Rows[i].Cells[5].Value.ToString();
                        frmclient.textEmail.Text = dvgclient.Rows[i].Cells[6].Value.ToString();
                        frmclient.textPays.Text = dvgclient.Rows[i].Cells[7].Value.ToString();
                        frmclient.textVille.Text = dvgclient.Rows[i].Cells[8].Value.ToString();
                    }
                frmclient.lblTittre.Text = "Modifer Client";
                frmclient.btnActualiser.Visible = false;
                frmclient.ShowDialog();
            }else
            {
                MessageBox.Show(SelectVerif(), "modification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void User_Liste_Client_Load(object sender, EventArgs e)
        {
            Actualisedatagrid();  


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textrecherche.Text=="Rechercher")
            {
                textrecherche.Text = "";
                textrecherche.ForeColor = Color.Black;
            }
        }

        private void User_Liste_Client_Leave(object sender, EventArgs e)
        {
           
        }

        private void dvgclient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void bntajouteclient_Click(object sender, EventArgs e)
        {
            //affiche form ajout 
            view.Ajoute_Modifier_client frmclient = new view.Ajoute_Modifier_client(this);
            frmclient.ShowDialog();
        }
    }
}

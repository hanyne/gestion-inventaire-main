using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net.Mail;
using gestion_stock.classe;

namespace gestion_stock.view
{
    public partial class Ajoute_Modifier_client : Form
    {
        private UserControl usclient;

        public Ajoute_Modifier_client(UserControl userC)
        {
            InitializeComponent();
            this.usclient = userC;
        }
        //champs obligatoires
        String testobligatoire()
        {
            if(textnomclient.Text==""|| textnomclient.Text=="Nom de Client")
            {
                return "Entrer le Nom de Client";
            }
            if (textPrenom.Text == "" || textPrenom.Text == "Prenom de Client")
            {
                return "Entrer le Prenom de Client";
            }
            if (textAdresse.Text == "" || textAdresse.Text == "Adresse  Client")
            {
                return "Entrer l'adresse de Client";
            }
            if (textTelephone.Text == "" || textTelephone.Text == "Telephone Client")
            {
                return "Entrer le Telephone de Client";
            }
            if (textEmail.Text == "" || textEmail.Text == "Email Client")
            {
                return "Entrer l'email de Client";
            }
            if (textVille.Text == "" || textVille.Text == "Ville Client")
            {
                return "Entrer la ville de Client";
            }
            if (textPays.Text == "" || textPays.Text == "Pays Client")
            {
                return "Entrer le pays de Client";
            }
            //email valide ou nn
            if (textEmail.Text!=""||textEmail.Text!= "Email Client")
            {
                
                try
                {
                   new MailAddress(textEmail.Text);//verifier le mail valide ou nnn
                }catch (Exception e) {
                    return "Email Invalide";
                }

            }

                return null;
        }
        

        private void textNom_Enter(object sender, EventArgs e)
        {
            
        }

        private void textNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPrenom_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPrenom_Enter(object sender, EventArgs e)
        {
            if (textPrenom.Text == "Prenom de Client")
            {
                textPrenom.Text = "";

                textPrenom.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textAdresse_Enter(object sender, EventArgs e)
        {
            if (textAdresse.Text == "Adresse  Client")
            {
                textAdresse.Text = "";

                textAdresse.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textTelephone_Enter(object sender, EventArgs e)
        {
            if(textTelephone.Text == "Telephone Client")
            {
                textTelephone.Text = "";

                textTelephone.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textEmail_Enter(object sender, EventArgs e)
        {
            if(textEmail.Text == "Email Client")
            {
                textEmail.Text = "";

                textEmail.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textVille_Enter(object sender, EventArgs e)
        {
            if (textVille.Text == "Ville Client")
            {
                textVille.Text = "";

                textVille.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textPays_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPays_Enter(object sender, EventArgs e)
        {
            if (textPays.Text == "Pays Client")
            {
                textPays.Text = "";

                textPays.ForeColor = Color.WhiteSmoke;
            }
        }

        private void btnexitajout_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textTelephone_TextChanged(object sender, EventArgs e)
        {

        }

        private void textNomClient_Enter(object sender, EventArgs e)
        {
            
        }

        private void textnomclient_Enter_1(object sender, EventArgs e)
        {
            if (textnomclient.Text == "Nom de Client")
            {
                textnomclient.Text = "";

                textnomclient.ForeColor = Color.WhiteSmoke;
            }

        }

        private void textnomclient_Leave(object sender, EventArgs e)
        {
            if (textnomclient.Text == "")
            {
                textnomclient.Text = "Nom de Client";

                textnomclient.ForeColor = Color.WhiteSmoke;
            }

        }

        private void textPrenom_Leave(object sender, EventArgs e)
        {

            if (textPrenom.Text == "")
            {
                textPrenom.Text = "Prenom de Client";

                textPrenom.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textAdresse_TextChanged(object sender, EventArgs e)
        {

        }

        private void textAdresse_Leave(object sender, EventArgs e)
        {
            if (textAdresse.Text == "")
            {
                textAdresse.Text = "Adresse  Client";

                textAdresse.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textTelephone_Leave(object sender, EventArgs e)
        {
            if (textTelephone.Text == "")
            {
                textTelephone.Text = "Telephone Client";

                textTelephone.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textEmail_Leave(object sender, EventArgs e)
        {
            if (textEmail.Text == "")
            {
                textEmail.Text = "Email Client";

                textEmail.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textVille_Leave(object sender, EventArgs e)
        {
            if (textVille.Text == "")
            {
                textVille.Text = "Ville Client";

                textVille.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textPays_Leave(object sender, EventArgs e)
        {
            if (textPays.Text == "")
            {
                textPays.Text = "Pays Client";

                textPays.ForeColor = Color.WhiteSmoke;
            }
        }

        private void textTelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            //controle saisie num_telephone 
            if(e.KeyChar<48 || e.KeyChar > 57 )//code asci des num
            {
                e.Handled = true;
            }
            if(e.KeyChar==8)
            { 
                e.Handled = false;
            }
        }
        public int IDselect;
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if(testobligatoire()!=null)
            {
                MessageBox.Show(testobligatoire(),"Obligatoire",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }else
            if(lblTittre.Text=="Ajouter Client")
            
            {
                classe.Cls_client clclient = new classe.Cls_client();
                if(clclient.Ajouter_Client(textnomclient.Text,textPrenom.Text,textAdresse.Text,textTelephone.Text,textEmail.Text,textPays.Text,textVille.Text)==true)
                {
                    MessageBox.Show("Client Ajouté avec Succées", "Ajouer", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    (usclient as User_Liste_Client).Actualisedatagrid();
                }else
                {
                    MessageBox.Show("Nom et Prenom de client existe déja", "Ajouter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                }else {
                classe.Cls_client clclient = new classe.Cls_client();
                
                DialogResult R = MessageBox.Show("Voulez-vous vraiment modifier ce client", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                    clclient.Modifier_Client(IDselect, textnomclient.Text, textPrenom.Text, textAdresse.Text, textTelephone.Text, textEmail.Text, textPays.Text, textVille.Text);

                    // actualise datagrid
                    (usclient as User_Liste_Client).Actualisedatagrid();
                    MessageBox.Show("client modifier avec succes", "modification", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                }else
                {
                    MessageBox.Show("Modification est annulé", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        private void btnActualiser_Click(object sender, EventArgs e)
        {
            //Vider les champs
            textnomclient.Text = "Nom de Client";textnomclient.ForeColor = Color.Silver;
            textPrenom.Text = "Prenom de Client"; textPrenom.ForeColor = Color.Silver;
            textAdresse.Text = "Adresse Client"; textAdresse.ForeColor = Color.Silver;
            textTelephone.Text = "Telephone Client"; textTelephone.ForeColor = Color.Silver;
            textEmail.Text = "Email Client"; textEmail.ForeColor = Color.Silver;
            textVille.Text = "Ville Client"; textVille.ForeColor = Color.Silver;
            textPays.Text = "Pays Client"; textPays.ForeColor = Color.Silver;

        }

        private void Ajoute_Modifier_client_Load(object sender, EventArgs e)
        {

        }

        private void lblTittre_Click(object sender, EventArgs e)
        {

        }
    }
}

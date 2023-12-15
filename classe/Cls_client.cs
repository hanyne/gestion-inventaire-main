using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_stock.classe
{
    internal class Cls_client
    {
        private dbStockContext db = new dbStockContext();
        private Client C; //table Client
                          //fonction pour ajouter Client dans la bd
        public bool Ajouter_Client(string Nom, string Prenom, string Adresse, string Telephone, string Email, string Pays, string Ville)
        {

            C = new Client(); // Crée un nouveau client
            C.Nom_Client = Nom;
            C.Prenom_Client = Prenom;
            C.Adresse_Client = Adresse;
            C.Telephone_Client = Telephone;
            C.Pays_Client = Pays;
            C.Ville_Client = Ville;
             C.Email = Email;

            // Vérifie si le Nom et le Prenom existent déjà dans la base de données
           

            if (db.Clients.SingleOrDefault(s => s.Nom_Client == Nom && C.Prenom_Client == Prenom)== null)
            {
              db.Clients.Add(C); // Ajoute dans la table client
              db.SaveChanges(); // Enregistre dans la base de données
                return true;
            }
            else
            {
                return false;
            }
        }
          public void Modifier_Client(int id ,string Nom, string Prenom, string Adresse, string Telephone, string Email, string Pays, string Ville)
          {

            C = new Client(); // Crée un nouveau client
            C = db.Clients.SingleOrDefault(s => s.ID_Client == id);
            if (C != null)
            {

                C.Nom_Client = Nom;
                C.Prenom_Client = Prenom;
                C.Adresse_Client = Adresse;
                C.Telephone_Client = Telephone;
                C.Pays_Client = Pays;
                C.Ville_Client = Ville;
                C.Email = Email;
                db.SaveChanges ();
            }

        }


    }
}



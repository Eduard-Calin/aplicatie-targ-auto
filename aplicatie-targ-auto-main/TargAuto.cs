using System;
using System.Collections.Generic;
using System.Linq;
using LibrarieModele;       
using NivelStocareDate;     

namespace AppTargAuto
{
    public class TargAuto
    {
        // Lista internă pentru tranzacții 
        private List<Tranzactie> tranzactii = new List<Tranzactie>();

        // Metodă de adăugare
        public void AdaugaTranzactie(Tranzactie tranzactie)
        {
            tranzactii.Add(tranzactie);
        }

        // Metodă de preluare
        public List<Tranzactie> GetToateTranzactiile()
        {
            return tranzactii;
        }

        
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq; 
using LibrarieModele;

namespace NivelStocareDate
{
    public class StocareMemorie : IStocareTranzactii
    {
        // Lista internă unde păstrăm tranzacțiile cât timp rulează programul
        private List<Tranzactie> tranzactii = new List<Tranzactie>();

        // Metodă pentru adăugare
        public void AddTranzactie(Tranzactie t)
        {
            tranzactii.Add(t);
        }

        // Metodă pentru preluarea tuturor tranzacțiilor (pentru afișare)
        public List<Tranzactie> GetToate()
        {
            return tranzactii;
        }

        // TEMA LAB 4 & 8: Metodă de căutare utilizând LINQ
        public List<Tranzactie> CautaTranzactiiDupaModel(string modelCautat)
        {
            // Folosim .Where() din LINQ pentru a filtra lista
            var tranzactiiGasite = tranzactii
                .Where(t => t.Vehicul.Model.ToLower().Contains(modelCautat.ToLower()))
                .ToList();

            return tranzactiiGasite;
        }
    }
}
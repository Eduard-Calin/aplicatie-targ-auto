using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibrarieModele;

namespace NivelStocareDate
{
    public class AdministrareTranzactii_FisierText : IStocareTranzactii
    {
        private const string NUME_FISIER = "Tranzactii.txt";
        private const char SEPARATOR_PRINCIPAL = ';';

        public AdministrareTranzactii_FisierText()
        {
            if (!File.Exists(NUME_FISIER))
            {
                File.Create(NUME_FISIER).Dispose();
            }
        }

        public void AddTranzactie(Tranzactie t)
        {
            using (StreamWriter sw = new StreamWriter(NUME_FISIER, true))
            {
                sw.WriteLine(ConvertireLaSir(t));
            }
        }

        public List<Tranzactie> GetToate()
        {
            List<Tranzactie> tranzactii = new List<Tranzactie>();
            if (!File.Exists(NUME_FISIER)) return tranzactii;

            using (StreamReader sr = new StreamReader(NUME_FISIER))
            {
                string? linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(linie))
                    {
                        tranzactii.Add(ConvertireDinSir(linie));
                    }
                }
            }
            return tranzactii;
        }

        public List<Tranzactie> CautaTranzactiiDupaModel(string modelCautat)
        {
            string modelLow = (modelCautat ?? string.Empty).ToLower();
            return GetToate()
                .Where(t => t.Vehicul.Model.ToLower().Contains(modelLow))
                .ToList();
        }

        private string ConvertireLaSir(Tranzactie t)
        {
            return $"{t.Vanzator.Nume}{SEPARATOR_PRINCIPAL}{t.Cumparator.Nume}{SEPARATOR_PRINCIPAL}" +
                   $"{t.Vehicul.Model}{SEPARATOR_PRINCIPAL}{t.Vehicul.Pret}{SEPARATOR_PRINCIPAL}" +
                   $"{t.DataTranzactiei:yyyy-MM-dd}";
        }

        private Tranzactie ConvertireDinSir(string linie)
        {
            var date = linie.Split(SEPARATOR_PRINCIPAL);
            return new Tranzactie(
                new Persoana(date[0]),
                new Persoana(date[1]),
                new Vehicul("Marca", date[2], 2024, double.Parse(date[3]), CuloareVehicul.Alb, OptiuniVehicul.Standard),
                DateTime.Parse(date[4])
            );
        }
    }
}
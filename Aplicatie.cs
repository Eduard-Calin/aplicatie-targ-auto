using System;
using System.Collections.Generic;
using LibrarieModele;
using NivelStocareDate;

namespace AppTargAuto
{
    class Aplicatie
    {
        static void Main()
        {
            IStocareTranzactii stocare = FabricaStocare.GetBazaDate();

            while (true)
            {
                Console.WriteLine("\n=== MENIU TARG AUTO ===");
                Console.WriteLine("1. Adauga o tranzactie noua");
                Console.WriteLine("2. Afiseaza toate tranzactiile");
                Console.WriteLine("3. Cauta masina dupa model (Testare LINQ - Lab 4)");
                Console.WriteLine("4. Iesire");
                Console.Write("Alege o optiune: ");

                
                string optiune = Console.ReadLine() ?? string.Empty;

                switch (optiune)
                {
                    case "1":
                        AdaugaTranzactie(stocare);
                        break;
                    case "2":
                        AfiseazaLista(stocare.GetToate());
                        break;
                    case "3":
                        Console.Write("Introdu modelul masinii cautate: ");
                        string modelCautat = Console.ReadLine() ?? string.Empty; 
                        List<Tranzactie> gasite = stocare.CautaTranzactiiDupaModel(modelCautat);
                        AfiseazaLista(gasite);
                        break;
                    case "4":
                        Console.WriteLine("La revedere!");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida! Incearca din nou.");
                        break;
                }
            }
        }

        static void AdaugaTranzactie(IStocareTranzactii stocare)
        {
            Console.WriteLine("\n--- Introducere Date Tranzactie ---");

            Console.Write("Nume Vanzator: ");
            Persoana vanzator = new Persoana(Console.ReadLine() ?? string.Empty); 

            Console.Write("Nume Cumparator: ");
            Persoana cumparator = new Persoana(Console.ReadLine() ?? string.Empty); 

            Console.Write("Marca masina (ex: Dacia): ");
            string marca = Console.ReadLine() ?? string.Empty; 

            Console.Write("Model masina (ex: Logan): ");
            string model = Console.ReadLine() ?? string.Empty; 

            Console.Write("An fabricatie (ex: 2020): ");
            int an = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Pret (EUR): ");
            double pret = double.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Culoare: 0-Rosu, 1-Alb, 2-Negru, 3-Albastru, 4-Gri, 5-Altceva");
            Console.Write("Alege codul culorii: ");
            int culoareInt = int.Parse(Console.ReadLine() ?? "5");
            CuloareVehicul culoare = (CuloareVehicul)culoareInt;

            Console.WriteLine("Optiuni: 0-Standard, 1-AC, 2-Navigatie, 4-CutieAutomata, 8-Senzori");
            Console.WriteLine("(Poti aduna numerele pentru mai multe optiuni. Ex: 3 pt AC + Navigatie)");
            Console.Write("Alege codul optiunilor: ");
            int optiuniInt = int.Parse(Console.ReadLine() ?? "0");
            OptiuniVehicul optiuni = (OptiuniVehicul)optiuniInt;

            Vehicul vehicul = new Vehicul(marca, model, an, pret, culoare, optiuni);
            Tranzactie tranzactie = new Tranzactie(vanzator, cumparator, vehicul, DateTime.Now);

            stocare.AddTranzactie(tranzactie);
            Console.WriteLine("--> Tranzactie salvata cu succes!");
        }

        static void AfiseazaLista(List<Tranzactie> lista)
        {
            if (lista.Count == 0)
            {
                Console.WriteLine("\nNu a fost gasita nicio tranzactie!");
                return;
            }

            Console.WriteLine("\n--- Lista Tranzactii ---");
            foreach (var t in lista)
            {
                Console.WriteLine($"[{t.DataTranzactiei.ToShortDateString()}] {t.Vanzator.Nume} a vandut catre {t.Cumparator.Nume} un {t.Vehicul.Marca} {t.Vehicul.Model} ({t.Vehicul.Culoare}) la pretul de {t.Vehicul.Pret} EUR. Dotari: {t.Vehicul.Optiuni}");
            }
        }
    }
}
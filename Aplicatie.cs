using System;

class Aplicatie
{
    static void Main()
    {
        IStocareTranzactii stocare = FabricaStocare.GetStocare();
        TargAuto gestionare = new TargAuto();

        var toate = stocare.GetToate();
        foreach (var t in toate)
        {
            gestionare.AdaugaTranzactie(t);
        }

        while (true)
        {
            Console.WriteLine("\n1. Inregistreaza tranzactie");
            Console.WriteLine("2. Vezi tranzactii dintr-o zi");
            Console.WriteLine("3. Masina cea mai populara");
            Console.WriteLine("4. Vizualizeaza preturi pe interval");
            Console.WriteLine("5. Inchidere");

            var selectie = Console.ReadLine();
            if (selectie == "5") break;

            switch (selectie)
            {
                case "1":
                    Console.Write("Vanzator: ");
                    var vanzatorInput = Console.ReadLine()!;

                    Console.Write("Cumparator: ");
                    var cumparatorInput = Console.ReadLine()!;

                    Console.Write("Marca: ");
                    var marca = Console.ReadLine()!;

                    Console.Write("Model: ");
                    var tipModel = Console.ReadLine()!;

                    Console.Write("An productie: ");
                    int anProd = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Selecteaza culoare:");
                    foreach (var c in Enum.GetValues(typeof(CuloareVehicul)))
                        Console.WriteLine($"{(int)c} - {c}");
                    CuloareVehicul culoareSelectata = (CuloareVehicul)int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Alege dotari (separate prin virgula):");
                    foreach (var d in Enum.GetValues(typeof(DotariVehicul)))
                        if ((int)d != 0) Console.WriteLine($"{(int)d} - {d}");
                    var listaDotari = Console.ReadLine()!.Split(',');
                    DotariVehicul dotariFinale = DotariVehicul.FaraDotari;
                    foreach (var d in listaDotari)
                        dotariFinale |= (DotariVehicul)int.Parse(d);

                    Console.Write("Valoare: ");
                    double valoare = double.Parse(Console.ReadLine()!);

                    Console.Write("Data (yyyy-mm-dd): ");
                    DateTime dataTranzactie = DateTime.Parse(Console.ReadLine()!);

                    var vanzator = new Persoana(vanzatorInput);
                    var cumparator = new Persoana(cumparatorInput);
                    var vehicul = new Vehicul(marca, tipModel, anProd, culoareSelectata, dotariFinale);
                    var tranzactie = new Tranzactie(vanzator, cumparator, vehicul, dataTranzactie, valoare);

                    gestionare.AdaugaTranzactie(tranzactie);
                    stocare.Adauga(tranzactie);
                    break;

                case "2":
                    Console.Write("Data (yyyy-mm-dd): ");
                    DateTime zi = DateTime.Parse(Console.ReadLine()!);
                    gestionare.AfiseazaTranzactiiDinZi(zi);
                    break;

                case "3":
                    Console.WriteLine("Masina cea mai populara: " + gestionare.CeaMaiCautataMasina());
                    break;

                case "4":
                    Console.Write("De la (yyyy-mm-dd): ");
                    DateTime inceput = DateTime.Parse(Console.ReadLine()!);
                    Console.Write("Pana la (yyyy-mm-dd): ");
                    DateTime sfarsit = DateTime.Parse(Console.ReadLine()!);
                    gestionare.AfiseazaPreturiPePerioada(inceput, sfarsit);
                    break;
            }
        }
    }
}
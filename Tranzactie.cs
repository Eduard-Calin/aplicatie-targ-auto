using System;

public class Tranzactie
{
    public Persoana Vanzator { get; set; }
    public Persoana Cumparator { get; set; }
    public Vehicul Masina { get; set; }
    public DateTime DataTranzactie { get; set; }
    public double Pret { get; set; }

    public Tranzactie(Persoana vanzator, Persoana cumparator,
                      Vehicul masina, DateTime dataTranzactie, double pret)
    {
        Vanzator = vanzator;
        Cumparator = cumparator;
        Masina = masina;
        DataTranzactie = dataTranzactie;
        Pret = pret;
    }

    public Tranzactie(string linie)
    {
        var parti = linie.Split(';');

        DataTranzactie = DateTime.Parse(parti[0]);
        Vanzator = new Persoana(parti[1]);
        Cumparator = new Persoana(parti[2]);

        string[] masinaParts = parti[3].Split(',');
        Masina = new Vehicul(
            masinaParts[0],
            masinaParts[1],
            int.Parse(masinaParts[2]),
            (CuloareVehicul)Enum.Parse(typeof(CuloareVehicul), masinaParts[3]),
            (DotariVehicul)Enum.Parse(typeof(DotariVehicul), masinaParts[4])
        );

        Pret = double.Parse(parti[4]);
    }

    public override string ToString()
    {
        return $"{DataTranzactie};{Vanzator.NumeComplet};{Cumparator.NumeComplet};{Masina.Producator},{Masina.DenumireModel},{Masina.AnProductie},{Masina.Nuanta},{Masina.Dotari};{Pret}";
    }
}
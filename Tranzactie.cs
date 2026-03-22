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

    public override string ToString()
    {
        return $"{DataTranzactie.ToShortDateString()} - {Vanzator} -> {Cumparator} : {Masina}, Pret: {Pret} euro";
    }
}
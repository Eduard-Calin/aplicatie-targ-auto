using System;
using System.Collections.Generic;
using System.Linq;

public class TargAuto
{
    public List<Tranzactie> ListaTranzactii { get; set; } = new List<Tranzactie>();

    public void AdaugaTranzactie(Tranzactie tranzactieNoua)
    {
        bool vanzatorRepetat = ListaTranzactii.Any(x =>
            x.Vanzator.NumeComplet == tranzactieNoua.Vanzator.NumeComplet &&
            x.DataTranzactie.Date == tranzactieNoua.DataTranzactie.Date);

        bool cumparatorRepetat = ListaTranzactii.Any(x =>
            x.Cumparator.NumeComplet == tranzactieNoua.Cumparator.NumeComplet &&
            x.DataTranzactie.Date == tranzactieNoua.DataTranzactie.Date);

        if (vanzatorRepetat)
            Console.WriteLine("Atentie: vanzatorul are mai multe tranzactii in aceeasi zi!");

        if (cumparatorRepetat)
            Console.WriteLine("Atentie: cumparatorul are mai multe tranzactii in aceeasi zi!");

        ListaTranzactii.Add(tranzactieNoua);
    }

    public string CeaMaiCautataMasina()
    {
        var aparitii = new Dictionary<string, int>();

        foreach (var tranzactie in ListaTranzactii)
        {
            string identificator = $"{tranzactie.Masina.Producator}-{tranzactie.Masina.DenumireModel}";
            if (aparitii.ContainsKey(identificator))
                aparitii[identificator]++;
            else
                aparitii[identificator] = 1;
        }

        return aparitii.OrderByDescending(x => x.Value).FirstOrDefault().Key ?? "Niciuna";
    }

    public void AfiseazaTranzactiiDinZi(DateTime dataSelectata)
    {
        var rezultate = ListaTranzactii.Where(t => t.DataTranzactie.Date == dataSelectata.Date);
        foreach (var tranzactie in rezultate)
        {
            Console.WriteLine(tranzactie);
        }
    }

    public void AfiseazaPreturiPePerioada(DateTime dataStart, DateTime dataEnd)
    {
        var selectie = ListaTranzactii
            .Where(t => t.DataTranzactie.Date >= dataStart.Date && t.DataTranzactie.Date <= dataEnd.Date);

        foreach (var tranzactie in selectie)
        {
            Console.WriteLine($"{tranzactie.DataTranzactie.ToShortDateString()} - {tranzactie.Masina.DenumireModel} : {tranzactie.Pret} euro");
        }
    }

    public List<Persoana> ListaCumparatoriUniciZi(DateTime dataSelectata)
    {
        return ListaTranzactii
            .Where(t => t.DataTranzactie.Date == dataSelectata.Date)
            .Select(t => t.Cumparator)
            .Distinct()
            .ToList();
    }
}
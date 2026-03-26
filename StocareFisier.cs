using System.Collections.Generic;
using System.IO;

public class StocareFisier : IStocareTranzactii
{
    private string fisier;

    public StocareFisier(string cale)
    {
        fisier = cale;
        File.Open(fisier, FileMode.OpenOrCreate).Close();
    }

    public void Adauga(Tranzactie t)
    {
        using (StreamWriter sw = new StreamWriter(fisier, true))
        {
            sw.WriteLine(t.ToString());
        }
    }

    public List<Tranzactie> GetToate()
    {
        var lista = new List<Tranzactie>();

        using (StreamReader sr = new StreamReader(fisier))
        {
            string linie;
            while ((linie = sr.ReadLine()) != null)
            {
                lista.Add(new Tranzactie(linie));
            }
        }

        return lista;
    }
}
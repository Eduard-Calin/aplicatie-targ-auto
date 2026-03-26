using System.Collections.Generic;

public class StocareMemorie : IStocareTranzactii
{
    private List<Tranzactie> date = new List<Tranzactie>();

    public void Adauga(Tranzactie t)
    {
        date.Add(t);
    }

    public List<Tranzactie> GetToate()
    {
        return date;
    }
}
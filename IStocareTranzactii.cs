using System.Collections.Generic;

public interface IStocareTranzactii
{
    void Adauga(Tranzactie t);
    List<Tranzactie> GetToate();
}
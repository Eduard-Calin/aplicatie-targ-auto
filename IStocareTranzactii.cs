using System.Collections.Generic;
using LibrarieModele;

namespace NivelStocareDate
{
    public interface IStocareTranzactii
    {
        void AddTranzactie(Tranzactie t);
        List<Tranzactie> GetToate();
        List<Tranzactie> CautaTranzactiiDupaModel(string modelCautat);
    }
}
using System;

namespace LibrarieModele
{
    public class Tranzactie
    {
        public Persoana Vanzator { get; set; } = new Persoana();
        public Persoana Cumparator { get; set; } = new Persoana();
        public Vehicul Vehicul { get; set; } = new Vehicul();
        public DateTime DataTranzactiei { get; set; }
        public DateTime DataActualizare { get; set; } // Lab 9 Audit [cite: 1885]

        public Tranzactie() { }

        public Tranzactie(Persoana vanzator, Persoana cumparator, Vehicul vehicul, DateTime dataTranzactiei)
        {
            Vanzator = vanzator;
            Cumparator = cumparator;
            Vehicul = vehicul;
            DataTranzactiei = dataTranzactiei;
            DataActualizare = DateTime.Now; // Initializare automata [cite: 1886]
        }
    }
}
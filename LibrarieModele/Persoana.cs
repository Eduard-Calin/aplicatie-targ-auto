namespace LibrarieModele
{
    public class Persoana
    {
        public string Nume { get; set; } = string.Empty;

        public Persoana() { }

        public Persoana(string nume)
        {
            Nume = nume ?? string.Empty;
        }
    }
}
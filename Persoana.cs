public class Persoana
{
    public string NumeComplet { get; set; }

    public Persoana(string numeComplet)
    {
        NumeComplet = numeComplet;
    }

    public override string ToString()
    {
        return NumeComplet;
    }
}
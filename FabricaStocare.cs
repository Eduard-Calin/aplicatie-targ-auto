using System.Configuration;
using System.IO;

public static class FabricaStocare
{
    public static IStocareTranzactii GetStocare()
    {
        string tip = ConfigurationManager.AppSettings["tip"];

        if (tip == "fisier")
        {
            string cale = Directory.GetCurrentDirectory() + "\\tranzactii.txt";
            return new StocareFisier(cale);
        }

        return new StocareMemorie();
    }
}
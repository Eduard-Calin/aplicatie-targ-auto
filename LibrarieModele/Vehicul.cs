using System;

namespace LibrarieModele
{
    public enum CuloareVehicul { Rosu, Alb, Negru, Albastru, Gri, Altceva }
    
    [Flags]
    public enum OptiuniVehicul
    {
        Standard = 0,
        AerConditionat = 1,
        Navigatie = 2,
        CutieAutomata = 4,
        Decapotabila = 8,        
        Tractiune4x4 = 16,       
        GeamuriElectrice = 32    
    }

    public class Vehicul
    {
        public string Marca { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int AnFabricatie { get; set; }
        public double Pret { get; set; }
        public CuloareVehicul Culoare { get; set; }
        public OptiuniVehicul Optiuni { get; set; }

        public Vehicul() { }

        public Vehicul(string marca, string model, int anFabricatie, double pret, CuloareVehicul culoare, OptiuniVehicul optiuni)
        {
            Marca = marca ?? string.Empty;
            Model = model ?? string.Empty;
            AnFabricatie = anFabricatie;
            Pret = pret;
            Culoare = culoare;
            Optiuni = optiuni;
        }
    }
}
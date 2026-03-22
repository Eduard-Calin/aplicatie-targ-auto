using System;

[Flags]
public enum DotariVehicul
{
    FaraDotari = 0,
    Climatizare = 1,
    SistemNavigatie = 2,
    TransmisieAutomata = 4,
    ScauneIncalzire = 8,
    ConectivitateBluetooth = 16
}

public enum CuloareVehicul
{
    Negru,
    Alb,
    Albastru,
    Rosu,
    Verde,
    Galben,
    Gri
}

public class Vehicul
{
    public string Producator { get; set; }
    public string DenumireModel { get; set; }
    public int AnProductie { get; set; }
    public CuloareVehicul Nuanta { get; set; }
    public DotariVehicul Dotari { get; set; }

    public Vehicul(string producator, string denumireModel, int anProductie,
                   CuloareVehicul nuanta, DotariVehicul dotari)
    {
        Producator = producator;
        DenumireModel = denumireModel;
        AnProductie = anProductie;
        Nuanta = nuanta;
        Dotari = dotari;
    }

    public override string ToString()
    {
        return $"{Producator} {DenumireModel} ({AnProductie}) - Culoare: {Nuanta}, Dotari: {Dotari}";
    }
}
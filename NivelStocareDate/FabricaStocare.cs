using System;

namespace NivelStocareDate
{
    public static class FabricaStocare
    {
        public static IStocareTranzactii GetBazaDate()
        {
            // Folosim direct stocarea în memorie pentru acest stadiu al aplicației
            return new StocareMemorie();
        }
    }
}
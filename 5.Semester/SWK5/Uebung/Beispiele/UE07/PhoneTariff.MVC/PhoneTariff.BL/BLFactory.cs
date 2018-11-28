using System;
using System.Collections.Generic;
using PhoneTariff.Domain;

namespace PhoneTariff.BL
{
    public static class BLFactory
    {
        private static ITariffCalculator calculator;

        public static ITariffCalculator GetTariffCalculator()
        {
            if (calculator == null)
                calculator = new TariffCalculator();

            return calculator;
        }
    }
}
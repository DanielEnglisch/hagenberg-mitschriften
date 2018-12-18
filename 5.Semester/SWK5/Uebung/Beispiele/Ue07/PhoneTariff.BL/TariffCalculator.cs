using System.Collections.Generic;
using PhoneTariff.Domain;

namespace PhoneTariff.BL
{
    // TariffCalculator contains collection of all supported tariffs. It
    // provides methods for calculating the costs for a specific tariff
    // based on consumption parameters.
    internal class TariffCalculator : ITariffCalculator
    {
        // Collection of all fees in the different zones 
        private class TariffData
        {
            private struct RateData
            {
                public double PeakRate { get; set; }
                public double OffPeakRate { get; set; }
            }

            private IDictionary<string, RateData> rates =
              new Dictionary<string, RateData>();

            public TariffData(string id, string name)
            {
                this.Id = id;
                this.Name = name;
            }

            public string Id { get; private set; }

            public string Name { get; private set; }

            public void SetRate(string zoneId, double peak, double offPeak)
            {
                rates[zoneId] = new RateData { PeakRate = peak, OffPeakRate = offPeak };
            }

            public double TotalCosts(PhoneConsumption consumption)
            {
                double c = 0;
                foreach (ZoneConsumption zc in consumption.ZoneConsumptions)
                {
                    double peakRate = rates[zc.ZoneId].PeakRate;
                    double offPeakRate = rates[zc.ZoneId].OffPeakRate;
                    c += zc.PeakDuration * peakRate +
                         zc.OffPeakDuration * offPeakRate;
                }

                return c;
            }
        } // TariffData

        private IList<Zone> zones;
        private IDictionary<string, TariffData> tariffs;

        private void InitZones()
        {
            zones = new List<Zone>();
            zones.Add(new Zone("NAH", "Nahzone"));
            zones.Add(new Zone("FERN", "Fernzone"));
            zones.Add(new Zone("A1", "Mobilnetz A1"));
            zones.Add(new Zone("DREI", "drei"));
        }

        private void InitTariffs()
        {
            tariffs = new Dictionary<string, TariffData>();


            TariffData amiga = new TariffData("Amiga", "Amiga.Privat");
            amiga.SetRate("A1", 0.15, 0.14);
            amiga.SetRate("DREI", 0.18, 0.15);
            amiga.SetRate("FERN", 0.04, 0.02);
            amiga.SetRate("NAH", 0.04, 0.02);
            tariffs.Add(amiga.Id, amiga);

            TariffData avocalis = new TariffData("Avocalis", "Avocalis Festnetz Privat");
            avocalis.SetRate("A1", 0.19, 0.18);
            avocalis.SetRate("DREI", 0.28, 0.24);
            avocalis.SetRate("FERN", 0.05, 0.03);
            avocalis.SetRate("NAH", 0.05, 0.03);
            tariffs.Add(avocalis.Id, avocalis);

            TariffData tele2Direkt = new TariffData("Tele2-Direkt", "Tele2 Telefonie direkt");
            tele2Direkt.SetRate("A1", 0.21, 0.21);
            tele2Direkt.SetRate("DREI", 0.21, 0.21);
            tele2Direkt.SetRate("FERN", 0.05, 0.02);
            tele2Direkt.SetRate("NAH", 0.05, 0.02);
            tariffs.Add(tele2Direkt.Id, tele2Direkt);

            TariffData tele2Freizeit = new TariffData("Tele2-Freizeit", "Tele2 Telefonie direkt Freizeit");
            tele2Freizeit.SetRate("A1", 0.21, 0.21);
            tele2Freizeit.SetRate("DREI", 0.21, 0.21);
            tele2Freizeit.SetRate("FERN", 0.05, 0);
            tele2Freizeit.SetRate("NAH", 0.05, 0);
            tariffs.Add(tele2Freizeit.Id, tele2Freizeit);

            TariffData iTCE = new TariffData("i-TC-E", "i-TC-E Special");
            iTCE.SetRate("A1", 0.1, 0.1);
            iTCE.SetRate("DREI", 0.1, 0.1);
            iTCE.SetRate("FERN", 0.03, 0.02);
            iTCE.SetRate("NAH", 0.03, 0.01);
            tariffs.Add(iTCE.Id, iTCE);

            TariffData redTelecom = new TariffData("Red-Telecom-1", "Red Telecom 1025");
            redTelecom.SetRate("A1", 0.15, 0.14);
            redTelecom.SetRate("DREI", 0.16, 0.14);
            redTelecom.SetRate("FERN", 0.04, 0.02);
            redTelecom.SetRate("NAH", 0.04, 0.02);
            tariffs.Add(redTelecom.Id, redTelecom);

            TariffData drei = new TariffData("drei", "3 Festnetz");
            drei.SetRate("A1", 0.15, 0.15);
            drei.SetRate("DREI", 0.05, 0.05);
            drei.SetRate("FERN", 0.035, 0.025);
            drei.SetRate("NAH", 0.035, 0.015);
            tariffs.Add(drei.Id, drei);
        }

        public TariffCalculator()
        {
            InitZones();
            InitTariffs();
        }

        public ICollection<Zone> GetAllZones()
        {
            return zones;
        }

        public ICollection<Tariff> GetAllTariffs()
        {
            List<Tariff> tValues = new List<Tariff>();
            foreach (TariffData t in tariffs.Values)
                tValues.Add(new Tariff(t.Id, t.Name));

            return tValues;
        }

        public double TotalCosts(string tariffKey, PhoneConsumption cons)
        {
            return tariffs[tariffKey].TotalCosts(cons);
        }
    } // TariffCalc
}
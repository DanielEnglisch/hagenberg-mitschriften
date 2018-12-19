using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneTariff.Domain
{
    [Serializable]
    public class Rate
    {
        public Rate()
        {
        }

        public Rate(Tariff tariff, Zone zone, double peakRate, double offPeakRate)
        {
            this.Tariff = tariff;
            this.Zone = zone;
            this.PeakRate = peakRate;
            this.OffPeakRate = offPeakRate;
        }

        public Tariff Tariff { get; set; }
        public Zone Zone { get; set; }
        public double PeakRate { get; set; }
        public double OffPeakRate { get; set; }
    }
}

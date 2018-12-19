using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneTariff.Domain
{
    public class PhoneConsumption
    {
        public PhoneConsumption()
        {
            ZoneConsumptions = new List<ZoneConsumption>();
        }

        public List<ZoneConsumption> ZoneConsumptions { get; set; }

        public void SetConsumption(string zoneId, double peak, double offPeak)
        {
            ZoneConsumption zCons = new ZoneConsumption
            {
                ZoneId = zoneId,
                PeakDuration = peak,
                OffPeakDuration = offPeak
            };

            int zoneIdx = ZoneConsumptions.FindIndex(c => c.ZoneId == zoneId);

            if (zoneIdx < 0)
                ZoneConsumptions.Add(zCons);
            else
                ZoneConsumptions[zoneIdx] = zCons;
        }

        public void Reset()
        {
            ZoneConsumptions.Clear();
        }
    }
}

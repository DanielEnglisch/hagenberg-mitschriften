using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneTariff.Domain
{
    public struct ZoneConsumption : IComparable
    {
        public string ZoneId { get; set; }
        public double PeakDuration { get; set; }
        public double OffPeakDuration { get; set; }

        public int CompareTo(object obj)
        {
            return this.ZoneId.CompareTo(obj);
        }
    }
}

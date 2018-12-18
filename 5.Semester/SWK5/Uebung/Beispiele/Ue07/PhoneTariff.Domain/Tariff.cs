using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneTariff.Domain
{
    [Serializable]
    public class Tariff
    {
        public Tariff()
        {
        }

        public Tariff(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

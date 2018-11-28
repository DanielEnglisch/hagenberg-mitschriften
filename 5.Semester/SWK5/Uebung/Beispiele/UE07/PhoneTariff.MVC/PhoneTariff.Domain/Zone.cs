﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneTariff.Domain
{
    [Serializable]
    public class Zone
    {
        public Zone()
        {
        }

        public Zone(string id, string name)
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

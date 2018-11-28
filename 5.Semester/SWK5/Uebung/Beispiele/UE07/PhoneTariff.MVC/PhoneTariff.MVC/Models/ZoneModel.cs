using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneTariff.MVC.Models
{
    public class ZoneModel
    {
        public string  ZoneName { get; set; }
        public string TextBoxName { get; set; }
        public string ComboBoxName { get; set; }
        public int Duration { get; set; }
        public int PeakPercent { get; set; }
   
    }
}
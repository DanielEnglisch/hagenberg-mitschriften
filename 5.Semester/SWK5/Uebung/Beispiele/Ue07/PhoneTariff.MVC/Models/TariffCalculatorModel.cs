using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneTariff.MVC.Models
{
    public class TariffCalculatorModel
    {
        [Required(ErrorMessage = "Please enter a number.")]
        [Integer(ErrorMessage = "Please enter an integer")]
        [Min(0, ErrorMessage = "Please enter a number above 0")]
        public int LocalDuration { get; set; }
        public int LocalPeakPercent { get; set; }

        public int NationalDuration { get; set; }
        public int NationalPeakPercent { get; set; }

        public double TotalCost { get; set; }

        public IEnumerable<SelectListItem> TariffList { get; set; }

        public string SelectedTariff { get; set; }
    }
}
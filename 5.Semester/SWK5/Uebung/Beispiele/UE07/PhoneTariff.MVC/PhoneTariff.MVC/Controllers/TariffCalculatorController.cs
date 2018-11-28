using PhoneTariff.BL;
using PhoneTariff.Domain;
using PhoneTariff.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneTariff.MVC.Controllers
{
    public class TariffCalculatorController : Controller
    {
        private ITariffCalculator tariffCalculator = BLFactory.GetTariffCalculator();
        // GET: TariffCalculator
        [HttpGet]
        public ActionResult Index()
        {

            var model = new TariffCalculatorModel()
            {
                LocalPeakPercent = 50,
                NationalPeakPercent = 50,
                TariffList = tariffCalculator.GetAllTariffs()
                    .Select(tariff => new SelectListItem()
                    {
                        Value = tariff.Id,
                        Text = tariff.Name,
                        Selected = false
                    })
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TariffCalculatorModel model)
        {
            if (!tariffCalculator.GetAllTariffs().Any(t => t.Id == model.SelectedTariff))
            {
                ModelState.AddModelError(nameof(model.SelectedTariff), "invalid tarif");
            }
            if (ModelState.IsValid)
            {
                var consumption = new PhoneConsumption();

                consumption.SetConsumption(
                    "NAH",
                    model.LocalDuration * model.LocalPeakPercent / 100.0,
                    model.LocalDuration * (100 - model.LocalPeakPercent) / 100.0);

                consumption.SetConsumption(
                    "FERN",
                    model.NationalDuration * model.NationalPeakPercent / 100.0,
                    model.NationalDuration * (100 - model.NationalPeakPercent) / 100.0);


                model.TotalCost = tariffCalculator.TotalCosts(model.SelectedTariff, consumption);
            }

            model.TariffList = tariffCalculator.GetAllTariffs()
                        .Select(tariff => new SelectListItem()
                        {
                            Value = tariff.Id,
                            Text = tariff.Name,
                            Selected = false
                        });

            return View(model);
        }

        [HttpPost]
        public ActionResult CalculateAsync(TariffCalculatorModel model)
        {
            if (!tariffCalculator.GetAllTariffs().Any(t => t.Id == model.SelectedTariff))
            {
                ModelState.AddModelError(nameof(model.SelectedTariff), "invalid tarif");
            }
            if (ModelState.IsValid)
            {
                var consumption = new PhoneConsumption();

                consumption.SetConsumption(
                    "NAH",
                    model.LocalDuration * model.LocalPeakPercent / 100.0,
                    model.LocalDuration * (100 - model.LocalPeakPercent) / 100.0);

                consumption.SetConsumption(
                    "FERN",
                    model.NationalDuration * model.NationalPeakPercent / 100.0,
                    model.NationalDuration * (100 - model.NationalPeakPercent) / 100.0);


                var totalCost = tariffCalculator.TotalCosts(model.SelectedTariff, consumption);

                return Json(totalCost.ToString("C2"));
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
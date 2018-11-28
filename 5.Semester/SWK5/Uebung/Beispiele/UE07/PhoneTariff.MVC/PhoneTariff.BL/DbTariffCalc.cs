using System.Collections.Generic;
using System.Collections;
using PhoneTariff.BL.Common;
using PhoneTariff.DAL.Common;
using PhoneTariff.Domain;

// collection of classes that allow the calculation of phone tariffs
// depending on phone profiles.
namespace PhoneTariff.BL.Db {
  // TariffCalc contains collection of all supported tariffs. It
  // provides methods for calculation the costs of a specific tariff
  // based on a user profile.
  internal class TariffCalc : ITariffCalc {

    // Collection of all fees in the different tariffs 
    private class TariffData {
      private struct RateData {
        public double PeakRate { get; set; }
        public double OffPeakRate { get; set; }
      }
    
      private IDictionary<string, RateData> rates = new Dictionary<string, RateData>();

      public TariffData(string id, string name) {
        this.Id = id;
        this.Name = name;
      }

      public string Id { get; private set; }

      public string Name { get; private set;  }

      public void SetRate(string zoneId, double peak, double offPeak) {
        rates[zoneId] = new RateData { PeakRate = peak, OffPeakRate = offPeak };
      }

      public double TotalCosts(PhoneConsumption consumption) {
        double c = 0;
        foreach (ZoneConsumption zc in consumption.ZoneConsumptions) {
          double peakRate = rates[zc.ZoneId].PeakRate;
          double offPeakRate = rates[zc.ZoneId].OffPeakRate;
          c += zc.PeakDuration * peakRate +
               zc.OffPeakDuration * offPeakRate;
        }
        return c;
      }
    }

    private IDatabase database = DALFactory.CreateDatabase();
    private List<Zone> zoneList;
    private IDictionary<string, TariffData> tariffList;

    private void InitZones() {
      zoneList = new List<Zone>(DALFactory.CreateZoneDao(database).FindAll());
    }

    private void InitRates() {
      IList<Rate> rdList = DALFactory.CreateRateDao(database).FindAll();

      tariffList = new Dictionary<string, TariffData>();

      foreach (Rate rd in rdList) {
        if (! tariffList.ContainsKey(rd.Tariff.Id))
          tariffList.Add(rd.Tariff.Id, new TariffData(rd.Tariff.Id, rd.Tariff.Name));
        tariffList[rd.Tariff.Id].SetRate(rd.Zone.Id, rd.PeakRate, rd.OffPeakRate);
      }
    }

    public TariffCalc() {
      InitZones();
      InitRates();
    }

    public ICollection<Zone> GetAllZones() {
      return zoneList;
    }

    public ICollection<Tariff> GetAllTariffs() {
      List<Tariff> tValues = new List<Tariff>();
      foreach (TariffData t in tariffList.Values)
        tValues.Add(new Tariff(t.Id, t.Name));

      return tValues;
    }

    public double TotalCosts(string tariffKey, PhoneConsumption cons) {
      return tariffList[tariffKey].TotalCosts(cons);
    }
  }
}
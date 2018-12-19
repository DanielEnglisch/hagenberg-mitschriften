package swk5;


import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "CurrencyData")
public class CurrencyData   {

  private String symbol = null;
  private String name = null;
  private String country = null;
  private Double euroRate = null;

  @XmlElement(name = "Symbol")
  public String getSymbol() {
    return symbol;
  }

	public void setSymbol(String symbol) {
    this.symbol = symbol;
  }

  @XmlElement(name = "Name")
  public String getName() {
    return name;
  }

  public void setName(String name) {
    this.name = name;
  }

  @XmlElement(name = "Country")
  public String getCountry() {
    return country;
  }

  public void setCountry(String country) {
    this.country = country;
  }

  @XmlElement(name = "EuroRate")
  public Double getEuroRate() {
    return euroRate;
  }

  public void setEuroRate(Double euroRate) {
    this.euroRate = euroRate;
  }
}

package swk5;

import java.util.List;

import javax.ws.rs.client.Client;
import javax.ws.rs.client.ClientBuilder;
import javax.ws.rs.client.Entity;
import javax.ws.rs.client.WebTarget;
import javax.ws.rs.core.GenericType;
import javax.ws.rs.core.MediaType;

import io.swagger.client.ApiException;
import io.swagger.client.api.CurrenciesApi;

public class JavaConverterClient {

	public static void main(String[] args) {
		
		//testJaxRs();
		testSwagger();
	}

	private static void testSwagger() {
		CurrenciesApi converter = new CurrenciesApi();
		
		try {
			io.swagger.client.model.CurrencyData data = converter.currenciesGetBySymbol("USD");
			System.out.printf("%s: %.4f", data.getName(), data.getEuroRate());
			
		} catch (ApiException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private static void testJaxRs() {
		Client client = ClientBuilder.newClient();
		
		WebTarget target = client.target("http://localhost:56114").path("api/currencies");
		CurrencyData data = target.path("USD").request(MediaType.APPLICATION_JSON).get(CurrencyData.class);
		
		System.out.printf("%s: %.4f", data.getName(), data.getEuroRate());
		
		data.setEuroRate(1.138);
		
		target.request().put(Entity.entity(data, MediaType.APPLICATION_JSON));
		
		// da java generics nicht im il code verwendet wird, wird bei get()
		// List<CurrencyData>.class die Klasse verloren, workaround:
		GenericType<List<CurrencyData>> currencyListType =
				new GenericType<List<CurrencyData>>() {};
		List<CurrencyData> currencies =
				target.request(MediaType.APPLICATION_JSON).get(currencyListType);
		
		for (CurrencyData curr : currencies) {
			System.out.println(curr.getName());
		}
	}

}

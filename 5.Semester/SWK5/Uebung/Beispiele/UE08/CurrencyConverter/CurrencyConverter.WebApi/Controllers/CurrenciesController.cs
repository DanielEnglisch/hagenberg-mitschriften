using CurrencyConverter.BL;
using CurrencyConverter.Domain;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CurrencyConverter.WebApi.Controllers
{
    [RoutePrefix("api/currencies")]
    [ValidationActionFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CurrenciesController : ApiController
    {
        private ICurrencyCalculator calculator = BLFactory.GetCalculator();

        // route funktioniert nur wenn der request ein GET request ist
        // name ist rein intern in der api damit die route identifiziert werden kann zb bei Post
        [HttpGet]
        [Route("{symbol}", Name = nameof(GetBySymbol))]
        [SwaggerResponse(HttpStatusCode.OK, typeof(CurrencyData))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public CurrencyData GetBySymbol(string symbol)
        {
            if (!calculator.CurrencyExists(symbol))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return calculator.GetCurrencyData(symbol);
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<CurrencyData> GetAll()
        {
            return calculator.GetCurrencies().Select(symbol => calculator.GetCurrencyData(symbol));
        }

        // api/currencies/EUR/rates/USD
        [HttpGet]
        [Route("{source}/rates/{target}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(double))]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void))]
        public double RateOfExchange(string source, string target)
        {
            if (!calculator.CurrencyExists(source) || !calculator.CurrencyExists(target))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return calculator.RateOfExchange(source, target);
        }

        // Currencies aktualisieren
        // FromBody wird verwendet um nachzusehen ob ein CurrencyData Object im Body existiert (wird geparst)
        // ModelState setzt vorraus das ein Property in CurrencyData das Attribut
        //   [Required] hat sonst ist das immer ok
        [HttpPut]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "No content")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(void), Description = "Not found")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(void), Description = "Bad request")]
        public void Update([FromBody]CurrencyData data)
        {
            if (!calculator.CurrencyExists(data.Symbol))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            calculator.Update(data);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, typeof(void), Description = "Inserted")]
        [SwaggerResponse(HttpStatusCode.Conflict, typeof(void), Description = "Already exists")]
        public HttpResponseMessage Insert([FromBody] CurrencyData data)
        {
            if (calculator.CurrencyExists(data.Symbol))
            {
                return Request.CreateErrorResponse(
                    HttpStatusCode.Conflict, 
                    $"Currency {data.Symbol} already exists.");
            }

            calculator.Insert(data);

            var response =  Request.CreateResponse(
                    HttpStatusCode.Created);

            string uri = Url.Link(nameof(GetBySymbol), new { symbol = data.Symbol });
            response.Headers.Location = new Uri(uri);

            return response;
        }
    }
}

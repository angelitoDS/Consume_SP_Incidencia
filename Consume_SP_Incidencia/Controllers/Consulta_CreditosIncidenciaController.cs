using System.Web.Http;
using System.Configuration;
using Consume_SP_Incidencia.Models.Request;
using System.Collections.Generic;

namespace Consume_SP_Incidencia.Controllers
{
    public class Consulta_CreditosIncidenciaController : ApiController
    {
        /// <summary>
        /// Consumo del SP Consulta_FaltantesConIncidenciaController
        /// </summary>
        /// <param name="context">Json de Créditos para consultar por SP</param>
        /// <returns></returns>
        // POST: api/Consulta_CreditosIncidenciaController
        [HttpPost]
        public IHttpActionResult Get(JsonRequest context)
        {
            Helper obj = new Helper();
            List<ResultJsonCreditosRequest> ResultCredito = new List<ResultJsonCreditosRequest>();
            return Ok(obj.GetDatosSPCreditos(ConfigurationManager.ConnectionStrings["bd_DocEntry_ANEC_DIGITAL_1001_Prod"].ConnectionString, context, ResultCredito, "prc_Consulta_CreditosIncidencia", "@Creditos"));
        }
    }
}
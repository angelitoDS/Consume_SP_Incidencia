using System.Web.Http;
using System.Configuration;
using Consume_SP_Incidencia.Models.Request;
using System.Collections.Generic;

namespace Consume_SP_Incidencia.Controllers
{
    public class Consulta_DocumentosConIncidenciaController : ApiController
    {
        // POST: api/Consulta_DocumentosConIncidenciaController
        [HttpPost]
        /// <summary>
        /// Consumo del SP prc_Consulta_DocumentosConIncidencia
        /// </summary>
        /// <param name="context">Json de Créditos para consultar por SP</param>
        /// <returns></returns>
        public IHttpActionResult Get(JsonRequest context)
        {
            Helper obj = new Helper();
            List<ResultJsonRequest> ResultCredito = new List<ResultJsonRequest>();
            return Ok(obj.GetDatosSP(ConfigurationManager.ConnectionStrings["bd_DocEntry_ANEC_DIGITAL_1001_Prod"].ConnectionString, context, ResultCredito, "prc_Consulta_DocumentosConIncidencia", "@Creditos"));
        }

    }
}

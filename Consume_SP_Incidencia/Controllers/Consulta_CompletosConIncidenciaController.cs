using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Consume_SP_Incidencia.Controllers
{
    public class Consulta_CompletosConIncidenciaController : ApiController
    {
        [HttpPost]
        //public  Consulta_CompletosConIncidencia()
        public IHttpActionResult Get(Models.Request.JsonRequest context)
        {
            DataTable Result_Credito = new DataTable();
            Result_Credito.Columns.Add("Credito", typeof(string));
            Result_Credito.Columns.Add("Documento", typeof(string));
            Result_Credito.Columns.Add("Incidencia", typeof(string));

            using (SqlConnection sqlConnection = new SqlConnection( ConfigurationManager.ConnectionStrings["bd_DocEntry_ANEC_DIGITAL_1001_Prod"].ConnectionString ))
            {
                using (SqlCommand sc = new SqlCommand("prc_Consulta_CompletosConIncidencia", sqlConnection))
                {
                    using (IDataReader idr = GetParams(context))
                    {
                        sc.Parameters.Add("@Creditos", SqlDbType.Structured).Value = idr;
                        sqlConnection.Open();
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sc.CommandType = CommandType.StoredProcedure;
                            using (SqlDataReader sqlDataReader = sc.ExecuteReader())
                            {
                                if (sqlDataReader != null && sqlDataReader.FieldCount > 0)
                                {
                                    while (sqlDataReader.Read())
                                    {
                                        Result_Credito.Rows.Add(sqlDataReader[0], sqlDataReader[1], sqlDataReader[2]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return Ok(Result_Credito);
        }

        public static DataTableReader GetParams(Models.Request.JsonRequest context)
        {
            using (DataTable dt = new DataTable("Creditos"))
            {
                dt.Columns.Add("Credito", typeof(string));
                foreach (string credito in context.Creditos)
                {
                    dt.Rows.Add(credito);
                }
                dt.AcceptChanges();

                return dt.CreateDataReader();
            }
        }
    }
}

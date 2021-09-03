using System.Data;
using System.Data.SqlClient;
using Consume_SP_Incidencia.Models.Request;
using System.Collections.Generic;
using System.Configuration;

namespace Consume_SP_Incidencia.Controllers
{
    public class Helper
    {
        public List<ResultJsonCreditosRequest> GetDatosSPCreditos(string connection, JsonRequest context, List<ResultJsonCreditosRequest> ResultCredito, string SP, string ParametroSP)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand sc = new SqlCommand(SP, sqlConnection))
                {
                    using (IDataReader idr = GetParams(context))
                    {
                        sc.Parameters.Add(ParametroSP, SqlDbType.Structured).Value = idr;
                        sqlConnection.Open();
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sc.CommandType = CommandType.StoredProcedure;
                            sc.CommandTimeout = int.Parse(ConfigurationManager.AppSettings.Get("executionTimeout"));
                            using (SqlDataReader sqlDataReader = sc.ExecuteReader())
                            {
                                if (sqlDataReader != null && sqlDataReader.FieldCount > 0)
                                {
                                    while (sqlDataReader.Read())
                                    {
                                        ResultJsonCreditosRequest Result = new ResultJsonCreditosRequest
                                        {
                                            Credito = sqlDataReader[0].ToString(),
                                            Incidencia = sqlDataReader[1].ToString()
                                        };

                                        ResultCredito.Add(Result);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ResultCredito;
        }

        public List<ResultJsonRequest> GetDatosSP(string connection, JsonRequest context, List<ResultJsonRequest> ResultCredito, string SP, string ParametroSP)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connection))
            {
                using (SqlCommand sc = new SqlCommand(SP, sqlConnection))
                {
                    using (IDataReader idr = GetParams(context))
                    {
                        sc.Parameters.Add(ParametroSP, SqlDbType.Structured).Value = idr;
                        sqlConnection.Open();
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sc.CommandType = CommandType.StoredProcedure;
                            sc.CommandTimeout = int.Parse(ConfigurationManager.AppSettings.Get("executionTimeout"));
                            using (SqlDataReader sqlDataReader = sc.ExecuteReader())
                            {
                                if (sqlDataReader != null && sqlDataReader.FieldCount > 0)
                                {
                                    while (sqlDataReader.Read())
                                    {
                                        ResultJsonRequest Result = new ResultJsonRequest
                                        {
                                            Credito = sqlDataReader[0].ToString(),
                                            Documento = sqlDataReader[1].ToString(),
                                            Incidencia = sqlDataReader[2].ToString()
                                        };

                                        ResultCredito.Add(Result);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ResultCredito;
        }

        public static DataTableReader GetParams(JsonRequest context)
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
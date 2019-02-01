using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.InvoiceProcessBatch
{
    public static class InvoiceProcessBatchFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<InvoiceProcessBatchResponse> CreateBatch(FwApplicationConfig appConfig, FwUserSession userSession, InvoiceProcessBatchRequest request)
        {
            string batchId = "";
            InvoiceProcessBatchResponse response = new InvoiceProcessBatchResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "createchargebatch2", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@asof", SqlDbType.DateTime, ParameterDirection.Input, request.AsOfDate);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.LocationId);
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);
                    batchId = qry.GetParameter("@chgbatchid").ToString();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }

            if (!string.IsNullOrEmpty(batchId))
            {
                response.Batch = new InvoiceProcessBatchLogic();
                response.Batch.SetDependencies(appConfig, userSession);
                response.Batch.BatchId = batchId;
                await response.Batch.LoadAsync<InvoiceProcessBatchLogic>();
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ExportInvoiceResponse> ExportInvoice(FwApplicationConfig appConfig, FwUserSession userSession, ExportInvoiceRequest request)
        {
            FwJsonDataTable dt = null;
            FwJsonDataTable exportFields = null;
            FwJsonDataTable exportSettings = null;
            string exportString = "";
            StringBuilder sb = new StringBuilder();
            ExportInvoiceResponse response = new ExportInvoiceResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select exportstring from dataexportsetting where settingname = 'DEAL INVOICE G/L SUMMARY'");
                    exportSettings = await qry.QueryToFwJsonTableAsync(true, 0);
                    exportString = exportSettings.Rows[0][0].ToString();
                }

                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select * from dataexportfield");
                    exportFields = await qry.QueryToFwJsonTableAsync(true, 0);
                }

                using (FwSqlCommand qry = new FwSqlCommand(conn, "exportinvoices", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Input, request.BatchId);
                    qry.AddColumn("invoiceid", "invoiceid", FwDataTypes.Text, true, false, false);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                    //await qry.ExecuteNonQueryAsync(true);
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    FwJsonDataTable dt2 = null;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, "exportinvoiceglsummary", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, dt.Rows[i][0]);
                        dt2 = await qry.QueryToFwJsonTableAsync(true, 0);
                        string lineText = exportString;
                        for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                        {
                            char[] delimiterChars = { ' ', ','};
                            string[] fields = lineText.Split(delimiterChars);
                            string fieldName = "";
                            foreach (string s in fields)
                            {
                                if (s.StartsWith('@'))
                                {
                                    for (int k = 0; k <= exportFields.Rows.Count; k++)
                                    {
                                        if (exportFields.Rows[k][7].ToString().Equals(s))
                                        {
                                            fieldName = exportFields.Rows[k][4].ToString();
                                            int fieldIndex = dt2.ColumnIndex[fieldName];
                                            string fieldValue = dt2.Rows[j][fieldIndex].ToString();
                                            lineText = lineText.Replace(s, fieldValue);
                                            break;
                                        }
                                    }
                                }
                            }
                            sb.AppendLine(lineText);
                        }

                    }
                }
                string path = @"C:\TEMP\Batch" + request.BatchId + ".csv";
                using (var tw = new StreamWriter(path, true))
                {
                    tw.Write(sb);
                    tw.Close();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

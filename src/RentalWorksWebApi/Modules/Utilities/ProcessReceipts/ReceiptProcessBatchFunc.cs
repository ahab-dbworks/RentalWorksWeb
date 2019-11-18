using FwCore.Controllers;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Utilities.ReceiptProcessBatch
{

    public class ReceiptProcessBatchRequest
    {
        public string OfficeLocationId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }


    public class ReceiptProcessBatchResponse : TSpStatusResponse
    {
        public ReceiptProcessBatchLogic Batch;
    }

    public class ExportReceiptRequest
    {
        public string BatchId { get; set; }
    }

    public class ExportReceiptResponse : TSpStatusResponse
    {
        public ReceiptProcessBatchLogic batch = null;
        public string downloadUrl = "";
    }


    public static class ReceiptProcessBatchFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ReceiptProcessBatchResponse> CreateBatch(FwApplicationConfig appConfig, FwUserSession userSession, ReceiptProcessBatchRequest request)
        {
            string batchId = "";
            ReceiptProcessBatchResponse response = new ReceiptProcessBatchResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "createarchargebatchweb", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@fromdate", SqlDbType.DateTime, ParameterDirection.Input, request.FromDate);
                    qry.AddParameter("@todate", SqlDbType.NVarChar, ParameterDirection.Input, request.ToDate);
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    batchId = qry.GetParameter("@chgbatchid").ToString();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            if (!string.IsNullOrEmpty(batchId))
            {
                response.Batch = new ReceiptProcessBatchLogic();
                response.Batch.SetDependencies(appConfig, userSession);
                response.Batch.BatchId = batchId;
                await response.Batch.LoadAsync<ReceiptProcessBatchLogic>();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ExportReceiptResponse> Export(FwApplicationConfig appConfig, FwUserSession userSession, ReceiptProcessBatchLogic batch)
        {
            FwJsonDataTable dt = null;
            FwJsonDataTable exportFields = null;
            StringBuilder sb = new StringBuilder();
            ExportReceiptResponse response = new ExportReceiptResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                string exportString = await AppFunc.GetStringDataAsync(appConfig, "dataexportsetting", "settingname", RwConstants.DATA_EXPORT_SETTINGS_TYPE_RECEIPT_DETAIL, "exportstring");  //hard-coded for 4Wall

                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select * from dataexportfield");
                    exportFields = await qry.QueryToFwJsonTableAsync(true, 0);
                }

                using (FwSqlCommand qry = new FwSqlCommand(conn, "exportreceipts", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Input, batch.BatchId);
                    qry.AddColumn("arid", "arid", FwDataTypes.Text, true, false, false);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    FwJsonDataTable dt2 = null;
                    using (FwSqlCommand qry = new FwSqlCommand(conn, "exportreceiptdetails", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.AddParameter("@arid", SqlDbType.NVarChar, ParameterDirection.Input, dt.Rows[i][0]);
                        dt2 = await qry.QueryToFwJsonTableAsync(true, 0);
                        string lineText = string.Empty;
                        for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                        {
                            lineText = exportString;
                            char[] delimiterChars = { ' ', ',' };
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

                                            //below is how we could maybe get the dates formatting correctly, but it's not working because "DataType" is currently always Text
                                            //string fieldValue = "";
                                            //if (dt2.Columns[fieldIndex].DataType.Equals(FwDataTypes.Date))
                                            //{
                                            //    fieldValue = FwConvert.ToUSShortDate(FwConvert.ToDateTime(dt2.Rows[j][fieldIndex].ToString()));
                                            //}
                                            //else
                                            //{
                                            //    fieldValue = dt2.Rows[j][fieldIndex].ToString();
                                            //}

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
                string downloadFileName = batch.BatchNumber + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
                string filename = userSession.WebUsersId + "_" + batch.BatchNumber + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + "_csv";
                string directory = FwDownloadController.GetDownloadsDirectory();
                string path = Path.Combine(directory, filename);

                using (var tw = new StreamWriter(path, false))
                {
                    tw.Write(sb);
                    tw.Flush();
                    tw.Close();
                }
                response.batch = batch;
                response.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}";
                response.success = true;
            }
            return response;
        }
    }
}

using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Settings.FiscalYear;

namespace WebApi.Modules.Utilities.VendorInvoiceProcessBatch
{
    public class VendorInvoiceProcessBatchRequest
    {
        public string LocationId { get; set; }
    }

    public class VendorInvoiceProcessBatchResponse : TSpStatusResponse
    {
        public VendorInvoiceProcessBatchLogic Batch { get; set; }
    }

    public static class VendorInvoiceProcessBatchFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<VendorInvoiceProcessBatchResponse> CreateBatch(FwApplicationConfig appConfig, FwUserSession userSession, VendorInvoiceProcessBatchRequest request)
        {
            string batchId = "";
            VendorInvoiceProcessBatchResponse response = new VendorInvoiceProcessBatchResponse();

            if (await FiscalFunc.DateIsInClosedMonth(appConfig, userSession, DateTime.Today))
            {
                response.success = false;
                response.msg = "Vendor Invoices cannot be Processed to a Closed month.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, "createvendorinvoicechargebatch", appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                        qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.LocationId);
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
                    response.Batch = new VendorInvoiceProcessBatchLogic();
                    response.Batch.SetDependencies(appConfig, userSession);
                    response.Batch.BatchId = batchId;
                    await response.Batch.LoadAsync<VendorInvoiceProcessBatchLogic>();
                }

            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

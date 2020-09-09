using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Billing.Invoice
{
    public class CreditInvoiceRequest
    {
        public string InvoiceId { get; set; }
        public decimal? Percent { get; set; }
        public decimal? Amount { get; set; }
        public bool? Allocate { get; set; }
        public decimal? UsageDays { get; set; }
        public string Notes { get; set; }
        public bool? TaxOnly { get; set; }
        public DateTime? CreditFromDate { get; set; }
        public DateTime? CreditToDate { get; set; }
        public string CreditMethod { get; set; }
        public bool? AdjustCost { get; set; }
    }

    public class CreditInvoiceReponse : TSpStatusResponse
    {
        public string CreditId { get; set; }
        public InvoiceLogic credit { get; set; }
    }

    public class ToggleInvoiceApprovedResponse : TSpStatusResponse
    {
    }
    public static class InvoiceFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> VoidInvoice(FwApplicationConfig appConfig, FwUserSession userSession, string invoiceId)
        {
            TSpStatusResponse response = new TSpStatusResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "deleteinvoice", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@void", SqlDbType.NVarChar, ParameterDirection.Input, "T");
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = true;
                response.msg = "";
                //response.success = (qry.GetParameter("@status").ToInt32() == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreditInvoiceReponse> CreateInvoiceCredit(FwApplicationConfig appConfig, FwUserSession userSession, CreditInvoiceRequest request)
        {
            CreditInvoiceReponse response = new CreditInvoiceReponse();

            if (string.IsNullOrEmpty(request.CreditMethod))
            {
                response.success = false;
                response.msg = "No Credit Method indicated.";
            }
            else
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "createinvoicecreditweb", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, request.InvoiceId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@percent", SqlDbType.NVarChar, ParameterDirection.Input, request.Percent);
                    qry.AddParameter("@amount", SqlDbType.NVarChar, ParameterDirection.Input, request.Amount);
                    qry.AddParameter("@allocate", SqlDbType.NVarChar, ParameterDirection.Input, request.Allocate);
                    qry.AddParameter("@usagedays", SqlDbType.NVarChar, ParameterDirection.Input, request.UsageDays);
                    qry.AddParameter("@notes", SqlDbType.NVarChar, ParameterDirection.Input, request.Notes);
                    qry.AddParameter("@taxonly", SqlDbType.NVarChar, ParameterDirection.Input, request.TaxOnly);
                    qry.AddParameter("@creditfromdate", SqlDbType.Date, ParameterDirection.Input, request.CreditFromDate);
                    qry.AddParameter("@credittodate", SqlDbType.Date, ParameterDirection.Input, request.CreditToDate);
                    qry.AddParameter("@creditmethod", SqlDbType.NVarChar, ParameterDirection.Input, request.CreditMethod);
                    qry.AddParameter("@adjustcost", SqlDbType.NVarChar, ParameterDirection.Input, request.AdjustCost);
                    qry.AddParameter("@creditid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.CreditId = qry.GetParameter("@creditid").ToString();
                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = (response.status == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ToggleInvoiceApprovedResponse> ToggleInvoiceApproved(FwApplicationConfig appConfig, FwUserSession userSession, string invoiceId)
        {
            ToggleInvoiceApprovedResponse response = new ToggleInvoiceApprovedResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "toggleapproveinvoice", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@pushchangestopo", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                //qry.AddParameter("@approveifdealinvisnew", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TSpStatusResponse> AfterSaveInvoice(FwApplicationConfig appConfig, FwUserSession userSession, string invoiceId, FwSqlConnection conn = null)
        {
            TSpStatusResponse response = new TSpStatusResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }

            FwSqlCommand qry = new FwSqlCommand(conn, "aftersaveinvoiceweb", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, invoiceId);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.status = qry.GetParameter("@status").ToInt32();
            response.success = (response.status == 0);
            response.msg = qry.GetParameter("@msg").ToString();
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

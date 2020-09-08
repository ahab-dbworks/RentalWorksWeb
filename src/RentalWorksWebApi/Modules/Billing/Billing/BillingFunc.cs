using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Billing.Billing
{


    public class PopulateBillingRequest
    {
        public DateTime BillAsOfDate { get; set; }
        public string OfficeLocationId { get; set; }
        public string CustomerId { get; set; }
        public string DealId { get; set; }
        public string DepartmentId { get; set; }
        public string AgentId { get; set; }
        public string OrderId { get; set; }
        public bool? ShowOrdersWithPendingPO { get; set; }
        public bool? BillIfComplete { get; set; }
        public bool? CombinePeriods { get; set; }
        public bool? IncludeTotals { get; set; }
    }

    public class PopulateBillingResponse : TSpStatusResponse
    {
        public string SessionId { get; set; }
        public int BillingMessages { get; set; }
    }


    public class CreateInvoicesRequest
    {
        public string SessionId { get; set; } = "";
        public List<string> BillingIds { get; set; } = new List<string>();
    }

    public class CreateInvoicesResponse : TSpStatusResponse
    {
        public string InvoiceCreationBatchId { get; set; } = "";
    }

    public static class BillingFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PopulateBillingResponse> Populate(FwApplicationConfig appConfig, FwUserSession userSession, PopulateBillingRequest request)
        {
            PopulateBillingResponse response = new PopulateBillingResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webpopulatebilling", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@asofdate", SqlDbType.Date, ParameterDirection.Input, request.BillAsOfDate);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.OfficeLocationId);
                    qry.AddParameter("@customerid", SqlDbType.NVarChar, ParameterDirection.Input, request.CustomerId);
                    qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, request.DealId);
                    qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, request.DepartmentId);
                    qry.AddParameter("@agentid", SqlDbType.NVarChar, ParameterDirection.Input, request.AgentId);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                    qry.AddParameter("@pending", SqlDbType.NVarChar, ParameterDirection.Input, request.ShowOrdersWithPendingPO);
                    qry.AddParameter("@combineperiods", SqlDbType.NVarChar, ParameterDirection.Input, request.CombinePeriods);
                    qry.AddParameter("@billifcomplete", SqlDbType.NVarChar, ParameterDirection.Input, request.BillIfComplete);
                    qry.AddParameter("@includetotals", SqlDbType.NVarChar, ParameterDirection.Input, request.IncludeTotals);
                    //               @flatorder       char(01) = 'F',
                    //               @flatbill        char(01) = 'F',
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@billingmsgs", SqlDbType.Int, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.SessionId = qry.GetParameter("@sessionid").ToString();
                    response.BillingMessages = qry.GetParameter("@billingmsgs").ToInt32();
                    response.success = true;
                    response.msg = "";
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<CreateInvoicesResponse> CreateInvoices(FwApplicationConfig appConfig, FwUserSession userSession, CreateInvoicesRequest request)
        {
            CreateInvoicesResponse response = new CreateInvoicesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "webcreateinvoices", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                    qry.AddParameter("@billingids", SqlDbType.NVarChar, ParameterDirection.Input, string.Join(',', request.BillingIds));
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@invoicebatchid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.InvoiceCreationBatchId = qry.GetParameter("@invoicebatchid").ToString();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> GetNextBillingWorksheetNumberAsync(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, FwSqlConnection conn = null)
        {
            string worksheetNo = "";
            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "getnextworksheetno", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
            //qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            //qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, locationId);
            qry.AddParameter("@worksheetno", SqlDbType.NVarChar, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            worksheetNo = qry.GetParameter("@worksheetno").ToString().TrimEnd();
            return worksheetNo;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

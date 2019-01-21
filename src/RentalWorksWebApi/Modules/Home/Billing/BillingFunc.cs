using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Billing
{

    public class CreateInvoicesRequest
    {
        public string SessionId { get; set; } = "";
        public List<string> BillingIds { get; set; } = new List<string>();
    }

    public class CreateInvoicesResponse : TSpStatusReponse
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
                    //               @pending         char(01) = 'F',
                    //               @flatorder       char(01) = 'F',
                    //               @flatbill        char(01) = 'F',
                    //               @combineperiods  char(01) = 'F',
                    //               @billifcomplete  char(01) = 'T',
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);
                    response.SessionId = qry.GetParameter("@sessionid").ToString();
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
                    await qry.ExecuteNonQueryAsync(true);
                    response.InvoiceCreationBatchId = qry.GetParameter("@invoicebatchid").ToString();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

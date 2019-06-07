using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Home.Invoice
{
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
    }
}

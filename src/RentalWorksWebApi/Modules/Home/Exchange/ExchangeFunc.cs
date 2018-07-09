using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Home.Exchange
{


    public class TExchangeItemReponse : TSpStatusReponse
    {
        //public string InventoryId;
        //public string OrderItemId;
        //public int QuantityStaged;
    }

    public static class ExchangeFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> CreateExchangeContract(FwApplicationConfig appConfig, FwUserSession userSession, string OrderId, string DealId, string DepartmentId)
        {
            string contractId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "createexchangecontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, DealId);
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, DepartmentId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@exchangecontractid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                contractId = qry.GetParameter("@exchangecontractid").ToString();
            }
            return contractId;
        }
        /*
         
create procedure dbo.exchangebc(@exchangecontractid  char(08),
                                @inbarcode           varchar(40),
                                @outbarcode          varchar(40),
                                @pendingorderid      char(08) = '',
                                @pendingmasteritemid char(08) = '',
                                @allowcrossicode     char(01) = 'F',
                                @torepair            char(10) = 'DEFAULT',   --// DEFAULT, T, F
                                @usersid             char(08),
                                @status              integer      = 0  output,
                                @msg                 varchar(255) = '' output)
         
         
         */
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<TExchangeItemReponse> ExchangeItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string inCode, int? quantity, string outCode)
        {
            TExchangeItemReponse response = new TExchangeItemReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "exchangebc", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@exchangecontractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                qry.AddParameter("@inbarcode", SqlDbType.NVarChar, ParameterDirection.Input, inCode);
                qry.AddParameter("@outbarcode", SqlDbType.NVarChar, ParameterDirection.Input, outCode);
                //if (quantity != null)
                //{
                //    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                //}
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                //response.InventoryId = qry.GetParameter("@masterid").ToString();
                //response.OrderItemId = qry.GetParameter("@outputmasteritemid").ToString();
                //response.QuantityExchanged = qry.GetParameter("@qtystaged").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = ((response.status == 0) || (response.status == 107));
                response.msg = qry.GetParameter("@msg").ToString();

                //if (response.success)
                //{
                //    qry = new FwSqlCommand(conn, "staginggetmasterinfo", appConfig.DatabaseSettings.QueryTimeout);
                //    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                //    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, response.InventoryId);
                //    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //    qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                //    qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                //    qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                //    qry.AddParameter("@qtysub", SqlDbType.Int, ParameterDirection.Output);
                //    qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);
                //    qry.AddParameter("@qtyout", SqlDbType.Int, ParameterDirection.Output);
                //    qry.AddParameter("@qtyin", SqlDbType.Int, ParameterDirection.Output);
                //    qry.AddParameter("@qtyremaining", SqlDbType.Int, ParameterDirection.Output);
                //    await qry.ExecuteNonQueryAsync(true);

                //    response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                //    response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                //    response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                //    response.InventoryStatus.QuantitySub = qry.GetParameter("@qtysub").ToInt32();
                //    response.InventoryStatus.QuantityExchanged = qry.GetParameter("@qtystaged").ToInt32();
                //    response.InventoryStatus.QuantityOut = qry.GetParameter("@qtyout").ToInt32();
                //    response.InventoryStatus.QuantityIn = qry.GetParameter("@qtyin").ToInt32();
                //    response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();
                //}
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

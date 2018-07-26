using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Home.Exchange
{


    public class ExchangeItemSpStatusReponse : TSpStatusReponse
    {
        public string OrderId;
        public string OrderNumber;
        public string OrderDescription;
        public string DealId;
        public string Deal;
        public string DepartmentId;
        public string InventoryId;
        public string ICode;
        public string ItemDescription;
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
        public static async Task<ExchangeItemSpStatusReponse> ExchangeItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string orderId, string dealId, string departmentId, string inCode, int? quantity, string outCode)
        {
            ExchangeItemSpStatusReponse response = new ExchangeItemSpStatusReponse();

            if (string.IsNullOrEmpty(outCode))  //user is supplying an In Code.  We are validating the code to provide metadata about the item
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "getinbarcodeexchangeiteminfo", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, inCode);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, dealId);
                    qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, departmentId);
                    //@warehouseid char(08) = null,
                    //@ordertype varchar(15)  = null,
                    qry.AddParameter("@returnitemorderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemdealid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemdeal", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemdepartmentid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemmasterid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemtrackedby", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemrentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemvendorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemconsignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemconsignoragreementid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitempoid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemreturntowarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemordertranid", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@returniteminternalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemmasterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemdescription", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemorderno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemorderdesc", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemavailfromdatetime", SqlDbType.DateTime, ParameterDirection.Output);
                    qry.AddParameter("@returnitemavailtodatetime", SqlDbType.DateTime, ParameterDirection.Output);
                    qry.AddParameter("@returnitempendingrepairid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);
                    response.OrderId = qry.GetParameter("@returnitemorderid").ToString().TrimEnd();
                    response.OrderNumber = qry.GetParameter("@returnitemorderno").ToString().TrimEnd();
                    response.OrderDescription = qry.GetParameter("@returnitemorderdesc").ToString().TrimEnd();
                    response.DealId = qry.GetParameter("@returnitemdealid").ToString().TrimEnd();
                    response.Deal = qry.GetParameter("@returnitemdeal").ToString().TrimEnd();
                    response.DepartmentId = qry.GetParameter("@returnitemdepartmentid").ToString().TrimEnd();
                    response.InventoryId = qry.GetParameter("@returnitemmasterid").ToString().TrimEnd();
                    response.ICode = qry.GetParameter("@returnitemmasterno").ToString().TrimEnd();
                    response.ItemDescription = qry.GetParameter("@returnitemdescription").ToString().TrimEnd();

                    response.msg = qry.GetParameter("@returnmsg").ToString();
                    response.success = (string.IsNullOrEmpty(response.msg));
                }
            }
            else
            {

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


                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inrentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@invendorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inmasterid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inmasterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inmaster", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inreturntowarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@indescription", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@intrackedby", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inordertranid", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@ininternalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inconsignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@inconsignoragreementid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outrentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outvendorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasterid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmaster", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outdescription", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outtrackedby", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outordertranid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outinternalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outconsignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outconsignoragreementid", SqlDbType.NVarChar, ParameterDirection.Output);


                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);

                    // begin outputs
                    response.OrderId = qry.GetParameter("@orderid").ToString().TrimEnd();
                    //response.OrderNumber = qry.GetParameter("@returnitemorderno").ToString().TrimEnd();
                    //response.OrderDescription = qry.GetParameter("@returnitemorderdesc").ToString().TrimEnd();
                    //response.DealId = qry.GetParameter("@returnitemdealid").ToString().TrimEnd();
                    //response.Deal = qry.GetParameter("@returnitemdeal").ToString().TrimEnd();
                    //response.DepartmentId = qry.GetParameter("@returnitemdepartmentid").ToString().TrimEnd();
                    response.InventoryId = qry.GetParameter("@outmasterid").ToString().TrimEnd();
                    response.ICode = qry.GetParameter("@outmasterno").ToString().TrimEnd();
                    response.ItemDescription = qry.GetParameter("@outmaster").ToString().TrimEnd();
                    // end outputs




                    response.status = qry.GetParameter("@status").ToInt32();
                    response.success = ((response.status == 0) || (response.status == 107));
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

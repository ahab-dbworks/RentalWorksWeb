using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Home.CheckIn
{

    public class OrderInventoryStatus
    {
        public string ICode;
        public string Description;
        public int QuantityOrdered;
        public int QuantitySub;
        public int QuantityStaged;
        public int QuantityOut;
        public int QuantityIn;
        public int QuantityRemaining;
    }

    public class TCheckInItemReponse : TSpStatusReponse
    {
        public string ContractId;
        public string OrderId;
        public string OrderNumber;
        public string OrderDescription;
        public string DealId;
        public string Deal;
        public string DepartmentId;
        public string Department;
        public string InventoryId;
        public string OrderItemId;
        public OrderInventoryStatus InventoryStatus = new OrderInventoryStatus();
    }

    public static class CheckInFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> CreateCheckInContract(FwApplicationConfig appConfig, FwUserSession userSession, string OrderId, string DealId, string DepartmentId)
        {
            string contractId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                if (OrderId == null)
                {
                    OrderId = "";
                }
                FwSqlCommand qry = new FwSqlCommand(conn, "createincontract", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, OrderId);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, DealId);
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, DepartmentId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                contractId = qry.GetParameter("@contractid").ToString();
            }
            return contractId;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TCheckInItemReponse> CheckInItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, string code, int? quantity)
        {

            /*
             
create procedure dbo.pdacheckinitem(@code                   varchar(255),
                                    @usersid                char(08),
                                    @qty                    numeric ,
                                    @neworderaction         char(01),            --//Y = yes check-In, S = Swap else do nothing
                                    @moduletype             char(01),            --//O = Order       , T = Transfer,     P = Package Truck
                                    @checkinmode            char(01),            --//M = Multi Order , O = Single Order, D = Deal, S = Session
                                    @containeritemid        char(08)   = '',  --//mv 5/2/16 CAS-16066-F6P9
                                    @containeroutcontractid char(08)   = '',  --//mv 5/2/16 CAS-16066-F6P9
                                    @trackedby              varchar(255)    = null,
                                    @aisle                  varchar(255)    = null,
                                    @shelf                  varchar(255)    = null,
                                    @parentid               char(08)        = null,
                                    @disablemultiorder      char(01)        = '',  --//eg 1/15/15 CAS-09425-H05S
                                    @vendorid               char(08)        = '' output, --//input/output
                                    @vendor                 varchar(255)    = '' output,
                                    @incontractid           char(08)        = '' output, --//input/output
                                    @orderid                char(08)        = '' output, --//input/output
                                    @dealid                 char(08)        = '' output, --//input/output
                                    @departmentid           char(08)        = '' output, --//input/output
                                    @itemorderid            char(08)        = '' output,
                                    @masteritemid           char(08)        = '' output,
                                    @masterid               char(08)        = '' output,
                                    @warehouseid            char(08)        = '' output,
                                    @ordertranid            integer         = 0  output,
                                    @internalchar           char(01)        = '' output,
                                    @rentalitemid           char(08)        = '' output,
                                    @isicode                char(01)        = '' output,
                                    @orderno                varchar(255)    = '' output,
                                    @masterno               varchar(255)    = '' output,
                                    @description            varchar(255)    = '' output,
                                    @allowswap              char(01)        = '' output,
                                    @qtyordered             numeric(09, 02) = 0  output,
                                    @subqty                 numeric(09, 02) = 0  output,
                                    @stillout               numeric(09, 02) = 0  output,
                                    @totalin                numeric(09, 02) = 0  output,
                                    @sessionin              numeric(09, 02) = 0  output,
                                    @pendingrepairid        char(08)        = '' output,  --//jh 05/12/2016 CAS-17895-K7P2
                                    @exchangecontractid     char(08)        = '' output,  --//input/output

                                    @genericmsg             varchar(255)    = '' output,
                                    @status                 numeric         = 0  output,
                                    @msg                    varchar(255)    = '' output
             
             
             */
            TCheckInItemReponse response = new TCheckInItemReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "pdacheckinitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@incontractid", SqlDbType.NVarChar, ParameterDirection.InputOutput, contractId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, code);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                if (quantity != null)
                {
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                }
                qry.AddParameter("@neworderaction", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@moduletype", SqlDbType.NVarChar, ParameterDirection.Input, "O");
                qry.AddParameter("@checkinmode", SqlDbType.NVarChar, ParameterDirection.Input, "O");
                qry.AddParameter("@itemorderid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@orderdesc", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@deal", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@department", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@isicode", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@subqty", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@stillout", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@totalin", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.ContractId = qry.GetParameter("@incontractid").ToString();
                response.OrderId = qry.GetParameter("@itemorderid").ToString();
                response.OrderNumber = qry.GetParameter("@orderno").ToString();
                response.OrderDescription = qry.GetParameter("@orderdesc").ToString();
                response.DealId = qry.GetParameter("@dealid").ToString();
                response.Deal = qry.GetParameter("@deal").ToString();
                response.DepartmentId = qry.GetParameter("@departmentid").ToString();
                response.Department = qry.GetParameter("@department").ToString();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.OrderItemId = qry.GetParameter("@masteritemid").ToString();

                response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                response.InventoryStatus.QuantitySub = qry.GetParameter("@subqty").ToInt32();
                //response.InventoryStatus.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                response.InventoryStatus.QuantityOut = qry.GetParameter("@stillout").ToInt32();
                response.InventoryStatus.QuantityIn = qry.GetParameter("@totalin").ToInt32();
                //response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();

                if ((response.status == 0) && ((quantity == null) || (quantity == 0)) && (qry.GetParameter("@isicode").ToString().Equals("T")))
                {
                    response.status = 107;
                }

            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

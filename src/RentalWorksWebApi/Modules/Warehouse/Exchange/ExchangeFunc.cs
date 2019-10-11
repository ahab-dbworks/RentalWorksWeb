using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Warehouse.Exchange
{

    public class ExchangeItemStatus
    {
        public string InventoryId;
        public string ICode;
        public string AvailableFor;
        public string Description;
        public string WarehouseId;
        public string Warehouse;
        public string VendorId;
        public string Vendor;
        public string PurchaseOrderId;
        public string PurchaseOrderNumber;
        public string ConsignorId;
        public string Consignor;
    }


    public class ExchangeItemSpStatusResponse : TSpStatusResponse
    {
        public string OrderId;
        public string OrderNumber;
        public string OrderDescription;
        public string DealId;
        public string Deal;
        public string DepartmentId;
        public ExchangeItemStatus ItemStatus = new ExchangeItemStatus();
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
                await qry.ExecuteNonQueryAsync();
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
        public static async Task<ExchangeItemSpStatusResponse> ExchangeItem(FwApplicationConfig appConfig, FwUserSession userSession, string contractId, /*string orderId, string dealId, string departmentId,*/ string inCode, int? quantity, string outCode)
        {
            ExchangeItemSpStatusResponse response = new ExchangeItemSpStatusResponse();

            if (string.IsNullOrEmpty(outCode))  //user is supplying an In Code.  We are validating the code to provide metadata about the item
            {
                using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
                {
                    FwSqlCommand qry = new FwSqlCommand(conn, "getinbarcodeexchangeiteminfo", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@exchangecontractid", SqlDbType.NVarChar, ParameterDirection.Input, contractId);
                    qry.AddParameter("@barcode", SqlDbType.NVarChar, ParameterDirection.Input, inCode);
                    //qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    //qry.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Input, dealId);
                    //qry.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Input, departmentId);
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
                    qry.AddParameter("@returnitemvendor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemconsignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemconsignor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemconsignoragreementid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitempoid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitempono", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemwarehouse", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemreturntowarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemordertranid", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@returniteminternalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemmasterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemavailfor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemdescription", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemorderno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemorderdesc", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnitemavailfromdatetime", SqlDbType.DateTime, ParameterDirection.Output);
                    qry.AddParameter("@returnitemavailtodatetime", SqlDbType.DateTime, ParameterDirection.Output);
                    qry.AddParameter("@returnitempendingrepairid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@returnmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    response.OrderId = qry.GetParameter("@returnitemorderid").ToString().TrimEnd();
                    response.OrderNumber = qry.GetParameter("@returnitemorderno").ToString().TrimEnd();
                    response.OrderDescription = qry.GetParameter("@returnitemorderdesc").ToString().TrimEnd();
                    response.DealId = qry.GetParameter("@returnitemdealid").ToString().TrimEnd();
                    response.Deal = qry.GetParameter("@returnitemdeal").ToString().TrimEnd();
                    response.DepartmentId = qry.GetParameter("@returnitemdepartmentid").ToString().TrimEnd();
                    response.ItemStatus.InventoryId = qry.GetParameter("@returnitemmasterid").ToString().TrimEnd();
                    response.ItemStatus.ICode = qry.GetParameter("@returnitemmasterno").ToString().TrimEnd();
                    response.ItemStatus.AvailableFor = qry.GetParameter("@returnitemavailfor").ToString().TrimEnd();
                    response.ItemStatus.Description = qry.GetParameter("@returnitemdescription").ToString().TrimEnd();
                    response.ItemStatus.WarehouseId = qry.GetParameter("@returnitemwarehouseid").ToString().TrimEnd();
                    response.ItemStatus.Warehouse = qry.GetParameter("@returnitemwarehouse").ToString().TrimEnd();
                    response.ItemStatus.VendorId = qry.GetParameter("@returnitemvendorid").ToString().TrimEnd();
                    response.ItemStatus.Vendor = qry.GetParameter("@returnitemvendor").ToString().TrimEnd();
                    response.ItemStatus.PurchaseOrderId = qry.GetParameter("@returnitempoid").ToString().TrimEnd();
                    response.ItemStatus.PurchaseOrderNumber = qry.GetParameter("@returnitempono").ToString().TrimEnd();
                    response.ItemStatus.ConsignorId = qry.GetParameter("@returnitemconsignorid").ToString().TrimEnd();
                    response.ItemStatus.Consignor = qry.GetParameter("@returnitemconsignor").ToString().TrimEnd();
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
                    qry.AddParameter("@outvendor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outpoid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outpono", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasterid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outavailfor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmaster", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outwarehouse", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outdescription", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outtrackedby", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outordertranid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outinternalchar", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outconsignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outconsignor", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@outconsignoragreementid", SqlDbType.NVarChar, ParameterDirection.Output);


                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();

                    // begin outputs
                    response.OrderId = qry.GetParameter("@orderid").ToString().TrimEnd();
                    //response.OrderNumber = qry.GetParameter("@returnitemorderno").ToString().TrimEnd();
                    //response.OrderDescription = qry.GetParameter("@returnitemorderdesc").ToString().TrimEnd();
                    //response.DealId = qry.GetParameter("@returnitemdealid").ToString().TrimEnd();
                    //response.Deal = qry.GetParameter("@returnitemdeal").ToString().TrimEnd();
                    //response.DepartmentId = qry.GetParameter("@returnitemdepartmentid").ToString().TrimEnd();
                    response.ItemStatus.InventoryId = qry.GetParameter("@outmasterid").ToString().TrimEnd();
                    response.ItemStatus.ICode = qry.GetParameter("@outmasterno").ToString().TrimEnd();
                    response.ItemStatus.AvailableFor = qry.GetParameter("@outavailfor").ToString().TrimEnd();
                    response.ItemStatus.Description = qry.GetParameter("@outmaster").ToString().TrimEnd();
                    response.ItemStatus.WarehouseId = qry.GetParameter("@outwarehouseid").ToString().TrimEnd();
                    response.ItemStatus.Warehouse = qry.GetParameter("@outwarehouse").ToString().TrimEnd();
                    response.ItemStatus.VendorId = qry.GetParameter("@outvendorid").ToString().TrimEnd();
                    response.ItemStatus.Vendor = qry.GetParameter("@outvendor").ToString().TrimEnd();
                    response.ItemStatus.PurchaseOrderId = qry.GetParameter("@outpoid").ToString().TrimEnd();
                    response.ItemStatus.PurchaseOrderNumber = qry.GetParameter("@outpono").ToString().TrimEnd();
                    response.ItemStatus.ConsignorId = qry.GetParameter("@outconsignorid").ToString().TrimEnd();
                    response.ItemStatus.Consignor = qry.GetParameter("@outconsignor").ToString().TrimEnd();
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

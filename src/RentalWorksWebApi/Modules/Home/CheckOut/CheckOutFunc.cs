using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Home.CheckOut
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

public class TStageItemReponse: TSpStatusReponse
    {
        public string InventoryId;
        public string OrderItemId;
        public int QuantityStaged;

        public OrderInventoryStatus InventoryStatus = new OrderInventoryStatus();
    }

    public static class CheckOutFunc
    {
        /*
                                       @vendorid                     char(08)      = '',
                                       @meter                        numeric(11,2) = 0,
                                       @location                     varchar(255)  = '',
                                       @spaceid                      char(08)      = '',
                                       @spacetypeid                  char(08)      = '',
                                       @facilitiestypeid             char(08)      = '',
                                       @additemtoorder               char(01)      = 'F',
                                       @additemtopackagemasteritemid char(08)      = '',
                                       @addcompletetoorder           char(01)      = 'F',
                                       @addcontainertoorder          char(01)      = 'F',
                                       @overridereservation          char(01)      = 'F',
                                       @stageconsigned               char(01)      = 'F',
                                       @transferrepair               char(01)      = 'F',
                                       @removefromcontainer          char(01)      = 'F',
                                       @releasefromrepair            char(01)      = 'F',       --//jh 06/30/2015 CAS-15904-IVQS
                                       @autostageacc                 char(01)      = 'F',
                                       @contractid                   char(08)      = '',  --// only supply a value if item should go out immediately
                                       @ignoresuspendedin            char(01)      = '',
                                       @incompletecontainerok        char(01)      = 'F',
                                       @ignoreassignedcontract       char(01)      = '',
                                       @rentalitemid                 char(08)      = ''  output,
                                       @consignorid                  char(08)      = ''  output,   --//also used as an input value for quantity items
                                       @consignoragreementid         char(08)      = ''  output,   --//also used as an input value for quantity items
                                       @consignorpoid                char(08)      = ''  output,
                                       @exceptionbatchid             char(30)      = ''  output,

                     */
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TStageItemReponse> StageItem(FwApplicationConfig appConfig, FwUserSession userSession, string orderId, /*string orderItemId,*/ string code, int? quantity)
        {
            TStageItemReponse response = new TStageItemReponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "stageitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                //qry.AddParameter("@masteritemid", SqlDbType.NVarChar, ParameterDirection.Input, orderItemId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, code);
                if (quantity != null)
                {
                    qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                }
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@outputmasteritemid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync(true);
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.OrderItemId = qry.GetParameter("@outputmasteritemid").ToString();
                response.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = ((response.status == 0) || (response.status == 107));
                response.msg = qry.GetParameter("@msg").ToString();

                if (response.success) {
                    qry = new FwSqlCommand(conn, "staginggetmasterinfo", appConfig.DatabaseSettings.QueryTimeout);
                    qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, orderId);
                    qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, response.InventoryId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@description", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtysub", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtystaged", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyout", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyin", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@qtyremaining", SqlDbType.Int, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync(true);

                    response.InventoryStatus.ICode = qry.GetParameter("@masterno").ToString();
                    response.InventoryStatus.Description = qry.GetParameter("@description").ToString();
                    response.InventoryStatus.QuantityOrdered = qry.GetParameter("@qtyordered").ToInt32();
                    response.InventoryStatus.QuantitySub = qry.GetParameter("@qtysub").ToInt32();
                    response.InventoryStatus.QuantityStaged = qry.GetParameter("@qtystaged").ToInt32();
                    response.InventoryStatus.QuantityOut = qry.GetParameter("@qtyout").ToInt32();
                    response.InventoryStatus.QuantityIn = qry.GetParameter("@qtyin").ToInt32();
                    response.InventoryStatus.QuantityRemaining = qry.GetParameter("@qtyremaining").ToInt32();
                }
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

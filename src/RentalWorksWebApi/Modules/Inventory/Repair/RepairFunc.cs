using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.RepairPart;

namespace WebApi.Modules.Inventory.Repair
{
    public class ToggleRepairEstimateResponse : TSpStatusResponse
    {
        public RepairLogic repair;
    }
    public class ToggleRepairCompleteResponse : TSpStatusResponse
    {
        public RepairLogic repair;
    }
    public class RepairReleaseItemsResponse : TSpStatusResponse
    {
        public RepairLogic repair;
    }
    public class VoidRepairResponse : TSpStatusResponse
    {
        public RepairLogic repair;
    }
    

    public static class RepairFunc
    {

        //-------------------------------------------------------------------------------------------------------
        public static async Task<ToggleRepairEstimateResponse> ToggleRepairEstimate(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            ToggleRepairEstimateResponse response = new ToggleRepairEstimateResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "togglerepairestimated", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ToggleRepairCompleteResponse> ToggleRepairComplete(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            ToggleRepairCompleteResponse response = new ToggleRepairCompleteResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "togglerepaircomplete", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<RepairReleaseItemsResponse> ReleaseRepairItems(FwApplicationConfig appConfig, FwUserSession userSession, string repairId, int quantity)
        {
            RepairReleaseItemsResponse response = new RepairReleaseItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "releaserepairitems", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@qty", SqlDbType.Int, ParameterDirection.Input, quantity);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<VoidRepairResponse> VoidRepair(FwApplicationConfig appConfig, FwUserSession userSession, string repairId)
        {
            VoidRepairResponse response = new VoidRepairResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "voidrepairweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, repairId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.success = (qry.GetParameter("@status").ToInt32() == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<string> InsertRepairPackage(FwApplicationConfig appConfig, FwUserSession userSession, RepairPartLogic rpi)
        {
            string newRepairPartId = "";
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "insertrepairpackage", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@repairid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.RepairId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, rpi.WarehouseId);
                qry.AddParameter("@catalogid", SqlDbType.NVarChar, ParameterDirection.Input, "");
                qry.AddParameter("@packagetype", SqlDbType.NVarChar, ParameterDirection.Input, rpi.ItemClass);
                qry.AddParameter("@qty", SqlDbType.NVarChar, ParameterDirection.Input, rpi.Quantity);
                qry.AddParameter("@primaryrepairpartid", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                newRepairPartId = qry.GetParameter("@primaryrepairpartid").ToString();
            }
            return newRepairPartId;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

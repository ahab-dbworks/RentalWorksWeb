using FwStandard.Models;
using FwStandard.SqlServer;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Inventory.Inventory
{

    public class ChangeInventoryTrackedByRequest
    {
        public string InventoryId { get; set; }
        public string OldTrackedBy { get; set; }
        public string NewTrackedBy { get; set; }
    }

    public class ChangeInventoryTrackedByResponse : TSpStatusResponse
    {
        public int BarCodesCreated { get; set; }
    }

    public static class InventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<ChangeInventoryTrackedByResponse> ChangeInventoryTrackedBy(FwApplicationConfig appConfig, FwUserSession userSession, ChangeInventoryTrackedByRequest request, FwSqlConnection conn = null)
        {
            ChangeInventoryTrackedByResponse response = new ChangeInventoryTrackedByResponse();

            if (conn == null)
            {
                conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);
            }
            FwSqlCommand qry = new FwSqlCommand(conn, "changemastertrackedby", appConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, request.InventoryId);
            qry.AddParameter("@oldtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.OldTrackedBy);
            qry.AddParameter("@newtrackedby", SqlDbType.NVarChar, ParameterDirection.Input, request.NewTrackedBy);
            qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
            qry.AddParameter("@success", SqlDbType.NVarChar, ParameterDirection.Output);
            qry.AddParameter("@barcodescreated", SqlDbType.Int, ParameterDirection.Output);
            await qry.ExecuteNonQueryAsync();
            response.success = FwConvert.ToBoolean(qry.GetParameter("@success").ToString());
            response.BarCodesCreated = FwConvert.ToInt32(qry.GetParameter("@barcodescreated").ToString());
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}

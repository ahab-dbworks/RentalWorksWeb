using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.PhysicalInventory
{
    //-------------------------------------------------------------------------------------------------------
    public class PhysicalInventoryUpdateICodesRequest
    {
        public string PhysicalInventoryId { get; set; }
    }
    public class PhysicalInventoryUpdateICodesResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public static class PhysicalInventoryFunc
    {
        public static async Task<PhysicalInventoryUpdateICodesResponse> UpdateICodes(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryUpdateICodesRequest request)
        {
            PhysicalInventoryUpdateICodesResponse response = new PhysicalInventoryUpdateICodesResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "picycleupdateicodesweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }

}
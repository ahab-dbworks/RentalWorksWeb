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
    public class PhysicalInventoryPrescanRequest
    {
        public string PhysicalInventoryId { get; set; }
    }
    public class PhysicalInventoryPrescanResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public class PhysicalInventoryInitiateRequest
    {
        public string PhysicalInventoryId { get; set; }
    }
    public class PhysicalInventoryInitiateResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public class PhysicalInventoryApproveRequest
    {
        public string PhysicalInventoryId { get; set; }
    }
    public class PhysicalInventoryApproveResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public class PhysicalInventoryCloseRequest
    {
        public string PhysicalInventoryId { get; set; }
    }
    public class PhysicalInventoryCloseResponse : TSpStatusResponse { }
    //-------------------------------------------------------------------------------------------------------
    public static class PhysicalInventoryFunc
    {
        //-------------------------------------------------------------------------------------------------------
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
        public static async Task<PhysicalInventoryPrescanResponse> Prescan(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryPrescanRequest request)
        {
            PhysicalInventoryPrescanResponse response = new PhysicalInventoryPrescanResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "prescan", appConfig.DatabaseSettings.QueryTimeout);
                //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PhysicalInventoryInitiateResponse> Initiate(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryInitiateRequest request)
        {
            PhysicalInventoryInitiateResponse response = new PhysicalInventoryInitiateResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "initiate", appConfig.DatabaseSettings.QueryTimeout);
                //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PhysicalInventoryApproveResponse> Approve(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryApproveRequest request)
        {
            PhysicalInventoryApproveResponse response = new PhysicalInventoryApproveResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "approve", appConfig.DatabaseSettings.QueryTimeout);
                //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PhysicalInventoryCloseResponse> Close(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryCloseRequest request)
        {
            PhysicalInventoryCloseResponse response = new PhysicalInventoryCloseResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "close", appConfig.DatabaseSettings.QueryTimeout);
                //qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
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
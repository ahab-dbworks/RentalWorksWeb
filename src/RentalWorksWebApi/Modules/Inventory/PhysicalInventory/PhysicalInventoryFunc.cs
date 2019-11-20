using FwStandard.Models;
using FwStandard.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.Inventory.PhysicalInventory
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
    public class PhysicalInventoryCountBarCodeRequest
    {
        public string PhysicalInventoryId { get; set; }
        public string BarCode { get; set; }
    }
    public class PhysicalInventoryCountBarCodeResponse : TSpStatusResponse
    {
        public string InventoryId { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class PhysicalInventoryCountQuantityRequest
    {
        public string PhysicalInventoryId { get; set; }
        public string ICode { get; set; }
        public decimal? Quantity { get; set; }
        public bool? Replace { get; set; }
    }
    public class PhysicalInventoryCountQuantityResponse : TSpStatusResponse
    {
        public string InventoryId { get; set; }
        public string ICode { get; set; }
        public string Description { get; set; }
    }
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
                FwSqlCommand qry = new FwSqlCommand(conn, "physicalprescanweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
        public static async Task<PhysicalInventoryInitiateResponse> Initiate(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryInitiateRequest request)
        {
            PhysicalInventoryInitiateResponse response = new PhysicalInventoryInitiateResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "physicalinitiateweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
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
        public static async Task<PhysicalInventoryCountBarCodeResponse> CountBarCode(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryCountBarCodeRequest request)
        {
            PhysicalInventoryCountBarCodeResponse response = new PhysicalInventoryCountBarCodeResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "physicalcountitemweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.BarCode);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@isicode", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@showaddrep", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@master", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@consignor", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@physicalitemid", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@countedqty", SqlDbType.Decimal, ParameterDirection.Output);
                //qry.AddParameter("@genericmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.ICode = qry.GetParameter("@masterno").ToString();
                response.Description = qry.GetParameter("@master").ToString();
                response.ItemId = qry.GetParameter("@rentalitemid").ToString();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<PhysicalInventoryCountQuantityResponse> CountQuantity(FwApplicationConfig appConfig, FwUserSession userSession, PhysicalInventoryCountQuantityRequest request)
        {
            PhysicalInventoryCountQuantityResponse response = new PhysicalInventoryCountQuantityResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "physicalcountitemweb", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@physicalid", SqlDbType.NVarChar, ParameterDirection.Input, request.PhysicalInventoryId);
                qry.AddParameter("@code", SqlDbType.NVarChar, ParameterDirection.Input, request.ICode);
                qry.AddParameter("@qty", SqlDbType.Decimal, ParameterDirection.Input, request.Quantity);
                qry.AddParameter("@addreplace", SqlDbType.NVarChar, ParameterDirection.Input, request.Replace.GetValueOrDefault(false) ? RwConstants.PHYSICAL_INVENTORY_COUNT_REPLACE : RwConstants.PHYSICAL_INVENTORY_COUNT_ADD);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                //qry.AddParameter("@isicode", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@showaddrep", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterno", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@master", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@rentalitemid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@consignorid", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@consignor", SqlDbType.NVarChar, ParameterDirection.Output);
                //qry.AddParameter("@physicalitemid", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@countedqty", SqlDbType.Decimal, ParameterDirection.Output);
                //qry.AddParameter("@genericmsg", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.InventoryId = qry.GetParameter("@masterid").ToString();
                response.ICode = qry.GetParameter("@masterno").ToString();
                response.Description = qry.GetParameter("@master").ToString();
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
                FwSqlCommand qry = new FwSqlCommand(conn, "approvexxxxxx", appConfig.DatabaseSettings.QueryTimeout);
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
                FwSqlCommand qry = new FwSqlCommand(conn, "closexxxxxxx", appConfig.DatabaseSettings.QueryTimeout);
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
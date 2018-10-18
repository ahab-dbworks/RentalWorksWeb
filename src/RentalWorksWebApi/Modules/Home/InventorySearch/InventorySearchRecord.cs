using FwStandard.BusinessLogic;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Home.InventorySearch
{
    [FwSqlTable("tmpsearchsession")]
    public class InventorySearchRecord : AppDataReadWriteRecord
    {
        public InventorySearchRecord()
        {
            BeforeSave += OnBeforeSaveInventorySearch;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SessionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        // this property is only used to hold the return value in the overridden save method below
        public decimal? TotalQuantityInSession { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInventorySearch(object sender, BeforeSaveDataRecordEventArgs e)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                TSpStatusReponse response = new TSpStatusReponse();

                FwSqlCommand qry = new FwSqlCommand(conn, "savetmpsearchsession", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, SessionId);
                qry.AddParameter("@parentid", SqlDbType.NVarChar, ParameterDirection.Input, ParentId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, WarehouseId);
                qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, Quantity);
                qry.AddParameter("@totalqtyinsession", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                int i = qry.ExecuteNonQueryAsync(true).Result;
                TotalQuantityInSession = qry.GetParameter("@totalqtyinsession").ToDecimal();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();

                if (!response.success)
                {
                    throw new System.Exception("Cannot save search quantity: " + response.msg);
                }
            }
            e.PerformSave = false;
        }
        //-------------------------------------------------------------------------------------------------------   
        public async Task<InventorySearchGetTotalResponse> GetTotalAsync(InventorySearchGetTotalRequest request)
        {
            InventorySearchGetTotalResponse response = new InventorySearchGetTotalResponse();
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessiongettotal", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@totalqtyinsession", SqlDbType.Float, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                int i = await qry.ExecuteNonQueryAsync(true);
                response.TotalQuantityInSession = qry.GetParameter("@totalqtyinsession").ToDecimal();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------   
        public async Task<bool> AddToAsync(InventorySearchAddToRequest request)
        {
            bool b = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, UserSession.UsersId);
                int i = await qry.ExecuteNonQueryAsync(true);
            }
            return b;
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}
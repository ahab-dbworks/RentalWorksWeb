using FwStandard.BusinessLogic;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

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
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInventorySearch(object sender, BeforeSaveEventArgs e)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "savetmpsearchsession", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, SessionId);
                qry.AddParameter("@masterid", SqlDbType.NVarChar, ParameterDirection.Input, InventoryId);
                qry.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Input, WarehouseId);
                qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, Quantity);
                int i = qry.ExecuteNonQueryAsync(true).Result;
            }
            e.PerformSave = false;
        }
        //-------------------------------------------------------------------------------------------------------   
        public async Task<bool> AddToAsync(InventorySearchRequest request)
        {
            bool b = false;
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "tmpsearchsessionaddtoorder", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, request.SessionId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.OrderId);
                int i = await qry.ExecuteNonQueryAsync(true);
            }
            return b;
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}
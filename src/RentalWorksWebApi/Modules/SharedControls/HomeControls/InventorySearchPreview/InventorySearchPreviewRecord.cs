using FwStandard.BusinessLogic;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Data;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.HomeControls.InventorySearchPreview
{
    [FwSqlTable("tmpsearchsession")]
    public class InventorySearchPreviewRecord : AppDataReadWriteRecord
    {
        public InventorySearchPreviewRecord()
        {
            BeforeSave += OnBeforeSaveInventorySearchPreview;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text, sqltype: "int", isPrimaryKey: true)]
        public string Id { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "grandparentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GrandParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSaveInventorySearchPreview(object sender, BeforeSaveDataRecordEventArgs e)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "savetmpsearchsession", this.AppConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@id", SqlDbType.Int, ParameterDirection.Input, Id);
                qry.AddParameter("@qty", SqlDbType.Float, ParameterDirection.Input, Quantity);
                int i = qry.ExecuteNonQueryAsync().Result;
            }
            e.PerformSave = false;
        }
        //-------------------------------------------------------------------------------------------------------   
    }
}
using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.Models; 
using FwStandard.Data; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.WarehouseBarCodeSkip
{
    [FwSqlTable("barcodeskip")]
    public class WarehouseBarCodeSkipRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeskipid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseBarCodeSkipId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeskip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40, required: true)]
        public string WarehouseBarCodeSkip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
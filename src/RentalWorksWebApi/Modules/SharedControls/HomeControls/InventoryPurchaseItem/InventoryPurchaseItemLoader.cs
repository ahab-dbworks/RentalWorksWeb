using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.InventoryPurchaseItem
{
    [FwSqlTable("barcodeholdingview")]
    public class InventoryPurchaseItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodeholdingid", modeltype: FwDataTypes.Integer, isPrimaryKey: true, identity: true)]
        public int? InventoryPurchaseItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgdate", modeltype: FwDataTypes.Date)]
        public string ManufactureDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printqty", modeltype: FwDataTypes.Integer)]
        public int? PrintQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "serialno", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mixedcaseserialno", modeltype: FwDataTypes.Boolean)]
        public bool? SerialNumberIsMixedCase { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(sessionid > '')"); 
            select.AddWhere("(issuspend <> 'T')"); 
            addFilterToSelect("SessionId", "sessionid", select, request);
            addFilterToSelect("BarCode", "barcode", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

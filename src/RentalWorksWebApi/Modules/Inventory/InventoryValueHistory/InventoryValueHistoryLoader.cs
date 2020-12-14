using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Inventory.InventoryValueHistory
{
    [FwSqlTable("rptinvusageownhistoryview")]
    public class InventoryValueHistoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ownership", modeltype: FwDataTypes.Text)]
        public string Ownership { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datechange", modeltype: FwDataTypes.Date)]
        public string DateChange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtychange", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityChange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costextended", modeltype: FwDataTypes.Decimal)]
        public decimal? CostExtended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currency", modeltype: FwDataTypes.Text)]
        public string Currency { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbolandcode", modeltype: FwDataTypes.Text)]
        public string CurrencySymbolAndCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changedesc", modeltype: FwDataTypes.Text)]
        public string ChangeDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid01", modeltype: FwDataTypes.Text)]
        public string Uniqueid01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid02", modeltype: FwDataTypes.Text)]
        public string Uniqueid02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid03", modeltype: FwDataTypes.Text)]
        public string Uniqueid03 { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

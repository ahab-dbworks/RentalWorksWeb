using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Agent.OrderManifest
{
    [FwSqlTable("dbo.funcvaluesheet(@orderid, @rentalvalue, @salesvalue, @applanguageid, @outonly, @excel)")]
    public class OrderManifestLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quantity", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderitemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryoforigin", modeltype: FwDataTypes.Text)]
        public string Countryoforigin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueperitem", modeltype: FwDataTypes.Decimal)]
        public decimal? Valueperitem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dimensionslwh", modeltype: FwDataTypes.Text)]
        public string Dimensionslwh { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string Mfgserial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string MfgPartNo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "valueextended", modeltype: FwDataTypes.Decimal)]
        public decimal? Valueextended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightlbs", modeltype: FwDataTypes.Integer)]
        public int? Weightlbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightoz", modeltype: FwDataTypes.Integer)]
        public int? Weightoz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightkg", modeltype: FwDataTypes.Integer)]
        public int? Weightkg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weightgr", modeltype: FwDataTypes.Integer)]
        public int? Weightgr { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightlbs", modeltype: FwDataTypes.Integer)]
        public int? Extweightlbs { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightoz", modeltype: FwDataTypes.Integer)]
        public int? Extweightoz { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightkg", modeltype: FwDataTypes.Integer)]
        public int? Extweightkg { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extweightgr", modeltype: FwDataTypes.Integer)]
        public int? Extweightgr { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string orderId          = GetUniqueIdAsString("OrderId", request) ?? ""; 
            string rentalValue      = GetUniqueIdAsString("RentalValue", request) ?? ""; 
            string salesValue       = GetUniqueIdAsString("SalesValue", request) ?? ""; 
            string appLanguageId    = GetUniqueIdAsString("AppLanguageId", request) ?? ""; 
            string outOnly          = GetUniqueIdAsString("OutOnly", request) ?? ""; 
            string excel            = GetUniqueIdAsString("Excel", request) ?? ""; 

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            //select.AddWhere("orderid(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 

            select.AddParameter("@orderid", orderId); 
            select.AddParameter("@rentalvalue", rentalValue); 
            select.AddParameter("@salesvalue", salesValue); 
            select.AddParameter("@applanguageid", appLanguageId); 
            select.AddParameter("@outonly", outOnly); 
            select.AddParameter("@excel", excel); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

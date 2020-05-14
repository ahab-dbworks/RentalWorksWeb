using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.StagedItem
{
    [FwSqlTable("dbo.funcstagingscanned(@orderid, @warehouseid)")]
    public class StagedItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public StagedItemLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string Rfid { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quantity", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outdatetime", modeltype: FwDataTypes.DateTime)]
        public string StagedDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor
        {
            get { return getRecTypeColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quoteprint", modeltype: FwDataTypes.Boolean)]
        public bool? QuotePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderprint", modeltype: FwDataTypes.Boolean)]
        public bool? OrderPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PickListPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractoutprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractOutPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnListPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceprint", modeltype: FwDataTypes.Boolean)]
        public bool? InvoicePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractinprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractInPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreceiveprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractReceivePrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractreturnprint", modeltype: FwDataTypes.Boolean)]
        public bool? ContractReturnPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreceivelistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderReceivelistPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poreturnlistprint", modeltype: FwDataTypes.Boolean)]
        public bool? PurchaseOrderReturnlistPrint { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outreceiveusersid", modeltype: FwDataTypes.Text)]
        public string StagedUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outuser", modeltype: FwDataTypes.Text)]
        public string StagedUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor
        {
            get { return getVendorColor(VendorId, ConsignorId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor
        {
            get { return getICodeColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        private string getICodeColor(string itemClass)
        {
            return AppFunc.GetItemClassICodeColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getVendorColor(string subVendorId, string consignorId)
        {
            return (!string.IsNullOrEmpty(subVendorId) ? RwGlobals.SUB_COLOR : (!string.IsNullOrEmpty(consignorId) ? RwGlobals.CONSIGNMENT_COLOR : null));
        }
        //------------------------------------------------------------------------------------ 
        private string getDescriptionColor(string itemClass)
        {
            return AppFunc.GetItemClassDescriptionColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getRecTypeColor(string recType)
        {
            return AppFunc.GetInventoryRecTypeColor(recType);
        }
        //------------------------------------------------------------------------------------    
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddParameter("@orderid", orderId);
            select.AddParameter("@warehouseid", warehouseId);
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("ICodeColor")] = getICodeColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(row[dt.GetColumnNo("ItemClass")].ToString());
                        row[dt.GetColumnNo("RecTypeColor")] = getRecTypeColor(row[dt.GetColumnNo("RecType")].ToString());
                        row[dt.GetColumnNo("VendorColor")] = getVendorColor(row[dt.GetColumnNo("VendorId")].ToString(), row[dt.GetColumnNo("ConsignorId")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using WebApi.Logic;

namespace WebApi.Modules.Home.CheckOutPendingItem
{
    [FwSqlTable("dbo.funccheckoutexception2(@orderid, @warehouseid, @contractid)")]
    public class CheckOutPendingItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public CheckOutPendingItemLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exceptionflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsException { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "someout", modeltype: FwDataTypes.Boolean)]
        public bool? SomeOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor
        {
            get { return getICodeColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subvendorid", modeltype: FwDataTypes.Text)]
        public string SubVendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text)]
        public string ConsignorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text)]
        public string ConsignorAgreementId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor
        {
            get { return getVendorColor(SubVendorId, ConsignorId); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reservedrentalitems", modeltype: FwDataTypes.Decimal)]
        public decimal? ReservedItems { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystagedandout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStagedAndOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysub", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantitySub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysubstagedandout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantitySubStagedAndOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysubout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantitySubOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsMissing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingqty", modeltype: FwDataTypes.Decimal)]
        public decimal? MissingQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string MissingColor
        {
            get { return getMissingColor(IsMissing.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeDisplay
        {
            get { return getRecTypeDisplay(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor
        {
            get { return determineRecTypeColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containermasterid", modeltype: FwDataTypes.Text)]
        public string ContainerInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text)]
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scannablemasterid", modeltype: FwDataTypes.Text)]
        public string ScannableInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            string warehouseId = GetUniqueIdAsString("WarehouseId", request) ?? "";
            string contractId = GetUniqueIdAsString("ContractId", request) ?? "";
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(exceptionflg = 'T')");
            select.AddWhere("(qtyordered > 0 or qtystagedandout > 0)");
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            select.AddParameter("@orderid", orderId);
            select.AddParameter("@warehouseid", warehouseId);
            select.AddParameter("@contractid", contractId);
        }
        //------------------------------------------------------------------------------------ 
        private string getICodeColor(string itemClass)
        {
            return AppFunc.GetItemClassICodeColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getDescriptionColor(string itemClass)
        {
            return AppFunc.GetItemClassDescriptionColor(itemClass);
        }
        //------------------------------------------------------------------------------------ 
        private string getMissingColor(bool isMissing)
        {
            return (isMissing ? RwGlobals.STAGING_PENDING_ITEMS_MISSING_COLOR : null);
        }
        //------------------------------------------------------------------------------------ 
        private string getVendorColor(string subVendorId, string consignorId)
        {
            return (!string.IsNullOrEmpty(subVendorId) ? RwGlobals.SUB_COLOR : (!string.IsNullOrEmpty(consignorId) ? RwGlobals.CONSIGNMENT_COLOR : null));
        }
        //------------------------------------------------------------------------------------ 
        private string getRecTypeDisplay(string recType)
        {
            return AppFunc.GetInventoryRecTypeDisplay(recType);
        }
        //------------------------------------------------------------------------------------ 
        private string determineRecTypeColor(string recType)
        {
            return AppFunc.GetInventoryRecTypeColor(recType);
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
                        row[dt.GetColumnNo("VendorColor")] = getVendorColor(row[dt.GetColumnNo("SubVendorId")].ToString(), row[dt.GetColumnNo("ConsignorId")].ToString());
                        row[dt.GetColumnNo("MissingColor")] = getMissingColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsMissing")].ToString()));
                        row[dt.GetColumnNo("RecTypeDisplay")] = getRecTypeDisplay(row[dt.GetColumnNo("RecType")].ToString());
                        row[dt.GetColumnNo("RecTypeColor")] = determineRecTypeColor(row[dt.GetColumnNo("RecType")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
namespace WebApi.Modules.Home.CheckOutPendingItem
{
    [FwSqlTable("dbo.funccheckoutexception2(@orderid, @warehouseid, @contractid)")]
    public class CheckOutPendingItemLoader : AppDataLoadRecord
    {
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
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
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
        [FwSqlDataField(column: "vendorcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor { get; set; }
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
        [FwSqlDataField(column: "missingcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string MissingColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
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
    }
}

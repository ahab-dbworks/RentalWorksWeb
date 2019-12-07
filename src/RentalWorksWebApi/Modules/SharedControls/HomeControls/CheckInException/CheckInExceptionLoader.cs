using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebApi;
namespace WebApi.Modules.HomeControls.CheckInException
{
    [FwSqlTable("dbo.funccheckinexception(@contractid, @rectype, @containeritemid, @showall)")]
    public class CheckInExceptionLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exceptionflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsException { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "somein", modeltype: FwDataTypes.Boolean)]
        public bool? SomeItemsIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystagedandout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStagedAndOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysub", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysubstagedandout", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantityStagedAndOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtysubout", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantityOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyin", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtystillout", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityStillOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsMissing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "missingqty", modeltype: FwDataTypes.Decimal)]
        public decimal? MissingQuantity { get; set; }
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
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isbarcode", modeltype: FwDataTypes.Boolean)]
        public bool? IsBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subbyquantity", modeltype: FwDataTypes.Boolean)]
        public bool? IsSub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isconsignor", modeltype: FwDataTypes.Boolean)]
        public bool? IsConsignor { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            string contractId = GetUniqueIdAsString("ContractId", request) ?? "x-x-x";
            string rectype = GetUniqueIdAsString("RecType", request) ?? "";
            string containerItemId = GetUniqueIdAsString("ContainerItemId", request) ?? "";
            bool showAll = GetUniqueIdAsBoolean("ShowAll", request).GetValueOrDefault(false);

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request);
            select.AddParameter("@contractid", contractId);
            select.AddParameter("@rectype", rectype);
            select.AddParameter("@containeritemid", containerItemId); 
            select.AddParameter("@showall", showAll);

            //select.AddWhereIn("rectype", RwConstants.RECTYPE_RENTAL + "," + RwConstants.RECTYPE_SALE); //jhtodo should only show Sales for Transfers
            select.AddWhereIn("rectype", RwConstants.RECTYPE_RENTAL); //jhtodo should only show Sales for Transfers

        }
        //------------------------------------------------------------------------------------ 
    }
}

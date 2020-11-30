using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.PurchaseOrderReceiveItem
{
    [FwSqlTable("dbo.funcpoitemreceive(@poid, @receivecontractid)")]
    public class PurchaseOrderReceiveItemLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text)]
        public string ContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "linetype", modeltype: FwDataTypes.Text)]
        public string LineType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string SubOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string SubOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderno", modeltype: FwDataTypes.Text)]
        public string SubOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masternodisplay", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreceived", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReceived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyremaining", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityRemaining { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreturned", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReturned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "qtyneedbarcode", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityNeedBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcodecount", modeltype: FwDataTypes.Integer)]
        public int? BarCodeCount { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {

            useWithNoLock = false;
            bool showFullyReceived = GetUniqueIdAsBoolean("ShowFullyReceived", request) ?? false;
            if (string.IsNullOrEmpty(PurchaseOrderId))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("PurchaseOrderId"))
                    {
                        PurchaseOrderId = uniqueIds["PurchaseOrderId"].ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(ContractId))
            {
                if ((request != null) && (request.uniqueids != null))
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("ContractId"))
                    {
                        ContractId = uniqueIds["ContractId"].ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(PurchaseOrderId))
            {
                if (!string.IsNullOrEmpty(PurchaseOrderItemId))
                {
                    PurchaseOrderId = AppFunc.GetStringDataAsync(AppConfig, "masteritem", "masteritemid", PurchaseOrderItemId, "orderid").Result;
                }
            }


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if (!showFullyReceived)
            {
                select.AddWhere("((qtyremaining > 0) or (qty <> 0))");
            }

            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request);

            select.AddParameter("@poid", PurchaseOrderId);
            select.AddParameter("@receivecontractid", ContractId);
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [FwSqlTable("powebbrowseview")]
    public class PurchaseOrderBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PurchaseOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string PurchaseOrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdate", modeltype: FwDataTypes.Date)]
        public string PurchaseOrderDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertotal", modeltype: FwDataTypes.Decimal)]
        public decimal? Total { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origorderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refno", modeltype: FwDataTypes.Text)]
        public string ReferenceNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "currencycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesccolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor { get; set; }
        //------------------------------------------------------------------------------------



        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("VendorId", "VendorId", select, request);
            //addFilterToSelect("OrderId", "poorderid", select, request);
            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";

            if (!string.IsNullOrEmpty(orderId))
            {
                select.AddWhere("exists (select * from poorder poo where poo.poid = " + TableAlias + ".orderid and poo.orderid = @orderid)");
                select.AddParameter("@orderid", orderId);
            }


            if (GetMiscFieldAsBoolean("ReceiveFromVendor", request).GetValueOrDefault(false))
            {
                select.AddWhere("orderno > ''");
                select.AddWhereIn("and", "status", RwConstants.PURCHASE_ORDER_STATUS_NEW + "," + RwConstants.PURCHASE_ORDER_STATUS_OPEN);

                string receivingWarehouseId = GetMiscFieldAsString("ReceivingWarehouseId", request);
                if (!string.IsNullOrEmpty(receivingWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @receivingwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = t.orderid and mi.warehouseid = @receivingwhid))");
                    select.AddParameter("@receivingwhid", receivingWarehouseId);
                }
            }
            else if (GetMiscFieldAsBoolean("ReturnToVendor", request).GetValueOrDefault(false))
            {
                select.AddWhere("orderno > ''");
                select.AddWhereIn("and", "status", RwConstants.PURCHASE_ORDER_STATUS_OPEN + "," + RwConstants.PURCHASE_ORDER_STATUS_RECEIVED + "," + RwConstants.PURCHASE_ORDER_STATUS_COMPLETE);

                string returningWarehouseId = GetMiscFieldAsString("ReturningWarehouseId", request);
                if (!string.IsNullOrEmpty(returningWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @returningwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = t.orderid and mi.warehouseid = @returningwhid))");
                    select.AddParameter("@returningwhid", returningWarehouseId);
                }
            }
            else if (GetMiscFieldAsBoolean("AssignBarCodes", request).GetValueOrDefault(false))
            {
                select.AddWhere("orderno > ''");
                //select.AddWhere("qtytobarcode > 0");
                select.AddWhere("exists (select * from barcodeholding bch with (nolock) where bch.orderid = " + TableAlias + ".orderid and bch.ordertranid is not null)");
                select.AddWhereIn("and", "status", RwConstants.PURCHASE_ORDER_STATUS_OPEN + "," + RwConstants.PURCHASE_ORDER_STATUS_RECEIVED + "," + RwConstants.PURCHASE_ORDER_STATUS_COMPLETE);

                string assigningWarehouseId = GetMiscFieldAsString("AssigningWarehouseId", request);
                if (!string.IsNullOrEmpty(assigningWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @assigningwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = t.orderid and mi.warehouseid = @assigningwhid))");
                    select.AddParameter("@assigningwhid", assigningWarehouseId);
                }
            }


            AddActiveViewFieldToSelect("Status", "status", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

        }
        //------------------------------------------------------------------------------------    
    }
}

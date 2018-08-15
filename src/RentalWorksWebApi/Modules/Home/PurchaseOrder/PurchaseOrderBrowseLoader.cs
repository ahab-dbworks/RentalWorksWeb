using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    [FwSqlTable("poview")]
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



        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("VendorId", "VendorId", select, request);
            addFilterToSelect("OrderId", "poorderid", select, request);


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
                select.AddWhereIn("and", "status", RwConstants.PURCHASE_ORDER_STATUS_OPEN + "," + RwConstants.PURCHASE_ORDER_STATUS_RECEIVED + "," + RwConstants.PURCHASE_ORDER_STATUS_COMPLETE);

                string assigningWarehouseId = GetMiscFieldAsString("AssigningWarehouseId", request);
                if (!string.IsNullOrEmpty(assigningWarehouseId))
                {
                    select.AddWhere(" ((warehouseid = @assigningwhid) or exists (select * from masteritem mi with (nolock) where mi.orderid = t.orderid and mi.warehouseid = @assigningwhid))");
                    select.AddParameter("@assigningwhid", assigningWarehouseId);
                }
            }




            if ((request != null) && (request.activeview != null))
            {
                switch (request.activeview)
                {
                    case "NEW":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_NEW);
                        break;
                    case "OPEN":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_OPEN);
                        break;
                    case "RECEIVED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_RECEIVED);
                        break;
                    case "COMPLETE":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_COMPLETE);
                        break;
                    case "CLOSED":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_CLOSED);
                        break;
                    case "SNAPSHOT":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_SNAPSHOT);
                        break;
                    case "VOID":
                        select.AddWhere("(status = @orderstatus)");
                        select.AddParameter("@orderstatus", RwConstants.PURCHASE_ORDER_STATUS_VOID);
                        break;
                    case "ALL":
                        break;
                }

                if (request.activeview.Contains("WarehouseId="))
                {
                    string whId = request.activeview.Replace("WarehouseId=", "");
                    if (!whId.Equals("ALL"))
                    {
                        select.AddWhere("(warehouseid = @whid)");
                        select.AddParameter("@whid", whId);
                    }
                }

                string locId = "ALL";
                if (request.activeview.Contains("OfficeLocationId="))
                {
                    locId = request.activeview.Replace("OfficeLocationId=", "");
                }
                else if (request.activeview.Contains("LocationId="))
                {
                    locId = request.activeview.Replace("LocationId=", "");
                }
                if (!locId.Equals("ALL"))
                {
                    select.AddWhere("(locationid = @locid)");
                    select.AddParameter("@locid", locId);
                }

            }
        }
        //------------------------------------------------------------------------------------    
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi;
using WebApi.Logic;
using System.Text;

namespace WebApi.Modules.Agent.PurchaseOrder
{
    [FwSqlTable("powebbrowseview")]
    public class PurchaseOrderBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public PurchaseOrderBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
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
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
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
        [FwSqlDataField(column: "receivedeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? ReceiveDeliveryDropShip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returndeliverydropship", modeltype: FwDataTypes.Boolean)]
        public bool? ReturnDeliveryVendorRetrieve { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "needsapproval", modeltype: FwDataTypes.Boolean)]
        public bool? NeedsApproval { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyholding", modeltype: FwDataTypes.Integer)]
        public int? QuantityHolding { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtytobarcode", modeltype: FwDataTypes.Integer)]
        public int? QuantityToBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertypeid", modeltype: FwDataTypes.Text)]
        public string PoTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordertype", modeltype: FwDataTypes.Text)]
        public string PoType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitionno", modeltype: FwDataTypes.Text)]
        public string RequisitionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requisitiondate", modeltype: FwDataTypes.Date)]
        public string RequisitionDate { get; set; }
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
        [FwSqlDataField(column: "locdefaultcurrencyid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationDefaultCurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CurrencyColor
        {
            get { return determineCurrencyColor(CurrencyId, OfficeLocationDefaultCurrencyId); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StatusColor
        {
            get { return determineStatusColor(NeedsApproval.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string PurchaseOrderNumberColor
        {
            get { return determinePoNumberColor(QuantityToBarCode.GetValueOrDefault(0)); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string VendorColor
        {
            get { return determineVendorColor(QuantityHolding.GetValueOrDefault(0)); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return determineDescriptionColor(ReceiveDeliveryDropShip.GetValueOrDefault(false), ReturnDeliveryVendorRetrieve.GetValueOrDefault(false)); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectno", modeltype: FwDataTypes.Text)]
        public string ProjectNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectdesc", modeltype: FwDataTypes.Text)]
        public string Project{ get; set; }
        //------------------------------------------------------------------------------------ 




        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("OfficeLocationId", "locationid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("VendorId", "VendorId", select, request);
            addFilterToSelect("ProjectId", "projectid", select, request);

            string orderId = GetUniqueIdAsString("OrderId", request) ?? "";
            if (!string.IsNullOrEmpty(orderId))
            {
                select.AddWhere("exists (select * from poorder poo where poo.poid = " + TableAlias + ".orderid and poo.orderid = @orderid)");
                select.AddParameter("@orderid", orderId);
            }

            string inventoryId = GetUniqueIdAsString("InventoryId", request) ?? "";
            if (!string.IsNullOrEmpty(inventoryId))
            {
                //select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.masterid = @masterid)");

                //justin hoffman 07/08/2020 #2690
                string classification = AppFunc.GetStringDataAsync(AppConfig, "master", "masterid", inventoryId, "class").Result;
                if (classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
                {
                    select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.parentid = @masterid)");
                }
                else
                {
                    select.AddWhere("exists (select * from masteritem mi where mi.orderid = " + TableAlias + ".orderid and mi.masterid = @masterid)");
                }


                select.AddParameter("@masterid", inventoryId);
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


            //ALL, AGENT
            if ((request != null) && (request.activeviewfields != null))
            {
                if (request.activeviewfields.ContainsKey("My"))
                {
                    StringBuilder myWhere = new StringBuilder();
                    List<string> myValues = request.activeviewfields["My"];
                    foreach (string myValue in myValues)
                    {
                        if (myValue.ToUpper().Equals(RwConstants.MY_AGENT_ACTIVE_VIEW_VALUE))
                        {
                            if (myWhere.Length > 0)
                            {
                                myWhere.AppendLine("or");
                            }
                            myWhere.AppendLine("agentid = @myusersid");
                        }
                    }
                    if (myWhere.Length > 0)
                    {
                        myWhere.Insert(0, "(");
                        myWhere.AppendLine(")");
                        select.AddWhere(myWhere.ToString());
                        select.AddParameter("@myusersid", this.UserSession.UsersId);
                    }
                }
            }


            AddActiveViewFieldToSelect("Status", "status", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

        }
        //------------------------------------------------------------------------------------    
        private string determinePoNumberColor(int qtyToBarCode)
        {
            return (qtyToBarCode > 0 ? RwGlobals.PO_ITEMS_NEED_BARCODE_COLOR : null);
        }
        //------------------------------------------------------------------------------------    
        private string determineDescriptionColor(bool isDropShip, bool isVendorRetrieve)
        {
            return (isDropShip|| isVendorRetrieve ? RwGlobals.PO_DROP_SHIP_COLOR : null);
        }
        //------------------------------------------------------------------------------------    
        private string determineVendorColor(int qtyHolding)
        {
            return (qtyHolding > 0 ? RwGlobals.PO_ITEMS_IN_HOLDING_COLOR : null);
        }
        //------------------------------------------------------------------------------------    
        private string determineStatusColor(bool needsApproval)
        {
            return (needsApproval ? RwGlobals.PO_NEEDS_APPROVAL_COLOR : null);
        }
        //------------------------------------------------------------------------------------    
        private string determineCurrencyColor(string poCurrencyId, string locCurrencyId)
        {
            string color = null;
            if (!string.IsNullOrEmpty(poCurrencyId))
            {
                if (!poCurrencyId.Equals(locCurrencyId))
                {
                    color = RwGlobals.FOREIGN_CURRENCY_COLOR;
                }
            }
            return color;
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
                        row[dt.GetColumnNo("PurchaseOrderNumberColor")] = determinePoNumberColor(FwConvert.ToInt32(row[dt.GetColumnNo("QuantityToBarCode")].ToString()));
                        row[dt.GetColumnNo("DescriptionColor")] = determineDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("ReceiveDeliveryDropShip")].ToString()), FwConvert.ToBoolean(row[dt.GetColumnNo("ReturnDeliveryVendorRetrieve")].ToString()));
                        row[dt.GetColumnNo("VendorColor")] = determineVendorColor(FwConvert.ToInt32(row[dt.GetColumnNo("QuantityHolding")].ToString()));
                        row[dt.GetColumnNo("StatusColor")] = determineStatusColor(FwConvert.ToBoolean(row[dt.GetColumnNo("NeedsApproval")].ToString()));
                        row[dt.GetColumnNo("CurrencyColor")] = determineCurrencyColor(row[dt.GetColumnNo("CurrencyId")].ToString(), row[dt.GetColumnNo("OfficeLocationDefaultCurrencyId")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using WebApi;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.OrderStatusSummary
{
    [FwSqlTable("dbo.getorderstatussummary(@orderid)")]
    public class OrderStatusSummaryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public OrderStatusSummaryLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternodisplayweb", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeColor
        {
            get { return getICodeColor(ItemClass); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
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
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string MasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedMasterItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouseid", modeltype: FwDataTypes.Text)]
        public string OutWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwhcode", modeltype: FwDataTypes.Text)]
        public string OutWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouse", modeltype: FwDataTypes.Text)]
        public string OutWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwarehouseid", modeltype: FwDataTypes.Text)]
        public string InWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwhcode", modeltype: FwDataTypes.Text)]
        public string InWarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwarehouse", modeltype: FwDataTypes.Text)]
        public string InWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyorderedcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string QuantityOrderedColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stageqty", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stageqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedQuantityFilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedqtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StagedQuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "outqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantityfilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string OutQuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuspendout", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inqty", modeltype: FwDataTypes.Decimal)]
        public decimal? InQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? InQuantityFilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inqtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string InQuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "issuspendin", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspendIn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReturnedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activityqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ActivityQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subactivityqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubActivityQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreceived", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReceived { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyreturned", modeltype: FwDataTypes.Decimal)]
        public decimal? QuantityReturned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notyetstagedqty", modeltype: FwDataTypes.Decimal)]
        public decimal? NotYetStagedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "toomanystagedout", modeltype: FwDataTypes.Boolean)]
        public bool? TooManyStagedOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notyetstagedqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? NotYetStagedQuantityFilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stilloutqty", modeltype: FwDataTypes.Decimal)]
        public decimal? StillOutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stilloutqtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StillOutQuantityColor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnflg", modeltype: FwDataTypes.Text)]
        public string IsReturn { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string PoOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string PoMasteritemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor
        {
            get { return determineRecTypeColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Text)]
        public string OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haspoitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasPoItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string ManufacturerPartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iswardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? IsWardrobe { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprops", modeltype: FwDataTypes.Boolean)]
        public bool? IsProps { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitcost", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutextendedcost", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedOutExtendedCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitprice", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedoutextendedprice", modeltype: FwDataTypes.Decimal)]
        public decimal? StagedOutExtendedPrice { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;

            //string orderId = "!none!";
            //string filterStatus = string.Empty;
            //if (request != null)
            //{
            //    if (request.uniqueids != null)
            //    {
            //        IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
            //        if (uniqueIds.ContainsKey("OrderId"))
            //        {
            //            orderId = uniqueIds["OrderId"].ToString();
            //        }
            //    }
            //
            //    if (request.filterfields != null)
            //    {
            //        IDictionary<string, string> filterfields = ((IDictionary<string, string>)request.filterfields);
            //        if (filterfields.ContainsKey("Status"))
            //        {
            //            filterStatus = filterfields["Status"].ToString();
            //        }
            //    }
            //}

            string orderId = GetUniqueIdAsString("OrderId", request) ?? "!none!";
            string filterStatus = GetFilterFieldAsString("Status", request) ?? "";


            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            AddFilterFieldToSelect("InventoryTypeId", "inventorydepartmentid", select, request);
            AddFilterFieldToSelect("CategoryId", "categoryid", select, request);
            AddFilterFieldToSelect("SubCategoryId", "subcategoryid", select, request);
            AddFilterFieldToSelect("WarehouseId", "outwarehouseid", select, request);
            AddFilterFieldToSelect("InventoryId", "masterid", select, request);
            AddFilterFieldToSelect("Description", "description", select, request);

            select.AddParameter("@orderid", orderId);


            switch (filterStatus)
            {

                //Orders and Containers
                case RwConstants.ORDER_STATUS_FILTER_STAGED_ONLY:
                    select.AddWhere("(stageqtyfilter <> 0)");
                    break;
                case RwConstants.ORDER_STATUS_FILTER_NOT_YET_STAGED:
                    select.AddWhere("(notyetstagedqtyfilter > 0)");
                    break;
                case RwConstants.ORDER_STATUS_FILTER_STILL_OUT:
                    select.AddWhere("(stilloutqty <> 0)");
                    break;
                case RwConstants.ORDER_STATUS_FILTER_IN_ONLY:
                    select.AddWhere("(inqtyfilter <> 0)");
                    break;

                //Purchase Orders
                case RwConstants.PURCHASE_ORDER_STATUS_FILTER_NOT_YET_RECEIVED:
                    select.AddWhere("(qtyreceived = 0)");
                    break;
                case RwConstants.PURCHASE_ORDER_STATUS_FILTER_RECEIVED:
                    select.AddWhere("(qtyreceived > 0)");
                    break;
                case RwConstants.PURCHASE_ORDER_STATUS_FILTER_RETURNED:
                    select.AddWhere("(qtyreturned > 0)");
                    break;
                default: break;
            }

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
                        row[dt.GetColumnNo("RecTypeColor")] = determineRecTypeColor(row[dt.GetColumnNo("RecType")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}

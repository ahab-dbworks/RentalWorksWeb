using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Home.OrderStatusSummary
{
    [FwSqlTable("masteritem")]
    public class OrderStatusSummaryLoader : RwDataLoadRecord
    {
        private string orderId;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternodisplay", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
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
        [FwSqlDataField(column: "subqty", modeltype: FwDataTypes.Decimal)]
        public decimal? SubQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stageqty", modeltype: FwDataTypes.Decimal)]
        public decimal? StageQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stageqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? StageQuantityFilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantityfilter { get; set; }
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
        [JsonIgnore]
        public override string TableName
        {
            get
            {
                return "dbo.getorderstatussummary('" + orderId + "')";
            }
        }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("OrderId"))
                {
                    orderId = uniqueIds["OrderId"].ToString();
                }
            }
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
} 

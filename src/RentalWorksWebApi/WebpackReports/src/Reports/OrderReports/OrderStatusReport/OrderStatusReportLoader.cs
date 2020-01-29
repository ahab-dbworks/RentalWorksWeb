using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.OrderStatusReport
{
    [FwSqlTable("getorderstatussummary(@orderid)")]
    public class OrderStatusReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'detail'", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternodisplay", modeltype: FwDataTypes.Text)]
        public string ICodeNoDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternodisplayweb", modeltype: FwDataTypes.Text)]
        public string ICodeDisplayWeb { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masternocolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ICodeNoColor { get; set; }
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
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptioncolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptiOnColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nestedmasteritemid", modeltype: FwDataTypes.Text)]
        public string NestedmasteritemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parentid", modeltype: FwDataTypes.Text)]
        public string ParentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouseid", modeltype: FwDataTypes.Text)]
        public string OutwarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwhcode", modeltype: FwDataTypes.Text)]
        public string Outwhcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outwarehouse", modeltype: FwDataTypes.Text)]
        public string OutWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwarehouseid", modeltype: FwDataTypes.Text)]
        public string InWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inwhcode", modeltype: FwDataTypes.Text)]
        public string WarehouseIcode { get; set; }
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
        public decimal? StageQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stageqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? StageQuantityFilter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedqtycolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string StagedQuantityColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqty", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqtyfilter", modeltype: FwDataTypes.Decimal)]
        public decimal? OutQuantityFilter { get; set; }
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
        public string InQuantitycolor { get; set; }
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
        public string Itemclass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "returnflg", modeltype: FwDataTypes.Text)]
        public string ReturnFlag { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poorderid", modeltype: FwDataTypes.Text)]
        public string POOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pomasteritemid", modeltype: FwDataTypes.Text)]
        public string POOrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "optioncolor", modeltype: FwDataTypes.Boolean)]
        public bool? OptionColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "haspoitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasPOItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mfgpartno", modeltype: FwDataTypes.Text)]
        public string MfgPartNo { get; set; }
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
        public async Task<FwJsonDataTable> RunReportAsync(OrderStatusReportRequest request)
        {
            useWithNoLock = false;
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                //--------------------------------------------------------------------------------- 
                // below uses a "select" query to populate the FwJsonDataTable 
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, "getorderstatussummary", this.AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    qry.AddParameter("@orderid", SqlDbType.Text, ParameterDirection.Input, request.OrderId);
                    AddPropertiesAsQueryColumns(qry);
                    dt = await qry.QueryToFwJsonTableAsync(false, 0);
                }
                //--------------------------------------------------------------------------------- 
            }
            if (request.IncludeSubHeadingsAndSubTotals)
            {
                //dt.InsertSubTotalRows("Warehouse", "RowType", totalFields);
                //dt.InsertSubTotalRows("Department", "RowType", totalFields);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }
}

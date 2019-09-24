using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
namespace WebApi.Modules.Reports.PickListReport
{


    [FwSqlTable("picklistrptview")]
    public class PickListItemReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistid", modeltype: FwDataTypes.Text)]
        public string PicklistId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "plorderno", modeltype: FwDataTypes.Text)]
        //public string OrderNumber { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "plorderdesc", modeltype: FwDataTypes.Text)]
        //public string OrderDescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "plwarehouseid", modeltype: FwDataTypes.Text)]
        //public string WarehouseId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "plwarehouse", modeltype: FwDataTypes.Text)]
        //public string Warehouse { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouseid", modeltype: FwDataTypes.Text)]
        public string TrasferToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouse", modeltype: FwDataTypes.Text)]
        public string TrasferToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string DeliverType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)]
        public string RequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddatetime", modeltype: FwDataTypes.Text)]
        public string RequiredDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "targetshipdate", modeltype: FwDataTypes.Date)]
        public string TargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickno", modeltype: FwDataTypes.Text)]
        public string PickNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        public string PhoneExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphoneext", modeltype: FwDataTypes.Text)]
        public string AgentPhoneExtension{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requestsentto", modeltype: FwDataTypes.Text)]
        public string RequestSentTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date)]
        public string PrepDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedbyphoneext", modeltype: FwDataTypes.Text)]
        public string OrderedByPhoneExtension{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickqty", modeltype: FwDataTypes.Integer)]
        public int? PickQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagedqty", modeltype: FwDataTypes.Integer)]
        public int? StagedQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outqty", modeltype: FwDataTypes.Integer)]
        public int? OutQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qtyordered", modeltype: FwDataTypes.Integer)]
        public int? QuantityOrdered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inlocationqty", modeltype: FwDataTypes.Integer)]
        public int? InLocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Integer)]
        public int? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptionwrate", modeltype: FwDataTypes.Text)]
        public string DescriptionWithRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICodeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string PartNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string OrderItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plainmasterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsbarcode", modeltype: FwDataTypes.Text)]
        public string RentalSaleBarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsserialno", modeltype: FwDataTypes.Text)]
        public string RentalSaleSerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string AisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string ShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleshelfloc", modeltype: FwDataTypes.Text)]
        public string AisleShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemaisleloc", modeltype: FwDataTypes.Text)]
        public string ItemAisleLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemshelfloc", modeltype: FwDataTypes.Text)]
        public string ItemShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemaisleshelfloc", modeltype: FwDataTypes.Text)]
        public string ItemAisleShelfLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastname", modeltype: FwDataTypes.Text)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstname", modeltype: FwDataTypes.Text)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "middleinitial", modeltype: FwDataTypes.Boolean)]
        public bool? MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string NameFirstMiddleLast { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string ItemClass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string ItemOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorderpicklist", modeltype: FwDataTypes.Text)]
        public string ItemOrderPickList { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypesequence", modeltype: FwDataTypes.Boolean)]
        public bool? RecTypeSequence { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issub", modeltype: FwDataTypes.Boolean)]
        public bool? IsSub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isconsign", modeltype: FwDataTypes.Boolean)]
        public bool? IsConsign { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvendor", modeltype: FwDataTypes.Text)]
        public string SubVendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentorderby", modeltype: FwDataTypes.Integer)]
        public int? DepartmentOrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string PickDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string PickTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdatetime", modeltype: FwDataTypes.Text)]
        public string PickDateTime{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "metered", modeltype: FwDataTypes.Boolean)]
        public bool? Metered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflict", modeltype: FwDataTypes.Boolean)]
        public bool? IsConflict { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summarizebymaster", modeltype: FwDataTypes.Boolean)]
        public bool? SummarizeByICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appimageid", modeltype: FwDataTypes.Text)]
        public string AppImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprep", modeltype: FwDataTypes.Boolean)]
        public bool? Isprep { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasreservedrentalitem", modeltype: FwDataTypes.Boolean)]
        public bool? HasReservedItem { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> LoadItems(PickListReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
				select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    addStringFilterToSelect("picklistid", request.PickListId, select);
                    select.AddOrderBy("orderno, pickdate, rectypesequence, itemorder, masterno");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
            string[] totalFields = new string[] { "PickQuantity" };
            dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalFields);
            dt.InsertSubTotalRows("InventoryType", "RowType", totalFields);
            dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            return dt;
        }
        //------------------------------------------------------------------------------------ 
    }



    [FwSqlTable("picklistrpttitleview")]
    public class PickListReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistid", modeltype: FwDataTypes.Text)]
        public string PicklistId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouseid", modeltype: FwDataTypes.Text)]
        public string TransferToWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouse", modeltype: FwDataTypes.Text)]
        public string TransferToWarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string DeliverType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)]
        public string RequiredDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string RequiredTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddatetime", modeltype: FwDataTypes.Text)]
        public string RequiredDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "targetshipdate", modeltype: FwDataTypes.Date)]
        public string TargetShipDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickno", modeltype: FwDataTypes.Text)]
        public string PickNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        public string PhoneExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphoneext", modeltype: FwDataTypes.Text)]
        public string AgentPhoneExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requestsentto", modeltype: FwDataTypes.Text)]
        public string RequestSentTo { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date)]
        public string PrepDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string PrepTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string EstimatedStartDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string EstimatedStartTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string EstimatedStopDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string EstimatedStopTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string OrderedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedbyphoneext", modeltype: FwDataTypes.Text)]
        public string OrderedByPhoneExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        public FwJsonDataTable Items { get; set; } 
        //------------------------------------------------------------------------------------ 
        public async Task<PickListReportLoader> RunReportAsync(PickListReportRequest request)
        {

            Task<PickListReportLoader> taskPickList;
            Task<FwJsonDataTable> taskPickListItems;

            PickListReportLoader pickList = null;
            PickListItemReportLoader pickListItems = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();

                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    addStringFilterToSelect("picklistid", request.PickListId, select);

                    // load pick list header here
                    select.SetQuery(qry);
                    taskPickList = qry.QueryToTypedObjectAsync<PickListReportLoader>();

                    // load pick list items here
                    pickListItems = new PickListItemReportLoader();
                    pickListItems.SetDependencies(AppConfig, UserSession);
                    taskPickListItems = pickListItems.LoadItems(request);

                    await Task.WhenAll(new Task[] { taskPickList, taskPickListItems });

                    pickList = taskPickList.Result;
                    if (pickList != null)
                    {
                        pickList.Items = taskPickListItems.Result;
                    }

                }
            }

            return pickList;
        }
        //------------------------------------------------------------------------------------ 
    }
}

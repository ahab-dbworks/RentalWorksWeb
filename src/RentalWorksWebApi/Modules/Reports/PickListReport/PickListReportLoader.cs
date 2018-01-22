using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Reports.PickListReport
{
    [FwSqlTable("picklistrptview")]
    public class PickListReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picklistid", modeltype: FwDataTypes.Text)]
        public string PicklistId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string Custno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string Dealno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plorderno", modeltype: FwDataTypes.Text)]
        public string Plorderno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plorderdesc", modeltype: FwDataTypes.Text)]
        public string Plorderdesc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string Orderlocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plwarehouseid", modeltype: FwDataTypes.Text)]
        public string PlwarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plwarehouse", modeltype: FwDataTypes.Text)]
        public string Plwarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouseid", modeltype: FwDataTypes.Text)]
        public string TrasfertowarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trasfertowarehouse", modeltype: FwDataTypes.Text)]
        public string Trasfertowarehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text)]
        public string Pono { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "delivertype", modeltype: FwDataTypes.Text)]
        public string Delivertype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddate", modeltype: FwDataTypes.Date)]
        public string Requireddate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requiredtime", modeltype: FwDataTypes.Text)]
        public string Requiredtime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requireddatetime", modeltype: FwDataTypes.Text)]
        public string Requireddatetime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "targetshipdate", modeltype: FwDataTypes.Date)]
        public string Targetshipdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickno", modeltype: FwDataTypes.Text)]
        public string Pickno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        public string Phoneextension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agent", modeltype: FwDataTypes.Text)]
        public string Agent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "agentphoneext", modeltype: FwDataTypes.Text)]
        public string Agentphoneext { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "requestsentto", modeltype: FwDataTypes.Text)]
        public string Requestsentto { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prepdate", modeltype: FwDataTypes.Date)]
        public string Prepdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preptime", modeltype: FwDataTypes.Text)]
        public string Preptime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentfrom", modeltype: FwDataTypes.Date)]
        public string Estrentfrom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estfromtime", modeltype: FwDataTypes.Text)]
        public string Estfromtime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estrentto", modeltype: FwDataTypes.Date)]
        public string Estrentto { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "esttotime", modeltype: FwDataTypes.Text)]
        public string Esttotime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Text)]
        public string Orderedby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedbyphoneext", modeltype: FwDataTypes.Text)]
        public string Orderedbyphoneext { get; set; }
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
        public int? Quantityordered { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inlocationqty", modeltype: FwDataTypes.Integer)]
        public int? InlocationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignqty", modeltype: FwDataTypes.Integer)]
        public int? ConsignQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "descriptionwrate", modeltype: FwDataTypes.Text)]
        public string Descriptionwrate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string Masterno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partnumber", modeltype: FwDataTypes.Text)]
        public string Partnumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string Trackedby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text)]
        public string MasteritemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "plainmasterno", modeltype: FwDataTypes.Text)]
        public string Plainmasterno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsbarcode", modeltype: FwDataTypes.Text)]
        public string Rsbarcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rsserialno", modeltype: FwDataTypes.Text)]
        public string Rsserialno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleloc", modeltype: FwDataTypes.Text)]
        public string Aisleloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shelfloc", modeltype: FwDataTypes.Text)]
        public string Shelfloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "aisleshelfloc", modeltype: FwDataTypes.Text)]
        public string Aisleshelfloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemaisleloc", modeltype: FwDataTypes.Text)]
        public string Itemaisleloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemshelfloc", modeltype: FwDataTypes.Text)]
        public string Itemshelfloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemaisleshelfloc", modeltype: FwDataTypes.Text)]
        public string Itemaisleshelfloc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string Orderno { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastname", modeltype: FwDataTypes.Text)]
        public string Lastname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstname", modeltype: FwDataTypes.Text)]
        public string Firstname { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "middleinitial", modeltype: FwDataTypes.Boolean)]
        public bool? Middleinitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string Namefml { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemclass", modeltype: FwDataTypes.Text)]
        public string Itemclass { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorder", modeltype: FwDataTypes.Text)]
        public string Itemorder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemorderpicklist", modeltype: FwDataTypes.Text)]
        public string Itemorderpicklist { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string Rectype { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string Rectypedisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypesequence", modeltype: FwDataTypes.Boolean)]
        public bool? Rectypesequence { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "issub", modeltype: FwDataTypes.Boolean)]
        public bool? Issub { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isconsign", modeltype: FwDataTypes.Boolean)]
        public bool? Isconsign { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subvendor", modeltype: FwDataTypes.Text)]
        public string Subvendor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "consignor", modeltype: FwDataTypes.Text)]
        public string Consignor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventorydepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string Inventorydepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentorderby", modeltype: FwDataTypes.Integer)]
        public int? Departmentorderby { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdate", modeltype: FwDataTypes.Date)]
        public string Pickdate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "picktime", modeltype: FwDataTypes.Text)]
        public string Picktime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pickdatetime", modeltype: FwDataTypes.Text)]
        public string Pickdatetime { get; set; }
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
        public string Whcode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conflict", modeltype: FwDataTypes.Boolean)]
        public bool? Conflict { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "summarizebymaster", modeltype: FwDataTypes.Boolean)]
        public bool? Summarizebymaster { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bold", modeltype: FwDataTypes.Boolean)]
        public bool? Bold { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appimageid", modeltype: FwDataTypes.Text)]
        public string AppimageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isprep", modeltype: FwDataTypes.Boolean)]
        public bool? Isprep { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasreservedrentalitem", modeltype: FwDataTypes.Boolean)]
        public bool? Hasreservedrentalitem { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("PickListId", "picklistid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
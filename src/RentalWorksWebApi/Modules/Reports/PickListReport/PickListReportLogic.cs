using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Reports.PickListReport
{
    public class PickListReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListReportLoader pickListReportLoader = new PickListReportLoader();
        public PickListReportLogic()
        {
            dataLoader = pickListReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string PicklistId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Custno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Dealno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Plorderno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Plorderdesc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Orderlocation { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PlwarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Plwarehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TrasfertowarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Trasfertowarehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pono { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Delivertype { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Requireddate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Requiredtime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Requireddatetime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Targetshipdate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pickno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Phoneextension { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agentphoneext { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Requestsentto { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Prepdate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Preptime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Estrentfrom { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Estfromtime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Estrentto { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Esttotime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Orderedby { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Orderedbyphoneext { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? PickQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? StagedQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? OutQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Quantityordered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? InlocationQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? ConsignQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Description { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Descriptionwrate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MasterId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Masterno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Partnumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Trackedby { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MasteritemId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Barcode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Plainmasterno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Rsbarcode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Rsserialno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Aisleloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Shelfloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Aisleshelfloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemaisleloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemshelfloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemaisleshelfloc { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UsersId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Orderno { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Lastname { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Firstname { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Middleinitial { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Namefml { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemclass { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemorder { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Itemorderpicklist { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Rectype { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Rectypedisplay { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Rectypesequence { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Issub { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Isconsign { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Subvendor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Consignor { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventorydepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Inventorydepartment { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CategoryId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Category { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? Departmentorderby { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pickdate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Picktime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Pickdatetime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Metered { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Whcode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Conflict { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Summarizebymaster { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Bold { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AppimageId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Isprep { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? Hasreservedrentalitem { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
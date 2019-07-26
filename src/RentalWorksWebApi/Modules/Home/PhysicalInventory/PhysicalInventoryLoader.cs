using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.PhysicalInventory
{
    [FwSqlTable("physicalinventorywebview")]
    public class PhysicalInventoryLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, isPrimaryKey:true)]
        public string PhysicalInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string OfficeLocationCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        //public string Location { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scheduledate", modeltype: FwDataTypes.Date)]
        public string ScheduleDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preinitdate", modeltype: FwDataTypes.DateTime)]
        public string PreScanDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "initdate", modeltype: FwDataTypes.DateTime)]
        public string InitiateDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalno", modeltype: FwDataTypes.Text)]
        public string PhysicalInventoryNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counttype", modeltype: FwDataTypes.Text)]
        public string CountType { get; set; }
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
        [FwSqlDataField(column: "subcategoryid", modeltype: FwDataTypes.Text)]
        public string SubCategoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "subcategory", modeltype: FwDataTypes.Text)]
        public string SubCategory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Text)]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclea", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleb", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankB { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclec", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycled", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankD { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclee", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankE { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclef", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankF { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleg", modeltype: FwDataTypes.Boolean)]
        public bool? CycleRankG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "afromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? AFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "atovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? AToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? BFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "btovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? BToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? CFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ctovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? CToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? DFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dtovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? DToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "efromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? EFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "etovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? EToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ffromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? FFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ftovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? FToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gfromvalue", modeltype: FwDataTypes.Decimal)]
        public decimal? GFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gtovalue", modeltype: FwDataTypes.Decimal)]
        public decimal? GToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step0", modeltype: FwDataTypes.Integer)]
        public int? StepPreScan { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step1", modeltype: FwDataTypes.Integer)]
        public int? StepInitiate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step2", modeltype: FwDataTypes.Integer)]
        public int? StepPrintCountSheets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step3", modeltype: FwDataTypes.Integer)]
        public int? StepCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step4", modeltype: FwDataTypes.Integer)]
        public int? StepPrintExceptionReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step5", modeltype: FwDataTypes.Integer)]
        public int? StepPrintDiscrepancyReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step6", modeltype: FwDataTypes.Integer)]
        public int? StepRecount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step7", modeltype: FwDataTypes.Integer)]
        public int? StepPrintRecountAnalysisReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step8", modeltype: FwDataTypes.Integer)]
        public int? StepPrintReconciliationReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step9", modeltype: FwDataTypes.Integer)]
        public int? StepApproveCounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step10", modeltype: FwDataTypes.Integer)]
        public int? StepClose { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step11", modeltype: FwDataTypes.Integer)]
        public int? StepCloseWithoutAdjustments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step12", modeltype: FwDataTypes.Integer)]
        public int? StepPrintResults { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclelast", modeltype: FwDataTypes.Date)]
        public string CycleLastCounted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycletrackedby", modeltype: FwDataTypes.Text)]
        public string CycleTrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleaisle", modeltype: FwDataTypes.Text)]
        public string CycleAisle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleshelf", modeltype: FwDataTypes.Text)]
        public string CycleShelf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlyowned", modeltype: FwDataTypes.Boolean)]
        public bool? CycleOnlyIncludeInventoryWithNonZeroQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedpurchasecost", modeltype: FwDataTypes.Boolean)]
        public bool? ApprovedPurchaseCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countout", modeltype: FwDataTypes.Boolean)]
        public bool? CountInventoryThatIsOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isspace", modeltype: FwDataTypes.Boolean)]
        public bool? FacilitiesInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prescanautocountout", modeltype: FwDataTypes.Boolean)]
        public bool? PresInitializeAutomaticallyCountInventoryThatIsOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeowned", modeltype: FwDataTypes.Boolean)]
        public bool? CycleIncludeOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeconsigned", modeltype: FwDataTypes.Boolean)]
        public bool? CycleIncludeConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectitemsby", modeltype: FwDataTypes.Boolean)]
        public bool? Selectitemsby_not_used { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludenoavail", modeltype: FwDataTypes.Boolean)]
        public bool? ExcludeInventoryWithNoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbyid", modeltype: FwDataTypes.Text)]
        public string ApprovedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedby", modeltype: FwDataTypes.Text)]
        public string ApprovedByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedate", modeltype: FwDataTypes.Date)]
        public string ApproveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 

            AddActiveViewFieldToSelect("OfficeLocationId", "locationid", select, request);
            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);

        }
        //------------------------------------------------------------------------------------ 
    }
}

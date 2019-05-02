using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.PhysicalInventory
{
    [FwSqlTable("physical")]
    public class PhysicalInventoryRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string PhysicalInventoryId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scheduledate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string ScheduleDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "preinitdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string PreInitializeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        //public string Location { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rank", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public string Rank { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step0", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPreScan { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step1", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepInitiate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step2", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintCountSheets { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step3", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepCount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step4", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintExceptionReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step5", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintDiscrepancyReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step6", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepRecount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step7", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintRecountAnalysisReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step8", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintReconciliationReport { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step9", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepApproveCounts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step10", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepClose { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step11", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepCloseWithoutAdjustments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ApprovedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string Approvedate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "initdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string InitializeDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclelast", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string CycleLastCounted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycletrackedby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CycleTrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclea", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CycleRankA { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleb", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankB { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclec", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycled", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankD { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleaisle", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string CycleAisle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleshelf", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 4)]
        public string CycleShelf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlyowned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleOnlyIncludeOwnedwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approvedpurchasecost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ApprovedPurchaseCost { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countout", modeltype: FwDataTypes.Boolean, sqltype: "varchar")]
        public bool? CountInventoryThatIsOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "counttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8, required: true)]
        public string CountType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isspace", modeltype: FwDataTypes.Boolean, sqltype: "varchar")]
        public bool? FacilitiesInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "physicalno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string PhysicalInventoryNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 1, required: true)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "step12", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? StepPrintResults { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prescanautocountout", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PresInitializeAutomaticallyCountInventoryThatIsOut { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlyactivenegative", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OnlyNegativeInventory { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "onlyinactivenegative", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Onlyinactivenegative_not_used { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeowned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleIncludeOwned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "includeconsigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleIncludeConsigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "selectitemsby", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Selectitemsby_not_used { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "excludenoavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ExcludeInventoryWithNoAvailabilityCheck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankE { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cyclef", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankF { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cycleg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? CycleRankG { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

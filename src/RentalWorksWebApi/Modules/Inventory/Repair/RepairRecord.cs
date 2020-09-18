using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Modules.HomeControls.InventoryAvailability;

namespace WebApi.Modules.Inventory.Repair
{
    [FwSqlTable("repair")]
    public class RepairRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string RepairId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "status", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Status { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagecontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DamageContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DamageScannedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairitemstatusid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RepairItemStatusId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagedealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DamageDealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, required: true)]
        public string RepairType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chargeorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ChargeOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "itemdesc", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string ItemDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 16)]
        public string RepairNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pono", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 25)]
        public string PoNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "outsiderepair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OutsideRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pending", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PoPending { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Billable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damageorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damagescannedbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DamageScannedById { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ldorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LossAndDamageOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "damage", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string Damage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "correction", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string Correction { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string RepairDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "priority", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Priority { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatebyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string EstimateByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "estimatedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string EstimateDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completebyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CompleteByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "completedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string CompleteDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "duedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string DueDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billinglocationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BillingLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billingwarehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string BillingWarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transferid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TransferId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "softwareversion", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string SoftwareVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "footcandles", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? FootCandles { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingrepair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PendingRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "totalalerted", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? TotalAlerted { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------  
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcnote", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string QcNote { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairautocompleteqc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AutoCompleteQC { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleRepairEstimateResponse> ToggleEstimate()
        {
            return await RepairFunc.ToggleRepairEstimate(AppConfig, UserSession, RepairId);
        }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleRepairCompleteResponse> ToggleComplete()
        {
            ToggleRepairCompleteResponse response = await RepairFunc.ToggleRepairComplete(AppConfig, UserSession, RepairId);
            if (response.success)
            {
                if (!string.IsNullOrEmpty(InventoryId) && !string.IsNullOrEmpty(WarehouseId))
                {
                    FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                    string classification = FwSqlCommand.GetStringDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", InventoryId, "class").Result;
                    InventoryAvailabilityFunc.RequestRecalc(InventoryId, WarehouseId, classification);
                }
            }
            return response;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<RepairReleaseItemsResponse> ReleaseItems(int quantity)
        {
            RepairReleaseItemsResponse response = await RepairFunc.ReleaseRepairItems(AppConfig, UserSession, RepairId, quantity);
            if (response.success)
            {
                if (!string.IsNullOrEmpty(InventoryId) && !string.IsNullOrEmpty(WarehouseId))
                {
                    FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                    string classification = FwSqlCommand.GetStringDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", InventoryId, "class").Result;
                    InventoryAvailabilityFunc.RequestRecalc(InventoryId, WarehouseId, classification);
                }
            }
            return response;
        }
        //------------------------------------------------------------------------------------ 
        public async Task<VoidRepairResponse> Void()
        {
            VoidRepairResponse response = await RepairFunc.VoidRepair(AppConfig, UserSession, RepairId);
            if (response.success)
            {
                if (!string.IsNullOrEmpty(InventoryId) && !string.IsNullOrEmpty(WarehouseId))
                {
                    FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString);
                    string classification = FwSqlCommand.GetStringDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", InventoryId, "class").Result;
                    InventoryAvailabilityFunc.RequestRecalc(InventoryId, WarehouseId, classification);
                }
            }
            return response;
        }
        //------------------------------------------------------------------------------------ 
    }
}

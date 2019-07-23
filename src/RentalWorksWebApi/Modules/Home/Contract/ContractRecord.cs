using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.Contract
{
    [FwSqlTable("contract")]
    public class ContractRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ContractId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractno", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string ContractNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ContractDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContractTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contracttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string ContractType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DocumentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ModifiedByUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "notes", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8000)]
        public string Notes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "multiorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsMultiOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string BillingDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pendingexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PendingExchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unassigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Unassigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "needreconcile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? NeedReconcile { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repair", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Repair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Exchange { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "suspend", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Suspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "forcedsuspend", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ForcedSuspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "void", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Void { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exchangecontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ExchangeContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionno", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? SessionNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "migrated", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsMigrated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deliveryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DeliveryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "origunassigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OriginalUnassigned { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "truck", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Truck { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldatechangereason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string BillingDateChangeReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "packingslipno", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string PackingSlipNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "parts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Parts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "termsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TermsConditionsId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transfer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Transfer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "responsiblepersonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ResponsiblePersonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "overridepastdue", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OverridePastDue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseinternaloutcontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PurchaseInternalOutContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "characterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CharacterId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printonorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? PrintOnOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sessionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string SessionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "quikinoneorderpercontract", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? QuikInOneOrderPerContract { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

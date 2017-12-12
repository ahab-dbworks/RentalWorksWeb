using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Home.ItemQc
{
    [FwSqlTable("rentalitemqc")]
    public class ItemQcRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemqcid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ItemQcId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string QcByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcdatetime", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string QcDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastorderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string LastOrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequiredasof", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string QcRequiredAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ConditionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "incontractid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InContractId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
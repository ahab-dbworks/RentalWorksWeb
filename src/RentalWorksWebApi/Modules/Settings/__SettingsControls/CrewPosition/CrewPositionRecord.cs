using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.CrewPosition
{
    [FwSqlTable("crewposition")]
    public class CrewPositionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewpositionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CrewPositionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string CrewId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "effectivedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string EffectiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enddate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string EndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costdaily", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? CostDaily { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costhourly", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? CostHourly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costmonthly", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? CostMonthly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costweekly", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? CostWeekly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
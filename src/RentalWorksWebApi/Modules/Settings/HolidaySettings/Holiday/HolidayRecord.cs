using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.HolidaySettings.Holiday
{
    [FwSqlTable("holidaydefinition")]
    public class HolidayRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "holidayid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string HolidayId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "holiday", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Holiday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custom", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Custom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "observed", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Observed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 2, required: true)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedmonth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? FixedMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedday", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? FixedDay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedyear", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? FixedYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dayofweek", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DayOfWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weekofmonth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? WeekOfMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Adjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "offsetholidayid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OffsetHolidayId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
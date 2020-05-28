using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.CalendarSettings.Holiday
{
    [FwSqlTable("holidaydefinitionview")]
    public class HolidayLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "holidayid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string HolidayId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "holiday", modeltype: FwDataTypes.Text)]
        public string Holiday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "custom", modeltype: FwDataTypes.Boolean)]
        public bool? Custom { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "observed", modeltype: FwDataTypes.Boolean)]
        public bool? Observed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedmonth", modeltype: FwDataTypes.Integer)]
        public int? FixedMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedday", modeltype: FwDataTypes.Integer)]
        public int? FixedDay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fixedyear", modeltype: FwDataTypes.Integer)]
        public int? FixedYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dayofweek", modeltype: FwDataTypes.Integer)]
        public int? DayOfweek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weekofmonth", modeltype: FwDataTypes.Integer)]
        public int? WeekOfMonth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Integer)]
        public int? Adjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "offsetholidayid", modeltype: FwDataTypes.Text)]
        public string OffsetHolidayId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "offsetholiday", modeltype: FwDataTypes.Text)]
        public string OffsetHoliday { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
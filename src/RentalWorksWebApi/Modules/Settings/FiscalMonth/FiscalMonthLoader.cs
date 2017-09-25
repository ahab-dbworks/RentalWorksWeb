using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Settings.FiscalMonth
{
    [FwSqlTable("fiscalmonthview")]
    public class FiscalMonthLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalmonthid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string FiscalMonthId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalyearid", modeltype: FwDataTypes.Text)]
        public string FiscalYearId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthdisplay", modeltype: FwDataTypes.Text)]
        public string MonthDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.UTCDateTime)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.UTCDateTime)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthyear", modeltype: FwDataTypes.Text)]
        public string MonthYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "workdays", modeltype: FwDataTypes.Integer)]
        public int WorkDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year", modeltype: FwDataTypes.Text)]
        public string Year { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month", modeltype: FwDataTypes.Integer)]
        public int Month { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalorder", modeltype: FwDataTypes.Text)]
        public string FiscalOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "closed", modeltype: FwDataTypes.Boolean)]
        public bool Closed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
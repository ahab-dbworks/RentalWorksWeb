using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.FiscalMonth
{
    [FwSqlTable("fiscalmonthview")]
    public class FiscalMonthLoader : AppDataLoadRecord
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
        [FwSqlDataField(column: "fromdate", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "todate", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthyear", modeltype: FwDataTypes.Text)]
        public string MonthYear { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "workdays", modeltype: FwDataTypes.Integer)]
        public int? WorkDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "year", modeltype: FwDataTypes.Text)]
        public string Year { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "month", modeltype: FwDataTypes.Integer)]
        public int? Month { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fiscalorder", modeltype: FwDataTypes.Text)]
        public string FiscalOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "closed", modeltype: FwDataTypes.Boolean)]
        public bool? Closed { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("FiscalYearId", "fiscalyearid", select, request); 
        }
        //------------------------------------------------------------------------------------    } 
    }
}
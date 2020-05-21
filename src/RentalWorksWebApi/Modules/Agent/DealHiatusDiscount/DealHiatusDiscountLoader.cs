using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Agent.DealHiatusDiscount
{
    [FwSqlTable("billschedview")]
    public class DealHiatusDiscountLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billschedid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealHiatusDiscountId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epno", modeltype: FwDataTypes.Integer)]
        public int? EpisodeNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epdatefrom", modeltype: FwDataTypes.Date)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epdateto", modeltype: FwDataTypes.Date)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epdays", modeltype: FwDataTypes.Integer)]
        public int? Days { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabledays", modeltype: FwDataTypes.Decimal)]
        public decimal? BillableDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billweekend", modeltype: FwDataTypes.Boolean)]
        public bool? BillWeekends { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billholiday", modeltype: FwDataTypes.Boolean)]
        public bool? BillHolidays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatus", modeltype: FwDataTypes.Boolean)]
        public bool? IsHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatusdiscount", modeltype: FwDataTypes.Decimal)]
        public decimal? HiatusDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eporder", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorateflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsProrated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changescheduleflg", modeltype: FwDataTypes.Boolean)]
        public bool? DoChangeSchedule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isepisode", modeltype: FwDataTypes.Boolean)]
        public bool? IsEpisode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("DealId", "dealid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

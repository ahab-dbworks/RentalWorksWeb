using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Agent.DealHiatusDiscount
{
    [FwSqlTable("billsched")]
    public class DealHiatusDiscountRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billschedid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DealHiatusDiscountId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epdatefrom", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string FromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epdateto", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string ToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "epno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? EpisodeNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsHiatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hiatusdiscount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? HiatusDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "eporder", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billweekend", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? BillWeekends { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billholiday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? BillHolidays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "scheduletypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ScheduletypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changescheduleflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? DoChangeSchedule { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prorateflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsProrated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isepisode", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsEpisode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "billabledays", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? BillableDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

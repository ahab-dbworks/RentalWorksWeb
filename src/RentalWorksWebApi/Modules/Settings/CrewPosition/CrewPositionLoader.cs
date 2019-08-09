using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.CrewPosition
{
    [FwSqlTable("crewpositionview")]
    public class CrewPositionLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewpositionid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CrewPositionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string CrewId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string LaborTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "effectivedate", modeltype: FwDataTypes.Date)]
        public string EffectiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enddate", modeltype: FwDataTypes.Date)]
        public string EndDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costhourly", modeltype: FwDataTypes.Decimal)]
        public decimal? CostHourly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costdaily", modeltype: FwDataTypes.Decimal)]
        public decimal? CostDaily { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costweekly", modeltype: FwDataTypes.Decimal)]
        public decimal? CostWeekly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "costmonthly", modeltype: FwDataTypes.Decimal)]
        public decimal? CostMonthly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimary{ get; set; }
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
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("CrewId", "contactid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
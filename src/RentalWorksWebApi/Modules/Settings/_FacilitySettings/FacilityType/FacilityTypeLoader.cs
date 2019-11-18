using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.FacilityType
{
    [FwSqlTable("inventorydepartment")]
    public class FacilityTypeLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string FacilityTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string FacilityType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean)]
        public bool? Facilities { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stagescheduling", modeltype: FwDataTypes.Boolean)]
        public bool? StageScheduling { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultdw", modeltype: FwDataTypes.Decimal, precision: 5, scale: 3)]
        public decimal? FacilitiesDefaultDw { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donotdefaultrate", modeltype: FwDataTypes.Boolean)]
        public bool? FacilitiesDoNotDefaultRateOnBooking { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool? GroupProfitLoss { get; set; }
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
            select.AddWhere("(space='T')");
        }
        //------------------------------------------------------------------------------------
    }
}

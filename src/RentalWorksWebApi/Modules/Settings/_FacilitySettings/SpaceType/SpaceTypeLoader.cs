using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.FacilitySettings.SpaceType
{
    [FwSqlTable("spacetypeview")]
    public class SpaceTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SpaceTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetype", modeltype: FwDataTypes.Text)]
        public string SpaceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeclassification", modeltype: FwDataTypes.Text)]
        public string SpaceTypeClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string FacilityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemasterno", modeltype: FwDataTypes.Text)]
        public string RateICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemaster", modeltype: FwDataTypes.Text)]
        public string RateDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateunitid", modeltype: FwDataTypes.Text)]
        public string RateUnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rateunit", modeltype: FwDataTypes.Text)]
        public string RateUnit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whitetext", modeltype: FwDataTypes.Boolean)]
        public bool? WhiteText { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "nonbillable", modeltype: FwDataTypes.Boolean)]
        public bool? NonBillable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "forreportsonly", modeltype: FwDataTypes.Boolean)]
        public bool? ForReportsOnly { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "addtodescription", modeltype: FwDataTypes.Boolean)]
        public bool? AddToDescription { get; set; }
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
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
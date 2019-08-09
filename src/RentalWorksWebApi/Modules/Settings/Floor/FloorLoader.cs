using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.Floor
{
    [FwSqlTable("floorview")]
    public class FloorLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string FloorId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floor", modeltype: FwDataTypes.Text)]
        public string Floor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text)]
        public string BuildingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sqft", modeltype: FwDataTypes.Decimal)]
        public decimal? SquareFeet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commonsqft", modeltype: FwDataTypes.Decimal)]
        public decimal? CommonSquareFeet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorplanid", modeltype: FwDataTypes.Text)]
        public string FloorPlanId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hasfloorplan", modeltype: FwDataTypes.Boolean)]
        public bool? HasFloorPlan { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BuildingId", "buildingid", select, request); 
        }
        //------------------------------------------------------------------------------------    
    }
}
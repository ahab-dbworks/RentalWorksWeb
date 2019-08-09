using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.SpaceRate
{
    [FwSqlTable("spacerateview")]
    public class SpaceRateLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacerateid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SpaceRateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text)]
        public string BuildingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorid", modeltype: FwDataTypes.Text)]
        public string FloorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string SpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string FacilityTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string FacilityType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeid", modeltype: FwDataTypes.Text)]
        public string SpaceTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetype", modeltype: FwDataTypes.Text)]
        public string SpaceType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ratemasterid", modeltype: FwDataTypes.Text)]
        public string RateId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        //public string WarehouseId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "price", modeltype: FwDataTypes.Decimal)]
        public decimal? Price { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hourlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? HourlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dailyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "weeklyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week2rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week3rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week4rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "week5rate", modeltype: FwDataTypes.Decimal)]
        public decimal? Week5Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "monthlyrate", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "stagescheduling", modeltype: FwDataTypes.Boolean)]
        public bool? StageScheduling { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text)]
        public string UnitId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetypeclassification", modeltype: FwDataTypes.Text)]
        public string SpaceTypeClassification { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BuildingId", "buildingid", select, request);
            addFilterToSelect("FloorId", "floorid", select, request);
            addFilterToSelect("SpaceId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
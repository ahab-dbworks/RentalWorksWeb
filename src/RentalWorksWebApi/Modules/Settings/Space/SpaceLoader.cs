using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.Space
{
    [FwSqlTable("masterspaceview")]
    public class SpaceLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        //public string LocationId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SpaceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "room", modeltype: FwDataTypes.Text)]
        public string Space { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text)]
        public string BuildingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "building", modeltype: FwDataTypes.Text)]
        public string Building { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingtype", modeltype: FwDataTypes.Text)]
        public string BuildingType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorid", modeltype: FwDataTypes.Text)]
        public string FloorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floor", modeltype: FwDataTypes.Text)]
        public string Floor { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "roomid", modeltype: FwDataTypes.Text)]
        //public string RoomId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        //public string Master { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        //public string Description { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingroom", modeltype: FwDataTypes.Text)]
        public string BuildingSpace { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingfloorroom", modeltype: FwDataTypes.Text)]
        public string BuildingFloorSpace { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacesqft", modeltype: FwDataTypes.Decimal)]
        public decimal? SquareFeet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacefromdate", modeltype: FwDataTypes.Date)]
        public string SpaceFromDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacetodate", modeltype: FwDataTypes.Date)]
        public string SpaceToDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "commonsqftflg", modeltype: FwDataTypes.Boolean)]
        public bool? CommonSquareFeet { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydimensionid", modeltype: FwDataTypes.Text)]
        public string PrimaryDimensionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "widthft", modeltype: FwDataTypes.Integer)]
        public int? WidthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "heightft", modeltype: FwDataTypes.Integer)]
        public int? HeightFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lengthft", modeltype: FwDataTypes.Integer)]
        public int? LengthFt { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "occupancy", modeltype: FwDataTypes.Integer)]
        public int? Occupancy { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg1", modeltype: FwDataTypes.Text)]
        //public string Chg1 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg2", modeltype: FwDataTypes.Text)]
        //public string Chg2 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg3", modeltype: FwDataTypes.Text)]
        //public string Chg3 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg4", modeltype: FwDataTypes.Text)]
        //public string Chg4 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg5", modeltype: FwDataTypes.Text)]
        //public string Chg5 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg6", modeltype: FwDataTypes.Text)]
        //public string Chg6 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg7", modeltype: FwDataTypes.Text)]
        //public string Chg7 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg8", modeltype: FwDataTypes.Text)]
        //public string Chg8 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg9", modeltype: FwDataTypes.Text)]
        //public string Chg9 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chg10", modeltype: FwDataTypes.Text)]
        //public string Chg10 { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderbybuilding", modeltype: FwDataTypes.Integer)]
        //public int? Orderbybuilding { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderbyfloor", modeltype: FwDataTypes.Integer)]
        //public int? Orderbyfloor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderbyroom", modeltype: FwDataTypes.Integer)]
        //public int? Orderbyroom { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("BuildingId", "buildingid", select, request); 
            addFilterToSelect("FloorId", "floorid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
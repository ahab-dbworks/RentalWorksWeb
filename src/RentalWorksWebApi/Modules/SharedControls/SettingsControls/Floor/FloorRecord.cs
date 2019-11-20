using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.Floor
{
    [FwSqlTable("floor")]
    public class FloorRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string FloorId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floor", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5, required: true)]
        public string Floor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string BuildingId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "floorplanid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string FloorPlanId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
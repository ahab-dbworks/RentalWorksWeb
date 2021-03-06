using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.FacilitySettings.Venue
{
    [FwSqlTable("building")]
    public class VenueRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string VenueId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "building", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string Venue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string VenueCode { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderlocation { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "createbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string CreatebyusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "createdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        //public string Createdate { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required:true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatebyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string UpdatebyusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        //public string Updatedate { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "buildingtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string BuildingType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string WebAddress { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
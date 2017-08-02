using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DocumentType
{
    [FwSqlTable("documenttype")]
    public class DocumentTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "documenttypeid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string DocumentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "documenttype", dataType: FwDataTypes.Text, length: 30)]
        public string DocumentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "floorplan", dataType: FwDataTypes.Boolean)]
        public bool Floorplan { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "videos", dataType: FwDataTypes.Boolean)]
        public bool Videos { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "panoramic", dataType: FwDataTypes.Boolean)]
        public bool Panoramic { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "autoattachtoemail", dataType: FwDataTypes.Boolean)]
        public bool AutomaticallyAttachToEmail { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.DocumentSettings.DocumentType
{
    [FwSqlTable("documenttype")]
    public class DocumentTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "documenttypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string DocumentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "documenttype", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string DocumentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "floorplan", modeltype: FwDataTypes.Boolean)]
        public bool? Floorplan { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "videos", modeltype: FwDataTypes.Boolean)]
        public bool? Videos { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "panoramic", modeltype: FwDataTypes.Boolean)]
        public bool? Panoramic { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "autoattachtoemail", modeltype: FwDataTypes.Boolean)]
        public bool? AutomaticallyAttachToEmail { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

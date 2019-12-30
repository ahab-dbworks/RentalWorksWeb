using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.ExportSettings.DataExportFormat
{
    [FwSqlTable("webdataexportformat")]
    public class DataExportFormatRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dataexportformatid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? DataExportFormatId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exporttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string ExportType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "active", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportstring", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: -1, required: true)]
        public string ExportString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultformat", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 1)]
        public bool? DefaultFormat { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

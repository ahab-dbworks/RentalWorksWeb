using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace FwStandard.Grids.AppDocument
{
    [FwSqlTable("appdocument")]
    public class AppDocumentRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appdocumentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string AppDocumentId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inputbyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InputByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtoemail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? AttachToEmail { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachdate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string AttachDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId1 { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId2 { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1int", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? UniqueId1Int { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attachtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string AttachTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documenttypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DocumentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

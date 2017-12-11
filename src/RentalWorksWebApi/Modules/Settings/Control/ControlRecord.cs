using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.Control
{
    [FwSqlTable("control")]
    public class ControlRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ControlId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 70)]
        public string Company { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "system", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 70)]
        public string System { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "maxrows", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? Maxrows { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "imagepath", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Imagepath { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dbversion", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string Dbversion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "build", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 3)]
        public string Build { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
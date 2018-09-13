using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.Hotfix
{
    [FwSqlTable("hotfix")]
    public class HotfixRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hotfixid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string HotfixId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "servername", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Servername { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "hotfixbegin", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public string HotfixBegin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.DateTime, sqltype: "datetime")]
        public string HotfixEnd { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

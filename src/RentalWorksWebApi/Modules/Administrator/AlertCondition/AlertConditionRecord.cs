using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.AlertCondition
{
    [FwSqlTable("webalertcondition")]
    public class AlertConditionRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertconditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey:true)]
        public string AlertConditionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string FieldName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "value", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Value { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

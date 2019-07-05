using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
namespace FwStandard.Modules.Administrator.AlertCondition
{
    [FwSqlTable("webalertcondition")]
    public class AlertConditionRecord : FwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string AlertId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "alertconditionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey:true)]
        public string AlertConditionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string FieldName1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50, required: true)]
        public string Condition { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fieldname2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, required: true)]
        public string FieldName2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "value", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string Value { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("AlertId", "alertid", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}

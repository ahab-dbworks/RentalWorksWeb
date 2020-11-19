using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.CreditCardPinPad
{
    [FwSqlTable("creditcardpinpad")]
    public class CreditCardPinPadRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "creditcardpinpadid", modeltype: FwDataTypes.Integer, sqltype: "int", isPrimaryKey: true, identity: true)]
        public int? CreditCardPinPadId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "code", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string Code { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

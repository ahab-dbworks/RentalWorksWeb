using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.OrderContact
{
    [FwSqlTable("ordercontact")]
    public class OrderContactRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordercontactid", modeltype: FwDataTypes.Integer, sqltype: "int", identity: true, isPrimaryKey: true)]
        public int? OrderContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CompanyContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderedby", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsOrderedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsPrintable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "productioncontact", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsProductionContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

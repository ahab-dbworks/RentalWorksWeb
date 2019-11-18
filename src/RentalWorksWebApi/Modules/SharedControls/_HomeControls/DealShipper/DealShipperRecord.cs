using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Home.DealShipper
{
    [FwSqlTable("dealshipper")]
    public class DealShipperRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealshipperid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DealShipperId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipperacct", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        public string ShipperAcct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
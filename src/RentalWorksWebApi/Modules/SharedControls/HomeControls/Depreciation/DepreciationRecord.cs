using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Depreciation
{
    [FwSqlTable("depreciation")]
    public class DepreciationRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationid", modeltype: FwDataTypes.Integer, identity: true, sqltype: "int", isPrimaryKey: true)]
        public int? DepreciationId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationdate", modeltype: FwDataTypes.Date, sqltype: "datetime", required: true)]
        public string DepreciationDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "purchaseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string PurchaseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "depreciationqty", modeltype: FwDataTypes.Integer, sqltype: "int")]
        public int? DepreciationQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "unitdepreciationamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? UnitDepreciationAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "adjustment", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsAdjustment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

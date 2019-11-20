using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.InventorySettings.InventoryRank
{
    [FwSqlTable("rank")]
    public class InventoryRankRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string InventoryRankId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rankupdated", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime")]
        public string RankUpdated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "type", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, required: true)]
        public string Type { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "afromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? AFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "atovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? AToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "bfromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? BFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "btovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? BToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cfromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? CFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ctovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? CToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dfromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? DFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dtovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        public decimal? DToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "efromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? EFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "etovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? EToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ffromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? FFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ftovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? FToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gfromvalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? GFromValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gtovalue", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        public decimal? GToValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
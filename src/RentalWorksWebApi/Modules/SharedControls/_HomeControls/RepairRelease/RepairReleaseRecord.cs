using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.RepairRelease
{
    [FwSqlTable("repairrelease")]
    public class RepairReleaseRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairreleaseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string RepairReleaseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releasedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime", required: true)]
        public string ReleaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releaseqty", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        public decimal? ReleaseQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

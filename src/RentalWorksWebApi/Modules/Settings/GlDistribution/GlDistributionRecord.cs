using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data;
namespace RentalWorksWebApi.Modules.Settings.GlDistribution
{
    [FwSqlTable("gldistribution")]
    public class GlDistributionRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "gldistributionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string GlDistributionId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string GlAccountId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "accounttype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 50)]
        public string AccountType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
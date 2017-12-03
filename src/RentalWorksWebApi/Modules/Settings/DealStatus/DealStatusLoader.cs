using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.DealStatus
{
    [FwSqlTable("dealstatusview")]
    public class DealStatusLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealstatusid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealstatus", modeltype: FwDataTypes.Text)]
        public string DealStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statustype", modeltype: FwDataTypes.Text)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatusid", modeltype: FwDataTypes.Text)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "creditstatus", modeltype: FwDataTypes.Text)]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

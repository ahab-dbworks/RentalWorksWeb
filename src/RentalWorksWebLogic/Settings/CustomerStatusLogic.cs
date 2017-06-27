using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebLogic.Settings
{
    [FwSqlTable("custstatus", hasInsert: true, hasUpdate: true, hasDelete: true)]
    public class CustomerStatusLogic : FwBusinessLogic
    {
        [FwSqlDataField("custstatusid", FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";

        [FwSqlDataField("custstatus", FwDataTypes.Text)]
        public string CustomerStatus { get; set; } = "";

        [FwSqlDataField("statustype", FwDataTypes.Text)]
        public string StatusType { get; set; } = "";

        [FwSqlDataField("creditstatusid", FwDataTypes.Text)]
        public string CreditStatusId { get; set; } = "";

        [FwSqlDataField("datestamp", FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; } = null;

        [FwSqlDataField("inactive", FwDataTypes.Boolean)]
        public string Inactive { get; set; } = "";
        //------------------------------------------------------------------------------------
    }
}

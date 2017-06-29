using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("glaccount", hasInsert: true, hasUpdate: true, hasDelete: true)]
    public class GlAccountRecord : RwDataRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField("glaccountid", FwDataTypes.Text, isPrimaryKey: true)]
        public string GlAccountId { get; set; } = "";

        [FwSqlDataField("glno", FwDataTypes.Text)]
        public string GlAccountNo { get; set; } = "";

        [FwSqlDataField("glacctdesc", FwDataTypes.Text)]
        public string GlAccountDescription { get; set; } = "";

        [FwSqlDataField("gltype", FwDataTypes.Text)]
        public string GlAccountType { get; set; } = "";

        [FwSqlDataField("datestamp", FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; } = null;

        [FwSqlDataField("inactive", FwDataTypes.Boolean)]
        public string Inactive { get; set; } = "";
        //------------------------------------------------------------------------------------
    }
}

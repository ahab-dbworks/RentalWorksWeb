using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("glaccount")]
    public class GlAccountRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glaccountid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string GlAccountId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glno", dataType: FwDataTypes.Text, length: 20)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "glacctdesc", dataType: FwDataTypes.Text, length: 35)]
        public string GlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "gltype", dataType: FwDataTypes.Text, length: 10)]
        public string GlAccountType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Administrator.SystemUpdateHistoryLog
{
    [FwSqlTable("systemupdatehistorylog")]
    public class SystemUpdateHistoryLogLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistorylogid", modeltype: FwDataTypes.Integer, isPrimaryKey: true, identity: true)]
        public int? SystemUpdateHistoryLogId { get; set; } = 0;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemupdatehistoryid", modeltype: FwDataTypes.Integer, required: true)]
        public int? SystemUpdateHistoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "msg", modeltype: FwDataTypes.Text)]
        public string Messsage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("SystemUpdateHistoryId", "systemupdatehistoryid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}

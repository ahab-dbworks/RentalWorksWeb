using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;

namespace FwStandard.Modules.Administrator.WebAuditJson
{
    [FwSqlTable("webauditjsonview")]
    public class WebAuditJsonLoader : FwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webauditid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? WebAuditId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modulename", modeltype: FwDataTypes.Text)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "recordtitle", modeltype: FwDataTypes.Text)]
        public string Title { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid3", modeltype: FwDataTypes.Text)]
        public string UniqueId3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "json", modeltype: FwDataTypes.Text)]
        public string Json { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            //justin 10/10/2018 prevent query from returning all data if no UniqueIds are provided.
            if (string.IsNullOrWhiteSpace(GetUniqueIdAsString("UniqueId1", request)))
            {
                select.AddWhere("uniqueid1 = @bogus");
                select.AddParameter("@bogus", "~x~x~x~x");
            }
            else
            {
                addFilterToSelect("UniqueId1", "uniqueid1", select, request);
                addFilterToSelect("UniqueId2", "uniqueid2", select, request);
                addFilterToSelect("UniqueId3", "uniqueid3", select, request);
            }
            addFilterToSelect("ModuleName", "modulename", select, request);
        }
        //------------------------------------------------------------------------------------ 
    }
}

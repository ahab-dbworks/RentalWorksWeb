using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.MiscType
{
    [FwSqlTable("inventorydepartment")]
    public class MiscTypeLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string MiscTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string MiscType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean)]
        public bool? Misc { get; set; }
        ////------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool? GroupProfitLoss { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(misc='T')");
        }
        //------------------------------------------------------------------------------------
    }
}

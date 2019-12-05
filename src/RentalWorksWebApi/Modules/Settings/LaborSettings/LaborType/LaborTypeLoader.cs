using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi;

namespace WebApi.Modules.Settings.LaborSettings.LaborType
{
    [FwSqlTable("dbo.funcinventorytype(@rectype)")]
    public class LaborTypeLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string LaborTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string LaborType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean)]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "profitlossgroup", modeltype: FwDataTypes.Boolean)]
        public bool? GroupProfitLoss { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categorycount", modeltype: FwDataTypes.Integer)]
        public int? CategoryCount { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            useWithNoLock = false;
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();

            string recType = GetUniqueIdAsString("RecType", request) ?? RwConstants.RECTYPE_LABOR;
            select.AddParameter("@rectype", recType);

            select.AddWhere("(labor='T')");

            if (GetUniqueIdAsBoolean("HasCategories", request).GetValueOrDefault(false))
            {
                select.AddWhere("(categorycount > 0)");
            }

        }
        //------------------------------------------------------------------------------------
    }
}

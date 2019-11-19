using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.PropsSettings.PropsCondition
{
    [FwSqlTable("condition")]
    public class PropsConditionLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "conditionid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PropsConditionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "condition", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string PropsCondition { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean)]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean)]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "sets", modeltype: FwDataTypes.Boolean)]
        public bool? Sets { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "props", modeltype: FwDataTypes.Boolean)]
        public bool? Props { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "wardrobe", modeltype: FwDataTypes.Boolean)]
        public bool? Wardrobe { get; set; }
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
            select.AddWhere("(props='T')");
        }
        //------------------------------------------------------------------------------------

    }
}

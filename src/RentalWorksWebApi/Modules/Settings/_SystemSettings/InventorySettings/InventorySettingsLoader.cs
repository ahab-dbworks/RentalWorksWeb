using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.SystemSettings.InventorySettings
{
    [FwSqlTable("controlview")]
    public class InventorySettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string InventorySettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'Inventory Settings'", modeltype: FwDataTypes.Text)]
        public string InventorySettingsName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invmask", modeltype: FwDataTypes.Text)]
        public string ICodeMask { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignmasterno", modeltype: FwDataTypes.Boolean)]
        public bool? UserAssignedICodes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Integer)]
        public int? NextICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "icodeprefix", modeltype: FwDataTypes.Text)]
        public string ICodePrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
        }
        //------------------------------------------------------------------------------------ 
    }
}
 
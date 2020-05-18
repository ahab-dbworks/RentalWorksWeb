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
        [FwSqlDataField(column: "enable3weekpricing", modeltype: FwDataTypes.Boolean)]
        public bool? Enable3WeekPricing { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckoutretiredreasonid", modeltype: FwDataTypes.Text)]
        public string SalesCheckOutRetiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckoutretiredreason", modeltype: FwDataTypes.Text)]
        public string SalesCheckOutRetiredReason { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckinunretiredreasonid", modeltype: FwDataTypes.Text)]
        public string SalesCheckInUnretiredReasonId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salescheckinunretiredreason", modeltype: FwDataTypes.Text)]
        public string SalesCheckInUnretiredReason { get; set; }
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

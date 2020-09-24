using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.SystemSettings.SystemSettings
{
    [FwSqlTable("controlview")]
    public class SystemSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SystemSettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
        public string CompanyName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "system", modeltype: FwDataTypes.Text)]
        public string SystemName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dbversion", modeltype: FwDataTypes.Text)]
        public string DatabaseVersion { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sharedeals", modeltype: FwDataTypes.Boolean)]
        public bool? ShareDealsAcrossOfficeLocations { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "userassignedvendorno", modeltype: FwDataTypes.Boolean)]
        public bool? IsVendorNumberAssignedByUser{ get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Integer)]
        public int? LastVendorNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowdeletebatchedreceipt", modeltype: FwDataTypes.Boolean)]
        public bool? AllowDeleteExportedReceipts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablereceipts", modeltype: FwDataTypes.Boolean)]
        public bool? EnableReceipts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "betaupdates", modeltype: FwDataTypes.Boolean)]
        public bool? EnableBetaUpdates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qaupdates", modeltype: FwDataTypes.Boolean)]
        public bool? EnableQaUpdates { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enablepayments", modeltype: FwDataTypes.Boolean)]
        public bool? EnablePayments { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "allowdeletebatchedpayment", modeltype: FwDataTypes.Boolean)]
        public bool? AllowDeleteExportedPayments { get; set; }
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
 
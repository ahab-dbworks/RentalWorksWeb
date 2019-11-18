using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.DocumentBarCodeSettings
{
    [FwSqlTable("controlview")]
    public class DocumentBarCodeSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DocumentBarCodeSettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "'Document Bar Code Settings'", modeltype: FwDataTypes.Text)]
        public string DocumentBarCodeSettingsName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "documentbarcodestyle", modeltype: FwDataTypes.Text)]
        public string DocumentBarCodeStyle { get; set; }
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
 
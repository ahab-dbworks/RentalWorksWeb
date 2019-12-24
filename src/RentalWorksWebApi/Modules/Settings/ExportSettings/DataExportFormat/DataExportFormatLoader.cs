using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.ExportSettings.DataExportFormat
{
    [FwSqlTable("webdataexportformat")]
    public class DataExportFormatLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dataexportformatid", modeltype: FwDataTypes.Integer, isPrimaryKey: true)]
        public int? DataExportFormatId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exporttype", modeltype: FwDataTypes.Text)]
        public string ExportType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "active", modeltype: FwDataTypes.Boolean)]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exportstring", modeltype: FwDataTypes.Text)]
        public string ExportString { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultformat", modeltype: FwDataTypes.Boolean)]
        public bool? DefaultFormat { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("ExportType", "exporttype", select, request);
            addFilterToSelect("DefaultFormat", "defaultformat", select, request);
        }

    }
}

using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using WebApi;

namespace WebApi.Modules.UtilitiesControls.ExportSettings
{
    [FwSqlTable("dataexportsetting")]
    public class ExportSettingsRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dataexportsettingid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string ExportSettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "doexport", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Active { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "doexportfooter", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ExportFooter { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "doexportheader", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ExportHeader { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "exportstring", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string ExportString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "footerstring", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string FooterString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "headerstring", modeltype: FwDataTypes.Text, sqltype: "text")]
        public string HeaderString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option01", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 10)]
        public bool? Option01 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option01filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Option01FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option02", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 10)]
        public bool? Option02 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option02filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Option02FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option03", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 10)]
        public bool? Option03 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "option03filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Option03FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settingname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if (GetMiscFieldAsBoolean("Invoices", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("where", "settingname",
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_HEADER + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_DETAIL + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_GL_SUMMARY + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_TAX + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_NOTE);
            }
            else if (GetMiscFieldAsBoolean("Receipts", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("where", "settingname",
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_RECEIPT_HEADER + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_RECEIPT_DETAIL);
            }
            else if (GetMiscFieldAsBoolean("VendorInvoices", request).GetValueOrDefault(false))
            {
                select.AddWhereIn("where", "settingname",
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_HEADER + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_DETAIL + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_GL_SUMMARY + "," +
                    RwConstants.DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_TAX);
            }
            addFilterToSelect("Description", "settingname", select, request);
        }
        //------------------------------------------------------------------------------------    
    }
}

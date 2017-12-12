using FwStandard.BusinessLogic;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.WarehouseCatalog
{
    [FwSqlTable("whcatalog")]
    public class WarehouseCatalogRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcatalogid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string WarehouseCatalogId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcatalog", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, required: true)]
        public string WarehouseCatalog { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "catalogtype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string CatalogType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            if (CatalogType != null)
            {
                if (!(CatalogType.Equals("RENTAL") || CatalogType.Equals("SALES") || CatalogType.Equals("PARTS")))
                {
                    isValid = false;
                    validateMsg = "Invalid CatalogType: " + CatalogType;
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}

using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;
namespace WebApi.Modules.Settings.InventorySettings.WarehouseCatalog
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
        protected override async Task<FwValidateResult> ValidateAsync(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, FwValidateResult result)
        {
            if (result.IsValid && CatalogType != null)
            {
                if (!(CatalogType.Equals("RENTAL") || CatalogType.Equals("SALES") || CatalogType.Equals("PARTS")))
                {
                    result.IsValid = false;
                    result.ValidateMsg = "Invalid CatalogType: " + CatalogType;
                }
            }
            await Task.CompletedTask;
            return result;
        }
        //------------------------------------------------------------------------------------ 
    }
}

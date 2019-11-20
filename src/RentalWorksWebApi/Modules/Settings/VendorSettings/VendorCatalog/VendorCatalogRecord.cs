using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VendorSettings.VendorCatalog
{
    [FwSqlTable("vendorcatalog")]
    public class VendorCatalogRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorcatalogid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string VendorCatalogId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorcatalog", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, required: true)]
        public string VendorCatalog { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cattype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string CatalogType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ShipViaId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxrate", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? TaxRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "globalmarkup", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        public decimal? GlobalMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.VendorSettings.VendorCatalog
{
    [FwSqlTable("vendorcatalogview")]
    public class VendorCatalogLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorcatalogid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorCatalogId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorcatalog", modeltype: FwDataTypes.Text)]
        public string VendorCatalog { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cattype", modeltype: FwDataTypes.Text)]
        public string CatalogType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendor", modeltype: FwDataTypes.Text)]
        public string Vendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartmentid", modeltype: FwDataTypes.Text)]
        public string InventoryTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inventorydepartment", modeltype: FwDataTypes.Text)]
        public string InventoryType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "categoryid", modeltype: FwDataTypes.Text)]
        public string CategoryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "category", modeltype: FwDataTypes.Text)]
        public string Category { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxrate", modeltype: FwDataTypes.Decimal)]
        public decimal? TaxRate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "globalmarkup", modeltype: FwDataTypes.Decimal)]
        public decimal? GlobalMarkup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
        public string Carrier { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text)]
        public string ShipviaId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text)]
        public string Shipvia { get; set; }
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
            addFilterToSelect("VendorId", "vendorid", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}

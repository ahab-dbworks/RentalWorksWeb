using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Vendor
{
    [FwSqlTable("vendorview")]
    public class VendorBrowseLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VendorId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorno", modeltype: FwDataTypes.Text)]
        public string VendorNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorfml", modeltype: FwDataTypes.Text)]
        public string VendorDisplayName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vendorclass", modeltype: FwDataTypes.Text)]
        public string VendorClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text)]
        public string DefaultRate { get; set; }
        ////------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text)]
        public string BillingCycleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiod", modeltype: FwDataTypes.Text)]
        public string BillingCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "prcontactid", modeltype: FwDataTypes.Text)]
        public string PrimaryContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prcontact", modeltype: FwDataTypes.Text)]
        public string PrimaryContact { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prphone", modeltype: FwDataTypes.Text)]
        public string PrimaryContactPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prext", modeltype: FwDataTypes.Text)]
        public string PrimaryContactExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("Insurance", "insurance", select, request);
            addFilterToSelect("Freight", "freight", select, request);
            addFilterToSelect("SubRent", "subrent", select, request);
            addFilterToSelect("SubSale", "subsales", select, request);
            addFilterToSelect("SubMisc", "submisc", select, request);
            addFilterToSelect("SubLabor", "sublabor", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}

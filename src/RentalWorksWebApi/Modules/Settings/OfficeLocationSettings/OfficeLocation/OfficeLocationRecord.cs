using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.OfficeLocationSettings.OfficeLocation
{
    [FwSqlTable("location")]
    public class OfficeLocationRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string LocationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text, maxlength: 4, required: true)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------        
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 60)]
        public string CompanyName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webaddress", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string WebAddress { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ratetype", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        public string RateType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationcolor", modeltype: FwDataTypes.OleToHtmlColor, sqltype: "int")]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultpoordertypeid", modeltype: FwDataTypes.Text)]
        public string DefaultPurchasePoTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "glprefix", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string GlPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glsuffix", modeltype: FwDataTypes.Text, maxlength: 10)]
        public string GlSuffix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "useprefix", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseNumberPrefix { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "prefix", modeltype: FwDataTypes.Text, maxlength: 2)]
        public string NumberPrefix { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "usereq", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseRequisitionNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samequoteno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSameNumberForQuoteAndOrder { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "samebatchno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseSameNumberForAllExportBatches { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedbyorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UserOrderNumberAndSuffixForInvoice { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoicesnumberedforhiatus", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? UseHInHiatusInvoiceNumbers { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string Zip { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------        
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}



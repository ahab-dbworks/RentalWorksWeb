﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.TaxOption
{
    [FwSqlTable("taxoption")]
    public class TaxOptionRecord: RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string TaxOptionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text, maxlength: 15, required: true)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availforsales", modeltype: FwDataTypes.Boolean)]
        public bool? AvailableForSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availforpurchase", modeltype: FwDataTypes.Boolean)]
        public bool? AvailableForPurchases { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text, maxlength: 1, required: true)]
        public string TaxCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxrule", modeltype: FwDataTypes.Text, maxlength: 1)]
        public string TaxRule { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxrental", modeltype: FwDataTypes.Boolean)]
        public bool? RentalExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxsales", modeltype: FwDataTypes.Boolean)]
        public bool? SalesExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.Decimal, precision: 6, scale: 4)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxlabor", modeltype: FwDataTypes.Boolean)]
        public bool? LaborExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontax", modeltype: FwDataTypes.Boolean)]
        public bool? TaxOnTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontaxaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string TaxOnTaxAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxaccountid1", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string TaxAccountId1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxaccountid2", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string TaxAccountId2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxitemcode", modeltype: FwDataTypes.Text, maxlength: 50)]
        public string QuickBooksTaxItemCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxitemdescription", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string QuickBooksTaxItemDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxvendor", modeltype: FwDataTypes.Text, maxlength: 100)]
        public string QuickBooksTaxVendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxgroup", modeltype: FwDataTypes.Boolean)]
        public bool? QuickBooksTaxGroup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "gstexportcode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string GstExportCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pstexportcode", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string PstExportCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

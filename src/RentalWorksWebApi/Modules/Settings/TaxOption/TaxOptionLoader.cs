﻿using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.TaxOption
{
    [FwSqlTable("taxoptionview")]
    public class TaxOptionLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string TaxOptionId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoption", modeltype: FwDataTypes.Text, required: true)]
        public string TaxOption { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availforsales", modeltype: FwDataTypes.Boolean)]
        public bool AvailableForSales { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "availforpurchase", modeltype: FwDataTypes.Boolean)]
        public bool AvailableForPurchases { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text, required: true)]
        public string TaxCountry { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxrule", modeltype: FwDataTypes.Text)]
        public string TaxRule { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxrental", modeltype: FwDataTypes.Boolean)]
        public bool RentalExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxsales", modeltype: FwDataTypes.Boolean)]
        public bool SalesExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.Decimal)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "donottaxlabor", modeltype: FwDataTypes.Boolean)]
        public bool LaborExempt { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontax", modeltype: FwDataTypes.Boolean)]
        public bool TaxOnTax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontaxaccountid", modeltype: FwDataTypes.Text)]
        public string TaxOnTaxAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontaxglno", modeltype: FwDataTypes.Text)]
        public string TaxOnTaxAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxontaxglacctdesc", modeltype: FwDataTypes.Text)]
        public string TaxOnTaxAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxaccountid1", modeltype: FwDataTypes.Text)]
        public string TaxAccountId1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxglno1", modeltype: FwDataTypes.Text)]
        public string TaxAccountNo1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxglacctdesc1", modeltype: FwDataTypes.Text)]
        public string TaxAccountDescription1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxaccountid2", modeltype: FwDataTypes.Text)]
        public string TaxAccountId2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxglno2", modeltype: FwDataTypes.Text)]
        public string TaxAccountNo2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxglacctdesc2", modeltype: FwDataTypes.Text)]
        public string TaxAccountDescription2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxitemcode", modeltype: FwDataTypes.Text)]
        public string QuickBooksTaxItemCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxitemdescription", modeltype: FwDataTypes.Text)]
        public string QuickBooksTaxItemDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxvendor", modeltype: FwDataTypes.Text)]
        public string QuickBooksTaxVendor { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxgroup", modeltype: FwDataTypes.Boolean)]
        public bool QuickBooksTaxGroup { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "gstexportcode", modeltype: FwDataTypes.Text)]
        public string GstExportCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pstexportcode", modeltype: FwDataTypes.Text)]
        public string PstExportCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}

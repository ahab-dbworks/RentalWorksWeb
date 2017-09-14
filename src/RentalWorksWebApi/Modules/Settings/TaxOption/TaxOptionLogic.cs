using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.TaxOption
{
    public class TaxOptionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        TaxOptionRecord taxOption = new TaxOptionRecord();
        TaxOptionLoader taxOptionLoader = new TaxOptionLoader();

        public TaxOptionLogic()
        {
            dataRecords.Add(taxOption);
            dataLoader = taxOptionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string TaxOptionId { get { return taxOption.TaxOptionId; } set { taxOption.TaxOptionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string TaxOption { get { return taxOption.TaxOption; } set { taxOption.TaxOption = value; } }
        public bool AvailableForSales { get { return taxOption.AvailableForSales; } set { taxOption.AvailableForSales = value; } }
        public bool AvailableForPurchases { get { return taxOption.AvailableForPurchases; } set { taxOption.AvailableForPurchases = value; } }
        public string TaxCountry { get { return taxOption.TaxCountry; } set { taxOption.TaxCountry = value; } }
        public string TaxRule { get { return taxOption.TaxRule; } set { taxOption.TaxRule = value; } }
        public decimal RentalTaxRate1 { get { return taxOption.RentalTaxRate1; } set { taxOption.RentalTaxRate1 = value; } }
        public decimal RentalTaxRate2 { get { return taxOption.RentalTaxRate2; } set { taxOption.RentalTaxRate2 = value; } }
        public bool RentalExempt { get { return taxOption.RentalExempt; } set { taxOption.RentalExempt = value; } }
        public decimal SalesTaxRate1 { get { return taxOption.SalesTaxRate1; } set { taxOption.SalesTaxRate1 = value; } }
        public decimal SalesTaxRate2 { get { return taxOption.SalesTaxRate2; } set { taxOption.SalesTaxRate2 = value; } }
        public bool SalesExempt { get { return taxOption.SalesExempt; } set { taxOption.SalesExempt = value; } }
        public decimal LaborTaxRate1 { get { return taxOption.LaborTaxRate1; } set { taxOption.LaborTaxRate1 = value; } }
        public decimal LaborTaxRate2 { get { return taxOption.LaborTaxRate2; } set { taxOption.LaborTaxRate2 = value; } }
        public bool LaborExempt { get { return taxOption.LaborExempt; } set { taxOption.LaborExempt = value; } }
        public bool TaxOnTax { get { return taxOption.TaxOnTax; } set { taxOption.TaxOnTax = value; } }
        public string TaxOnTaxAccountId { get { return taxOption.TaxOnTaxAccountId; } set { taxOption.TaxOnTaxAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOnTaxAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOnTaxAccountDescription { get; set; }
        public string TaxAccountId1 { get { return taxOption.TaxAccountId1; } set { taxOption.TaxAccountId1 = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxAccountNo1 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxAccountDescription1 { get; set; }
        public string TaxAccountId2 { get { return taxOption.TaxAccountId2; } set { taxOption.TaxAccountId2 = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxAccountNo2 { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxAccountDescription2 { get; set; }
        public string QuickBooksTaxItemCode { get { return taxOption.QuickBooksTaxItemCode; } set { taxOption.QuickBooksTaxItemCode = value; } }
        public string QuickBooksTaxItemDescription { get { return taxOption.QuickBooksTaxItemDescription; } set { taxOption.QuickBooksTaxItemDescription = value; } }
        public string QuickBooksTaxVendor { get { return taxOption.QuickBooksTaxVendor; } set { taxOption.QuickBooksTaxVendor = value; } }
        public bool QuickBooksTaxGroup { get { return taxOption.QuickBooksTaxGroup; } set { taxOption.QuickBooksTaxGroup = value; } }
        public string GstExportCode { get { return taxOption.GstExportCode; } set { taxOption.GstExportCode = value; } }
        public string PstExportCode { get { return taxOption.PstExportCode; } set { taxOption.PstExportCode = value; } }
        public string DateStamp { get { return taxOption.DateStamp; } set { taxOption.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
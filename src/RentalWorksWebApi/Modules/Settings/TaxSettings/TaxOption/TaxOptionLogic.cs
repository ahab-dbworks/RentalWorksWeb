using FwStandard.AppManager;
using Microsoft.AspNetCore.Mvc;
using WebApi.Logic;
using System.Threading.Tasks;
using FwStandard.BusinessLogic;
using System.Reflection;
using WebApi;

namespace WebApi.Modules.Settings.TaxSettings.TaxOption
{
    [FwLogic(Id:"4yhNNN27osykE")]
    public class TaxOptionLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"CWdBF74y309ho", IsPrimaryKey:true)]
        public string TaxOptionId { get { return taxOption.TaxOptionId; } set { taxOption.TaxOptionId = value; } }

        [FwLogicProperty(Id:"CWdBF74y309ho", IsRecordTitle:true)]
        public string TaxOption { get { return taxOption.TaxOption; } set { taxOption.TaxOption = value; } }

        [FwLogicProperty(Id:"tjvUKVsk9k3g")]
        public bool? AvailableForSales { get { return taxOption.AvailableForSales; } set { taxOption.AvailableForSales = value; } }

        [FwLogicProperty(Id:"nZHGwOWy6KzK")]
        public bool? AvailableForPurchases { get { return taxOption.AvailableForPurchases; } set { taxOption.AvailableForPurchases = value; } }

        [FwLogicProperty(Id:"T0qmvcp8wGac")]
        public string TaxCountry { get { return taxOption.TaxCountry; } set { taxOption.TaxCountry = value; } }

        [FwLogicProperty(Id:"8YvUWvtvJuPB")]
        public string TaxRule { get { return taxOption.TaxRule; } set { taxOption.TaxRule = value; } }

        [FwLogicProperty(Id:"rW8xZCjafOUe")]
        public decimal? RentalTaxRate1 { get { return taxOption.RentalTaxRate1; } set { taxOption.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id:"UGgjV3Ggndqp")]
        public decimal? RentalTaxRate2 { get { return taxOption.RentalTaxRate2; } set { taxOption.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id:"dBGD91qvsA0J")]
        public bool? RentalExempt { get { return taxOption.RentalExempt; } set { taxOption.RentalExempt = value; } }

        [FwLogicProperty(Id:"EjkJyV94zLpQ")]
        public decimal? SalesTaxRate1 { get { return taxOption.SalesTaxRate1; } set { taxOption.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id:"4MtQ4wIKj6qM")]
        public decimal? SalesTaxRate2 { get { return taxOption.SalesTaxRate2; } set { taxOption.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id:"hFNqXVmKoUWv")]
        public bool? SalesExempt { get { return taxOption.SalesExempt; } set { taxOption.SalesExempt = value; } }

        [FwLogicProperty(Id:"UIzUjlNa4aTS")]
        public decimal? LaborTaxRate1 { get { return taxOption.LaborTaxRate1; } set { taxOption.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id:"DkE5o0yGplfD")]
        public decimal? LaborTaxRate2 { get { return taxOption.LaborTaxRate2; } set { taxOption.LaborTaxRate2 = value; } }

        [FwLogicProperty(Id:"SPHvVmh9crXo")]
        public bool? LaborExempt { get { return taxOption.LaborExempt; } set { taxOption.LaborExempt = value; } }

        [FwLogicProperty(Id:"S4ZjoYCpTrZa")]
        public bool? TaxOnTax { get { return taxOption.TaxOnTax; } set { taxOption.TaxOnTax = value; } }

        [FwLogicProperty(Id:"sdaqWyx4qhfu")]
        public string TaxOnTaxAccountId { get { return taxOption.TaxOnTaxAccountId; } set { taxOption.TaxOnTaxAccountId = value; } }

        [FwLogicProperty(Id:"QyWJ4qOHIbKPY", IsReadOnly:true)]
        public string TaxOnTaxAccountNo { get; set; }

        [FwLogicProperty(Id:"QyWJ4qOHIbKPY", IsReadOnly:true)]
        public string TaxOnTaxAccountDescription { get; set; }

        [FwLogicProperty(Id:"mGRHKmnmsodE")]
        public string TaxAccountId1 { get { return taxOption.TaxAccountId1; } set { taxOption.TaxAccountId1 = value; } }

        [FwLogicProperty(Id:"QIwNSoWvtPVwf", IsReadOnly:true)]
        public string TaxAccountNo1 { get; set; }

        [FwLogicProperty(Id:"yXODHhQP59wib", IsReadOnly:true)]
        public string TaxAccountDescription1 { get; set; }

        [FwLogicProperty(Id:"CXLwu7Giv0z0")]
        public string TaxAccountId2 { get { return taxOption.TaxAccountId2; } set { taxOption.TaxAccountId2 = value; } }

        [FwLogicProperty(Id:"Yd1RrcKIzkBQC", IsReadOnly:true)]
        public string TaxAccountNo2 { get; set; }

        [FwLogicProperty(Id:"iw6oDON0zVNXK", IsReadOnly:true)]
        public string TaxAccountDescription2 { get; set; }

        [FwLogicProperty(Id:"tf9SbJJIvPqi")]
        public string QuickBooksTaxItemCode { get { return taxOption.QuickBooksTaxItemCode; } set { taxOption.QuickBooksTaxItemCode = value; } }

        [FwLogicProperty(Id:"DQjBxPHxkLqr")]
        public string QuickBooksTaxItemDescription { get { return taxOption.QuickBooksTaxItemDescription; } set { taxOption.QuickBooksTaxItemDescription = value; } }

        [FwLogicProperty(Id:"rNRgUZ4oTpfC")]
        public string QuickBooksTaxVendor { get { return taxOption.QuickBooksTaxVendor; } set { taxOption.QuickBooksTaxVendor = value; } }

        [FwLogicProperty(Id:"A9Nl3kz1nQvK")]
        public bool? QuickBooksTaxGroup { get { return taxOption.QuickBooksTaxGroup; } set { taxOption.QuickBooksTaxGroup = value; } }

        [FwLogicProperty(Id:"5SSIB1EtFRiV")]
        public string GstExportCode { get { return taxOption.GstExportCode; } set { taxOption.GstExportCode = value; } }

        [FwLogicProperty(Id:"jQ0YgL93DIm3")]
        public string PstExportCode { get { return taxOption.PstExportCode; } set { taxOption.PstExportCode = value; } }

        [FwLogicProperty(Id:"jM8hPxA7WL5r")]
        public bool? Inactive { get { return taxOption.Inactive; } set { taxOption.Inactive = value; } }

        [FwLogicProperty(Id:"hRw2lhMqWjqg")]
        public string DateStamp { get { return taxOption.DateStamp; } set { taxOption.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(TaxOptionLogic).GetProperty(nameof(TaxOptionLogic.TaxCountry));
                string[] acceptableValues = { RwConstants.TAX_COUNTRY_USA, RwConstants.TAX_COUNTRY_CANADA };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public bool ForceRates()
        {
            bool success = taxOption.ForceRatesAsync().Result;
            return success;
        }
        //------------------------------------------------------------------------------------
    }
}

using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.CompanyTaxOption
{
    [FwLogic(Id:"xRpX57VC8ejQ")]
    public class CompanyTaxOptionLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DealLocationRecord dealLocation = new DealLocationRecord();
        CompanyTaxOptionLoader companyTaxOptionLoader = new CompanyTaxOptionLoader();
        public CompanyTaxOptionLogic()
        {
            dataRecords.Add(dealLocation);
            dataLoader = companyTaxOptionLoader;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"4sEmvebo4HYt", IsPrimaryKey:true)]
        public int? Id { get { return dealLocation.Id; } set { dealLocation.Id = value; } }

        [FwLogicProperty(Id:"RsGFhHbhQ2a0", IsPrimaryKey:true, IsPrimaryKeyOptional: true)]
        public string InternalChar { get { return dealLocation.InternalChar; } set { dealLocation.InternalChar = value; } }

        [FwLogicProperty(Id:"54gaHwbVfPx")]
        public string CompanyId { get { return dealLocation.CompanyId; } set { dealLocation.CompanyId = value; } }

        [FwLogicProperty(Id:"kzCHnDTWBT9")]
        public string LocationId { get { return dealLocation.LocationId; } set { dealLocation.LocationId = value; } }

        [FwLogicProperty(Id:"WzdYTFjDyq8M", IsRecordTitle:true, IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"mLhOtQdg8dU")]
        public string TaxOptionId { get { return dealLocation.TaxOptionId; } set { dealLocation.TaxOptionId = value; } }

        [FwLogicProperty(Id:"ajSRpSTVvKG8", IsRecordTitle:true, IsReadOnly:true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id:"2fcmryKXd03J", IsReadOnly:true)]
        public string TaxCountry { get; set; }

        [FwLogicProperty(Id:"mb3O98t4JcoY", IsReadOnly:true)]
        public string TaxRule { get; set; }

        [FwLogicProperty(Id:"0HIewOodviyg", IsReadOnly:true)]
        public decimal? RentalTaxRate1 { get; set; }

        [FwLogicProperty(Id:"XVcdzuuSpFYZ", IsReadOnly:true)]
        public decimal? RentalTaxRate2 { get; set; }

        [FwLogicProperty(Id:"sKP7i7P6qFr7", IsReadOnly:true)]
        public bool? RentalExempt { get; set; }

        [FwLogicProperty(Id: "EGi468fgmzJ6M", IsReadOnly: true)]
        public string RentalTaxDisplay { get; set; }

        [FwLogicProperty(Id:"eKeiHJSzgrrg", IsReadOnly:true)]
        public decimal? SalesTaxRate1 { get; set; }

        [FwLogicProperty(Id:"ttSFy2sK14ZL", IsReadOnly:true)]
        public decimal? SalesTaxRate2 { get; set; }

        [FwLogicProperty(Id:"r1w9NybUhGO8", IsReadOnly:true)]
        public bool? SalesExempt { get; set; }

        [FwLogicProperty(Id: "UDhzbPEDBv0vC", IsReadOnly: true)]
        public string SalesTaxDisplay { get; set; }

        [FwLogicProperty(Id:"LFjmIzmsjAQ0", IsReadOnly:true)]
        public decimal? LaborTaxRate1 { get; set; }

        [FwLogicProperty(Id:"W1LX1fQK3jMd", IsReadOnly:true)]
        public decimal? LaborTaxRate2 { get; set; }

        [FwLogicProperty(Id:"EffEZhvjbY9O", IsReadOnly:true)]
        public bool? LaborExempt { get; set; }

        [FwLogicProperty(Id: "FqwhcdBcv6A60", IsReadOnly: true)]
        public string LaborTaxDisplay { get; set; }

        [FwLogicProperty(Id:"FsMCJPisgLs")]
        public string DateStamp { get { return dealLocation.DateStamp; } set { dealLocation.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}

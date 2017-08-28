using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.CompanyTaxOption
{
    public class CompanyTaxOptionLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string Id { get { return dealLocation.Id; } set { dealLocation.Id = value; } }
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string InternalChar { get { return dealLocation.InternalChar; } set { dealLocation.InternalChar = value; } }
        public string CompanyId { get { return dealLocation.CompanyId; } set { dealLocation.CompanyId = value; } }
        public string LocationId { get { return dealLocation.LocationId; } set { dealLocation.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Location { get; set; }
        public string TaxOptionId { get { return dealLocation.TaxOptionId; } set { dealLocation.TaxOptionId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string TaxOption { get; set; }
        public string DateStamp { get { return dealLocation.DateStamp; } set { dealLocation.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}

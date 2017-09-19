using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Home.CompanyTaxResale
{
    public class CompanyTaxResaleLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        CompanyTaxResaleRecord companyTaxResale = new CompanyTaxResaleRecord();
        CompanyTaxResaleLoader companyTaxResaleLoader = new CompanyTaxResaleLoader();
        public CompanyTaxResaleLogic()
        {
            dataRecords.Add(companyTaxResale);
            dataLoader = companyTaxResaleLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CompanyTaxResaleId { get { return companyTaxResale.CompanyTaxResaleId; } set { companyTaxResale.CompanyTaxResaleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyId { get { return companyTaxResale.CompanyId; } set { companyTaxResale.CompanyId = value; } }
        public string StateId { get { return companyTaxResale.StateId; } set { companyTaxResale.StateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string StateCode { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string ResaleNumber { get { return companyTaxResale.ResaleNumber; } set { companyTaxResale.ResaleNumber = value; } }
        //------------------------------------------------------------------------------------
    }
}
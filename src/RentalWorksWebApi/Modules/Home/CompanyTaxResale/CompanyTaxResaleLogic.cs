using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Home.CompanyTaxResale
{
    [FwLogic(Id:"rseHWd4aXWQP")]
    public class CompanyTaxResaleLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"2mMkJvfZwaEc", IsPrimaryKey:true)]
        public string CompanyTaxResaleId { get { return companyTaxResale.CompanyTaxResaleId; } set { companyTaxResale.CompanyTaxResaleId = value; } }

        [FwLogicProperty(Id:"L74qDtOHhnW8", IsReadOnly:true)]
        public string CompanyId { get { return companyTaxResale.CompanyId; } set { companyTaxResale.CompanyId = value; } }

        [FwLogicProperty(Id:"XpUB44N2ljp")]
        public string StateId { get { return companyTaxResale.StateId; } set { companyTaxResale.StateId = value; } }

        [FwLogicProperty(Id:"yVIYMgLVqMHM", IsReadOnly:true)]
        public string StateCode { get; set; }

        [FwLogicProperty(Id:"dpJlIS39M50s", IsRecordTitle:true)]
        public string ResaleNumber { get { return companyTaxResale.ResaleNumber; } set { companyTaxResale.ResaleNumber = value; } }

        //------------------------------------------------------------------------------------
    }
}

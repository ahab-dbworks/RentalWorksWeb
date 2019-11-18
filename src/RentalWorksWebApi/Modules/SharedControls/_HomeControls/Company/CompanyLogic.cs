using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.Company
{
    [FwLogic(Id:"pZaHjHLzMJNF")]
    public class CompanyLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CompanyLoader companyLoader = new CompanyLoader();
        public CompanyLogic()
        {
            dataLoader = companyLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"DhHKJgrYVy6E", IsPrimaryKey:true, IsReadOnly:true)]
        public string CompanyId { get; set; }

        [FwLogicProperty(Id:"DhHKJgrYVy6E", IsReadOnly:true)]
        public string CompanyNumber { get; set; }

        [FwLogicProperty(Id:"DhHKJgrYVy6E", IsRecordTitle:true, IsReadOnly:true)]
        public string Company { get; set; }

        [FwLogicProperty(Id:"DhHKJgrYVy6E", IsReadOnly:true)]
        public string CompanyType { get; set; }

        [FwLogicProperty(Id:"DhHKJgrYVy6E", IsReadOnly:true)]
        public string CompanyTypeColor { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}

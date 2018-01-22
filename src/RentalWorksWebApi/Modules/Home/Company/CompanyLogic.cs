using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.Company
{
    public class CompanyLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CompanyLoader companyLoader = new CompanyLoader();
        public CompanyLogic()
        {
            dataLoader = companyLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true, isPrimaryKey: true)]
        public string CompanyId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Company { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CompanyTypeColor { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
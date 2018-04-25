using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.CustomModule
{
    public class CustomModuleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomModuleLoader customModuleLoader = new CustomModuleLoader();
        public CustomModuleLogic()
        {
            dataLoader = customModuleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string ModuleName { get; set; }
        //------------------------------------------------------------------------------------ 
     }
}
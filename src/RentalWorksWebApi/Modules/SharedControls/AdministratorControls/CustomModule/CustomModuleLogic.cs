using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.AdministratorControls.CustomModule
{
    [FwLogic(Id:"sq2JvMWQ7Gku")]
    public class CustomModuleLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CustomModuleLoader customModuleLoader = new CustomModuleLoader();
        public CustomModuleLogic()
        {
            dataLoader = customModuleLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"7HaUiDzsTkQ9", IsRecordTitle:true, IsReadOnly:true)]
        public string ModuleName { get; set; }

        //------------------------------------------------------------------------------------ 
     }
}

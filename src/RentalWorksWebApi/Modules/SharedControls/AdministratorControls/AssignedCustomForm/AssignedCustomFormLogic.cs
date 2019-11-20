using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.AdministratorControls.AssignedCustomForm
{
    [FwLogic(Id: "9fOOx8zYT2ZE")]
    public class AssignedCustomFormLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AssignedCustomFormLoader assignedCustomFormLoader = new AssignedCustomFormLoader();
        public AssignedCustomFormLogic()
        {
            dataLoader = assignedCustomFormLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "bwdSefmyhyBNt", IsReadOnly: true)]
        public string WebFormId { get; set; }
        [FwLogicProperty(Id: "Lt8kUdkWMPsO", IsReadOnly: true)]
        public string BaseForm { get; set; }
        [FwLogicProperty(Id: "OmurgKWOq31v", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "84fRfwJ5PR2b9", IsReadOnly: true)]
        public string Html { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}

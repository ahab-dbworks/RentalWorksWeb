using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Branches.RentalWorksWeb..Modules.Administrator
{
    public class IntegrationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public IntegrationMenu() : base("{518B038E-F22A-4B23-AA47-F4F56709ADC3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{FB20D01C-E35E-4762-9A49-AAF809BEB344}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{5012FD71-D335-4A8C-8912-FE088B6D46F6}", nodeForm.Id);
                    //tree.AddSubMenu("{739C0F47-2C37-49A6-808E-61D830A4D1C2}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{F12FD632-6C66-43EB-9F00-5588BBF143FB}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
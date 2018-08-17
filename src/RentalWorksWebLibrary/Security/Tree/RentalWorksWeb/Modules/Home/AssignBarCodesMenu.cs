using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class AssignBarCodesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AssignBarCodesMenu() : base("{4B9C17DE-7FC0-4C33-B953-26FC90F32EA0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{1574E7CD-718C-49BE-8066-B3EA6612F3A6}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{6FD45C93-8FBB-4FEA-961B-F37FB1AAF233}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
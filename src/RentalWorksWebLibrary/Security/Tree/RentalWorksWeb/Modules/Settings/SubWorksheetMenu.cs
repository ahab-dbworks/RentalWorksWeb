using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SubWorksheetMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubWorksheetMenu() : base("{F24BDA2F-B37C-45C1-B08E-588D02D50B7C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{9F502B62-FDFE-4724-B5C9-672EB5917C7A}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FCB95E31-AC5F-4FF4-80B1-8041D0EC50A2}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
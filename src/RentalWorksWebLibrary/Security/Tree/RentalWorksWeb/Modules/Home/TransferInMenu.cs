using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class TransferInMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TransferInMenu() : base("{D9F487C2-5DC1-45DF-88A2-42A05679376C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{F594AF9F-74A5-4EE8-B92C-981655C2CB04}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{CE49DDDC-03DE-4E49-8F3E-32302D52DE57}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class TransferOutMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TransferOutMenu() : base("{91E79272-C1CF-4678-A28F-B716907D060C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{0078732B-5708-4698-B20A-9CD6F34A068D}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{39227DD7-B211-4755-AE67-D5C411D284D0}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
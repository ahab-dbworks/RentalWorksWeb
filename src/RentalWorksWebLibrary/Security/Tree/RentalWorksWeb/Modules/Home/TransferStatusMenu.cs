using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class TransferStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TransferStatusMenu() : base("{58D5D354-136E-40D5-9675-B74FD7807D6F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A7C41240-4E07-4030-9C65-7C08B2416D96}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{370978F9-236F-4782-915A-B85FC6E30971}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
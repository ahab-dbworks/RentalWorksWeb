using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class ExchangeContainerItemMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeContainerItemMenu() : base("{6B8D5B55-2B79-4569-B0B8-97920295EEDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A2493E85-ACDA-4231-B30D-FF215B62FCBD}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2825E3E8-E770-4B41-8CF7-EEEAB69D1742}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
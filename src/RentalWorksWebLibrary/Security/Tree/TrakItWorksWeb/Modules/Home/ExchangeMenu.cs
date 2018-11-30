using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class ExchangeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeMenu() : base("{F9012ABC-B97E-433B-A604-F1DADFD6D7B7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{9D1AEEEC-52AD-4AAE-8C20-8BAE6A6571E0}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{FAB65378-FF61-4DB6-841B-824ECC169000}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
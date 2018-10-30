using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakItWorksWeb.Modules.Settings
{
    public class ExchangeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ExchangeMenu() : base("{76A62932-CBBA-403E-8BF2-0C2283BBAD8D}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{388D6E84-BC78-46B4-9BFB-1C29984CE541}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{A55816DB-A398-4899-A278-F587ECB5FD5A}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class CountQuantityInventoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CountQuantityInventoryMenu() : base("{0A02B28D-C025-4579-993B-860832F8837F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{E0C59996-4F77-4B96-86F0-B0BD17437B50}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4B6F1903-DFA7-44C2-AE45-25DE9B450C92}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
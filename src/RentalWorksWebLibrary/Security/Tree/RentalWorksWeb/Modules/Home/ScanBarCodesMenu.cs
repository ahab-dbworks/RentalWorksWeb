using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class ScanBarCodesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ScanBarCodesMenu() : base("{C8683D4F-70C1-40CD-967A-0891B14664E8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{C676805F-F663-467D-9336-68B5298298F2}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{EC9E7E55-31B4-4A90-8D1B-36373D319B57}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
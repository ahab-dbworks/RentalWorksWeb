using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ReceiptProcessBatchMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReceiptProcessBatchMenu() : base("{0BB9B45C-57FA-47E1-BC02-39CEE720792C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{1E03BAE6-2B1F-45D0-931B-C74F1FA392A9}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2FE90A15-942E-4A12-BFA8-9B85153C5D95}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
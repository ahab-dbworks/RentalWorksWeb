using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InvoiceProcessBatchMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceProcessBatchMenu() : base("{5DB3FB9C-6F86-4696-867A-9B99AB0D6647}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Form
            var nodeForm = tree.AddForm("{A8879A01-2AEF-44BC-85DC-7E3096FDE067}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{97E5D898-F21E-4849-BE99-8AEE3F07EC14}", nodeForm.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
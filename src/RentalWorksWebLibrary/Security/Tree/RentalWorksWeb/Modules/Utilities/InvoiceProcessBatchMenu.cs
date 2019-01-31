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
            var nodeFormSubMenu = tree.AddSubMenu("{02576433-1741-4276-9B98-5FBC00E14DDB}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{4B1EB8C1-8BFA-4DD4-B249-B84B2C2DABAE}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Export Settings", "{28D5F4EF-9A60-4D7F-B294-4B302B88413F}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
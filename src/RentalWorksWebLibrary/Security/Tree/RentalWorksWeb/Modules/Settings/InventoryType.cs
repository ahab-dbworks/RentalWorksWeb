using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class InventoryTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryTypeMenu() : base("{D62E0D20-AFF4-46A7-A767-FF32F6EC4617}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C13FBFC1-33B4-484B-9A05-423D2F3B3665}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{92673FE8-5182-4E4A-ACAA-92912F600715}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{9969D9E2-EE7C-4DCA-A0BC-4E419B7803A2}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5D0762CC-39B6-4EB2-8565-57E9F7B51091}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2997E4B1-8377-4D25-82CD-130E840C9988}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{75358FC5-FBE0-49A8-8AFB-E73903A88D00}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9D73A6A0-8CC2-41D4-80EB-B64D0B7204D1}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E813C31A-64B4-4A97-B3F3-EE49B6B52808}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4AE80C6B-0C1E-4BC2-B7AE-894EBD0619FC}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{62CEBD16-D290-46EB-B7FB-E2079FADAB56}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{3C5FEE5E-2548-4F11-B5CD-213AD8D1DF54}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{2761B373-67BD-4938-B9AF-1C0739AA443E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{91969F01-3179-4C72-8007-E9175CA7DAF5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
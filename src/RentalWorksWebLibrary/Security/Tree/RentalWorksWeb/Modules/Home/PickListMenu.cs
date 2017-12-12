using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PickListMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PickListMenu() : base("{7B04E5D4-D079-4F3A-9CB0-844F293569ED}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{FCB3E82C-67DE-49CE-89A6-C9FA4E4B8D87}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{ECFFC00E-57A7-4913-930B-6F84E7FC35BE}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{1D5870B1-0F4A-4B20-B627-245D3AB2E54B}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{5EFB4142-55F0-4D48-88E3-A6413D2C9037}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A7FF09BB-F3A7-4D70-923A-CED02B413E05}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{17C78ADE-33E7-4CCC-BBC5-F38AC3507B72}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{217F9099-4C5F-4EBD-9D96-69B9BB0BE70B}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{CF591DED-CB90-46B3-BC82-490136175C4F}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{7B20077B-0183-497D-A318-A06958EC321C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{57C9AEB0-3665-49A1-9C16-5A4D21DDF672}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{7ED7A3E8-F825-44AB-B892-76A6DBA253EB}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8366DB71-62CB-456A-B188-5799A17E591D}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}
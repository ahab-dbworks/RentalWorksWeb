using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class PurchaseOrderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PurchaseOrderMenu() : base("{DA900327-CEAC-4CB0-9911-CAA2C67059C2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{22DD2C04-4765-4BD7-B561-BD8DCC2446B9}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{16A90348-153A-42FE-A7CC-593A5CA964B7}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{324B2B1C-D40C-429B-ABB5-6E51AEEBEC8F}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{4EDF1137-924A-48D1-8690-27B318EA75F7}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B6EF52E7-A7A7-4C91-A2E3-D352FB42890A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1D2B2CD9-06AB-419C-A93D-B5B683B0EF17}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{20E199B2-C5CD-45E0-AA33-52F2EC588E6A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5F0ABCB4-7948-45BC-9411-24ACEAEFAE58}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6BB621BA-A374-47AF-A5B2-4477D17FAF39}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{A41A9382-906A-411E-88AE-5D9C0BC7A621}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AEA628EA-CED5-44AA-A66A-DE1B2F6F8173}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{7EF9E16B-4CD8-4B38-AE2D-42654FC56925}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6059084E-8F1C-4AE7-900B-AAB72C3D3828}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{D46AF757-85BF-4859-AE83-56CD5C6ACE70}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Search", "{932A7318-3B62-4AA5-A0C5-904BB4BB9F17}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Receive From Vendor", "{B719A865-4B0A-4C60-A1D9-3614EC6D0515}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Return To Vendor", "{6D3E64B9-6B08-47B0-8F51-CFAE12651630}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Assign Bar Codes", "{D7722622-BBAE-4FE7-9DAD-1E2A4419CC3D}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
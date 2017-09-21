using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SalesCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesCategoryMenu() : base("{428619B5-ABDE-48C4-9B2F-CF6D2A3AC574}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{25F3A062-AB70-4A1F-8271-0ACA21DFA6B8}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{9213AE00-6F99-43A0-9629-C9C02D64BAD3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3B44C5CF-B44B-40BA-AE48-98DDE45E14CA}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{01CBBADC-AD94-4F6E-AD9D-C5EBC5B2065B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{177E6FB9-9AE5-4A4C-B5F9-FF4752D9E3D4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{14670DA6-5F97-4384-ACEA-0B066AF81ABF}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{A45B85A8-96CF-4694-AFB1-0F131F99E23D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{0DD54168-E640-4E6F-AC9B-328BCEE8A3EE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{2822C3AC-5B31-49FD-A61C-4AA1F02D49E4}", nodeBrowseMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
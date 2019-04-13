using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class POClassificationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POClassificationMenu() : base("{7291695A-4659-45B5-9086-8F6E66E2583B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{FD5E1F58-3512-41F0-A0B6-2F9DC7E53590}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8E48746D-B255-4C6E-A95A-ED4230D625C5}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{CC4B2DBC-537A-4B69-B1B3-7011D7E7222B}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{45456F3F-F23A-43AF-ACC9-6B0D6C17D9C5}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{36D610A6-2E85-471A-BA4B-5E9F4DD19717}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{1EA1D064-2BFE-4D55-8863-09FC0C5292BF}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BA1ACE01-21A0-4F7D-B8A3-B070C28461A2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F02EC0AA-46EA-4B26-9075-CD9B74B8C996}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{58CE506D-B476-485D-98FD-314E4AD00BB3}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{910D29D7-FDFE-4978-92AA-3EC213A1421F}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{A2E919CF-FC60-4CC5-933A-5EA821965441}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{696CCBD1-549D-4F88-9D16-1667F1EB3828}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{9E7E6FF5-1F67-4F76-B6F0-5376C8C3DCF6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

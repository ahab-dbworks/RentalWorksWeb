using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class MiscTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MiscTypeMenu() : base("{605641A3-55C4-4888-B6B3-60877F4BA314}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8686DC02-3ED3-443F-B7B7-D5B7B12E870B}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{AB6D7AAF-D3BA-43B3-816B-45D46612FFE8}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{3ED08A42-D57E-4185-886F-D6F902B0B575}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{85A92EDB-0339-4EEA-8725-F12C9DA31268}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{425404C8-4D89-4403-8249-177CCAECDCE8}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B0F26764-4468-4212-ADFC-96D3A9D2FA92}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{BDAE9BF6-721B-4495-AF44-D56D4D409607}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{48BE5855-BFE5-4F04-9CAA-56B2828377F1}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6C870A1C-2D89-42F0-A712-3581B975C1B0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{1A580C8C-BAC6-447F-A75B-A54FB8827FA5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F858ED74-0F76-4795-A56B-0657247E6B72}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{866FCD6E-9722-4B8E-AE30-E0C71B250398}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{BF03E2E5-3EF4-4B40-A04B-44D6BFB91F9E}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class FloorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public FloorGridMenu() : base("{472B5E4A-57BB-4DFB-AD6A-D0F71915124B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0567D5ED-2A54-41B2-B82B-0C2CC9170392}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{278E2917-6FC9-4D04-8FD1-4A2D621E1E5F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{83D3F84B-F5EB-4EC3-B16E-1ED063B02F25}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{313175E5-06EA-4272-ADD6-BBF96E3AF593}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{984186F5-0C1A-4D88-8916-021F93E04636}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{0FCBBD5A-2184-4731-BB3F-3E1F25A03506}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{1C16B572-B469-48E1-BC8D-F011E8B00027}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}
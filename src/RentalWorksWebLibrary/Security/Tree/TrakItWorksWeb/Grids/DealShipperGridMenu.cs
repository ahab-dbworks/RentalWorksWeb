using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class DealShipperGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealShipperGridMenu() : base("{DC46B97F-6664-4ED6-8D1C-6E0EA8B3BC38}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5D7F9002-6299-4F7F-AEA6-9B38429B16F1}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{64F90D79-16DD-470E-BBA4-1A3E990CDC6D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8DA3D1E8-F7EA-4658-A7CA-75C61337204E}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{46A18572-A1CD-46D6-9F0B-9508E25C2A4D}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{E62E9A1F-12C4-4A7B-A06B-9C9AD5BBDB01}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{FA6C0BD3-1755-46F7-9A4D-6C6AB27CC342}",    nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F4BB376F-1E3A-40F4-B948-5E1074BD3F52}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5E7A5E06-2116-4CD4-83A4-176C2A6CE9E6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
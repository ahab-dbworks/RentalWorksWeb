using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryAvailabilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAvailabilityGridMenu() : base("{548B5500-E8BD-4448-8B4C-2389D54DD803}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{11C6F0D0-D61C-484A-885C-35A0B3B07E9F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9BBD4171-B3DA-4B10-9C6A-261FAA22562C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1413AFC7-672E-40FF-8CC9-3D55742BF2E5}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6D0C0360-81F6-4B0E-90FE-B4123A61B1C3}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{5D130803-347D-4427-B530-FC4170CF3A1D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

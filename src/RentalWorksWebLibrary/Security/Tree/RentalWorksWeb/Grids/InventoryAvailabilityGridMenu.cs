using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryAvailabilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAvailabilityGridMenu() : base("{8241ACB4-9346-43D6-8D3C-B6567FAA0270}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5F781FBE-3CA4-45B6-860F-B58B602FF826}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6FD57A3A-35AF-46B1-A4AC-F9CC6ED67D8D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{3861E2BB-BB44-408B-8408-FF31FF49510F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{23399BD0-2A99-400B-B485-7D8429E7F3FC}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{C160E5CB-431F-4C70-B3B8-9E2A96A10EC1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
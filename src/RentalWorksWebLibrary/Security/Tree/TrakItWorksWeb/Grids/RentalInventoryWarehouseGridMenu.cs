using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class RentalInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryWarehouseGridMenu() : base("{9AC6FB16-BC42-42C3-91B7-9346D11CC405}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{7DA8E48E-CE91-4683-A078-B79C4834BBF3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{A70DFB63-6BAA-4C4C-8C96-249C0BC78047}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A6896911-198C-4606-822E-26258607E211}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B7AFF0C9-53E0-4605-B16D-48D8F662D60E}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{7585F523-44C5-40F0-B102-0151300A9F7D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

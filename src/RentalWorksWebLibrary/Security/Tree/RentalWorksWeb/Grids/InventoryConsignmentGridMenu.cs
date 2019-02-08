using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryConsignmentGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryConsignmentGridMenu() : base("{0D22AF5B-CF50-41EA-A8CC-D039C402E4CC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E8009EA8-54A9-4A60-B119-47AD41BA40E9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{62B4AA1D-7887-4FCC-B659-FE5F2B21D44D}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{84ADE440-F324-4434-9512-137987BED48F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{22CD5789-A0E4-4E9A-80D6-D4A6C3C6F7D8}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
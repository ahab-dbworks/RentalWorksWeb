using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryContainerGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryContainerGridMenu() : base("{494F7DD0-0D32-4FE0-B84A-BC7CD71CE9EC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9ABD5355-3FD4-430B-9E43-78678A7FB7D9}", MODULEID);
            tree.AddEditMenuBarButton("{15F089E9-0D51-400E-B7BB-135C5B983FF0}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
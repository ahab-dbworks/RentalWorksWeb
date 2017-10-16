using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryCompleteKitGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompleteKitGridMenu() : base("{797339C1-79C3-4FC0-82E4-7DA2FE150DDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CAF66991-4227-4D4B-841B-9403D7BEE78C}", MODULEID);
            tree.AddEditMenuBarButton("{4FC4A825-1430-4E5D-B631-12A4BCFBC992}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
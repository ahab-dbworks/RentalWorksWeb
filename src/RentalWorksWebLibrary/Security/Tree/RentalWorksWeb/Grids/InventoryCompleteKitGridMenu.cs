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
            tree.AddNewMenuBarButton("{1A7BB17B-AD19-4648-9E8E-1F5974DA3A30}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4CAB5C95-E093-4A94-BE94-A7753D24608C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Grids
{
    public class InventoryAttributeValueGridMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeValueGridMenu() : base("{D591CCE2-920C-440D-A6D7-6F4F21FC01B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CC68FF21-0CC4-49C2-BB7C-09DBCFC7EA7D}", MODULEID);
                tree.AddNewMenuBarButton("{27C327C2-69F4-459A-B8C0-7C051181564C}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{057DABBC-A3D4-4038-891A-96F90CE521B2}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{509BCEB8-D3C7-4959-B666-4A5BF7EF0CFE}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
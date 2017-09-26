using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeValueGridMenu() : base("{D591CCE2-920C-440D-A6D7-6F4F21FC01B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CC68FF21-0CC4-49C2-BB7C-09DBCFC7EA7D}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{ECAB5496-3B63-4F35-9227-CBF7B34115D0}", nodeGridMenuBar.Id);
                    //var nodeGridOptions = tree.AddSubMenuGroup("Options", "{604260A6-36AE-47D7-BCA3-DF6919F5717E}", nodeGridSubMenu.Id);
                tree.AddNewMenuBarButton("{27C327C2-69F4-459A-B8C0-7C051181564C}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{057DABBC-A3D4-4038-891A-96F90CE521B2}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{509BCEB8-D3C7-4959-B666-4A5BF7EF0CFE}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
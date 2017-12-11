using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryAttributeValueGridMenu() : base("{D591CCE2-920C-440D-A6D7-6F4F21FC01B8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CC68FF21-0CC4-49C2-BB7C-09DBCFC7EA7D}", MODULEID);
            tree.AddEditMenuBarButton("{057DABBC-A3D4-4038-891A-96F90CE521B2}",   nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{24284FA7-F471-4CBF-B5F7-5515A0B06609}",    nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{22C84626-E4E9-4563-BB6F-23C2E851DCB4}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
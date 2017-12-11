using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RateWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RateWarehouseGridMenu() : base("{2EC39399-B731-4B22-A5F3-1919A275AA56}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3E303010-127F-4A94-A0B4-F679C1A2C7FF}", MODULEID);
            tree.AddEditMenuBarButton("{37EAE3F8-2D14-482F-A0D6-9C98B7D74D9D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
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
            var nodeGridSubMenu = tree.AddSubMenu("{597A34AA-B7D2-4C0B-89D1-FF13CBEAB228}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{538FCD81-5225-470E-8A4E-1072369F4D4F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CECCBD8D-FD35-47EE-83AC-925F916E63C2}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{37EAE3F8-2D14-482F-A0D6-9C98B7D74D9D}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
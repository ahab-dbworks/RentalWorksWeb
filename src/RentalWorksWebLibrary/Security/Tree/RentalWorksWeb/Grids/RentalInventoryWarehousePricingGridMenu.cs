using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RentalInventoryWarehousePricingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryWarehousePricingGridMenu() : base("{B6E2330A-8116-43B7-BCDE-BD8630F4E3D8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{BFB71D6C-C39D-4D8C-BDAC-0ED8E512F6A2}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{AF9A5755-ACF9-428B-B7ED-7191962799F1}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E47EE9AD-7CE3-43FA-BD8B-D8FFC8952CBD}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2696626E-6F29-49E3-8EA4-DE831D4DBAE6}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E410826E-89EF-40EC-8894-5364B0A710AA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
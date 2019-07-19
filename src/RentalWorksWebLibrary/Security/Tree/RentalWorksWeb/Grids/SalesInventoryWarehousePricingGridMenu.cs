using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SalesInventoryWarehousePricingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SalesInventoryWarehousePricingGridMenu() : base("{9D1C2BA3-9D4F-4871-A7CD-5789D80A8E35}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{914FE29A-5F62-40EE-BAF8-83200E5E3587}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{AA957776-1CB2-4A5A-8420-B99589E07549}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{85978318-EFD7-4F92-AB13-D07F40525409}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6A624283-6055-47C4-B719-9DB6B4F091FB}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{C385B2D2-69FE-4965-9934-9F4364FF04EE}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
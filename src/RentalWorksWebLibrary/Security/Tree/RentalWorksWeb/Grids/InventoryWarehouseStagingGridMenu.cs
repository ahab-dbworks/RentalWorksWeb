using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryWarehouseStagingGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryWarehouseStagingGridMenu() : base("{3D9F7C07-4B47-4E4C-B573-331D694B979E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{636CD191-B0D4-4033-8817-64B828F0BEA2}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{18168939-B360-4585-A820-D45CEA55C6FE}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{52657A00-B32A-4A5F-A8FA-AF37E334E640}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CA5FACDC-5BEF-4975-BB13-5DC67364989E}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{27F85CF1-4173-4FCF-810F-42C926C35E26}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
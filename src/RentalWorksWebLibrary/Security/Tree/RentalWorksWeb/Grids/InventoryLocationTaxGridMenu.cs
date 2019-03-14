using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryLocationTaxGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryLocationTaxGridMenu() : base("{7DDD2E10-5A1E-4FE9-BBA5-FDBE99DF04F6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F3EEBCAC-EC1E-4B22-A6B8-625EA44839BC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{2C0AF4B4-3DD8-422D-82D8-B761EC67CE27}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{C151A88C-A5F2-4C02-ABA3-CD9C937A2AA9}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A94AB976-F210-425C-8787-50477EFFC792}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{B29C1E3E-B843-424B-BEDC-984DFBBED7D8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
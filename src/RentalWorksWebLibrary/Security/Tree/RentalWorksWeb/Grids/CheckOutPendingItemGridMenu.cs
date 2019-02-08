using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CheckOutPendingItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckOutPendingItemGridMenu() : base("{28DA22B8-D429-4751-B97D-8210D78C9402}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5BFDD131-A05F-4106-9153-8B993DFC43D4}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6C380F3F-978D-4248-B4B8-B68D820BB760}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B8ABEC83-CACB-4C9B-B6AC-4079EBF5513D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E1F56D9B-4D26-4BA4-922A-3473FFB12C08}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POReturnItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReturnItemGridMenu() : base("{10CF4A1A-3F85-4A8C-A4D7-ACEC1DB12CFC}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{13EA730D-54E9-4DBD-8C8B-B5DEBAA788BD}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{26A2AE6C-827C-466F-97B3-B944954E6557}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{0D5E2BAD-182D-4C21-BD24-42A6E3670BE4}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9B0EEF5C-9466-42FD-AAC5-689B8ADEBA95}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
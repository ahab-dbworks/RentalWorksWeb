using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckInSwapGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInSwapGrid() : base("{CDEEA7D1-3738-4BD6-BBDC-75BD044DFE56}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{2677F1A3-0E04-4CC3-A798-4C309FEA4D41}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{BA591E88-C952-4CF6-81D3-CFD9BF304C15}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F57DAC15-2CF9-4C16-A096-88E281C3F4C8}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{552DB94C-9777-4F75-B2AC-5C2C3187344F}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


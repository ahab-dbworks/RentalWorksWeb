using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInQuantityItemsGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInQuantityItemsGrid() : base("{2D2D0746-D66E-476E-9750-C11BA93A20C9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FEA467EB-40AE-4E44-9EA7-7916B86A1CA8}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CB70D523-69DC-4091-A11B-0C028F486C4B}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{3DD148AA-A7EA-4249-B847-AB6B3292B0E0}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{93789365-5145-40F0-9144-7BCB784AD59D}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

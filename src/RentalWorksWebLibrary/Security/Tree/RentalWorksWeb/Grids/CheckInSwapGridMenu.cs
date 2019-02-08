using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckInSwapGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInSwapGrid() : base("{47563A6D-1B0A-43C2-AE0E-8EF7AEB5D13B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5050EFC5-28A3-4D57-85D7-D7D6E52656E8}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{98D67DA9-3ACD-406B-AB0D-8C1EF0200802}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4627A7F6-C499-45B1-AC7C-44887B777E06}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{52E88C40-8F47-4232-BFAD-FF59636301D4}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

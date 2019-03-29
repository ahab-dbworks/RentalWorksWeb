using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckedOutItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedOutItemGrid() : base("{A65E700E-486D-41DE-BBE1-485FCBF1E5A3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{495A4448-292F-4ECE-AEE9-A8FDEA678B9C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DD70C47B-D526-431F-85DE-F210CCB74382}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B58C4880-041A-4FAA-A407-EEDAD91CA780}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{CEB02D63-17AF-474F-87D7-CA1D8DDF67E0}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


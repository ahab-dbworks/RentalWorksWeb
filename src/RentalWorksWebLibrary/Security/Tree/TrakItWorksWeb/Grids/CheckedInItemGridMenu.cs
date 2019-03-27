using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckedInItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedInItemGrid() : base("{85778C01-2ACC-4ADF-97EC-7386E6F32415}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A97DFA18-C35B-4A62-9644-A5040F55562B}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{95736994-7D2A-4DC6-9BAA-62D761A866D0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{03FB7C31-C01C-4DFF-B8FE-3BDCACBFB149}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E976F086-831C-46DA-A866-7946F8673749}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


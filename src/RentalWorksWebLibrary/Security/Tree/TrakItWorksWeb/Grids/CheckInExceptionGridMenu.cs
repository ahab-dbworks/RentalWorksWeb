using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class CheckInExceptionGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckInExceptionGrid() : base("{81282AE6-63B5-4A97-A066-0592CE276D58}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{34BD6DBD-3698-430B-A9EF-278739E129F0}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3158DB75-A5CD-4A62-B469-35740FD6802C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{7A3C2406-30D1-421F-8571-325A2EC49CB6}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F3CDA8DA-E952-4ACC-989F-25BC6A420909}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


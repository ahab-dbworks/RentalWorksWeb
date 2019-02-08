using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class GlDistributionGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GlDistributionGrid() : base("{A41DF75D-A3A3-40B8-84E0-7B8F8DACDC35}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3824BD3D-C561-4ACD-8867-7B60631770D7}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0C4126B8-3CCD-461E-A7DE-A1A2593D15D2}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2A3C0BA7-DCBC-45E6-B9D2-6A949166F2EE}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8191F025-07C6-4641-959C-83D6A2A31900}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

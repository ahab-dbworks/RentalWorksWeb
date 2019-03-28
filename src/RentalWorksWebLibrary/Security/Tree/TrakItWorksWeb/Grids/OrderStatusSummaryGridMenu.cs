using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderStatusSummaryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderStatusSummaryGridMenu() : base("{B155DFBC-F429-4A90-82C3-C26B4AF61E86}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{72C5F4A9-28BF-428E-BF95-FE6EAA56AA1A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{42A81969-80F5-4221-8F0D-681EB54F69D0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2E869895-8B85-48E7-BB6F-56497A39DC42}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7E296B4F-46CC-4E01-9862-25C39BF3CBDB}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

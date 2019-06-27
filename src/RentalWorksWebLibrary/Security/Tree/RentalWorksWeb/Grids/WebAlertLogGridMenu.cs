using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class WebAlertLogGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WebAlertLogGridMenu() : base("{5C6EF9EF-9ECE-4364-9B0C-A8C2D7EAE694}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{00DB26F5-F13F-4A48-B9DB-A88C4A680DBC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B094B899-4B4E-4AFF-A1E6-6F4F09D45B2B}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{55FF25AB-4A33-46CA-9D29-065B31AFBE49}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D5EB205B-F5DE-4744-A7B3-E07218159535}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
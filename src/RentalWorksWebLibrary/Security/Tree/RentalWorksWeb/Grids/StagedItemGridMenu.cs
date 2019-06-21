using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class StagedItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public StagedItemGridMenu() : base("{132DEBAB-45F6-4977-A1A8-BAE5AC152780}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{246CC445-AC0B-4260-AF45-4918A5F76EA2}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EBA25499-7519-43DA-A4BE-2F3F0AC56FB0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{96866F31-E8BA-49E1-B674-A9F1D9921F76}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A8C4C1CB-FB93-409C-8340-AF034EA3BCEE}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Unstage Selected Items", "{43010EEE-85B8-4444-9FA7-A0A4A0ABC8CF}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class ReportSettingsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ReportSettingsGridMenu() : base("{0B524E5D-0644-445D-B9FA-9E15A827F1B2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6A8E0B83-699A-4CD4-8855-930E9D2CC1FF}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{7B9FC5E7-C1BC-4DC7-8B03-5697FE45FD55}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5E7ED461-E844-4887-85A7-9EEA2FF01853}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{3EA70630-DBF0-442C-ABBE-2E6729C2C1E5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

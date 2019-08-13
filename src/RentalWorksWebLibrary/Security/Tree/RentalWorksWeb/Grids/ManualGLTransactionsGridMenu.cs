using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class ManualGlTransactionsGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ManualGlTransactionsGrid() : base("{9EE2D14A-E79B-4D27-9B8E-5DDDEDD8862E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8EE392E6-BE9C-4EFA-8B8C-14BA30AC98CC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{ED6BB5DF-F380-4274-AE69-D38D93657C97}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D3756360-176F-4DD2-8AD5-701442AA7278}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EB7B78AD-771B-4015-AD49-294984568C57}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{7A4624DC-1000-4218-AB67-B308DD7A249D}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{2A7C3AFB-2CE1-426E-B52E-0AB7B6EEE93E}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6B38B5E4-6BD6-4E45-993E-45A8F7277FE5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

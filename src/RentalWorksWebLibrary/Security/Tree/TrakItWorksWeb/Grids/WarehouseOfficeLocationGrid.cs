using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseOfficeLocationGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseOfficeLocationGrid() : base("{70677F1B-83ED-4AF7-B243-95C5B14FDD5B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{04231B3B-2490-40A6-B5D3-DDD030BA4358}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{0ED00C6D-126B-484B-9AFA-D73CC5955855}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B8D15339-EAB0-4A8C-A6AA-D55126080967}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{192F2C59-16CE-4560-BF0B-A185A02B4C53}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{CEFFE40C-AA3F-452A-9162-213E750FDB48}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{63AB0007-3A12-47A0-8D4A-8F808A4E72EB}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{95EF421B-5C9A-4C84-946F-E3E80C398FD0}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

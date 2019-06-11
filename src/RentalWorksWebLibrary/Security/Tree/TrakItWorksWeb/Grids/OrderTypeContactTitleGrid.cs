using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class OrderTypeContactTitleGridGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeContactTitleGridGridMenu() : base("{6BA1DA32-42AA-4CFE-A61E-75A8A3487141}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D48737D2-A92A-48C4-8983-E50F9B7626D7}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{17BE82D1-265A-4704-984B-BC807108C2EB}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{58952AED-EA2D-48F8-AE94-677DCA12B860}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{814D319C-837C-4935-9D36-2B4AA394C9DA}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{C3C30D37-2022-4BFC-AE7C-BA49BEB73A13}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4B11AFBB-4C2B-4601-988C-B40E787D74E7}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9482D073-1993-486D-9359-F2E3C489EED2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

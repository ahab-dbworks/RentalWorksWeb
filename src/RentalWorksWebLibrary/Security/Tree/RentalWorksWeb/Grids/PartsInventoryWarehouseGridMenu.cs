using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventoryWarehouseGridMenu() : base("{162FA407-60F3-45E4-B604-AA655654CC09}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{9F1B54EF-60B7-4B25-B440-FFF8E0CE190F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{76E98684-24BA-41F6-BCA5-836C35519393}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D2951BDC-E1DB-42F4-B3C9-F05BAE431AF3}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{F4AEB873-31F2-41C2-AC80-C552AE9F4DFF}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{75B75CBA-EE4E-451D-9F54-69880B6967EF}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
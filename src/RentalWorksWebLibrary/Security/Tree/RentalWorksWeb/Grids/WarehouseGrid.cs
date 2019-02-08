using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseGrid() : base("{EF27A7FE-26D8-4F3C-85CD-9CD2D6FE57A5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D5F1A714-8C67-4208-862B-62A199CBDA75}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{9BB31C07-1676-46E4-AD03-76FB30B1AD38}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1DD53A91-432E-41A4-9C71-71539F71421F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{65EEC3B4-0167-4550-8304-DCB7F72F58AC}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{A823498E-9434-4DE0-9A51-93ED243AFF39}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{C405E58A-686E-49FE-9998-ABFB69ED1625}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7AFDE939-3610-4ED8-A317-F0E9C3166282}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

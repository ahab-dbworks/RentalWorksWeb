using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseOfficeLocationGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseOfficeLocationGrid() : base("{99C692AB-13CE-4113-88CF-6AC15821B9D4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{463D0F3C-CEE7-43D2-A8C7-1B250DE62A4E}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{5C152A5B-45A7-4457-B341-363F045C4C77}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{24F5B225-D563-45CE-AB99-C463113B50ED}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DD7C2A75-02E4-4A8A-8A5B-B6734EB1C96F}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{154B5A5B-A90C-4C81-8385-6946F1D8AC08}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C28C0A8E-63A3-4D11-974B-05605F9791AA}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{1B9B18C3-1E76-4423-9A53-CE43D54D89D2}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

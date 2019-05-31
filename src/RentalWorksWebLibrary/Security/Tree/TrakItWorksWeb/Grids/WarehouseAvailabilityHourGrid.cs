using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    class WarehouseAvailabilityHourGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseAvailabilityHourGrid() : base("{D69B1630-8B41-48BC-B362-2033F428A60C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{459612F5-90D0-4A7B-8274-48D60B67519C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3D20384C-5296-44A1-AFAC-2BC3619D921A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1F7D0539-72E5-4867-B991-3CBC15C79E19}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DAF750B0-EED8-4FFD-81C4-A8768CB0A006}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{DFED8490-7E25-4F76-B565-7C6E54114280}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{7D00B89E-F2C0-4C09-BCAF-A8BEF6CAED3A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{08D18B79-AFEE-4290-92B2-166C740B5D87}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

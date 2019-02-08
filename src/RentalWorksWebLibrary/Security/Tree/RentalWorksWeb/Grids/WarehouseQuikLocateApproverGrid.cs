using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WarehouseQuikLocateApproverGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseQuikLocateApproverGrid() : base("{597134F6-303E-4B69-A9B7-403082295AE1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{2682A479-9B5A-4CBA-84EA-1B0A484E9DDB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B8DC4D06-9030-493D-8D23-77EAF835223E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A1471F5B-4B0B-49C7-9EE8-C1209E19BBE9}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{46B165E4-CCDB-4DD0-89D6-1ABA62D8398E}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{99303555-1A5B-4313-A426-26A427917BCA}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A91BD0E1-1768-4E11-94A9-761A5C3A1CC0}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{D6D2B921-FF06-439B-AFA9-1EAE6EFD9E6E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

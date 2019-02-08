using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class BillingCycleEventsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public BillingCycleEventsGridMenu() : base("{8AAD752A-74B8-410D-992F-08398131EBA7}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{7D51A15F-09AF-4B24-9C1F-87F24F5A62D5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{20305F77-19ED-4DC4-ADC8-56144FC6391C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4B429874-9843-4636-86CF-F6B2129247C3}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{AC581C6C-543C-40E9-8D1D-2A47F6BD72AB}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{9FD8A16F-541B-4B5F-949C-951D5ACEF3EE}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
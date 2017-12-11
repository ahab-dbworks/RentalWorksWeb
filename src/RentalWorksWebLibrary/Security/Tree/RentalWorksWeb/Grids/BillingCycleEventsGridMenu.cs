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
                tree.AddEditMenuBarButton("{9FD8A16F-541B-4B5F-949C-951D5ACEF3EE}",   nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
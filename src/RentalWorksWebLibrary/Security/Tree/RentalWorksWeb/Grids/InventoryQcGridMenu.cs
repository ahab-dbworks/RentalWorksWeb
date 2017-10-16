using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryQcGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryQcGridMenu() : base("{C1EE89A8-2C6C-4709-AB0C-2BBC062160B5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{AAAA720D-EE63-4B46-9354-5D2E429A1633}", MODULEID);
            tree.AddEditMenuBarButton("{2DC900F2-1D81-4DAC-9666-776E3F764F6B}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
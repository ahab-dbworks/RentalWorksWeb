using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SubPurchaseOrderItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubPurchaseOrderItemGridMenu() : base("{27A93B3D-4E30-4854-88C0-292783E778B3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1CCC68F7-743E-4B39-AC54-FD11F6CA60CF}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{79273077-3983-4D22-B40F-BE0B169429DA}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{90858072-3C15-4A0D-B529-2B43700D21A1}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7AED453F-C40A-420A-B92C-5FF448831FD4}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{D94CE001-4223-463B-9D0C-1423B9B1AA22}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
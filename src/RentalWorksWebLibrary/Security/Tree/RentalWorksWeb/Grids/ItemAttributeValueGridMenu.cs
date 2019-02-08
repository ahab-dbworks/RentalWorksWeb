using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ItemAttributeValueGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ItemAttributeValueGridMenu() : base("{22D75843-E915-4956-9B25-C52E815F3C5E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{BBA5F3C3-C693-41FB-A7AB-220BD37AACEF}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{48946DA5-699B-48E3-9789-4BC3D4C29E7A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E168659B-5B18-4FB8-BB91-915D55823A79}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BD8C3A18-B10D-447D-8547-EB4101F1BB8C}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{C0DC47EE-80FE-49C4-B56D-E5845FA1B361}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{98FEFD06-C25D-4E1E-87BB-E2AF3FB0B189}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{3B58EB08-6BA8-4D56-A0C0-76EEF81DE9B3}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
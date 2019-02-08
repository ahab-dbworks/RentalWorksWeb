using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RentalInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryWarehouseGridMenu() : base("{3AC00695-4130-4A34-B4B2-BC6E3E950FB1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{92EF711F-C336-4DCA-B25F-58EEE5E89328}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{D6F64CC2-BEE0-4FB0-9D28-AB2FD8876EB6}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E25730B4-54F7-4F76-BA28-357042DF4922}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DC5441CD-DA61-49EF-9277-DA1EAE5CAE94}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{2624D496-5A68-4931-AB2B-C6DD1C5B0D65}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
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
            tree.AddEditMenuBarButton("{2624D496-5A68-4931-AB2B-C6DD1C5B0D65}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
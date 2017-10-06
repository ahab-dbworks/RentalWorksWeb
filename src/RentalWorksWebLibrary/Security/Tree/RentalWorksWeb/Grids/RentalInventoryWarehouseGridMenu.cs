using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RentalInventoryWarehouseGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RentalInventoryWarehouseGridMenu() : base("{3AC00695-4130-4A34-B4B2-BC6E3E950FB1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{ED2DF78A-FFB2-472B-8848-3C8D9988A68E}", MODULEID);
            tree.AddNewMenuBarButton("{989AAAAA-2C7B-426F-AC7A-0F3956E31492}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{77B1A0C0-07FC-4BD2-A687-EE5C6F0D0FEA}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{42E68A5E-1C5A-44C9-8DD6-6672C8678A58}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
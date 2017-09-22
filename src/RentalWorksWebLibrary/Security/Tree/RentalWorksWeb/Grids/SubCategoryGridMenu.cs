using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class SubCategoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubCategoryGridMenu() : base("{070EBAE0-903E-48CE-9285-BDC3ECC07C68}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{2DFF8053-1CD2-47B9-A9E7-DBB8B3669C0B}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{639F4EF6-EC17-4BC7-AFB6-1286523E69C7}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{301B2D56-A939-4F52-8976-C72499B3F32D}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{15A2FDC1-CA3A-43E7-9E11-B07C64EB7E6D}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{71297438-6A73-44B8-820F-55AC23B7D9F5}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{8FD7DB80-AFDD-48C7-B712-A69D93AE3BC0}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{97543228-BEBF-4A34-B2C7-B957F51248F5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
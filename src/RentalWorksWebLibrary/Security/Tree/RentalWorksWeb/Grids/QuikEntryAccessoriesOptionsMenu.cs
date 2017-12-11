using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class QuikEntryAccessoriesOptionsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikEntryAccessoriesOptionsMenu() : base("{27317105-BA68-417A-A592-86EEB977CA32}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{256CD8C9-37A5-4309-82E4-4DB30E9C6334}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{C0D68A7E-DDB1-40F9-91C2-3390F2CA39ED}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{E2CFA2B5-5334-413A-BF2E-6E3B0C53CB5F}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{F1DBB74E-F82A-4BF7-B7AD-9ABBA58F4DE4}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{109C3069-5F09-4B66-B106-DF904BF35373}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{F5EE3E55-CC9C-4E75-9CE6-1CD69CA16B3F}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{655C4E9A-6999-40D8-9215-0292696568F3}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
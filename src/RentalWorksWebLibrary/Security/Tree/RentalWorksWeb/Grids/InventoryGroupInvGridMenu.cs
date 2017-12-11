using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryGroupInvGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryGroupInvGridMenu() : base("{2EE8822B-F83E-4D8B-B055-4DA5853080C8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{14EFDBA6-F9F3-4C32-BC0E-F6B1EF5787A9}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{7EC5E84C-F51F-4341-933D-A724A1250D43}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{31C0A6B7-4646-4F1F-9F4A-77A1F791EE82}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{E0C96585-A551-4FF6-850F-BAB82AEB7EA5}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{D997FE48-77E3-49FF-BE9B-107ADF94E3AD}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{AB9BA8A0-99B8-4670-8DF1-8430B7C3BBE8}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{D5723CE3-94CF-49D4-B559-83DC8776583C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
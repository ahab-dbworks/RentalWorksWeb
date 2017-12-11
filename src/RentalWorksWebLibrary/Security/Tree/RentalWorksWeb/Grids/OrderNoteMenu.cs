using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderNoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderNoteMenu() : base("{45573B9C-B39D-4975-BC36-4A41362E1AF0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{76C89466-BB91-4C55-88B3-5382DE481561}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{A8D607D3-839C-4C62-B734-3483A925FE83}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{CC4CDD5B-8EF3-428F-9EC6-10733C55A059}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{5461EE0D-93D7-4639-8536-86CC164AD081}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{EC685033-3066-4295-A896-1996743737D3}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{1C3B1715-D536-4B3A-B288-684343271A61}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{F392962C-AF4E-42EF-A580-D32ACDC2B75F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
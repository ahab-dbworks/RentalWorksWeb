using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryCompleteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompleteGridMenu() : base("{3C3CE861-FEC6-41D6-A9B7-7AB28CC65F40}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{46753224-DFF1-474B-BCD4-F49905A4FFAB}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{4A614EA2-5372-460F-A1C7-5A5F5A15FE3A}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{588AF734-AEC0-40A2-AC47-475429ADC4BF}", nodeGridSubMenu.Id);
                        tree.AddDownloadExcelSubMenuItem("{70C62E36-6173-4CC2-89CC-9C78F669466C}", nodeBrowseOptions.Id);
                tree.AddEditMenuBarButton("{B4C0E33E-1AE7-45B3-A817-76B2A1C62352}", nodeGridMenuBar.Id);
                tree.AddNewMenuBarButton("{A2FDF523-F6C8-4939-9D82-56A31AA560F0}", nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{EAEA4094-B9C5-40AD-8D04-7FDAD163AC9E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
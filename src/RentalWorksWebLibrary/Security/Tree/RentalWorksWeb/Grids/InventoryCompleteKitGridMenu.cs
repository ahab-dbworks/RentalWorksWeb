using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class InventoryCompleteKitGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompleteKitGridMenu() : base("{797339C1-79C3-4FC0-82E4-7DA2FE150DDA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CAF66991-4227-4D4B-841B-9403D7BEE78C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B86AA789-A2F4-41D7-A82B-80D21BC06C7E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{28D95B56-A98F-4B28-A32E-E2AF734B2BD3}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{063B61EC-4718-4F97-984F-7696D034E254}", nodeBrowseOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
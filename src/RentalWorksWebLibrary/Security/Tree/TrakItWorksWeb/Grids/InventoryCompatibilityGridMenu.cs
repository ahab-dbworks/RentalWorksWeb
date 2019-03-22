using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryCompatibilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryCompatibilityGridMenu() : base("{AD2D5238-317D-4FDC-950E-E935293CE2F8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1E2890B7-0067-42A7-9765-DDA1290E547A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1723B370-415E-4727-AFA7-FC238D0EF0BD}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B8CA02F8-E3F6-45E0-A8E0-5EEC39C12B2F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6B3CBAA4-6358-4FE4-8583-842B30DD9379}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E95A06DE-2117-4CE4-98EC-C562192A99A2}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{80335912-E634-4B1F-823E-BB37B67651FD}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{74A6A24A-4A24-490F-B456-D6CAACDCCAF8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

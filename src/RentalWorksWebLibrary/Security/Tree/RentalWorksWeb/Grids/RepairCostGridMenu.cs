using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class RepairCostGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RepairCostGridMenu() : base("{38219D4D-C8F6-4C8C-B86B-D86D5F645251}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{08D9E43C-033D-4656-B94A-BDFC550E5D67}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E84BAB0C-D896-4E30-9779-7CBEF9496B65}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BB209D79-17BE-47DB-8119-96F531AA45CA}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5ABB5B73-7DC7-45C4-8892-8D2F00661EF2}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{626EF8E0-42C4-4C57-803E-2B72C2269126}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{A8BC2E60-4858-428E-9D45-D1DD459DD0C4}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9DA5CD25-C71E-491C-AF08-DA68F32A3B4F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
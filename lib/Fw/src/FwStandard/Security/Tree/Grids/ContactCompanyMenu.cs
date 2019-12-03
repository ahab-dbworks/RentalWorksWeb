namespace FwStandard.Security.Tree.Grids
{
    public class ContactCompanyMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactCompanyMenu() : base("{7E1840AE-9832-4E0E-9B1F-A2A115575852}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{781E3C11-51AF-4D0F-8B44-D11E2697A021}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1919D358-7B64-4C81-A7B5-212F0961EB4E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{72A64F90-9788-442D-8CDC-57BF9A61650B}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("Activate", "{1E82C664-0306-4C03-88EE-507922667AFD}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Inactivate", "{36827171-807C-45B5-8CAC-5885AADD56EB}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{20799BDE-802A-4B2B-8D0D-23A7BE2B6676}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{7CEAE7E8-EE8B-4856-B6A7-AC806ED5EDA4}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{51A0BBE9-495F-413E-9690-B0C47C58ED9F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
namespace FwStandard.Security.Tree.Grids
{
    public class ContactCompanyGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactCompanyGridMenu() : base("{68E99935-E0AB-4552-BBFF-46ED2965E4F0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CCE48C43-8E43-4F7B-B0FF-06A055C398AB}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{B8A47D73-81DA-489A-8879-BD2A31D49C5C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6E25A6CC-9A17-4226-8E66-741E9F2AB64F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{52969E01-20F8-493B-B64E-222F55EC7B0B}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{6D2C8640-BABA-46EC-A6C6-4440EB1219E8}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{60C5E33D-7243-4C8A-A46E-FCA633B49FDE}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{96A29C0A-3A75-43B4-8DE3-357F703C5C06}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
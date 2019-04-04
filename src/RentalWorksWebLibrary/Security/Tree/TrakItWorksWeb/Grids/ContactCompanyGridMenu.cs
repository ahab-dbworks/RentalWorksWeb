using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ContactCompanyGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactCompanyGridMenu() : base("{D336C756-8380-44D3-B772-3737E8B901B3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C7277681-8896-4CE8-B016-5D5D878CF511}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EE0D63F0-5D77-4C42-913B-2C78E936AC98}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{CC26BE09-98A1-4082-9432-BD0CFBB0F5A2}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{64CEFED5-9108-4C17-89CC-8E7D7F654AFF}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{E73857BB-C4B6-40ED-AC1A-267BBE7FA1A2}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F2018493-7EE3-4977-A3E7-C0EC7A1D53CC}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{7B39AB88-2C73-4B7D-8729-72AA9559352C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
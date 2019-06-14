using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AlertWebUsersGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AlertWebUsersGridMenu() : base("{774142FC-246D-453C-8B62-66FE5FFC1A8B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A3C01AEA-8FBE-4A75-B9B6-177625A716E3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3B7FE207-3326-4E98-90A1-C194D3CB6332}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A1291973-A521-42BB-886C-FF83FA600C69}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DC216AEF-6B30-4B92-A0F4-8D75E08F5BDB}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{1C252B0D-612B-4707-82D6-C049F3A471F6}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{53280FD9-27B3-4756-B610-E26C6BFBA12F}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E072127F-FB05-4305-8234-A4C83884B7DA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
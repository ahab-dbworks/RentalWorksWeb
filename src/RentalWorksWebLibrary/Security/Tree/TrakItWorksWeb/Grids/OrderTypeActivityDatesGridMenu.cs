using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class ActivityDatesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ActivityDatesMenu() : base("{0E1C0D08-C4F2-4BA9-9822-C22B4710AA2F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{014A8A04-F922-45DF-A5B9-E36534E976BD}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{6450B5CA-CF67-4417-A91D-7276FBE3B3CD}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{8E630A94-E54D-442E-A35F-744E848A927E}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9C55EEB9-C8BC-4199-B91A-EA26992CC62C}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{3B968625-0F59-4DE2-829E-CE290DDECE02}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{1C04E14D-9394-4689-A9D6-65112A1DDCFD}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{724F584F-DCD7-4B1B-9C67-DB1315D3D6D1}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
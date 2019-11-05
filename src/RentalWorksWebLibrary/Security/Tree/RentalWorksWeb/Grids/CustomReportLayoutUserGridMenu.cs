using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class CustomReportLayoutUserGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomReportLayoutUserGridMenu() : base("{BA97DD0B-C7A1-47C7-BA8B-6F5087F900BB}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{90570F2F-EA9C-49B7-8F29-6EB62330B054}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C9435731-1B8C-49C3-81E9-411A39E1AE64}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BC0EA2F1-58C6-4CA7-BE5F-B11440EDA3BE}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EB4A9814-4978-4411-AC5A-DBC8091E4E33}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{56D69519-FC0F-4345-9473-82E6565415A8}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{1523BE6F-E66C-4104-9CA8-F5289FA79DB7}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{620DF01D-5E85-4EE9-B432-F6E61BE0C90C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
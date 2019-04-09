using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class WidgetGroupGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WidgetGroupGridMenu() : base("{F33AEECB-006D-47A6-AB2C-82CEB2614120}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0AED2857-CA2A-46C7-B702-8FA83DAB54DD}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{5526782A-EA54-4028-9F59-A896744AF4D1}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A7043AEB-FA4C-4E45-B944-61994459F4AF}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B509E114-C6DE-4BDB-AF7F-7C78E044D4D9}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{0C0F034E-44FC-4E02-8698-4F464ABE8199}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{25FA7F8D-B28D-49B4-9F99-59C1F66E52BB}",   nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8ABA293D-1301-4394-8BB0-0676FD5A34EA}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
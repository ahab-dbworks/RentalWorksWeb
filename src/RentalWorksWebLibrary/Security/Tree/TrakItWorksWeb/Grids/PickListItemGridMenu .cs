using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class PickListItemGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PickListItemGridMenu() : base("{FA382FA5-C187-481D-8A8D-4755C12D4936}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{1D41EFA7-D4F5-46C3-8B10-B0D32353B4F3}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EC4C5AAF-1445-4971-8D25-17DCACC6E271}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{94B1DC5F-A904-4305-A296-1B85B35ACB3F}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6DBA55A1-7AFB-4D14-B08F-4D83A2F60964}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{35322CCC-F786-4BFE-8EAC-B4D3C024927F}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C6104BCB-7E36-4DB4-BFE5-4F9ED4EE2389}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}

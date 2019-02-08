using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class POApproverGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POApproverGridMenu() : base("{314CEEC5-6E42-4539-BD10-8F680A0F70F4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{56C5525A-5ECA-4FF4-A3BB-CB9EA20BA21F}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F59DE636-5179-457F-8683-53B8B698BEBF}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{4D3E2051-BA2D-4705-9D40-B0E770B31B37}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{9B1DF372-05C8-49A8-A38D-FB0E9F11FF25}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{FADDB167-1C12-4705-9125-70FF8C603B29}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{3A2806DC-60F7-4ECA-BCDB-6425208D5310}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{4F0FEFFB-0EFD-48E6-B657-A6A302D58B42}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}